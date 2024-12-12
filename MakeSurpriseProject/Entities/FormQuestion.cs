using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class FormQuestion
{
    public int FormQuestionId { get; set; }

    public string? QuestionText { get; set; }

    public virtual ICollection<FormOption> FormOptions { get; set; } = new List<FormOption>();
}
