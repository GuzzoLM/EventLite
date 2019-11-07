using System;
using EventLite;

namespace DocumentsApproval.Events
{
    public class DocumentApproved : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime DateApproved { get; set; }
    }
}