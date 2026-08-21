using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Helpers;
using SkyBookWeb.Models.ViewModels;

namespace SkyBookWeb.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginVM loginVM, string? returnUrl = null)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync
                (loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return View(loginVM);
        }
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new RegisterVM
            {
                RoleList = PopulateDataHelpers.PopulateRoleData()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> Register(RegisterVM registerVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                registerVM.RoleList = PopulateDataHelpers.PopulateRoleData();
                return View(registerVM);
            }

            var user = new ApplicationUser
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                StreetAddress = registerVM.StreetAddress,
                City = registerVM.City,
                State = registerVM.State,
                PostalCode = registerVM.PostalCode,
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if(result.Succeeded)
            {
                if(!string.IsNullOrEmpty(registerVM.Role))
                {
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Utilty.Constant.RoleCustomer);
                }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
