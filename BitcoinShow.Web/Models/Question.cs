using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    ///     Question
    /// </summary>
    [Table("Question", Schema = "bs")]
    public class Question
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public Question()
        {
        }
        /// <summary>
        ///     Question Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Question Title
        /// </summary>
        [Required()]
        [MaxLength(150)]
        public string Title { get; set; }

        /// <summary>
        ///     Question answer
        /// </summary>
        // [Required]
        // public Option Answer { get; set; }

        /// <summary>
        ///     Question options
        /// </summary>
        [Required]
        public List<Option> Options { get; set; }
    }
}