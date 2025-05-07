using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Service.Abstract;
using ZuZuVirtualMarket.WebUI.Models;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService<Product> _ServiceProduct;

        public ProductsController(IService<Product> serviceProduct)
        {
            _ServiceProduct = serviceProduct;
        }

        public async Task<IActionResult> Index(string q = "")
        {
            var databaseContext = _ServiceProduct.GetAllAsync(p => p.IsActive && p.Name.Contains(q) || p.Description.Contains(q));
            return View(await databaseContext);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _ServiceProduct.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductDetailViewModel() { Product = product,
                RelatedProducts = _ServiceProduct.GetQueryable().Where(p => p.IsActive && p.CategoryId == product.CategoryId && p.Id != product.Id)
            };
            return View(model);
        }
    }
}
