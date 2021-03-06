﻿using Pvp.Web.AppLogic.Models.Business;
using Pvp.Web.AppLogic.Models.DTO;
using Pvp.Web.AppLogic.DAL;
using Pvp.Web.AppLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Pvp.Web.AppLogic.Models.DTO.Abstract;
using Pvp.Web.AppLogic.Models.ViewModels;

namespace Pvp.Web.AppLogic.Modules
{
    public class MainModule
    {
        private PvpDbContext _context;

        public MainModule()
        {
            _context = PvpDbContext.Create();
        }
        public MainModule(PvpDbContext context)
        {
            _context = context;
        }

        public IList<Location> Locations => _context.Locations.AsNoTracking().ToList();
        public IList<Faq> Faqs => _context.Faqs.AsNoTracking().Where(p => p.Active).ToList();
        public IList<Review> CustomerReviewsForDisplay => _context.Reviews.Include("Location").AsNoTracking().ToList();

        public IList<Publication> Marketings => _context.Publications
            .Where(p => p.Active 
                && p.ExpiresOn > DateTime.Today 
                && p.PageLocation == PageLocation.Marketing)
            .Take(3)
            .ToList();
        public IList<Publication> Carousels => _context.Publications
            .Where(p => p.Active
                && p.ExpiresOn > DateTime.Today
                && p.PageLocation == PageLocation.Carousel)
            .Take(3)
            .ToList();
        public IList<Publication> Articles => _context.Publications
            .Where(p => p.Active
                && p.ExpiresOn > DateTime.Today
                && p.PageLocation == PageLocation.Article)
            .Take(5)
            .ToList();

        public IndexPageViewModel LoadIndexPage()
        {
            var model = new IndexPageViewModel()
            {
                Marketings = Marketings.ToList(),
                Carousels = Carousels.ToList(),
                Articles = Articles.ToList()
            };
            return model;
        }

        public int RejectionAttemptThreshhold { get; private set; } = 10;
        public int BannableAttemptThreshhold { get; private set; } = 200;
        public int AttemptHourTimelimit { get; private set; } = 1;
        public static int MaxMonthlyCommentThreshhold { get; private set; } = 3;

        public RequestAttemptLog LogRequest(string ipAddress, string requestType)
        {
            ipAddress = ipAddress.Trim();

            // CHECK FOR BANNED IPS
            if (_context.IpBlacklists.Any(p => p.IpAddress == ipAddress && p.BanExpires > DateTime.Now))
                return new RequestAttemptLog() { Rejected = true, SystemComment = RequestRejectionReasons.Abuse };

            // CHECK FOR TOO MANY REQUESTS
            var attempt = new RequestAttemptLog() { IpAddress = ipAddress, AttemptType = requestType };
            var attempts = _context.RequestAttemptLogs.Where(p => p.IpAddress == ipAddress && p.AttemptType == requestType).ToList();
            attempts = attempts.Where(p => p.Date.Year == DateTime.Now.Year
                    && p.Date.Month == DateTime.Now.Month
                    && p.Date.Day == DateTime.Now.Day
                    && (DateTime.Now - p.Date).Hours <= AttemptHourTimelimit).ToList();

            if(attempts.Count() > RejectionAttemptThreshhold)
            {
                attempt.Rejected = true;
                attempt.SystemComment = RequestRejectionReasons.TooFrequent;
            }
            // BAN IP IF ACTING MALICIOUSLY
            if(attempts.Count() > BannableAttemptThreshhold)
            {
                var previousBans = _context.IpBlacklists.Count(p => p.IpAddress == ipAddress);
                var daysForBan = (int)Math.Pow(2d, previousBans); // 2^NumberOfBans days

                _context.IpBlacklists.Add(new IpBlacklist() {
                    IpAddress = ipAddress,
                    BannedReason = $"{BannableAttemptThreshhold} attempts for {requestType} in {AttemptHourTimelimit} hour(s).",
                    BanExpires = daysForBan < 100 ? DateTime.Now.AddDays(daysForBan) : DateTime.Now.AddYears(10)
                });

                attempt.Rejected = true;
                attempt.SystemComment = RequestRejectionReasons.Abuse;                
            }

            _context.RequestAttemptLogs.Add(attempt);
            _context.SaveChanges();

            return attempt;
        }

        public CustomerReview SubmitCustomerReview(CustomerReview review)
        {
            return AttemptRequest(review.IpAddress, () =>
            {
                if (_context.CustomerReviews.Any(p => p.IpAddress == review.IpAddress
                     && p.Rating == review.Rating
                     && p.Comment == review.Comment))
                    throw new Exception("You have already entered this comment. Thank you!");

                if (_context.CustomerReviews
                        .Count(p => p.IpAddress == review.IpAddress
                         && p.CreatedDate.Year == DateTime.Now.Year 
                         && p.CreatedDate.Month == DateTime.Now.Month)
                        > MaxMonthlyCommentThreshhold)
                    throw new Exception($"You have reviewed us too often. You can enter {MaxMonthlyCommentThreshhold} comments per IP address.  We appreciate your support.");

                _context.CustomerReviews.Add(review);
                _context.SaveChanges();
                return _context.CustomerReviews
                    .Include("Location")
                    .First(p=>p.Id == review.Id);
            });
        }

        private T AttemptRequest<T>(string ipAddress, Func<T> func)
        {
            var type = typeof(T).Name;
            var request = LogRequest(ipAddress, type);
            if (request.Rejected)
                throw new Exception($"Your request was rejected: {request.SystemComment}.");

            return func.Invoke();
        }
        public Response<T> AttemptResponse<T>(Func<T> func)
        {
            try
            {
                var response = new Response<T>();
                response.ReturnObject = func.Invoke();
                response.Success = true;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<T>() { Success = false, Message = ex.Message };
            }
        }
    }
}