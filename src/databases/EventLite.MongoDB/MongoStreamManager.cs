using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventLite.MongoDB.DTO;
using EventLite.Streams;
using EventLite.Streams.StreamManager;
using MongoDB.Driver;

namespace EventLite.MongoDB
{
    internal class MongoStreamManager : IStreamManager
    {
        private const string _commitsCollection = "Stream_Commits";

        private const string _snapshotsCollection = "Stream_Snapshots";

        private const string _streamsCollection = "Streams";

        private readonly MongoClient _mongoClient;

        private readonly IMapper _mapper;

        private readonly IMongoDatabase _database;

        public MongoStreamManager(MongoClient mongoClient, MongoDBSettings mongoDBSettings, IMapper mapper)
        {
            _mongoClient = mongoClient;
            _mapper = mapper;
            _database = _mongoClient.GetDatabase(mongoDBSettings.Database);
        }

        public async Task<IResult> AddCommit(Commit commit)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            await commits.InsertOneAsync(_mapper.Map<CommitDTO>(commit));

            return Result.Successfull();
        }

        public async Task<IResult> AddSnapshot(Snapshot snapshot)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            await snapshots.InsertOneAsync(_mapper.Map<SnapshotDTO>(snapshot));

            return Result.Successfull();
        }

        public async Task<IResult> UpsertStream(EventStream stream)
        {
            var streams = _database.GetCollection<EventStreamDTO>(_streamsCollection);

            var filter = Builders<EventStreamDTO>.Filter.Eq(x => x.StreamId, stream.StreamId);

            var oldStream = await streams.Find(filter).Project(x => _mapper.Map<EventStream>(x)).FirstOrDefaultAsync();

            if (oldStream == null)
            {
                await streams.InsertOneAsync(_mapper.Map<EventStreamDTO>(stream));
                return Result.Successfull(); ;
            }

            var revFilter = Builders<EventStreamDTO>.Filter.Lt(x => x.HeadRevision, stream.HeadRevision);
            filter = Builders<EventStreamDTO>.Filter.And(filter, revFilter);
            var result = await streams.ReplaceOneAsync(filter, _mapper.Map<EventStreamDTO>(stream));

            return (result.ModifiedCount > 0)
                ? Result.Successfull()
                : Result.Failure(FailureReason.ConcurrencyError);
        }

        public async Task<IResult<Commit>> GetCommit(Guid streamId, int commitRev)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            var filterStreamID = Builders<CommitDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<CommitDTO>.Filter.Eq(x => x.CommitNumber, commitRev);
            var filter = Builders<CommitDTO>.Filter.And(filterStreamID, filterCommitRev);

            var foundCommit = await commits.Find(filter).Project(x => _mapper.Map<Commit>(x)).FirstOrDefaultAsync();

            return Result<Commit>.Successfull(foundCommit);
        }

        public async Task<IResult<List<Commit>>> GetCommits(Guid streamId, int minimumRevision = 0)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            var filterStreamID = Builders<CommitDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<CommitDTO>.Filter.Gte(x => x.CommitNumber, minimumRevision);
            var filter = Builders<CommitDTO>.Filter.And(filterStreamID, filterCommitRev);

            var foundCommits = await commits.Find(filter).Project(x => _mapper.Map<Commit>(x)).ToListAsync();

            return Result<List<Commit>>.Successfull(foundCommits);
        }

        public async Task<IResult<Snapshot>> GetSnapshot(Guid streamId, int snapshotRev)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            var filterStreamID = Builders<SnapshotDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterSnapshotRev = Builders<SnapshotDTO>.Filter.Eq(x => x.SnapshotRevision, snapshotRev);
            var filter = Builders<SnapshotDTO>.Filter.And(filterStreamID, filterSnapshotRev);

            var foundSnapshot = await snapshots.Find(filter).Project(x => _mapper.Map<Snapshot>(x)).FirstOrDefaultAsync();

            return Result<Snapshot>.Successfull(foundSnapshot);
        }

        public async Task<IResult<List<Snapshot>>> GetSnapshots(Guid streamId)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            var filter = Builders<SnapshotDTO>.Filter.Eq(x => x.StreamId, streamId);

            var foundSnapshots = await snapshots.Find(filter).Project(x => _mapper.Map<Snapshot>(x)).ToListAsync();

            return Result<List<Snapshot>>.Successfull(foundSnapshots);
        }

        public async Task<IResult<EventStream>> GetStream(Guid streamId)
        {
            var streams = _database.GetCollection<EventStreamDTO>(_streamsCollection);

            var filter = Builders<EventStreamDTO>.Filter.Eq(x => x.StreamId, streamId);

            var stream = await streams.Find(filter).Project(x => _mapper.Map<EventStream>(x)).FirstOrDefaultAsync();

            stream = stream ?? new EventStream(streamId);

            return Result<EventStream>.Successfull(stream);
        }
    }
}