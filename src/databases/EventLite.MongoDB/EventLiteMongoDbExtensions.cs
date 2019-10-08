using AutoMapper;
using EventLite.MongoDB.DTO;
using EventLite.MongoDB.Map;
using EventLite.Streams.StreamManager;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace EventLite.MongoDB
{
    public static class EventLiteMongoDbExtensions
    {
        public static IServiceCollection PersistWithMongoDB(this IServiceCollection services, MongoDBSettings mongoDBSettings)
        {
            MongoMappers.RegisterMaps();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DtoProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            var client = new MongoClient(mongoDBSettings.ConnectionString);
            var streamManager = new MongoStreamManager(client, mongoDBSettings, mapper);

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