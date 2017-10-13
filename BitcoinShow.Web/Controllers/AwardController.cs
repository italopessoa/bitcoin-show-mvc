using Microsoft.AspNetCore.Mvc;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Facade.Interface;

namespace BitcoinShow.Web.Controllers
{
    public class AwardController : Controller
    {
        private readonly IBitcoinShowFacade _bitcoinShowFacade;
        public AwardController(IBitcoinShowFacade bitcoinShowFacade)
        {
            _bitcoinShowFacade = bitcoinShowFacade;
        }

        public IActionResult Index()
        {
            return View(_bitcoinShowFacade.GetAwards());
        }

        public IActionResult Create()
        {
            return View(new AwardViewModel());
        }

        [HttpPost]
        public IActionResult Create(AwardViewModel awardViewModel)
        {  
            _bitcoinShowFacade.CreateAward(awardViewModel); 
            return RedirectToAction("Index",_bitcoinShowFacade.GetAwards());
        }

        public IActionResult Edit(int id)
        {
            return View(_bitcoinShowFacade.GetAward(id));
        }

        [HttpPost]
        public IActionResult Edit(AwardViewModel awardViewModel)
        {
            _bitcoinShowFacade.UpdateAward(awardViewModel);
            return RedirectToAction("Index");
        }
    }
}
