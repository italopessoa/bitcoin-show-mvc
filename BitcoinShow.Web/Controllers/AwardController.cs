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
            return View();
        }

        public IActionResult Create()
        {
            return View(new AwardViewModel());
        }

        [HttpPost]
        public IActionResult Create(AwardViewModel awardViewModel)
        {   
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(AwardViewModel AwardViewModel)
        {
            return RedirectToAction("Index");
        }
    }
}
