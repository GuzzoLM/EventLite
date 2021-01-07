namespace EventLite.Streams.StreamManager.Implementation
{
    internal class Snapshot : ISnapshot
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