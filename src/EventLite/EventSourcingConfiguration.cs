using EventLite.Streams.StreamManager;
using EventLite.Streams.StreamManager.Implementation;
using Microsoft.Extensions.DependencyInjection;

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
    }
}