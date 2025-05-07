using Microsoft.AspNetCore.Mvc;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Service.Abstract;
using ZuZuVirtualMarket.WebUI.ExtensionMethods;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IService<Product> _Service;

        public FavoritesController(IService<Product> service)
        {
            _Service = service;
        }
        public IActionResult Index()
        {
            var favorites = GetFavorites();
            return View(favorites);
        }
        private List<Product> GetFavorites()
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavorites")  ?? [];
        }
        public IActionResult Add(int ProductId)
        {
            var favorites = GetFavorites();
            var product = _Service.Find(ProductId);
            if (product != null && !favorites.Any(p=>p.Id == ProductId))
            {
                favorites.Add(product);
                HttpContext.Session.SetJson("GetFavorites", favorites);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int ProductId)
        {
            var favorites = GetFavorites();
            var product = _Service.Find(ProductId);
            if (product != null && favorites.Any(p=>p.Id == ProductId))
            {
                favorites.RemoveAll(i => i.Id == product.Id);
                HttpContext.Session.SetJson("GetFavorites", favorites);
            }
            return RedirectToAction("Index");
        }
    }
}
