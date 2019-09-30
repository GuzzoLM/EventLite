using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using EventLite.Streams.DTO;

namespace EventLite.Streams.StreamManager.Implementation.MongoDB
{
    internal class MongoStreamManager : IStreamManager
    {
        private const string _commitsCollection = "Stream_Commits";
        private const string _snapshotsCollection = "Stream_Snapshots";
        private const string _streamsCollection = "Streams";

        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MongoStreamManager(MongoClient mongoClient, MongoDBSettings mongoDBSettings)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(mongoDBSettings.Database);
        }

        public Task AddCommit(Commit commit)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            return commits.InsertOneAsync(commit.ToDTO());
        }

        public Task AddSnapshot<T>(Snapshot<T> snapshot)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            return snapshots.InsertOneAsync(snapshot.ToDTO());
        }

        public Task UpsertStream(EventStream stream)
        {
            var streams = _database.GetCollection<EventStreamDTO>(_streamsCollection);

            var filter = Builders<EventStreamDTO>.Filter.Eq(x => x.StreamId, stream.StreamId);

            var options = new UpdateOptions
            {
                IsUpsert = true
            };

            return streams.ReplaceOneAsync(filter, stream.ToDTO(), options);
        }

        public Task<Commit> GetCommit(Guid streamId, int commitRev)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            var filterStreamID = Builders<CommitDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<CommitDTO>.Filter.Eq(x => x.CommitNumber, commitRev);
            var filter = Builders<CommitDTO>.Filter.And(filterStreamID, filterCommitRev);

            return commits.Find(filter).Project(x => new Commit(x)).FirstOrDefaultAsync();
        }

        public Task<List<Commit>> GetCommits(Guid streamId, int minimumRevision = 0)
        {
            var commits = _database.GetCollection<CommitDTO>(_commitsCollection);

            var filterStreamID = Builders<CommitDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<CommitDTO>.Filter.Gte(x => x.CommitNumber, minimumRevision);
            var filter = Builders<CommitDTO>.Filter.And(filterStreamID, filterCommitRev);

            return commits.Find(filter).Project(x => new Commit(x)).ToListAsync();
        }

        public Task<Snapshot<T>> GetSnapshot<T>(Guid streamId, int snapshotRev)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            var filterStreamID = Builders<SnapshotDTO>.Filter.Eq(x => x.StreamId, streamId);
            var filterSnapshotRev = Builders<SnapshotDTO>.Filter.Eq(x => x.SnapshotRevision, snapshotRev);
            var filter = Builders<SnapshotDTO>.Filter.And(filterStreamID, filterSnapshotRev);

            return snapshots.Find(filter).Project(x => new Snapshot<T>(x)).FirstOrDefaultAsync();
        }

        public Task<List<Snapshot<T>>> GetSnapshots<T>(Guid streamId)
        {
            var snapshots = _database.GetCollection<SnapshotDTO>(_snapshotsCollection);

            var filter = Builders<SnapshotDTO>.Filter.Eq(x => x.StreamId, streamId);

            return snapshots.Find(filter).Project(x => new Snapshot<T>(x)).ToListAsync();
        }

        public async Task<EventStream> GetStream(Guid streamId)
        {
            var streams = _database.GetCollection<EventStreamDTO>(_streamsCollection);

            var filter = Builders<EventStreamDTO>.Filter.Eq(x => x.StreamId, streamId);

            var stream = await streams.Find(filter).Project(x => new EventStream(x)).FirstOrDefaultAsync();

            return stream ?? new EventStream(streamId);
        }
    }
}