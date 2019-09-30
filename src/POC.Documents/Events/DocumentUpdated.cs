using System;
using POC.Documents.Model;
using EventLite;

namespace POC.Documents.Events
{
    public class DocumentUpdated : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public Document Document { get; set; }
    }
}