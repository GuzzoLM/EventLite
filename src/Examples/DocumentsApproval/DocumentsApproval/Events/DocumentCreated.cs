using System;
using DocumentsApproval.Model;
using EventLite;

namespace DocumentsApproval.Events
{
    public class DocumentCreated : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public Document Document { get; set; }
    }
}