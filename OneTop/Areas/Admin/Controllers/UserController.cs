using Microsoft.AspNetCore.Mvc;

namespace OneTop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserManagement()
        {
            return View();
        }
    }
}
