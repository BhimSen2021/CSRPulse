using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class MailSendStatus
    {
        public int MsstatusId { get; set; }
        public int? CustomerId { get; set; }
        public string ToEmails { get; set; }

        public string CcEmails { get; set; }
        public int? SubjectId { get; set; }

        public string MailContent { get; set; }

        public DateTime SentOn { get; set; }

        public bool? Status { get; set; }
    }
}
