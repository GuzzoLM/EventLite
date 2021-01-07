namespace EventLite.Streams
{
    public interface ISnapshot
    {
        int SnapshotRevision { get; }

        int SnapshotHeadCommit { get; }

        object SnapshotData { get; }
    }
}