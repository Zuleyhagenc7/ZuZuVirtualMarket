using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Service.Abstract;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IService<News> _Service;

        public NewsController(IService<News> service)
        {
            _Service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _Service.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Invalid request!");
            }

            var news = await _Service
                .GetAsync(m => m.Id == id && m.IsActive);
            if (news == null)
            {
                return NotFound("No valid campaign found!");
            }

            return View(news);
        }
    }
}
