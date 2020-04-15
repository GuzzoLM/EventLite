using System;
using System.Threading.Tasks;

namespace EventLite.Streams
{
    public interface IStreamManager
    {
        /// <summary>
        /// Update a event stream or, if it doesn't exist, insert it
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task UpsertStream(IEventStream stream);

        /// <summary>
        /// Get a event stream with the supplied unique identifier
        /// </summary>
        /// <param name="streamId">The event stream unique identifier</param>
        /// <returns></returns>
        Task<IEventStream> GetStream(Guid streamId);

        /// <summary>
        /// Get a event stream with the supplied unique identifier at a determined point of time
        /// </summary>
        /// <param name="streamId">The event stream unique identifier</param>
        /// <param name="dateTime">The point of time in which to get the event stream</param>
        /// <returns></returns>
        Task<IEventStream> GetStreamAt(Guid streamId, DateTime dateTime);

        /// <summary>
        /// Get a event stream with the supplied unique identifier at a determined revision
        /// </summary>
        /// <param name="streamId">The event stream unique identifier</param>
        /// <param name="revision">The revision in which to get the event stream</param>
        /// <returns></returns>
        Task<IEventStream> GetStreamAt(Guid streamId, int revision);
    }
}