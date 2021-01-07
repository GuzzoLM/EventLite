using EventLite.MongoDB.Data;
using EventLite.Streams;

namespace EventLite.MongoDB.DTO
{
    internal class SnapshotDTO : ISnapshot
    {
        public int SnapshotRevision { get; set; }

        public int SnapshotHeadCommit { get; set; }

        public object SnapshotData { get; set; }

        public static ISnapshot From(Snapshot snapshot)
        {
            return new SnapshotDTO
            {
                SnapshotData = snapshot.SnapshotData,
                SnapshotHeadCommit = snapshot.SnapshotHeadCommit,
                SnapshotRevision = snapshot.SnapshotRevision
            };
        }
    }
}