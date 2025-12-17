using ClothingStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ClothingStoreContext context;
        public ProductController(ClothingStoreContext context)
        {
            this.context = context;
        }
        public IActionResult ProductManager()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult SearchByName()
        {
            string name = Request.Form["txtKeywords"].ToString();
            List<Product> categories = context.Products.Where(x => x.ProductName.Contains(name)).ToList();
            return View(categories);
        }
        public IActionResult DeleteProduct(int id)
        {
            Product product = context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("ProductManager");
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new Product());
        }
        [HttpPost]
        public IActionResult CreateProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model); context.SaveChanges();
                return RedirectToAction("ProductManager");

            }
            else
            {
                return View(model);
            }



        }


        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product p = context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            return View(p);
        }

        [HttpPost]
        public IActionResult EditProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                Product p = context.Products.Where(x => x.ProductId == model.ProductId).FirstOrDefault();

                p.ProductName = model.ProductName;
                p.Description = model.Description;
                p.Price = model.Price;
                p.Stock = model.Stock;
                p.DiscountPercent = model.DiscountPercent;
                p.ImageUrl = model.ImageUrl;
                p.CategoryName = model.CategoryName;
                context.SaveChanges();

                return RedirectToAction("ProductManager");
            }
            else
            {
                return View(model);
            }

        }

    }
}
