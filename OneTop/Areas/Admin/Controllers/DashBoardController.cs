using Microsoft.AspNetCore.Mvc;

namespace OneTop.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
