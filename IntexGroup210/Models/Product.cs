using System;
using System.Collections.Generic;

namespace IntexGroup210.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? Year { get; set; }

    public int? NumParts { get; set; }

    public int? Price { get; set; }

    public string? ImgLink { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
}
