using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BitcoinShow.Web.Models
{
    public enum LevelEnum : byte
    {
        [Description("Easy")]
        [Display(Name ="Easy")]
        Easy = 0,
        [Description("Medium")]
        [Display(Name = "Medium")]
        Medium = 1,
        [Description("Hard")]
        [Display(Name = "Hard")]
        Hard = 2,
        [Description("Very Hard")]
        [Display(Name = "Very Hard")]
        VeryHard = 3
    }
}
