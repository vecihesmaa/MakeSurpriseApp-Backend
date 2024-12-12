using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class Ilceler
{
    public int IlceId { get; set; }

    public int SehirId { get; set; }

    public string IlceAdi { get; set; } = null!;

    public string SehirAdi { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Sehirler Sehir { get; set; } = null!;

    public virtual ICollection<SemtMah> SemtMahs { get; set; } = new List<SemtMah>();
}
