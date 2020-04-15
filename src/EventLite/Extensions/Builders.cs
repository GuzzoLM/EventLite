using System;
using System.Collections.Generic;
using EventLite.Streams;
using EventLite.Streams.StreamManager.Implementation;

namespace EventLite.Extensions
{
    public static class Builders
    {
        public static IEventStream EventStreamBuilder(
            Guid streamId,
            int snapshotRevision = 0,
            int headRevision = 0,
            int unsnapshottedCommits = 0,
            IEnumerable<ICommit> events = null,
            ISnapshot snapshot = null)
        {
            return new EventStream(
                streamId,
                snapshotRevision,
                headRevision,
                unsnapshottedCommits,
                events,
                snapshot);
        }
    }
}