using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.ViewModels
{
    public class LocationDetails
    {
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string HoursOfOperation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string ZipCode { get; set; }
        public string Comments { get; set; }
    }
}