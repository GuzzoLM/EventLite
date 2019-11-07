using DocumentsApproval.Events;
using DocumentsApproval.Model;
using static EventLite.MongoDB.EventLiteMongoDbExtensions;

namespace DocumentsApproval
{
    public static class DomainClassMappings
    {
        public static void RegisterClassSerializationMaps()
        {
            RegisterClassToMongo<ArtifactsUpdated>();
            RegisterClassToMongo<DocumentApproved>();
            RegisterClassToMongo<DocumentCreated>();
            RegisterClassToMongo<DocumentDeleted>();
            RegisterClassToMongo<DocumentRejected>();
            RegisterClassToMongo<DocumentRenamed>();

            RegisterClassToMongo<Document>();
        }
    }
}