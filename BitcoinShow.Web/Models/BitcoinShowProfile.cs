using AutoMapper;

namespace BitcoinShow.Web.Models
{
    public class BitcoinShowProfile : Profile
    {
        public BitcoinShowProfile()
        {
            CreateMap<Award, AwardViewModel>();
            CreateMap<Question, QuestionViewModel>();
        }      
    }
}
