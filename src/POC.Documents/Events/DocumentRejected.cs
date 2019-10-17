using System;
using EventLite;

namespace POC.Documents.Events
{
    public class DocumentRejected : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public string RejectedBy { get; set; }

        public DateTime DateRejected { get; set; }
    }
}