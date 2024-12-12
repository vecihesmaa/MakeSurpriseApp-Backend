using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class Address
{
    public int UserId { get; set; }

    public int AddressId { get; set; }

    public string AddressTag { get; set; } = null!;

    public int ProvinceId { get; set; }

    public int DistrictId { get; set; }

    public int NeighbourhoodId { get; set; }

    public string FullAddress { get; set; } = null!;

    public virtual Ilceler District { get; set; } = null!;

    public virtual SemtMah Neighbourhood { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Sehirler Province { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
