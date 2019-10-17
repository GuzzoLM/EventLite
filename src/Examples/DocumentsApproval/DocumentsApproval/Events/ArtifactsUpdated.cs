using System;
using System.Collections.Generic;
using EventLite;

namespace DocumentsApproval.Events
{
    public class ArtifactsUpdated : IRaisedEvent
    {
        public DateTime Timstamp { get; set; }

        public string EventType { get; set; }

        public IDictionary<string, string> ArtifactsAdded { get; set; }

        public IEnumerable<string> ArtifactsRemoved { get; set; }
    }
}