using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public int UserId { get; set; }

    public int UserRelativeId { get; set; }

    public decimal Price { get; set; }

    public string? Note { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual UserRelative UserRelative { get; set; } = null!;
}
