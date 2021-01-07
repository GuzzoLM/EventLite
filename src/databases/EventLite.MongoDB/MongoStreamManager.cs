using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventLite.Extensions;
using EventLite.MongoDB.Data;
using EventLite.MongoDB.DTO;
using EventLite.Streams;
using MongoDB.Driver;

namespace EventLite.MongoDB
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

        public async Task UpsertStream(IEventStream stream)
        {
            var streamData = new EventStream
            {
                StreamId = stream.StreamId,
                HeadRevision = stream.HeadRevision,
                SnapshotRevision = stream.SnapshotRevision,
                UnsnapshottedCommits = stream.UnsnapshottedCommits
            };

            var commits = stream.UncommittedEvents.Select(x => new Commit
            {
                StreamId = stream.StreamId,
                CommitNumber = x.CommitNumber,
                Event = x.Event,
                Timestamp = x.Timestamp
            });

            var snapshots = stream.UncommittedSnapshots.Select(x => new Snapshot
            {
                StreamId = stream.StreamId,
                SnapshotHeadCommit = x.SnapshotHeadCommit,
                SnapshotData = x.SnapshotData,
                SnapshotRevision = x.SnapshotRevision
            });

            await UpsertStream(streamData);
            await AddCommits(commits);
            await AddSnapshots(snapshots);
        }

        public async Task<IEventStream> GetStream(Guid streamId)
        {
            var streamData = await GetStreamById(streamId);

            return (streamData != null) ?
                await BuildEventStream(streamData) :
                Builders.EventStreamBuilder(streamId);
        }

        public async Task<IEventStream> GetStreamAt(Guid streamId, DateTime datetime)
        {
            var streamData = await GetStreamById(streamId);

            return (streamData != null) ?
                await BuildEventStreamAt(streamData, datetime: datetime) :
                Builders.EventStreamBuilder(streamId);
        }

        public async Task<IEventStream> GetStreamAt(Guid streamId, int revision)
        {
            var streamData = await GetStreamById(streamId);

            return (streamData != null) ?
                await BuildEventStreamAt(streamData, maxRevision: revision) :
                Builders.EventStreamBuilder(streamId);
        }

        private async Task<IEventStream> BuildEventStreamAt(EventStream streamData, int maxRevision = 0, DateTime? datetime = null)
        {
            var commits = await GetCommits(
                streamId: streamData.StreamId,
                minimumRevision: 0,
                maxRevision: maxRevision,
                datetime: datetime);

            return Builders.EventStreamBuilder(
                streamId: streamData.StreamId,
                snapshotRevision: streamData.SnapshotRevision,
                headRevision: streamData.HeadRevision,
                events: commits);
        }

        private async Task<IEventStream> BuildEventStream(EventStream streamData)
        {
            var snapshot = await GetSnapshot(streamData.StreamId, streamData.SnapshotRevision);
            var commits = await GetCommits(streamData.StreamId, snapshot?.SnapshotHeadCommit ?? 0);

            return Builders.EventStreamBuilder(
                streamId: streamData.StreamId,
                snapshotRevision: streamData.SnapshotRevision,
                headRevision: streamData.HeadRevision,
                events: commits,
                snapshot: snapshot);
        }

        private Task<EventStream> GetStreamById(Guid streamId)
        {
            var streams = _database.GetCollection<EventStream>(_streamsCollection);

            var filter = Builders<EventStream>.Filter.Eq(x => x.StreamId, streamId);

            return streams.Find(filter).FirstOrDefaultAsync();
        }

        private Task<List<ICommit>> GetCommits(Guid streamId, int minimumRevision = 0, int maxRevision = 0, DateTime? datetime = null)
        {
            var commits = _database.GetCollection<Commit>(_commitsCollection);

            var filterStreamID = Builders<Commit>.Filter.Eq(x => x.StreamId, streamId);
            var filterCommitRev = Builders<Commit>.Filter.Gte(x => x.CommitNumber, minimumRevision);
            var filter = Builders<Commit>.Filter.And(filterStreamID, filterCommitRev);

            if (maxRevision > 0)
            {
                var filterCommitMaxRev = Builders<Commit>.Filter.Lte(x => x.CommitNumber, maxRevision);
                filter = Builders<Commit>.Filter.And(filter, filterCommitMaxRev);
            }

            if (datetime != null)
            {
                var filterCommitMaxDateTime = Builders<Commit>.Filter.Lte(x => x.Timestamp, datetime.Value.Ticks);
                filter = Builders<Commit>.Filter.And(filter, filterCommitMaxDateTime);
            }

            return commits.Find(filter).Project(x => CommitDTO.From(x)).ToListAsync();
        }

        private Task<ISnapshot> GetSnapshot(Guid streamId, int snapshotRev)
        {
            if (snapshotRev == 0)
            {
                return null;
            }

            var snapshots = _database.GetCollection<Snapshot>(_snapshotsCollection);

            var filterStreamID = Builders<Snapshot>.Filter.Eq(x => x.StreamId, streamId);
            var filterSnapshotRev = Builders<Snapshot>.Filter.Eq(x => x.SnapshotRevision, snapshotRev);
            var filter = Builders<Snapshot>.Filter.And(filterStreamID, filterSnapshotRev);

            return snapshots.Find(filter).Project(x => SnapshotDTO.From(x)).FirstOrDefaultAsync();
        }

        private Task AddSnapshots(IEnumerable<Snapshot> snapshots)
        {
            var snapshotsCollection = _database.GetCollection<Snapshot>(_snapshotsCollection);

            return snapshotsCollection.InsertManyAsync(snapshots);
        }

        private Task AddCommits(IEnumerable<Commit> commits)
        {
            var commitsCollection = _database.GetCollection<Commit>(_commitsCollection);

            return commitsCollection.InsertManyAsync(commits);
        }

        private Task UpsertStream(EventStream stream)
        {
            var streams = _database.GetCollection<EventStream>(_streamsCollection);

            var filter = Builders<EventStream>.Filter.Eq(x => x.StreamId, stream.StreamId);

            var options = new UpdateOptions
            {
                IsUpsert = true
            };

            return streams.ReplaceOneAsync(filter, stream, options);
        }
    }
}