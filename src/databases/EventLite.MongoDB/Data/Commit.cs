using System;

namespace EventLite.MongoDB.Data
{
    internal class Commit
    {
        public Guid StreamId { get; set; }

        public int CommitNumber { get; set; }

        public long Timestamp { get; set; }

        public IRaisedEvent Event { get; set; }
    }
}