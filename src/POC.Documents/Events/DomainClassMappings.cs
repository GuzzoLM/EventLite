using static EventLite.MongoDB.EventLiteMongoDbExtensions;

namespace POC.Documents.Events
{
    public static class DomainClassMappings
    {
        public static void RegisterClassSerializationMaps()
        {
            RegisterClassToMongo<DocumentCreated>();
            RegisterClassToMongo<DocumentRenamed>();
            RegisterClassToMongo<DocumentDeleted>();
        }
    }
}