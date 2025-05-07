using Microsoft.AspNetCore.Mvc;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Service.Abstract;

namespace ZuZuVirtualMarket.WebUI.ViewComponents
{
    //Reusable UI componenet used to display homepage category list
    public class Categories : ViewComponent
    {
        private readonly IService<Category> _Service;

        public Categories(IService<Category> service)
        {
            _Service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _Service.GetAllAsync(c => c.IsTopMenu && c.IsActive));
        }
    }
}
