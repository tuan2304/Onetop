using System;
using System.Collections.Generic;

namespace OneTop.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal? DiscountPercent { get; set; }

    public int? Stock { get; set; }

    public int? Quantity { get; set; }

    public string? ImageUrl { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
