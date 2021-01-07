using System;

namespace EventLite.Streams.StreamManager.Implementation
{
    internal class Commit : ICommit
    {
        public int CommitNumber { get; }

        public long Timestamp { get; }

        public IRaisedEvent Event { get; }

        public Commit(object @event, int commitNumber)
        {
            Event = @event;
            Timestamp = DateTime.UtcNow.Ticks;
            CommitNumber = commitNumber;
        }
    }
}