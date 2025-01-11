using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int UserRelativeId { get; set; }

    public string? GiftNote { get; set; }

    public decimal Price { get; set; }

    public int AddressId { get; set; }

    public int? ProductId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual Order Order { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual UserRelative UserRelative { get; set; } = null!;
}
