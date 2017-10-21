using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class CustomerReview : CustomerSubmittedDbObject
    {
        public bool? Approved { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 0 - 5
        public int UserId { get; set; }
    }
}
