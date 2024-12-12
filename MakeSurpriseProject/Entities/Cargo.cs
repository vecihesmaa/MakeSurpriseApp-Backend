using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class Cargo
{
    public int CargoId { get; set; }

    public string CargoStatus { get; set; } = null!;

    public int? OrderItemId { get; set; }

    public virtual OrderItem? OrderItem { get; set; }
}
