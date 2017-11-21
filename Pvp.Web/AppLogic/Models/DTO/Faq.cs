using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class Faq : DbObject
    {
        [AllowHtml]
        public string Question { get; set; }
        [AllowHtml]
        public string Answer { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
    }
}