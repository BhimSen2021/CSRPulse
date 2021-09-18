using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class MailDetail
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }       
        public string To { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string OTP { get; set; }
        public string Message { get; set; }
        public string Message2 { get; set; }
        public string Subject { get; set; }
    }
}
