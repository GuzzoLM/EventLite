using System;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager
{
    public interface IEventStreamReader
    {
        Task<TAggregate> GetAggregate<TAggregate>(Guid streamId) where TAggregate : IAggregateBase, new();
    }
}