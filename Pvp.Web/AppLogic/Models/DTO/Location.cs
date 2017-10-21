using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class Location : DbObject
    {
        [DisplayName("Location Name")]
        public string DisplayName { get; set; }
        [DisplayName("Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Hours")]
        public string HoursOfOperation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [DisplayName("State")]
        public string StateAbbreviation { get; set; } = "New Mexico";
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public string Comments { get; set; }        
        
        [ForeignKey("Menu")]
        public int? MenuId { get; set; }
        public virtual BioTrackThcMenu Menu { get; set; }
    }
}
