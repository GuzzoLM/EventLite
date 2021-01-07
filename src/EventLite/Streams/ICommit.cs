namespace EventLite.Streams
{
    public interface ICommit
    {
        int CommitNumber { get; }

        long Timestamp { get; }

        IRaisedEvent Event { get; }
    }
}