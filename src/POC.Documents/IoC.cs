using System;
using Microsoft.Extensions.DependencyInjection;
using POC.Documents.Events;
using EventLite;
using EventLite.Streams.StreamManager.Implementation.MongoDB;

namespace POC.Documents
{
    public static class IoC
    {
        private static IServiceCollection _services { get; set; }
        public static IServiceProvider ServiceProvider => _services.BuildServiceProvider();

        public static void RegisterServices()
        {
            _services = new ServiceCollection();

            _services.EventSourcingPersistenceWithMongo(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost",
                Database = "documents_stream"
            });

            _services.RegisterEventSourcing();

            DomainClassMappings.RegisterClassSerializationMaps();
        }
    }
}