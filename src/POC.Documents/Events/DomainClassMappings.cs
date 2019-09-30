using EventLite;

namespace POC.Documents.Events
{
    public static class DomainClassMappings
    {
        public static void RegisterClassSerializationMaps()
        {
            EventSourcingConfiguration.RegisterClassToMongo<DocumentCreated>();
            EventSourcingConfiguration.RegisterClassToMongo<DocumentUpdated>();
            EventSourcingConfiguration.RegisterClassToMongo<DocumentDeleted>();
        }
    }
}