using System.ComponentModel.DataAnnotations;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    ///     Entity that represents an Question option
    /// </summary>
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