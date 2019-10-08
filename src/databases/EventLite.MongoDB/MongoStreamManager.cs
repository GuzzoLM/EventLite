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

        public Task AddCommit(Commit commit)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            return commits.InsertOneAsync(_mapper.Map<CommitDTO>(commit));
        }

        public Task AddSnapshot(Snapshot snapshot)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            return snapshots.InsertOneAsync(_mapper.Map<SnapshotDTO>(snapshot));
        }

        public Task UpsertStream(EventStream stream)
        {
            var streams = _database.GetCollection<EventStreamDTO>(_streamsCollection);

            var filter = Builders<EventStreamDTO>.Filter.Eq(x => x.StreamId, stream.StreamId);

            var options = new UpdateOptions
            {
                IsUpsert = true
            };

            return streams.ReplaceOneAsync(filter, _mapper.Map<EventStreamDTO>(stream), options);
        }

        public Task<Commit> GetCommit(Guid streamId, int commitRev)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            var filterStreamID = Builders<CommitDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<CommitDTO>.Filter.Eq(x => x.CommitNumber, commitRev);
            var filter = Builders<CommitDTO>.Filter.And(filterStreamID, filterCommitRev);

            return commits.Find(filter).Project(x => _mapper.Map<Commit>(x)).FirstOrDefaultAsync();
        }

        public Task<List<Commit>> GetCommits(Guid streamId, int minimumRevision = 0)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            var filterStreamID = Builders<CommitDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<CommitDTO>.Filter.Gte(x => x.CommitNumber, minimumRevision);
            var filter = Builders<CommitDTO>.Filter.And(filterStreamID, filterCommitRev);

            return commits.Find(filter).Project(x => _mapper.Map<Commit>(x)).ToListAsync();
        }

        public Task<Snapshot> GetSnapshot(Guid streamId, int snapshotRev)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            var filterStreamID = Builders<SnapshotDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterSnapshotRev = Builders<SnapshotDTO>.Filter.Eq(x => x.SnapshotRevision, snapshotRev);
            var filter = Builders<SnapshotDTO>.Filter.And(filterStreamID, filterSnapshotRev);

            return snapshots.Find(filter).Project(x => _mapper.Map<Snapshot>(x)).FirstOrDefaultAsync();
        }

        public Task<List<Snapshot>> GetSnapshots(Guid streamId)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            var filter = Builders<SnapshotDTO>.Filter.Eq(x => x.StreamId, streamId);

            return snapshots.Find(filter).Project(x => _mapper.Map<Snapshot>(x)).ToListAsync();
        }

        public async Task<EventStream> GetStream(Guid streamId)
        {
            var streams = _database.GetCollection<EventStreamDTO>(_streamsCollection);

            var filter = Builders<EventStreamDTO>.Filter.Eq(x => x.StreamId, streamId);

            var stream = await streams.Find(filter).Project(x => _mapper.Map<EventStream>(x)).FirstOrDefaultAsync();

            return stream ?? new EventStream(streamId);
        }
    }
}