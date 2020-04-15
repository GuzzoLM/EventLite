using EventLite.Streams;

namespace EventLite.StreamManager
{
    public class Snapshot : ISnapshot
    {
        public int SnapshotRevision { get; }

        public int SnapshotHeadCommit { get; }

        public object SnapshotData { get; }

        public Snapshot(int snapshotRevision, int snapshotHeadCommit, object snapshotData)
        {
            SnapshotRevision = snapshotRevision;
            SnapshotHeadCommit = snapshotHeadCommit;
            SnapshotData = snapshotData;
        }
    }
}