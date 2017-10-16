using System;
using System.Collections.Generic;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _repository;

        public AwardService(IAwardRepository repository)
        {
            _repository = repository;
        }

        public Award Add(decimal successValue, decimal failValue, decimal quitValue, LevelEnum level)
        {
            ValidateArguments(successValue, failValue, quitValue, level);
            return _repository.Add(successValue, failValue, quitValue, level);
        }

        public void Delete(int id)
        {
            if (_repository.Get(id) != null)
            {
                _repository.Delete(id);
            }
            else
            {
                throw new InvalidOperationException($"There's no Award with ID value equal to {id}");
            }
        }

        public Award Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Award> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Award award)
        {
            ValidateArguments(award.Success, award.Fail, award.Quit, award.Level);
            _repository.Update(award);
        }

        private void ValidateArguments(decimal successValue, decimal failValue, decimal quitValue, LevelEnum level)
        {
            if (successValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(successValue), successValue, "The value must be greater than zero.");
            }
            if (failValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(failValue), failValue, "The value must be greater than or equal to zero.");
            }
            if (quitValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quitValue), quitValue, "The value must be greater than or equal to zero.");
            }
            if (successValue == failValue || successValue == quitValue || failValue == quitValue)
            {
                throw new ArgumentException("Award values can't be equal.");
            }
            if (successValue < failValue)
            {
                throw new ArgumentException("successValue can't be minor than failValue.");
            }
            if (successValue < quitValue)
            {
                throw new ArgumentException("successValue can't be minor than quitValue.");
            }
            if (quitValue < failValue)
            {
                throw new ArgumentException("quitValue can't be minor than failValue.");
            }
        }
    }
}
