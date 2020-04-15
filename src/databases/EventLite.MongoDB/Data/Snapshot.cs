using System;

namespace EventLite.MongoDB.Data
{
    internal class Snapshot
    {
        public Guid StreamId { get; set; }

        public int SnapshotRevision { get; set; }

        public int SnapshotHeadCommit { get; set; }

        public object SnapshotData { get; set; }
    }
}