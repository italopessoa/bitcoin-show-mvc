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

        public override bool Equals(object obj)
        {
            var award = obj as Award;
            if (award == null)
            {
                return false;
            }
            if (Success != award.Success || Fail != award.Fail
                || Quit != award.Quit || Level != award.Level
                || Id != award.Id)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + Success.GetHashCode();
                hash = hash * 23 + Fail.GetHashCode();
                hash = hash * 23 + Quit.GetHashCode();
                hash = hash * 23 + Level.GetHashCode();
                return hash;
            }
        }
    }
}
