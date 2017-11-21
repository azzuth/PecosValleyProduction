using Pvp.Web.AppLogic.Models.DTO.Abstract;
using Pvp.Web.AppLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class ApprovedReview : Review
    {
        public ApprovedReview()
        {
            SourceOfReview = "Customer Submission";
        }
        public ApprovedReview(CustomerReview review)
        {
            this.LocationId = review.LocationId;
            this.Comment = review.Comment;
            this.Rating = review.Rating;
            this.DateOfReview = review.CreatedDate;
            this.SourceOfReview = "Customer Submission";
    }
    }
}