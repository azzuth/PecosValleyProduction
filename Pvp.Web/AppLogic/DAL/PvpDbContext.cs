using Pvp.Web.AppLogic.Models.DTO;
using Pvp.Web.AppLogic.Models.DTO.Abstract;
using Pvp.Web.AppLogic.Models;
using Pvp.Web.AppLogic.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.DAL
{
    public class PvpDbContext : DbContext
    {

        public PvpDbContext()
#if DEBUG
            : base("PvpDbLocal")
#else
            : base("PvpDb")
#endif
        {
            this.Database.CreateIfNotExists();
        }

        public static PvpDbContext Create()
        {
            return new PvpDbContext();
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<BioTrackThcMenu> Menus { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }

        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }

        public DbSet<IpBlacklist> IpBlacklists { get; set; }
        public DbSet<RequestAttemptLog> RequestAttemptLogs { get; set; }

        // Marketing/Publications
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Faq> Faqs { get; set; }

    }
}