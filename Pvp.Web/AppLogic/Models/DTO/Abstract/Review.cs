using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.DTO.Abstract
{
    public class Review : DbObject
    {

        public Review()
        {

        }

        public Review(CustomerReview customerReview)
        {
            this.Comment = customerReview.Comment;
            this.DateOfReview = customerReview.CreatedDate;
            this.Location = customerReview.Location;
            this.Rating = customerReview.Rating;
        }

        public string Name { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public DateTime DateOfReview { get; set; } = DateTime.Now;
        public int PriorityOfDisplay { get; set; } = 10;
        public string SourceOfReview { get; set; }
        public string UrlToReview { get; set; }

        public bool DiscountRedeemed { get; set; } = false;
        public DateTime? DiscountRedeemedDate { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}