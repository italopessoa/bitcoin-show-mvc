using BitcoinShow.Web.Models;
using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Services
{
    public class OptionService : IOptionService
    {
        private readonly Repositories.Interface.IOptionRepository _repository;
        public OptionService(Repositories.Interface.IOptionRepository repository)
        {
            _repository = repository;
        }

        public object Add(string text)
        {
            var newOption = new Option { Text = text };
            _repository.Add(newOption);
            return newOption;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public object Get(int id)
        {
            return _repository.Get(id);
        }

        public object Update(object option)
        {
            _repository.Update(option as Option);
            return option;
        }
    }
}