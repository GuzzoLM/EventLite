using System;
using EventLite.Streams.DTO;

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

        internal Commit(CommitDTO commitDTO)
        {
            StreamId = commitDTO.StreamId;
            Event = commitDTO.Event;
            Timestamp = commitDTO.Timestamp;
            CommitNumber = commitDTO.CommitNumber;
        }

        internal CommitDTO ToDTO()
        {
            return new CommitDTO
            {
                CommitNumber = this.CommitNumber,
                Timestamp = this.Timestamp,
                Event = this.Event,
                StreamId = this.StreamId
            };
        }
    }
}