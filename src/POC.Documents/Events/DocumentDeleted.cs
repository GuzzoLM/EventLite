using System;
using EventLite;

namespace POC.Documents.Events
{
    public class DocumentDeleted : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public Guid DocumentId { get; set; }
    }
}