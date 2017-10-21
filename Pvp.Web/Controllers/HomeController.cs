using Pvp.Web.AppLogic.Modules;
using Pvp.Web.AppLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pvp.Web.AppLogic.Models.DTO;

namespace Pvp.Web.Controllers
{
    public class HomeController : Controller
    {
        MainModule _main;

        public HomeController()
        {
            _main = new MainModule();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SocialMedia()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Locations = _main.Locations;
            return View();
        }

        public ActionResult Videos()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LocationFooter()
        {
            var model = _main.Locations;
            return PartialView("_LocationsMap", model);
        }

        [HttpPost]
        public ActionResult AttemptAddComment(CustomerReview model)
        {
            if (ModelState.IsValid)
            {
                model.IpAddress = Request.UserHostAddress;        
                model = _main.SubmitCustomerReview(model);
            }
            return View("CommentSubmitted", model);
        }
    }
}