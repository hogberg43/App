using System;

namespace App.ErrorHandling.Domain
{
    public class ErrorInfo
    {
        public string OffendingUrl { get; set; }
        public string UserId { get; set; }
        public DateTime ErrorDate { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorDescription { get; set; }
        public string ApplicationName { get; set; }
        public int LogId { get; set; }
    }
}