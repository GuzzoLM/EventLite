using System;
using System.Linq;
using System.Threading.Tasks;
using EventLite.Exceptions;
using EventLite.Streams;
using EventLite.Streams.StreamManager;

namespace EventLite
{
    public abstract class AggregateBase<T> : IAggregateBase
    {
        private IStreamManager _streamManager;

        private IAggregateSettings _aggregateSettings;

        private EventStream _stream;

        private Guid _streamId;

        public abstract T AggregateDataStructure { get; set; }

        public async Task FollowStream(Guid streamId, IStreamManager streamManager, IAggregateSettings aggregateSettings)
        {
            _streamId = streamId;
            _streamManager = streamManager;
            _aggregateSettings = aggregateSettings;

            var commitMinimumRevision = 0;
            var streamResult = await _streamManager.GetStream(_streamId).ConfigureAwait(false);
            _stream = streamResult.Data;

            if (_stream.SnapshotRevision > 0)
            {
                var snapshot = await _streamManager.GetSnapshot(_stream.StreamId, _stream.SnapshotRevision);
                AggregateDataStructure = (T)snapshot.Data.SnapshotData;
                commitMinimumRevision = snapshot.Data.SnapshotHeadCommit + 1;
            }

            var commits = await _streamManager.GetCommits(_streamId, commitMinimumRevision).ConfigureAwait(false);

            var events = commits.Data
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

            Snapshot snapshot = null;

            _stream.HeadRevision += 1;
            var commit = new Commit(_streamId, @event, _stream.HeadRevision);

            _stream.UnsnapshottedCommits += 1;

            if (_stream.UnsnapshottedCommits == _aggregateSettings.CommitsBeforeSnapshot)
            {
                _stream.SnapshotRevision += 1;
                snapshot = new Snapshot(_streamId, _stream.SnapshotRevision, _stream.HeadRevision, AggregateDataStructure);
                _stream.UnsnapshottedCommits = 0;
            }

            var streamResult = await _streamManager.UpsertStream(_stream);

            switch (streamResult.FailureReason)
            {
                case FailureReason.ConcurrencyError:
                    throw new EventStreamConcurrencyException($"StreamID: {_streamId}");
                default:
                    break;
            }

            await _streamManager.AddCommit(commit);

            if (snapshot != null)
            {
                await _streamManager.AddSnapshot(snapshot);
            }
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