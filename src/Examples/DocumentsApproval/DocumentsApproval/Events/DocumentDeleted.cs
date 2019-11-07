using System;
using EventLite;

namespace DocumentsApproval.Events
{
    public class DocumentDeleted : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }
    }
}