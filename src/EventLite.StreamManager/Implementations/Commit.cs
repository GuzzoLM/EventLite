using System;
using EventLite.Streams;

namespace EventLite.StreamManager
{
    public class Commit : ICommit
    {
        public int CommitNumber { get; }

        public long Timestamp { get; }

        public object Event { get; }

        public Commit(object @event, int commitNumber)
        {
            Event = @event;
            Timestamp = DateTime.UtcNow.Ticks;
            CommitNumber = commitNumber;
        }
    }
}