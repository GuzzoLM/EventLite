using System;
using System.Collections.Generic;

namespace EventLite.Streams.StreamManager.Implementation
{
    internal class EventStream : IEventStream
    {
        public Guid StreamId { get; }
        public int HeadRevision { get; private set; }
        public int UnsnapshottedCommits { get; private set; }
        public int SnapshotRevision { get; private set; }
        public ISnapshot Snapshot { get; private set; }
        public IEnumerable<ICommit> Events { get; }
        public IEnumerable<ICommit> UncommittedEvents => _uncommittedEvents;
        public IEnumerable<ISnapshot> UncommittedSnapshots => _uncommittedSnapshots;

        public event EventHandler SnapshotRequired;

        public EventStream(
            Guid streamId,
            int snapshotRevision = 0,
            int headRevision = 0,
            int unsnapshottedCommits = 0,
            IEnumerable<ICommit> events = null,
            ISnapshot snapshot = null)
        {
            StreamId = streamId;
            HeadRevision = headRevision;
            SnapshotRevision = snapshotRevision;
            UnsnapshottedCommits = unsnapshottedCommits;
            Snapshot = snapshot;
            Events = events ?? new List<ICommit>();
            _uncommittedEvents = new List<ICommit>();
            _uncommittedSnapshots = new List<ISnapshot>();
        }

        public void AddEvent(object @event)
        {
            var commit = new Commit(@event, HeadRevision + 1);
            _uncommittedEvents.Add(commit);
            UnsnapshottedCommits++;
            HeadRevision++;

            if (_snapshotEnabled && UnsnapshottedCommits == AggregateSettings.SnapshotAtCommit)
            {
                EventHandler eventHandler = SnapshotRequired;
                eventHandler?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddSnapshot(object data)
        {
            Snapshot = new Snapshot(SnapshotRevision + 1, HeadRevision, data);
            _uncommittedSnapshots.Add(Snapshot);
            SnapshotRevision++;
        }

        private readonly List<ICommit> _uncommittedEvents;
        private readonly List<ISnapshot> _uncommittedSnapshots;
        private bool _snapshotEnabled => _aggregateSettings.SnapshotAtCommit > 0;
    }
}