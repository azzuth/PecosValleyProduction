using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pvp.Web.AppLogic.Models.DTO.Abstract;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class ContactRequest : RequestObject
    {
        public string Message { get; set; }
    }
}
