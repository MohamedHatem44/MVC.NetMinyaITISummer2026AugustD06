using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCDemoD06.Models;
using MVCDemoD06.ViewModels.Role;
using System.Threading.Tasks;

namespace MVCDemoD06.Controllers
{
    public class RoleController : Controller
    {
        /*------------------------------------------------------------------*/
        private readonly RoleManager<ApplicationRole> _roleManager;
        /*------------------------------------------------------------------*/
        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleVM createRoleVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createRoleVM);
            }

            ApplicationRole applicationRole = new ApplicationRole
            {
                Name = createRoleVM.RoleName
            };

            IdentityResult result = await _roleManager.CreateAsync(applicationRole);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(createRoleVM);
            }
            return RedirectToAction("Index", "Home");
        }
        /*------------------------------------------------------------------*/
    }
}