using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class FormOption
{
    public int FormOptionId { get; set; }

    public int FormQuestionId { get; set; }

    public string? OptionText { get; set; }

    public virtual ICollection<FormAnswer> FormAnswerEighthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerEleventhQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerFifteenthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerFifthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerFirstQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerFourteenthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerFourthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerNinthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerSecondQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerSeventhQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerSixthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerTenthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerThirdQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerThirteenthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual ICollection<FormAnswer> FormAnswerTwelfthQuestionAnswerNavigations { get; set; } = new List<FormAnswer>();

    public virtual FormQuestion FormQuestion { get; set; } = null!;
}
