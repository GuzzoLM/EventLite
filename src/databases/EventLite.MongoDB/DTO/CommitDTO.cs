using EventLite.MongoDB.Data;
using EventLite.Streams;

namespace EventLite.MongoDB.DTO
{
    internal class CommitDTO : ICommit
    {
        public int CommitNumber { get; set; }

        public long Timestamp { get; set; }

        public object Event { get; set; }

        public static ICommit From(Commit commit)
        {
            return new CommitDTO
            {
                CommitNumber = commit.CommitNumber,
                Event = commit.Event,
                Timestamp = commit.Timestamp
            };
        }
    }
}