using Microsoft.AspNetCore.Mvc;
using OneTop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OneTop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly ClothingStoreContext context;

        public OrderController(ClothingStoreContext context)
        {
            this.context = context;
        }

        public IActionResult OrderManagement()
        {
            var orders = context.Orders
                .Include(o => o.User)
                .Where(o => o.UserId != null)
                .ToList();

            return View(orders);
        }
        public IActionResult Confirm(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.Status = "Đang giao";
                context.SaveChanges();
            }

            return RedirectToAction("OrderManagement");
        }
        public IActionResult Complete(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.Status = "Hoàn thành";
                context.SaveChanges();
            }

            return RedirectToAction("OrderManagement");
        }

        public IActionResult Cancel(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.Status = "Đã hủy";
                context.SaveChanges();
            }

            return RedirectToAction("OrderManagement");
        }


    }
}
