using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class Sehirler
{
    public int SehirId { get; set; }

    public string SehirAdi { get; set; } = null!;

    public byte PlakaNo { get; set; }

    public int TelefonKodu { get; set; }

    public int RowNumber { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Ilceler> Ilcelers { get; set; } = new List<Ilceler>();
}
