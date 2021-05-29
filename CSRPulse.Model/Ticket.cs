using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class Ticket : BaseModel
    {
        public int TicketId { get; set; }
        public bool Status { get; set; }
        public string message { get; set; }
        public int ClosedBy { get; set; }
    }
}
