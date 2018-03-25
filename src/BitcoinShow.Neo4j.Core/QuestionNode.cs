using System.Collections.Generic;
using System.ComponentModel;
using Neo4j.Map.Extension.Attributes;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Core
{
    public enum QuestionDifficulty
    {
        [Description("easy")]
        Easy,
        [Description("medium")]
        Medium,
        [Description("hard")]
        Hard
    }

    public enum QuestionType
    {
        [Description("multiple")]
        MultipleChoice,
        [Description("boolean")]
        Boolean
    }

    /// <summary>
    /// 
    /// </summary>
    [Neo4jLabel("Question")]
    public class QuestionNode : Neo4jNode
    {
        /// <summary>
        /// 
        /// </summary>
        public QuestionNode(string title, QuestionDifficulty difficulty, QuestionType type, string correctAnswer, List<object> incorrectAnswers)
        {
            this.Title = title;
            this.Difficulty = difficulty;
            this.Type = type;
            this.CorrectAnswer = correctAnswer;
            this.IncorrectAnswers = incorrectAnswers;
        }

        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "difficulty")]
        public QuestionDifficulty Difficulty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "type")]
        public QuestionType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "correct_answer")]
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "incorrect_answers")]
        public List<object> IncorrectAnswers { get; set; }
    }
}
