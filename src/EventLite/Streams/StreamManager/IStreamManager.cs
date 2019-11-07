using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager
{
    public interface IStreamManager
    {
        /// <summary>
        /// Add a commi to the stream
        /// </summary>
        /// <param name="commit"></param>
        /// <returns></returns>
        Task AddCommit(Commit commit);

        /// <summary>
        /// Add a snapshot to the stream
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        Task AddSnapshot(Snapshot snapshot);


        /// <summary>
        /// Update a stream or, if it doesn't exist, insert it
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task UpsertStream(EventStream stream);


        /// <summary>
        /// Get a stream with the supplied unique identifier
        /// </summary>
        /// <param name="streamId"></param>
        /// <returns></returns>
        Task<EventStream> GetStream(Guid streamId);


        /// <summary>
        /// Get a stream's snapshot accordingly with the snapshot revision
        /// </summary>
        /// <param name="streamId"></param>
        /// <param name="snapshotRev"></param>
        /// <returns></returns>
        Task<Snapshot> GetSnapshot(Guid streamId, int snapshotRev);

        /// <summary>
        /// Get all snapshots from a stream
        /// </summary>
        /// <param name="streamId"></param>
        /// <returns></returns>
        Task<List<Snapshot>> GetSnapshots(Guid streamId);

        /// <summary>
        /// Get a stream's commit according to the commit revision
        /// </summary>
        /// <param name="streamId"></param>
        /// <param name="commitRev"></param>
        /// <returns></returns>
        Task<Commit> GetCommit(Guid streamId, int commitRev);

        /// <summary>
        /// Get all stream's commits that has a revision higher than the minimum revision supplied
        /// </summary>
        /// <param name="streamId"></param>
        /// <param name="minimumRevision"></param>
        /// <returns></returns>
        Task<List<Commit>> GetCommits(Guid streamId, int minimumRevision = 0);
    }
}