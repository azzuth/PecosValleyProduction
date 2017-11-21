using Pvp.Web.AppLogic.Modules;
using Pvp.Web.AppLogic.Models.DTO;
using System.Web.Mvc;
using Pvp.Web.AppLogic.Models.ViewModels;
using System.Linq;
using Pvp.Web.AppLogic.Models.DTO.Abstract;

namespace Pvp.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private AdminModule _admin;
        private MainModule _main;

        public AdminController()
        {
            _admin = new AdminModule();
            _main = new MainModule();
        }

        public readonly string Layout = "~/Views/Shared/_AdminLayout";

        // -- PROMOTIONS

        // -- REVIEWS
        public ActionResult ManageReviews()
        {
            return RedirectToAction("PendingReviews");
        }
        public ActionResult PendingReviews()
        {
            var model = new ManageReviewPage
            {
                Reviews = _admin.CustomerReviewsPending,
                ReviewPageTitle = "Pending Reviews",
                EditMode = true
            };
            return View("ManageReviews", model);
        }
        public ActionResult RejectedReviews()
        {
            var model = new ManageReviewPage
            {
                Reviews = _admin.CustomerReviewsRejected,
                ReviewPageTitle = "Rejected Reviews",
                EditMode = false
            };
            return View("ManageReviews", model);
        }
        public ActionResult ApprovedReviews()
        {
            var model = new ManageReviewPage
            {
                Reviews = _admin.CustomerReviewsApproved,
                ReviewPageTitle = "Approved Reviews",
                EditMode = false
            };
            return View("ManageReviews", model);
        }

        public ActionResult LiveReviews()
        {
            var model = new ManageReviewPage
            {
                Reviews = _main.CustomerReviewsForDisplay.Select(p => new CustomerReview()
                {
                    Id = p.Id,
                    Comment = p.Comment,
                    LocationId = p.LocationId,
                    Rating = p.Rating ?? 0,
                    IpAddress = "Not Stored once approved"
                }),
                ReviewPageTitle = "Live Reviews",
                EditMode = false
            };
            return View("ManageReviews", model);
        }

        [HttpGet]
        public ActionResult ReviewReview(int id)
        {
            var model = _admin.CustomerReviewsPending.FirstOrDefault(p => p.Id == id);

            return PartialView("ApproveReview", new Review(model));
        }
        [HttpPost]
        public ActionResult ApproveReview(int id)
        {
            var model = _admin.ApproveCustomerReview(id, ""); // TODO: use logged in user
            return RedirectToAction("PendingReviews", "Admin");
        }
        [HttpPost]
        public ActionResult RejectReview(int id)
        {
            var model = _admin.RejectCustomerReview(id, ""); // TODO: use logged in user
            return RedirectToAction("PendingReviews", "Admin");
        }
        [HttpPost]
        public ActionResult DeleteReview(string reviewTitlePage, int id)
        {
            if (reviewTitlePage.StartsWith("Live")) _admin.DeleteReview(id);
            else _admin.DeleteCustomerReview(id);

            switch (reviewTitlePage)
            {
                case "Approved Reviews":
                    return RedirectToAction("ApprovedReviews", "Admin");
                case "Rejected Reviews":
                    return RedirectToAction("RejectedReviews", "Admin");
                case "Live Reviews":
                    return RedirectToAction("LiveReviews", "Admin");
                default:
                    return RedirectToAction("PendingReviews", "Admin");
            }

        }

        // -- LOCATIONS
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
                var response = _main.AttemptResponse(() => { _admin.DeleteLocation(model.Id); return true; });
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

        // -- FAQs

        public ActionResult Faqs()
        {
            var model = _admin.Faqs;
            return View("Faqs", model);
        }

        [HttpGet]
        public ActionResult EditFaq(int id)
        {
            var model = _admin.GetFaq(id);
            return PartialView("EditFaq", model);
        }

        [HttpPost]
        public ActionResult DeleteFaq(Faq model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { _admin.DeleteFaq(model.Id); return true; });
                return RedirectToAction("Faqs");
            }
            return RedirectToAction("Faqs");
        }

        [HttpPost]
        public ActionResult EditFaq(Faq model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { return _admin.EditFaq(model); });
                return RedirectToAction("Faqs");
            }
            return RedirectToAction("Faqs");
        }

        [HttpGet]
        public ActionResult AddFaq()
        {
            return PartialView(new Faq());
        }

        [HttpPost]
        public ActionResult AddFaq(Faq model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { return _admin.AddFaq(model); });
                return RedirectToAction("Faqs");
            }
            return RedirectToAction("Faqs");
        }



        public ActionResult Publications()
        {
            var model = _admin.Publications;
            return View("Publications", model);
        }

        [HttpGet]
        public ActionResult EditPublication(int id)
        {
            var model = _admin.GetPublication(id);
            return PartialView("EditPublication", model);
        }

        [HttpPost]
        public ActionResult DeletePublication(Publication model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { _admin.DeletePublication(model.Id); return true; });
                return RedirectToAction("Publications");
            }
            return RedirectToAction("Publications");
        }

        [HttpPost]
        public ActionResult EditPublication(Publication model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { return _admin.EditPublication(model); });
                return RedirectToAction("Publications");
            }
            return RedirectToAction("Publications");
        }

        [HttpGet]
        public ActionResult AddPublication()
        {
            return PartialView(new Publication());
        }

        [HttpPost]
        public ActionResult AddPublication(Publication model)
        {
            if (ModelState.IsValid)
            {
                var response = _main.AttemptResponse(() => { return _admin.AddPublication(model); });
                return RedirectToAction("Publications");
            }
            return RedirectToAction("Publications");
        }
    }
}