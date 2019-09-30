using System;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager.Implementation
{
    public class EventStreamReader : IEventStreamReader
    {
        private readonly IStreamManager _streamManager;

        public EventStreamReader(IStreamManager streamManager)
        {
            _streamManager = streamManager;
        }

        public async Task<TAggregate> GetAggregate<TAggregate>(Guid streamId) where TAggregate : IAggregateBase, new()
        {
            var aggregate = new TAggregate();
            await aggregate.FollowStream(streamId, _streamManager).ConfigureAwait(false);
            return aggregate;
        }
    }
}