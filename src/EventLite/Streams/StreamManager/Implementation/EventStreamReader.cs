using System;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager.Implementation
{
    public class EventStreamReader : IEventStreamReader
    {
        private readonly IStreamManager _streamManager;
        private readonly IAggregateSettings _aggregateSettings;

        public EventStreamReader(IStreamManager streamManager, IAggregateSettings aggregateSettings)
        {
            _streamManager = streamManager;
            _aggregateSettings = aggregateSettings;
        }

        public async Task<TAggregate> GetAggregate<TAggregate>(Guid streamId) where TAggregate : IAggregateBase, new()
        {
            var aggregate = new TAggregate();
            await aggregate.FollowStream(streamId, _streamManager, _aggregateSettings).ConfigureAwait(false);
            return aggregate;
        }
    }
}