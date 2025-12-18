using Microsoft.AspNetCore.Mvc;

namespace OneTop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View("Cart");
        }
    }
}
