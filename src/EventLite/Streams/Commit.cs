using System;

namespace EventLite.Streams
{
    public class Commit
    {
        public readonly Guid StreamId;

        public readonly int CommitNumber;

        public readonly long Timestamp;

        public readonly object Event;

        public Commit(Guid streamId, object @event, int commitNumber)
        {
            StreamId = streamId;
            Event = @event;
            Timestamp = DateTime.UtcNow.Ticks;
            CommitNumber = commitNumber;
        }
    }
}