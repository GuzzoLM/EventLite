using System;
using EventLite;

namespace DocumentsApproval.Events
{
    public class DocumentRenamed : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public string Name { get; set; }
    }
}