using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pvp.Web.AppLogic.Models.DTO.Abstract
{
    public abstract class DbObject
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
    }
}
