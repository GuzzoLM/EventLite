using DocumentsApproval.Events;
using EventLite;
using EventLite.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DocumentsApproval
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

            _services.RegisterEventSourcing(new AggregateSettings());

            DomainClassMappings.RegisterClassSerializationMaps();
        }
    }
}