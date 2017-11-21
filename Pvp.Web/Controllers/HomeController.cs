using Pvp.Web.AppLogic.Modules;
using Pvp.Web.AppLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pvp.Web.AppLogic.Models.DTO;
using Pvp.Web.AppLogic.Models.DTO.Abstract;
using Microsoft.Owin;

namespace Pvp.Web.Controllers
{
    public class HomeController : Controller
    {
        MainModule _main;

        public HomeController()
        {
            _main = new MainModule();
        }

        string key = "PvpHomeId";

        public ActionResult Index()
        {
            return View(_main.LoadIndexPage());
        }

        public ActionResult SocialMedia()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var model = _main.Locations;
            return View(model);
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

        [HttpGet]
        public ActionResult LocationList()
        {
            var items = _main.Locations;
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Locations()
        {
            var items = _main.Locations;
            return View(items);
        }

        //TODO: Find solution to this.
        public void SetHomeId(string id)
        {
            var cookie = new HttpCookie(key, id);
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.AppendCookie(cookie);
            
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
            var model = _main.Faqs;
            return View("Faq", model);
        }

        [HttpGet]
        public ActionResult ReviewArea()
        {
            var model = _main.CustomerReviewsForDisplay;
            return PartialView("_CommentCardList", model);
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
                var response = _main.AttemptResponse(() => { 
                    model.IpAddress = Request.UserHostAddress;        
                    model = _main.SubmitCustomerReview(model);
                    return model;
                });

                if (response.Success)
                {
                    return View("CommentSubmitted", response.ReturnObject);
                }
                else
                {
                    return RedirectToAction("HandleError", "Home", new { message = response.Message });
                }
            }
            return View(model);
        }

        public ActionResult HandleError(string message)
        {
            return View(viewName: "ErrorMessage", model: message);
        }

    }
}