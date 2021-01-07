using EventLite.Streams;
using Microsoft.Extensions.DependencyInjection;
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
            var client = new MongoClient(mongoDBSettings.ConnectionString);
            var streamManager = new MongoStreamManager(client, mongoDBSettings);

            services.AddSingleton<IStreamManager>(streamManager);

            return services;
        }
    }
}