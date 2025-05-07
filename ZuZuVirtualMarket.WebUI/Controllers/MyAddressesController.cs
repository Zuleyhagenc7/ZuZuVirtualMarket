using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Core.Entities;
using ZuZuVirtual.Service.Abstract;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    [Authorize]
    public class MyAddressesController : Controller
    {
        private readonly IService<AppUser> _ServiceAppUser;
        private readonly IService<Address> _ServiceAddress;

        public MyAddressesController(IService<AppUser> service, IService<Address> serviceAddress)
        {
            _ServiceAppUser = service;
            _ServiceAddress = serviceAddress;
        }
        public async Task<IActionResult> Index()
        { 
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("User data not found! Please log out and log back in.");
            }
            var model = await _ServiceAddress.GetAllAsync(u => u.AppUserId == appUser.Id);
            return View(model);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if(appUser != null)
                    {
                        address.AppUserId = appUser.Id;
                        _ServiceAddress.Add(address);
                        await _ServiceAddress.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error Occurred!");
                }
            }
            ModelState.AddModelError("", "Registration Failed!");
            return View(address);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("User data not found! Please log out and log back in.");
            }
            var model = await _ServiceAddress.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if(model == null)
                return NotFound("Address information not found!");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Address address)
        {
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("User data not found! Please log out and log back in.");
            }
            var model = await _ServiceAddress.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
                return NotFound("Address information not found!");
            model.Title = address.Title;
            model.District = address.District;
            model.City = address.City;
            model.OpenAddress = address.OpenAddress;
            model.IsDeliveryAddress = address.IsDeliveryAddress;
            model.IsBillingAddress = address.IsBillingAddress;
            model.IsActive = address.IsActive;
            var otherAddresses = await _ServiceAddress.GetAllAsync(x => x.AppUserId == appUser.Id && x.Id != model.Id);
            foreach(var otherAddress in otherAddresses)
            {
                otherAddress.IsDeliveryAddress = false;
                otherAddress.IsBillingAddress = false;
                _ServiceAddress.UpDate(otherAddress);
            }
            try
            {
                _ServiceAddress.UpDate(model);
                await _ServiceAddress.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "Error Occurred!");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("User data not found! Please log out and log back in.");
            }
            var model = await _ServiceAddress.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
                return NotFound("Address information not found!");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Address address)
        {
            var appUser = await _ServiceAppUser.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return NotFound("User data not found! Please log out and log back in.");
            }
            var model = await _ServiceAddress.GetAsync(u => u.AddressGuid.ToString() == id && u.AppUserId == appUser.Id);
            if (model == null)
                return NotFound("Address information not found!");
            try
            {
                _ServiceAddress.Delete(model);
                await _ServiceAddress.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error Occurred!");
            }
            return View(model);
        }

    }
}
