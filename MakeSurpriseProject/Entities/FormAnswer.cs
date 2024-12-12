using System;
using System.Collections.Generic;

namespace MakeSurpriseProject.Entities;

public partial class FormAnswer
{
    public int FormAnswerId { get; set; }

    public int FirstQuestionAnswer { get; set; }

    public int SecondQuestionAnswer { get; set; }

    public int ThirdQuestionAnswer { get; set; }

    public int FourthQuestionAnswer { get; set; }

    public int FifthQuestionAnswer { get; set; }

    public int SixthQuestionAnswer { get; set; }

    public int SeventhQuestionAnswer { get; set; }

    public int EighthQuestionAnswer { get; set; }

    public int NinthQuestionAnswer { get; set; }

    public int TenthQuestionAnswer { get; set; }

    public int EleventhQuestionAnswer { get; set; }

    public int TwelfthQuestionAnswer { get; set; }

    public int ThirteenthQuestionAnswer { get; set; }

    public int FourteenthQuestionAnswer { get; set; }

    public int FifteenthQuestionAnswer { get; set; }

    public virtual FormOption EighthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption EleventhQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption FifteenthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption FifthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption FirstQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption FourteenthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption FourthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption NinthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption SecondQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption SeventhQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption SixthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption TenthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption ThirdQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption ThirteenthQuestionAnswerNavigation { get; set; } = null!;

    public virtual FormOption TwelfthQuestionAnswerNavigation { get; set; } = null!;

    public virtual ICollection<UserRelative> UserRelatives { get; set; } = new List<UserRelative>();
}
