using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Service.Abstract;
using ZuZuVirtualMarket.WebUI.Models;

namespace ZuZuVirtualMarket.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IService<Product> _ServiceProduct;
    private readonly IService<Slider> _ServiceSlider;
    private readonly IService<News> _ServicNews;
    private readonly IService<Contact> _ServicContact;


    public HomeController(IService<Product> serviceProduct, IService<Slider> serviceSlider, IService<News> servicNews, IService<Contact> servicContact)
    {
        _ServiceProduct = serviceProduct;
        _ServiceSlider = serviceSlider;
        _ServicNews = servicNews;
        _ServicContact = servicContact;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomePageViewModel()
        {
            Sliders = await _ServiceSlider.GetAllAsync(),
            News = await _ServicNews.GetAllAsync(news => news.IsActive),
            Products = await _ServiceProduct.GetAllAsync(p=>p.IsActive && p.IsHome)
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ContactUs(Contact contact)
    {
        if(ModelState.IsValid)
        {
            try
            {
               await _ServicContact.AddAsync(contact);
                var sonuc = await _ServicContact.SaveChangesAsync();
                if(sonuc > 0)
                {
                    TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                <strong>Your message has been sent!</strong>
    <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
    </div>";
                   // await MailHelper.SendMailAsync(contact);
                    return RedirectToAction("ContactUs");
                }
            }
            catch(Exception)
            {
                ModelState.AddModelError("","An error occurred!");
            }
        }
        return View(contact);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
//Represents a controller used to manage the homepage and general site oprations.
