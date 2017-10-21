using Pvp.Web.AppLogic.Models.DTO.Abstract;
using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO
{
    // Customer Reviews turn into ApprovedReviews on approval
   

    public class ExternalReview : Review
    {
        public ExternalReview()
        {
            SourceOfReview = "External Service";
        }
        public ExternalReview(Review review)
        {
            this.Comment = review.Comment;
            this.Rating = review.Rating;
            this.DateOfReview = review.CreatedDate;
            review.SourceOfReview = "External Service";
        }
    }

   
}
