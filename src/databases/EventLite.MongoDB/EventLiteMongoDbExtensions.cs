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
        /// <summary>
        /// Register a StreamManager implementation with MongoDB persistence.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mongoDBSettings"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Map a class to BSon format, in order to be persisted in MongoDB
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
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