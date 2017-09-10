namespace BitcoinShow.Web.Models
{
    public class Award
    {
        public int Id { get; set; }
        public QuestionLevel QuestionLevel { get; set; }

        public decimal Right { get; set; }
        public decimal Wrong { get; set; }
        public decimal Stop { get; set; }
    }
}