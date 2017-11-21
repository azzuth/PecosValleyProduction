using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class Publication : DbObject
    {
        [AllowHtml]
        public string PostHtml { get; set; }
        public string PostTitle { get; set; }
        public PageLocation PageLocation { get; set; }
        public string ImageFileName { get; set; } = "publicationPlaceholder.jpg";
        public DateTime ExpiresOn { get; set; } = DateTime.Now.AddYears(1);
        public bool Active { get; set; }
    }

    public enum PageLocation
    {
        Marketing,
        Carousel,
        Article
    }

}