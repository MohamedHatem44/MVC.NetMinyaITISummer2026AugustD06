using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCDemoD06.Models;
using MVCDemoD06.ViewModels.Auth;

namespace MVCDemoD06.Controllers
{
    public class AccountController : Controller
    {
        /*------------------------------------------------------------------*/
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /*------------------------------------------------------------------*/
        // Denpendency Injection - Constructor Injection
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var applicationUser = new ApplicationUser
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                UserName = registerVM.UserName
                //PasswordHash = registerVM.Password  XXXX
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }

            // Add Default Role to the newly registered user
            IdentityResult AddRoleResult = await _userManager.AddToRoleAsync(applicationUser, SystemRoles.Admin);
            if(!AddRoleResult.Succeeded)
            {
                foreach (var error in AddRoleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }

            return RedirectToAction("Login");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (applicationUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(applicationUser, loginVM.Password, loginVM.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        /*------------------------------------------------------------------*/
    }
}