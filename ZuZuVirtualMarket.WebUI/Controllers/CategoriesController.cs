using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Service.Abstract;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<Category> _Service;

        public CategoriesController(IService<Category> service)
        {
            _Service = service;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _Service.GetQueryable().Include( p => p.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
