using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class UserRelative
{
    public int UserRelativeId { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Tag { get; set; } = null!;

    public int? FormAnswerId { get; set; }

    public bool? UserRelativeType { get; set; }

    public virtual FormAnswer? FormAnswer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual User User { get; set; } = null!;
}
