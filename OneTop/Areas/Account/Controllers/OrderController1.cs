using Microsoft.AspNetCore.Mvc;
using OneTop.Models;
using OneTop.Extensions;

namespace OneTop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ClothingStoreContext ctx;

        public OrderController(ClothingStoreContext ctx)
        {
            this.ctx = ctx;
        }

        // Trang xác nhận đơn hàng
        public IActionResult Checkout()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Message"] = "Vui lòng đăng nhập để thanh toán";
                return RedirectToAction("Login", "Login");
            }

            var cart = HttpContext.Session.GetObject<List<CartItemModel>>("cart");

            if (cart == null || cart.Count == 0)
            {
                TempData["Message"] = "Giỏ hàng trống";
                return RedirectToAction("Index", "Home");
            }

            return View(cart);
        }


        // Xử lý đặt hàng
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemModel>>("cart");

            if (cart == null || cart.Count == 0)
            {
                TempData["Message"] = "Giỏ hàng trống";
                return RedirectToAction("Index", "Home");
            }

            // 🔑 LẤY USER ID TỪ SESSION
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["Message"] = "Vui lòng đăng nhập để đặt hàng";
                return RedirectToAction("Login", "Login");
            }

            // 1️⃣ Tạo Order
            Order order = new Order
            {
                UserId = userId,               // ✅ GẮN USER ID
                OrderDate = DateTime.Now,
                Status = "Chờ xử lý",
                PaymentMethod = "COD",
                TotalAmount = cart.Sum(x => x.Price * x.Quantity)
            };

            ctx.Orders.Add(order);
            ctx.SaveChanges(); // để có OrderId

            // 2️⃣ Tạo OrderDetail
            foreach (var item in cart)
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };

                ctx.OrderDetails.Add(detail);
            }

            ctx.SaveChanges();

            // 3️⃣ Xóa giỏ hàng
            HttpContext.Session.Remove("cart");

            TempData["Message"] = "Đặt hàng thành công!";
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
