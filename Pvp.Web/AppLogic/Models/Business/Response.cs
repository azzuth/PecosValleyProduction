using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.Business
{
    public class Response<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T ReturnObject { get; set; }
    }
}