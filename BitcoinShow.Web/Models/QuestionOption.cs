using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    /// Question option
    /// </summary>
    [Table("QuestionOptions")]
    public class QuestionOption
    {
        /// <summary>
        /// Option Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Option text
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Question to which this option belongs
        /// </summary>
        
        public int QuestionId { get; set; }

        // [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}