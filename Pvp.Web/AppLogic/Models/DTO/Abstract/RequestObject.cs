using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO.Abstract
{
    public abstract class RequestObject : CustomerSubmittedDbObject
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int? UserId { get; set; } // DO they have a user id?
        public bool? Responded { get; set; }
        public DateTime? RespondedOn { get; set; } = DateTime.Now;
    }
}
