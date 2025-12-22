using System.Collections.Generic;
using System.Security.Principal;

namespace OneTop.Models
{
    public class CartModel
    {
        public List<CartItemModel> items;

        public CartModel()
        {
            items = new List<CartItemModel>();
        }

        // Thêm sản phẩm vào giỏ
        public void Add(CartItemModel item)
        {
            bool isFound = false;

            foreach (var i in items)
            {
                if (i.ProductId == item.ProductId)
                {
                    isFound = true;
                    i.Quantity += item.Quantity;
                    break;
                }
            }

            if (!isFound)
            {
                items.Add(item);
            }
        }

        // Lấy toàn bộ sản phẩm
        public List<CartItemModel> getAllItems()
        {
            return items;
        }

        // Tính tổng tiền
        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var i in items)
            {
                total += i.Quantity * i.Price;
            }

            return total;
        }
    }
}
