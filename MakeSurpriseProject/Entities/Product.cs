using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? CategoryName { get; set; }

    public double? Price { get; set; }

    public string? Link { get; set; }

    public string? Picture { get; set; }

    public string? TargetGender { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
