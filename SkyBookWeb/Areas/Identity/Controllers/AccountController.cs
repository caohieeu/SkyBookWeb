using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkyBookWeb.Core.Entities;
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync
                (loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return View(loginVM);
        }
        public IActionResult Register()
        {
            var model = new RegisterVM
            {
                RoleList = [
                    new SelectListItem { Text = Utilty.Constant.RoleCustomer, Value = Utilty.Constant.RoleCustomer },
                    new SelectListItem { Text = Utilty.Constant.RoleEmployee, Value = Utilty.Constant.RoleEmployee },
                    new SelectListItem { Text = Utilty.Constant.RoleAdmin, Value = Utilty.Constant.RoleAdmin }
                ]
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
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
                await _signInManager.SignInAsync(user, isPersistent: false);
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
