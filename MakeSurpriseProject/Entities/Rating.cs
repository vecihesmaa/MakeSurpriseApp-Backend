using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class Rating
{
    public int RatingsId { get; set; }

    public int OrderItemId { get; set; }

    public int UserId { get; set; }

    public string? Comment { get; set; }

    public int? StarCount { get; set; }

    public virtual OrderItem OrderItem { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
