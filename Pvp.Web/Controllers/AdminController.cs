using Pvp.Web.AppLogic.Modules;
using Pvp.Web.AppLogic.Models.DTO;
using System.Web.Mvc;

namespace Pvp.Web.Controllers
{
    public class AdminController : Controller
    {
        private AdminModule _admin;
        private MainModule _main;

        public AdminController()
        {
            _admin = new AdminModule();
            _main = new MainModule();
        }

        public ActionResult Locations()
        {
            var model = _admin.Locations;
            return View("Locations", model);
        }

        [HttpGet]
        public ActionResult EditLocation(int id)
        {
            var model = _admin.GetLocation(id);
            return PartialView("EditLocation", model);
        }

        [HttpPost]
        public ActionResult DeleteLocation(Location model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { _admin.DeleteLocation(model.Id);  return true; });
                return RedirectToAction("Locations");
            }
            return RedirectToAction("Locations");
        }

        [HttpPost]
        public ActionResult EditLocation(Location model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { return _admin.EditLocation(model); });
                return RedirectToAction("Locations");
            }
            return RedirectToAction("Locations");
        }

        [HttpGet]
        public ActionResult AddLocation()
        {
            return PartialView(new Location());
        }

        [HttpPost]
        public ActionResult AddLocation(Location model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { return _admin.AddLocation(model); });
                return RedirectToAction("Locations");
            }
            return RedirectToAction("Locations");
        }
    }
}