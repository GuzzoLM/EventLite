using System;
using System.Collections.Generic;
using System.Text;

namespace EventLite.MongoDB.Data
{
    internal class EventStream
    {
        public Guid StreamId { get; set; }

        public int HeadRevision { get; set; }

        public int UnsnapshottedCommits { get; set; }

        public int SnapshotRevision { get; set; }
    }
}
