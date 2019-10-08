using System;
using EventLite;
using EventLite.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using POC.Documents.Events;

namespace POC.Documents
{
    public static class IoC
    {
        private static IServiceCollection _services { get; set; }

        public static IServiceProvider ServiceProvider => _services.BuildServiceProvider();

        public static void RegisterServices()
        {
            _services = new ServiceCollection();

            _services.PersistWithMongoDB(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost",
                Database = "documents_stream"
            });

            _services.RegisterEventSourcing(new AggregateSettings
            {
                CommitsBeforeSnapshot = 5
            });

            DomainClassMappings.RegisterClassSerializationMaps();
        }
    }
}