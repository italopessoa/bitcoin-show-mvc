using System.ComponentModel.DataAnnotations;

namespace BitcoinShow.Web.Models
{
    public class AwardViewModel
    {
        public int? Id { get; set; }

        [Required]
        public decimal Success { get; set; }

        [Required]
        public decimal Fail { get; set; }

        [Required]
        public decimal Quit { get; set; }

        public LevelEnum Level { get; set; }
    }
}
