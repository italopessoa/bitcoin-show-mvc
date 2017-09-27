using System;
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
        [Required(AllowEmptyStrings = false)]
        [MaxLength(200)]
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Option)) return false;

            if (obj == null || GetType() != obj.GetType())
                return false;

            Option option = (Option)obj;
            return (Id == option.Id) 
                && (!String.IsNullOrEmpty(Text) ? Text.Equals(option.Text) : String.IsNullOrEmpty(option.Text));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                if (!string.IsNullOrEmpty(Text))
                    hash = hash * 23 + Text.GetHashCode();
                return hash;
            }
        }
    }
}