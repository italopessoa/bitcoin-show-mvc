using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinShow.Web.Models
{
    [Table("Award")]
    public class Award
    {
        [Key]
        public int Id { get; set; }
    
        [Required]
        public decimal Success { get; set; }

        [Required]
        public decimal Fail { get; set; }

        [Required]
        public decimal Quit { get; set; }

        public LevelEnum Level { get; set; }
    }
}
