using Pvp.Web.AppLogic.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.ViewModels
{
    public class ManageReviewPage
    {
        public IEnumerable<CustomerReview> Reviews { get; set; }
        public bool EditMode = false;
        public string ReviewPageTitle = "Manage Reviews";
    }
}