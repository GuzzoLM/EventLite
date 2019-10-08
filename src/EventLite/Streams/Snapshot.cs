using System;

namespace EventLite.Streams
{
    public class Snapshot
    {
        public readonly Guid StreamId;

        public readonly int SnapshotRevision;

        public readonly int SnapshotHeadCommit;

        public readonly object SnapshotData;

        public Snapshot(Guid streamId, int snapshotRevision, int snapshotHeadCommit, object snapshotData)
        {
            StreamId = streamId;
            SnapshotRevision = snapshotRevision;
            SnapshotHeadCommit = snapshotHeadCommit;
            SnapshotData = snapshotData;
        }
    }
}