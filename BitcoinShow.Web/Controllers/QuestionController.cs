using BitcoinShow.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using BitcoinShow.Web.Models;
using System.Collections.Generic;

namespace BitcoinShow.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;
        public QuestionController(IQuestionService questionService, IOptionService optionService)
        {
            this._questionService = questionService;
            this._optionService = optionService;
        }

        public IActionResult Index()
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
            return View(result);
        }   

        [HttpPost]
        public IActionResult Create(QuestionViewModel model)
        {
            Question question = new Question();
            question.Title = model.Title;
            
            List<Option> options = new List<Option>();
            model.Options.ForEach(o => 
            {
                options.Add(this._optionService.Add(o.Text) as Option);
            });

            question.Answer = options[0];

            this._questionService.Add(question);
            options.ForEach(o => 
            {
                o.QuestionId = question.Id;
                this._optionService.Update(o);
            });

            List<QuestionViewModel> result = new List<QuestionViewModel>();
            this._questionService.GetAll().ForEach(q => 
            {
                result.Add(new QuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title
                });
            });
            return View("Index",result);
        }
        public IActionResult Create()
        {
            return View(new QuestionViewModel());
        }

        public IActionResult Edit(int id)
        {
            QuestionViewModel result = null;
            Question question = this._questionService.Get(id);
            if(question != null)
            {
                result = new QuestionViewModel
                {
                    Id = question.Id,
                    Title = question.Title
                };
            }

            return View(result);
        }
    }
}
