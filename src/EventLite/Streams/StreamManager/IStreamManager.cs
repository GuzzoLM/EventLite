using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager
{
    public interface IStreamManager
    {
        Task AddCommit(Commit commit);

        Task AddSnapshot<T>(Snapshot<T> snapshot);

        Task UpsertStream(EventStream stream);

        Task<EventStream> GetStream(Guid streamId);

        Task<Snapshot<T>> GetSnapshot<T>(Guid streamId, int snapshotRev);

        Task<List<Snapshot<T>>> GetSnapshots<T>(Guid streamId);

        Task<Commit> GetCommit(Guid streamId, int commitRev);

        Task<List<Commit>> GetCommits(Guid streamId, int minimumRevision = 0);
    }
}