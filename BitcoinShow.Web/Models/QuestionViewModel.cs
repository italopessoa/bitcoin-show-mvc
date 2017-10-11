using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BitcoinShow.Web.Models
{
    public class QuestionViewModel
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public QuestionViewModel()
        {
            Options = new List<OptionViewModel>
            {
                new OptionViewModel(),
                new OptionViewModel(),
                new OptionViewModel(),
                new OptionViewModel()
            };
        }

        /// <summary>
        ///     Question Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Question Title
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string Title { get; set; }

        public int AnswerIndex { get; set; }

        /// <summary>
        ///     Question answer
        /// </summary>
        [Required]
        public OptionViewModel Answer { get; set; }

        public List<OptionViewModel> Options { get; set;}

        public LevelEnum Level { get; set; }
    }
}
