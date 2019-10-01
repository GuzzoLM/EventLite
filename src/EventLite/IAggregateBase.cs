using System;
using System.Threading.Tasks;
using EventLite.Streams.StreamManager;

namespace EventLite
{
    public interface IAggregateBase
    {
        Task FollowStream(Guid streamId, IStreamManager streamManager, IAggregateSettings aggregateSettings);
    }
}