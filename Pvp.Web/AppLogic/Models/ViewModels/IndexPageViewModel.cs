using Pvp.Web.AppLogic.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.ViewModels
{
    public class IndexPageViewModel
    {
        public List<Publication> Articles { get; set; } = new List<Publication>();
        public List<Publication> Marketings { get; set; } = new List<Publication>();
        public List<Publication> Carousels { get; set; } = new List<Publication>();
    }
}