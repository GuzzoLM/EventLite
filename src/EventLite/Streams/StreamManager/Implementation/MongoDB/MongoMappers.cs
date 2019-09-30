using MongoDB.Bson.Serialization;
using EventLite.Streams.DTO;

namespace EventLite.Streams.StreamManager.Implementation.MongoDB
{
    public static class MongoMappers
    {
        public static void RegisterMaps()
        {
            BsonClassMap.RegisterClassMap<EventStreamDTO>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<SnapshotDTO>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<CommitDTO>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}