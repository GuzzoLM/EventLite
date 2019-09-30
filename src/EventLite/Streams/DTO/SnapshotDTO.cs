using System;

namespace EventLite.Streams.DTO
{
    internal class SnapshotDTO
    {
        public Guid StreamId { get; set; }
        public int SnapshotRevision { get; set; }
        public int SnapshotHeadCommit { get; set; }
        public object SnapshotData { get; set; }
    }
}