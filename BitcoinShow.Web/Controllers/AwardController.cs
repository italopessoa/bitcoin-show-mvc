using Microsoft.AspNetCore.Mvc;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Facade.Interface;
using Microsoft.AspNetCore.Cors;

namespace BitcoinShow.Web.Controllers
{
    public class AwardController : Controller
    {
        private readonly IBitcoinShowFacade _bitcoinShowFacade;
        public AwardController(IBitcoinShowFacade bitcoinShowFacade)
        {
            _bitcoinShowFacade = bitcoinShowFacade;
        }

        [HttpGet]
        [EnableCors("enable-cors")]
        public IActionResult List()
        {
            System.Collections.Generic.List<AwardViewModel> a = new System.Collections.Generic.List<AwardViewModel>();
            for (int i = 0; i < 10; i++)
            {
                a.Add(new AwardViewModel { Id = 1 + 1, Success = 1 + 2, Fail = 1 + 3, Quit = 14, Level = LevelEnum.Medium });
            }
            return Ok(_bitcoinShowFacade.GetAwards());
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Create", new AwardViewModel());
        }

        [HttpPost]
        public IActionResult Create(AwardViewModel awardViewModel)
        {
            if (ModelState.IsValid)
            {
                _bitcoinShowFacade.CreateAward(awardViewModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View("Create", _bitcoinShowFacade.GetAward(id));
        }

        [HttpPost]
        public IActionResult Edit(AwardViewModel awardViewModel)
        {
            _bitcoinShowFacade.UpdateAward(awardViewModel);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _bitcoinShowFacade.DeleteAward(id);
            return Ok();
        }
    }
}
