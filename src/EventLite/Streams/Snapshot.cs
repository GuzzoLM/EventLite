using System;

namespace EventLite.Streams
{
    public class Snapshot<T>
    {
        public readonly Guid StreamId;

        public readonly int SnapshotRevision;

        public readonly int SnapshotHeadCommit;

        public readonly T SnapshotData;

        public Snapshot(Guid streamId, int snapshotRevision, int snapshotHeadCommit, T snapshotData)
        {
            StreamId = streamId;
            SnapshotRevision = snapshotRevision;
            SnapshotHeadCommit = snapshotHeadCommit;
            SnapshotData = snapshotData;
        }
    }
}