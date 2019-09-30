using System;
using EventLite.Streams.DTO;

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

        internal EventStream(EventStreamDTO eventStreamDTO)
        {
            StreamId = eventStreamDTO.StreamId;
            HeadRevision = eventStreamDTO.HeadRevision;
            SnapshotRevision = eventStreamDTO.SnapshotRevision;
            UnsnapshottedCommits = eventStreamDTO.UnsnapshottedCommits;
        }

        internal EventStreamDTO ToDTO()
        {
            return new EventStreamDTO
            {
                HeadRevision = this.HeadRevision,
                SnapshotRevision = this.SnapshotRevision,
                StreamId = this.StreamId,
                UnsnapshottedCommits = this.UnsnapshottedCommits
            };
        }
    }
}