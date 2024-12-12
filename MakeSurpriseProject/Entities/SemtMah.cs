using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class SemtMah
{
    public int SemtMahId { get; set; }

    public string SemtAdi { get; set; } = null!;

    public string MahalleAdi { get; set; } = null!;

    public string PostaKodu { get; set; } = null!;

    public int IlceId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Ilceler Ilce { get; set; } = null!;
}
