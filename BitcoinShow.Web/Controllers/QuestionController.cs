using BitcoinShow.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using BitcoinShow.Web.Models;
using System.Collections.Generic;
using BitcoinShow.Web.Facade.Interface;

namespace BitcoinShow.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IBitcoinShowFacade _bitcoinShowFacade;
        public QuestionController(IBitcoinShowFacade bitcoinShowFacade)
        {
            this._bitcoinShowFacade = bitcoinShowFacade;
        }

        public IActionResult Index()
        {
            return View(this._bitcoinShowFacade.GetAllQuestions());
        }   

        [HttpPost]
        public IActionResult Create(QuestionViewModel model)
        {
            this._bitcoinShowFacade.CreateQuestion(model);
            return View("Index",this._bitcoinShowFacade.GetAllQuestions());
        }
        public IActionResult Create()
        {
            return View(new QuestionViewModel());
        }

        public IActionResult Edit(int id)
        {
            return View(this._bitcoinShowFacade.GetQuestion(id));
        }
    }
}
