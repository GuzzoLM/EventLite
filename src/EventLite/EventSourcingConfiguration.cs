using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using EventLite.Streams.StreamManager;
using EventLite.Streams.StreamManager.Implementation;
using EventLite.Streams.StreamManager.Implementation.MongoDB;

namespace EventLite
{
    public static class EventSourcingConfiguration
    {
        public static IServiceCollection RegisterEventSourcing(this IServiceCollection services, IAggregateSettings aggregateSettings)
        {
            services.AddSingleton(aggregateSettings);
            services.AddSingleton<IEventStreamReader, EventStreamReader>();

            return services;
        }

        public static IServiceCollection EventSourcingPersistenceWithMongo(this IServiceCollection services, MongoDBSettings mongoDBSettings)
        {
            MongoMappers.RegisterMaps();

            var client = new MongoClient(mongoDBSettings.ConnectionString);
            var streamManager = new MongoStreamManager(client, mongoDBSettings);

            services.AddSingleton<IStreamManager>(streamManager);

            return services;
        }

        public static void RegisterClassToMongo<TClass>()
        {
            BsonClassMap.RegisterClassMap<TClass>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}