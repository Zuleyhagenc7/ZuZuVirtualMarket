using Microsoft.AspNetCore.Authentication; //login
using Microsoft.AspNetCore.Authorization; //login
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims; //login
using ZuZuVirtual.core.Entities;
using ZuZuVirtual.Core.Entities;
using ZuZuVirtual.Service.Abstract;
using ZuZuVirtualMarket.WebUI.Models;
using ZuZuVirtualMarket.WebUI.Utils;

namespace ZuZuVirtualMarket.WebUI.Controllers
{
    public class AccountController : Controller
    {
        /*private readonly DatabaseContext _context;

        //public AccountController(DatabaseContext context)
        //{
            _context = context;
        }*/
        private readonly IService<AppUser> _Service;
        private readonly IService<Order> _ServiceOrder;

        public AccountController(IService<AppUser> service, IService<Order> serviceOrder)
        {
            _Service = service;
            _ServiceOrder = serviceOrder;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            AppUser user = await _Service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if(user is null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                SurName = user.SurName
            };
            return View(model);
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> IndexAsync(UserEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {

                    AppUser user = await _Service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is null)
                    {
                        user.SurName = model.SurName;
                        user.Phone = model.Phone;
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.Email = model.Email;
                        _Service.UpDate(user);
                        var sonuc = _Service.SaveChanges();

                        if (sonuc > 0)
                        {
                            TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                <strong>Your registration has been updated!</strong>
    <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
    </div>";
                            // await MailHelper.SendMailAsync(contact);
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error Occurred!");
                }
            }
            return View(model);
        }
        [Authorize] 
        public async Task<IActionResult> MyOrders()
        {
            AppUser user = await _Service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (user is null)
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("SignIn");
            }
            var model = _ServiceOrder.GetQueryable().Where(s => s.AppUserId == user.Id).Include(o => o.OrderLines).ThenInclude(p => p.Product);
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = await _Service.GetAsync(x=>x.Email == loginViewModel.Email & x.Password == loginViewModel.Password & x.IsActive);
                    if (account == null)
                    {
                        ModelState.AddModelError("", "Login Failed!");
                    }
                    else
                    {
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name, account.Name),
                            new(ClaimTypes.Role, account.IsAdmin ? "Admin" : "Customer"),
                            new(ClaimTypes.Email, account.Email),
                            new("UserId", account.Id.ToString()),
                            new("UserGuid", account.UserGuid.ToString()),
                        };
                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                
                }
                catch (Exception hata)
                {
                    //loglama
                    ModelState.AddModelError("", "Error Occurred!");
                }
            }
            return View(loginViewModel);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(AppUser appUser)
        {
            appUser.IsAdmin = false;
            appUser.IsActive = true;
            if (ModelState.IsValid)
            {
                await _Service.AddAsync(appUser);
                await _Service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        public IActionResult PasswordRenew()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordRenewAsync(string Email)
        {
            if(string.IsNullOrWhiteSpace(Email))
            {
                ModelState.AddModelError("", "Email cannot be left blank!");
                return View();
            }
            AppUser user = await _Service.GetAsync(x => x.Email == Email);
            if (user is null)
            {
                ModelState.AddModelError("", "The Email you entered was not found!");
                return View();
            }
            string mesaj = $"Dear {user.Name} {user.SurName} <br>Please to renew your password <a href='https://localhost:7063/Account/PasswordChange?user={user.UserGuid.ToString()}'>Click on</a> ";
            var sonuc = await MailHelper.SendMailAsync(Email, "Reset My Password", mesaj);
            if (sonuc)
            {
                TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                <strong>Your password renewal link has been sent to your email address!</strong>
    <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
    </div>";
            }
            else
            {
                TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                <strong>Your password reset link could not be sent to your email address!</strong>
    <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
    </div>";
            }
            return View();
        }
        public async Task<IActionResult> PasswordChangeAsync(string user)
        {
            if(user is null)
            {
                return BadRequest("Invalid Request!");
            }
            AppUser appUser = await _Service.GetAsync(x => x.UserGuid.ToString() == user);
            if (appUser is null)
            {
                return NotFound("Invalid Value!");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(string user, string Password)
        {
            if(user is null)
            {
                return BadRequest("Invalid Request!");
            }
            AppUser appUser = await _Service.GetAsync(x => x.UserGuid.ToString() == user);
            if (appUser is null)
            {
                ModelState.AddModelError("", "Invalid Value!");
                return View();
            }
            appUser.Password = Password;
            var sonuc = await _Service.SaveChangesAsync();
            if (sonuc > 0)
            {
                TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                <strong>Your password has been renewed.You can log in from the login screen.</strong>
    <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
    </div>";
            }
            else
            {
                ModelState.AddModelError("", "Update Failed!");
            }
            return View();
        }
    }
}
