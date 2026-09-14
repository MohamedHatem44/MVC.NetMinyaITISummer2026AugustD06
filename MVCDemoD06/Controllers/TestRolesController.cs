using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoD06.Controllers
{
    public class TestRolesController : Controller
    {
        /*------------------------------------------------------------------*/
        [Authorize]
        public IActionResult IndexV01()
        {
            return Content("Index V01");
        }
        /*------------------------------------------------------------------*/
        [Authorize(Roles = SystemRoles.User)]
        public IActionResult IndexV02()
        {
            return Content("Index V02");
        }
        /*------------------------------------------------------------------*/
        [Authorize(Roles = SystemRoles.Admin)]
        public IActionResult IndexV03()
        {
            return Content("Index V03");
        }
        /*------------------------------------------------------------------*/
        //[Authorize(Roles = "Admin,User")]
        [Authorize(Roles = SystemRoles.Admin + "," + SystemRoles.User)]
        public IActionResult IndexV04()
        {
            return Content("Index V04");
        }
        /*------------------------------------------------------------------*/
    }
}