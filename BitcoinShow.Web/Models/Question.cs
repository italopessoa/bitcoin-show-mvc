using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    ///       Question
    /// </summary>
    [Table("Question")]
    public class Question
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public Question()
        {
        }
        /// <summary>
        ///     Constructor
        /// </summary> 
        /// <param name="title"> Question title. </param>
        /// <param name="answer"> Question answer. </param>
        /// <param name="options"> Question options. </param>
        public Question(string title, Option answer, List<Option> options)
        {
            this.Title = title;
            this.Answer = answer;
            this.Options = options;
        }

        /// <summary>
        ///     Constructor
        /// </summary> 
        /// <param name="title"> Question title. </param>
        /// <param name="answer"> Question answer. </param>
        /// <param name="options"> Question options. </param>
        /// <param name="level"> Question level. </param>
        public Question(string title, Option answer, List<Option> options, LevelEnum level)
        {
            this.Title = title;
            this.Answer = answer;
            this.Options = options;
            this.Level = level;
        }

        /// <summary>
        ///     Question Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Question Title
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        ///     Question answer
        /// </summary>
        [Required]
        public Option Answer { get; set; }

        /// <summary>
        ///     Question options
        /// </summary>
        [Required]
        public List<Option> Options { get; set; }

        /// <summary>
        ///     Question level
        /// </summary>
        [Required]
        public LevelEnum Level { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Question question = (Question)obj;

            if ((Options == null && question.Options != null)
                || (Options != null && question.Options == null))
            {
                return false;
            }
            if (Level != question.Level)
            {
                return false;
            }
            if (Options != null)
            {
                if (Options.Count != question.Options.Count)
                {
                    return false;
                }
                for (int i = 0; i < Options.Count; i++)
                {
                    if (!Options[i].Equals(question.Options[i]))
                    {
                        return false;
                    }
                }
            }

            return (Id == question.Id) && (Title.Equals(question.Title))
                && Answer.Equals(question.Answer);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                if (!string.IsNullOrEmpty(Title))
                {
                    hash = hash * 23 + Title.GetHashCode();
                }
                if (Answer != null)
                {
                    hash = hash * 23 + Answer.GetHashCode();
                }
                if (Options != null)
                {
                    Options.ForEach(option =>
                    {
                        hash = hash * 23 + option.GetHashCode();
                    });
                }
                return hash;
            }
        }
    }
}
