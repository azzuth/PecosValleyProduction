using Pvp.Web.AppLogic.Models.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pvp.Web.AppLogic.Models.DTO
{
    public class RequestAttemptLog
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string AttemptType { get; set; }
        public bool Rejected { get; set; }
        public string SystemComment { get; set; }
    }

    public static class RequestTypes
    {
        public const string ContactRequest = "Contact Request";
        public const string AppointmentRequest = "Appointment Request";
        public const string CommentRequest = "Comment Request";
    }

    public static class RequestRejectionReasons
    {
        public const string TooFrequent = "Too many requests from this IP address in allowed time.";
        public const string Abuse = "This IP address is banned for abusive behavior.";
    }
}