using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class SpecialDay
{
    public int SpecialDayId { get; set; }

    public DateTime SpecialDayDate { get; set; }

    public string Title { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
