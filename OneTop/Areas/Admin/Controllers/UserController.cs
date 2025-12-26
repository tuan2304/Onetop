using Microsoft.AspNetCore.Mvc;
using OneTop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace OneTop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly ClothingStoreContext context;

        public UserController(ClothingStoreContext context)
        {
            this.context = context;
        }

        public IActionResult UserManagement()
        {
            var users = context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            var user = context.Users.Find(id);

            var orders = context.Orders.Where(o => o.UserId == id).ToList();
            foreach (var o in orders)
                o.UserId = null;   // Bỏ liên kết

            context.Users.Remove(user);
            context.SaveChanges();

            return RedirectToAction("UserManagement");
        }



        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(User model)
        {
            if (ModelState.IsValid)
            {
                // ensure username unique
                var existing = context.Users.FirstOrDefault(u => u.Username == model.Username);
                if (existing != null)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(model);
                }

                model.Role = model.Role ?? "Customer";
                context.Users.Add(model);
                context.SaveChanges();
                return RedirectToAction("UserManagement");
            }

            return View(model);
        }
    }
}
