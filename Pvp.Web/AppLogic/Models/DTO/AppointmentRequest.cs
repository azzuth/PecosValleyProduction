using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class AppointmentRequest : RequestObject
    {
        public DateTime RequestedDateTime { get; set; } = DateTime.Now;
    }
}
