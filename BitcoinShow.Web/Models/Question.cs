using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    ///     Question
    /// </summary>
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
        [MaxLength(100)]
        public string Title { get; set; }
    }
}