using System;
using System.Linq;
using System.Threading.Tasks;
using EventLite.Streams;
using EventLite.Streams.StreamManager;

namespace EventLite
{
    public abstract class AggregateBase<T> : IAggregateBase
    {
        private IStreamManager _streamManager;

        private EventStream _stream;

        private Guid _streamId;

        public abstract T AggregateDataStructure { get; set; }

        public async Task FollowStream(Guid streamId, IStreamManager streamManager)
        {
            _streamId = streamId;
            _streamManager = streamManager;

            var commitMinimumRevision = 0;
            _stream = await _streamManager.GetStream(_streamId).ConfigureAwait(false);

            if (_stream.SnapshotRevision > 0)
            {
                var snapshot = await _streamManager.GetSnapshot<T>(_stream.StreamId, _stream.SnapshotRevision);
                AggregateDataStructure = snapshot.SnapshotData;
                commitMinimumRevision = snapshot.SnapshotHeadCommit + 1;
            }

            var commits = await _streamManager.GetCommits(_streamId, commitMinimumRevision).ConfigureAwait(false);

            var events = commits
                .OrderBy(x => x.CommitNumber)
                .Select(x => x.Event);

            foreach (var @event in events)
            {
                ApplyEvent(@event);
            }
        }

        public async Task Save(object @event)
        {
            ApplyEvent(@event);

            _stream.HeadRevision += 1;
            var commit = new Commit(_streamId, @event, _stream.HeadRevision);
            await _streamManager.AddCommit(commit);

            _stream.UnsnapshottedCommits += 1;

            if (_stream.UnsnapshottedCommits == 10)
            {
                _stream.SnapshotRevision += 1;
                var snapshot = new Snapshot<T>(_streamId, _stream.SnapshotRevision, _stream.HeadRevision, AggregateDataStructure);
                await _streamManager.AddSnapshot<T>(snapshot);
                _stream.UnsnapshottedCommits = 0;
            }

            await _streamManager.UpsertStream(_stream);
        }

        private void ApplyEvent(object @event)
        {
            var handler = this.GetType().FindInterfaces(HandlerInterfaceFilter, ((IRaisedEvent)@event).EventType)[0];
            var handlerMethod = handler.GetMethod("Apply");
            handlerMethod.Invoke(this, new object[] { @event });
        }

        private static bool HandlerInterfaceFilter(Type typeObj, Object criteriaObj)
        {
            return typeObj.GetGenericArguments().Length > 0
                && typeObj.GetGenericArguments()[0].Name == criteriaObj.ToString();
        }
    }
}