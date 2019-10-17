using System;
using System.Collections.Generic;
using System.Text;
using EventLite;

namespace DocumentsApproval.Events
{
    public class DocumentApprovalStatusChanged : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public string ApprovedBy { get; set; }
        public string RejectedByBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
    }
}
