using System;

namespace EventLite.Streams
{
    public class EventStream
    {
        public readonly Guid StreamId;

        public int HeadRevision;

        public int UnsnapshottedCommits;

        public int SnapshotRevision;

        public EventStream(Guid streamId, int snapshotRevision = 0, int headRevision = 0, int unsnapshottedCommits = 0)
        {
            StreamId = streamId;
            HeadRevision = headRevision;
            SnapshotRevision = snapshotRevision;
            UnsnapshottedCommits = unsnapshottedCommits;
        }
    }
}