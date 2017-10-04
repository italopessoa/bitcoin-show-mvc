using System.Collections.Generic;
using BitcoinShow.Web.Facade.Interface;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Services.Interface;

namespace BitcoinShow.Web.Facade
{
    public class BitcoinShowFacade : IBitcoinShowFacade
    {
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;
        public BitcoinShowFacade(IQuestionService questionService, IOptionService optionService)
        {
            this._questionService = questionService;
            this._optionService = optionService;
        }

        public QuestionViewModel CreateQuestion(QuestionViewModel questionViewModel)
        {
            Question question = new Question();
            question.Title = questionViewModel.Title;
            
            List<Option> options = new List<Option>();
            questionViewModel.Options.ForEach(o => 
            {
                options.Add(this._optionService.Add(o.Text) as Option);
            });

            question.Answer = options[questionViewModel.AnswerIndex];

            this._questionService.Add(question);
            options.ForEach(o => 
            {
                o.QuestionId = question.Id;
                this._optionService.Update(o);
            });

            var result = new QuestionViewModel
            {
                Id = question.Id,
                Title = question.Title,
                Answer = new OptionViewModel 
                { 
                    Id = question.Answer.Id,
                    Text = question.Answer.Text 
                }
            };
            result.Options = new List<OptionViewModel>();
            question.Options.ForEach(o =>
            {
                result.Options.Add(new OptionViewModel 
                { 
                    Id = question.Answer.Id,
                    Text = question.Answer.Text
                });
            });

            return result;
        }

        public List<QuestionViewModel> GetAllQuestions()
        {
            List<QuestionViewModel> result = new List<QuestionViewModel>();
            this._questionService.GetAll().ForEach(q => 
            {
                result.Add(new QuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title
                });
            });

            return result;
        }

        public QuestionViewModel GetQuestion(int id)
        {
            Question question = this._questionService.Get(id);
            QuestionViewModel result = null;
            if(question != null)
            {
                result = new QuestionViewModel
                {
                    Id = question.Id,
                    Title = question.Title
                };
            }

            return result;
        }
  }
}
