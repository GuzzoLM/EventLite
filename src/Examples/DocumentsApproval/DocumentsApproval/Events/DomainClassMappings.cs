using static EventLite.MongoDB.EventLiteMongoDbExtensions;

namespace DocumentsApproval.Events
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