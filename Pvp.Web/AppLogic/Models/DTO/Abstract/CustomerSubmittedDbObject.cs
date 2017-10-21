using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO.Abstract
{
    public abstract class CustomerSubmittedDbObject
    {
        public int Id { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location {get; set;}

        public string IpAddress { get; set; }     // TODO: Hash IP address so we don't store PII..?
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ViewedDate { get; set; } = DateTime.Now;
        public string ViewedBy { get; set; }
    }
}
