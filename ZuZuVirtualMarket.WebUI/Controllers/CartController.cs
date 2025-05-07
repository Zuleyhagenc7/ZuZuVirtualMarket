using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Core.Entities;
using ZuZuVirtual.Service.Abstract;
using ZuZuVirtual.Service.Concrete;
using ZuZuVirtualMarket.WebUI.ExtensionMethods;
using ZuZuVirtualMarket.WebUI.Models;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<Product> _ServiceProduct;
        private readonly IService<Address> _ServiceAddress;
        private readonly IService<AppUser> _ServiceAppUser;
        private readonly IService<Order> _ServiceOrder;


        public CartController(IService<Product> serviceProduct, IService<Address> serviceAddress, IService<AppUser> serviceAppUser, IService<Order> serviceOrder)
        {
            _ServiceProduct = serviceProduct;
            _ServiceAddress = serviceAddress;
            _ServiceAppUser = serviceAppUser;
            _ServiceOrder = serviceOrder;
        }
        public IActionResult Index()
        {
            var cart = GetCart();
            var model = new CartViewModel()
            {
                CartLines = cart.CartLines,
                TotalPrice = cart.TotalPrice()
            };
            return View(model);
        }
        public IActionResult Add(int ProductId, int quantity = 1)
        {
            var product = _ServiceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int ProductId, int quantity = 1)
        {
            var product = _ServiceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.UpdateProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int ProductId)
        {
            var product = _ServiceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if(appUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var addresses = await _ServiceAddress.GetAllAsync(a => a.AppUserId == appUser.Id && a.IsActive);
            var model = new CheckoutViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(string CardNumber, string CardMonth, string CardYear, string CVV, string DeliveryAddress, string BillingAddress)
        {
            var cart = GetCart();
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if(appUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var addresses = await _ServiceAddress.GetAllAsync(a => a.AppUserId == appUser.Id && a.IsActive);
            var model = new CheckoutViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };
            if (string.IsNullOrWhiteSpace(CardNumber) || string.IsNullOrWhiteSpace(CardMonth) || string.IsNullOrWhiteSpace(CardYear) || string.IsNullOrWhiteSpace(CVV) || string.IsNullOrWhiteSpace(DeliveryAddress) || string.IsNullOrWhiteSpace(BillingAddress))
            {
                return View(model);
            }
            var billingAddress = addresses.FirstOrDefault(a => a.AddressGuid.ToString() == BillingAddress);
            var deliveryAddress = addresses.FirstOrDefault(a => a.AddressGuid.ToString() == DeliveryAddress);

            //Ödeme çekme
            var siparis = new Order
            {
                AppUserId = appUser.Id,
                BillingAddress = BillingAddress, //$"{billingAddress.OpenAddress} {billingAddress.District} {billingAddress.City}",
                DeliveryAddress = DeliveryAddress, // $"{deliveryAddress.OpenAddress} {billingAddress.District} {billingAddress.City}",
                CustomerId = appUser.UserGuid.ToString(),
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice(),
                OrderNumber = Guid.NewGuid().ToString(),
                OrderState = 0,
                OrderLines = []
            };
            foreach (var item in cart.CartLines)
            {
                siparis.OrderLines.Add(new OrderLine
                {
                    ProductId = item.Product.Id,
                    OrderId = siparis.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });
            }
            try
            {
                await _ServiceOrder.AddAsync(siparis);
                var sonuc = await _ServiceOrder.SaveChangesAsync();
                if(sonuc > 0)
                {
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("Thanks");
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Error Occurred!";
            }

            return View(model);
        }
        public IActionResult Thanks()
        {
            return View();
        }
        private CartService GetCart()
        {
            return HttpContext.Session.GetJson<CartService>("Cart") ?? new CartService();
        }
    }
}
