using Pvp.Web.AppLogic.Models.DTO;
using Pvp.Web.AppLogic.Models.DTO.Abstract;
using Pvp.Web.AppLogic.Models.ViewModels;
using Pvp.Web.AppLogic.DAL;
using Pvp.Web.AppLogic.Models;
using Pvp.Web.AppLogic.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Modules
{
    public class AdminModule
    {
        private PvpDbContext _context;

        public AdminModule(PvpDbContext context)
        {
            _context = context;
        }

        public AdminModule()
        {
            _context = PvpDbContext.Create();
        }

        private void ValidateLocation(Location location)
        {
            if (_context.Locations.Any(p => p.Address == location.Address && p.ZipCode == location.ZipCode))
                throw new Exception("This location already exists!");

            if (_context.Locations.Any(p => p.DisplayName.ToLower().Trim() == location.DisplayName.ToLower().Trim()))
                throw new Exception($"The location name {location.DisplayName} already exists in the database.");
        }

        public IList<Location> Locations => _context.Locations.AsNoTracking().ToList();
        public Location GetLocation(int id) => _context.Locations.Find(id);        
        public Location AddLocation(Location location)
        {
            ValidateLocation(location);

            _context.Locations.Add(location);
            _context.SaveChanges();
            return location;
        }
        public Location EditLocation(Location location)
        {

            _context.Locations.Attach(location);
            _context.Entry(location).State = EntityState.Modified;
            _context.SaveChanges();
            return location;
        }
        public void DeleteLocation(int id)
        {
            var item = _context.Locations.Find(id);
            _context.Locations.Remove(item);
            _context.SaveChanges();
        }

        // ADMIN STUFF FOR BIO TRACK THC MENU 
        //  TBD
        // LINK MENU TO LOCATION
        // ADD MENU ID?
        // API KEY?


        // List rejected customer reviews // List pending customer reviews // List Approved Reviews

        public void DeleteCustomerReview(int id)
        {
            var item = _context.CustomerReviews.Find(id);
            _context.CustomerReviews.Remove(item);
            _context.SaveChanges();
        }
        public void DeleteReview(int id)
        {
            var item = _context.Reviews.Find(id);
            _context.Reviews.Remove(item);
            _context.SaveChanges();

        }
        private IList<CustomerReview> CustomerReivews(bool? status = null) =>
            _context.CustomerReviews
                .Include("Location")
                .AsNoTracking()
                .Where(p => p.Approved == status).ToList();

        public IList<CustomerReview> CustomerReviewsPending => CustomerReivews();
        public IList<CustomerReview> CustomerReviewsApproved => CustomerReivews(true);
        public IList<CustomerReview> CustomerReviewsRejected => CustomerReivews(false);

        public IList<Faq> Faqs => _context.Faqs.AsNoTracking().ToList();
        public Faq GetFaq(int id) => _context.Faqs.Find(id);
        public Faq AddFaq(Faq faq)
        {
            var item = _context.Faqs.Add(faq);
            _context.SaveChanges();
            return item;
        }
        public Faq EditFaq(Faq faq)
        {
            _context.Faqs.Attach(faq);
            _context.Entry(faq).State = EntityState.Modified;
            _context.SaveChanges();
            return faq;
        }
        public void DeleteFaq(int id)
        {
            var item = _context.Faqs.Find(id);
            _context.Faqs.Remove(item);
            _context.SaveChanges();
        }

        public IList<Publication> Publications => _context.Publications.AsNoTracking().ToList();
        public Publication GetPublication(int id) => _context.Publications.Find(id);
        public Publication AddPublication(Publication publication)
        {
            var item = _context.Publications.Add(publication);
            _context.SaveChanges();
            return item;
        }
        public Publication EditPublication(Publication publication)
        {
            _context.Publications.Attach(publication);
            _context.Entry(publication).State = EntityState.Modified;
            _context.SaveChanges();
            return publication;
        }
        public void DeletePublication(int id)
        {
            var item = _context.Publications.Find(id);
            _context.Publications.Remove(item);
            _context.SaveChanges();
        }

        public CustomerReview RejectCustomerReview(int id, string currentUser)
        {
            var review = _context.CustomerReviews.Find(id);
            review.Approved = false;
            review.ViewedBy = currentUser;
            review.ViewedDate = DateTime.Now;
            _context.SaveChanges();

            return review;
        }
        public Review ApproveCustomerReview(int id, string currentUser)
        {
            var review = _context.CustomerReviews.Find(id);
            review.Approved = true;
            review.ViewedBy = currentUser;
            review.ViewedDate = DateTime.Now;

            var approvedReview = new ApprovedReview(review);
            _context.Reviews.Add(approvedReview);
            _context.SaveChanges();

            return approvedReview;
        }
        public Review AddExternalReview(Review review)
        {
            if (review.Rating == 0 || review.Comment.Length > 15)
                throw new Exception("Review must have a rating of at least 1 star and a comment length of more than 15 characters.");
            if (string.IsNullOrEmpty(review.UrlToReview) || string.IsNullOrEmpty(review.SourceOfReview))
                throw new Exception("External Reviews require a link to the site or location of the review.");


            var externalReview = new ExternalReview(review);
            _context.Reviews.Add(externalReview);
            _context.SaveChanges();
            return externalReview;
        }
    }
}