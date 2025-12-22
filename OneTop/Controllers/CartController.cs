using Microsoft.AspNetCore.Mvc;
using OneTop.Extensions;
using OneTop.Models;

namespace OneTop.Controllers
{
    public class CartController : Controller
    {
        private readonly ClothingStoreContext ctx;

        public CartController(ClothingStoreContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            List<CartItemModel> list =
                HttpContext.Session.GetObject<List<CartItemModel>>("cart");

            if (list == null)
            {
                list = new List<CartItemModel>();
            }

            CartModel cart = new CartModel();
            cart.items = list;

            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            CartItemModel item = new CartItemModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Quantity = 1
            };

            List<CartItemModel> list =
                HttpContext.Session.GetObject<List<CartItemModel>>("cart");

            if (list == null)
            {
                list = new List<CartItemModel>();
            }

            CartModel cart = new CartModel();
            cart.items = list;
            cart.Add(item);

            HttpContext.Session.SetObject("cart", cart.getAllItems());

            return RedirectToAction("Index");
        }
    }
}
