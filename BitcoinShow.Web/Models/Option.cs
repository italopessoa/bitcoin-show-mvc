using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    ///     Entity that represents a Question option
    /// </summary>
    [Table("Option", Schema = "bs")]
    public class Option
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public Option()
        {
        }

        /// <summary>
        ///     Option Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Option text
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Text { get; set; }
    }
}