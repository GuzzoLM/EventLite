using System;
using System.Collections.Generic;

namespace EventLite.Streams
{
    public interface IEventStream
    {
        Guid StreamId { get; }
        int HeadRevision { get; }
        int UnsnapshottedCommits { get; }
        int SnapshotRevision { get; }
        ISnapshot Snapshot { get; }
        IEnumerable<ICommit> Events { get; }
        IEnumerable<ICommit> UncommittedEvents { get; }
        IEnumerable<ISnapshot> UncommittedSnapshots { get; }

        event EventHandler SnapshotRequired;

        void AddEvent(object @event);

        void AddSnapshot(object data);
    }
}