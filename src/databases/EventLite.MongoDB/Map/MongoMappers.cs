using EventLite.MongoDB.DTO;
using MongoDB.Bson.Serialization;

namespace EventLite.MongoDB.Map
{
    internal static class MongoMappers
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