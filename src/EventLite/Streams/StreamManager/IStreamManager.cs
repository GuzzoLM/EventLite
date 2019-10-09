using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventLite.Streams.StreamManager
{
    public interface IStreamManager
    {
        Task<IResult> AddCommit(Commit commit);

        Task<IResult> AddSnapshot(Snapshot snapshot);

        Task<IResult> UpsertStream(EventStream stream);

        Task<IResult<EventStream>> GetStream(Guid streamId);

        Task<IResult<Snapshot>> GetSnapshot(Guid streamId, int snapshotRev);

        Task<IResult<List<Snapshot>>> GetSnapshots(Guid streamId);

        Task<IResult<Commit>> GetCommit(Guid streamId, int commitRev);

        Task<IResult<List<Commit>>> GetCommits(Guid streamId, int minimumRevision = 0);
    }
}