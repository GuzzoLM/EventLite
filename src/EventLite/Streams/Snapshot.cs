using System;
using EventLite.Streams.DTO;

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

        internal Snapshot(SnapshotDTO snapshotDTO)
        {
            StreamId = snapshotDTO.StreamId;
            SnapshotRevision = snapshotDTO.SnapshotRevision;
            SnapshotHeadCommit = snapshotDTO.SnapshotHeadCommit;
            SnapshotData = (T)snapshotDTO.SnapshotData;
        }

        internal SnapshotDTO ToDTO()
        {
            return new SnapshotDTO
            {
                SnapshotData = this.SnapshotData,
                SnapshotHeadCommit = this.SnapshotHeadCommit,
                SnapshotRevision = this.SnapshotRevision,
                StreamId = this.StreamId
            };
        }
    }
}