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
        private readonly IAwardService _awardService;
        
        public BitcoinShowFacade(IQuestionService questionService, IOptionService optionService, IAwardService awardService)
        {
            this._questionService = questionService;
            this._optionService = optionService;
            _awardService = awardService;
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

            question.Answer = options[questionViewModel.AnswerIndex.Value];
            question.Level = questionViewModel.Level;
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
                QuestionViewModel question = new QuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Level = q.Level
                };
                question.Options.Clear();
                question.Answer = new OptionViewModel
                {
                    Id = q.Answer.Id,
                    Text = q.Answer.Text,
                    QuestionId = q.Id
                };
                q.Options.ForEach(o => 
                {
                    question.Options.Add(new OptionViewModel
                    {
                        Id = o.Id,
                        Text = o.Text,
                        QuestionId = q.Id
                    });
                });
                question.AnswerIndex = q.Options.IndexOf(q.Answer);

                result.Add(question);
            });

            return result;
        }

        public QuestionViewModel GetQuestion(int id)
        {
            Question question = this._questionService.Get(id);
            QuestionViewModel result = null;
            if (question != null)
            {
                result = new QuestionViewModel
                {
                    Id = question.Id,
                    Title = question.Title,
                    Level = question.Level
                };
                result.Options.Clear();
                question.Options.ForEach(o => 
                {
                    result.Options.Add(new OptionViewModel
                    {
                        Id = o.Id,
                        Text = o.Text,
                        QuestionId = question.Id
                    });
                });
                result.AnswerIndex = question.Options.IndexOf(question.Answer);
            }

            return result;
        }

        public void UpdateQuestion(QuestionViewModel questionViewModel)
        {
            Question question = _questionService.Get(questionViewModel.Id.Value);
            question.Title = questionViewModel.Title;
            for (int i = 0; i < question.Options.Count; i++)
            {
                question.Options[i].Text = questionViewModel.Options[i].Text;
            }
            question.Answer = _optionService.Get(questionViewModel.Options[questionViewModel.AnswerIndex.Value].Id) as Option;
            question.Level = questionViewModel.Level;
            question.Options.ForEach(o =>
            {
                _optionService.Update(o);
            });

            _questionService.Update(question);
        }

        public AwardViewModel CreateAward(AwardViewModel awardViewModel)
        {
            throw new System.NotImplementedException();
        }

        public AwardViewModel GetAward(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<AwardViewModel> GetAwards()
        {
            throw new System.NotImplementedException();
        }
        
        public void UpdateAward(AwardViewModel awardViewModel)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAward(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
