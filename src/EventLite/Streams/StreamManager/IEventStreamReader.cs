using System;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager
{
    public interface IEventStreamReader
    {
        /// <summary>
        /// Gets an Aggregate of the specified type and with the specified unique id
        /// </summary>
        /// <typeparam name="TAggregate">The type of the Aggregate</typeparam>
        /// <param name="streamId">The Aggregate's unique identifier</param>
        /// <returns></returns>
        Task<TAggregate> GetAggregate<TAggregate>(Guid streamId) where TAggregate : IAggregateBase, new();
    }
}