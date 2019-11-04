using System;
using System.Threading.Tasks;
using EventLite.Streams.StreamManager;

namespace EventLite
{
    public interface IAggregateBase
    {
        /// <summary>
        /// Get an Aggregate with the supplyed Id and apply its events to reach the Aggregate's final state
        /// </summary>
        /// <param name="streamId">The Aggregate unique identifier</param>
        /// <param name="streamManager">The implemented manager for the stream</param>
        /// <param name="aggregateSettings">The settings for the aggregate</param>
        /// <returns></returns>
        Task FollowStream(Guid streamId, IStreamManager streamManager, IAggregateSettings aggregateSettings);

        /// <summary>
        /// Save an event in the Aggregate
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Save(object @event);
    }
}