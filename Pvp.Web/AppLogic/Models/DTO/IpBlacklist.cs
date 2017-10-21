using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class IpBlacklist
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string BannedReason { get; set; }
        public DateTime DateBanned { get; set; } = DateTime.Now;
        public DateTime? BanExpires { get; set; } = DateTime.Now.AddDays(2);
    }
}