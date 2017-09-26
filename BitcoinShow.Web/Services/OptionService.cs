using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Services
{
    public class OptionService : IOptionService
    {
        Repositories.Interface.IOptionRepository _repository;
        public OptionService(Repositories.Interface.IOptionRepository repository)
        {
            _repository = repository;
        }
        
        public object Add(string text)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public object Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(object option)
        {
            throw new System.NotImplementedException();
        }
    }
}