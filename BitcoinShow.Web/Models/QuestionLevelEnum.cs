using System.ComponentModel;

namespace BitcoinShow.Web.Models
{
    public enum QuestionLevelEnum
    {
        [Description("Easy question")]
        Easy = 1,
        [Description("Medium question")]
        Medium = 2,
        [Description("Hard question")]
        Hard = 3
    }
}