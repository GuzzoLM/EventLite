using DocumentsApproval.Events;
using EventLite;

namespace DocumentsApproval.Model
{
    public partial class DocumentAggregate : AggregateBase<Document>,
        IEventHandler<DocumentCreated>,
        IEventHandler<DocumentRenamed>,
        IEventHandler<ArtifactsUpdated>,
        IEventHandler<DocumentDeleted>,
        IEventHandler<DocumentApproved>,
        IEventHandler<DocumentRejected>
    {
        protected override int CommitsBeforeSnapshot => 10;

        public DocumentAggregate() : base()
        {
            AggregateDataStructure = null;
        }

        public override Document AggregateDataStructure { get; set; }

        public void Apply(DocumentCreated @event)
        {
            AggregateDataStructure = @event.Document;
        }

        public void Apply(DocumentRenamed @event)
        {
            AggregateDataStructure.Name = @event.Name;
        }

        public void Apply(DocumentDeleted @event)
        {
            AggregateDataStructure.DateDeleted = @event.Timstamp;
        }

        public void Apply(ArtifactsUpdated @event)
        {
            foreach (var removedArtifact in @event.ArtifactsRemoved)
            {
                AggregateDataStructure.Artifacts.Remove(removedArtifact);
            }

            foreach (var addedArtifact in @event.ArtifactsAdded)
            {
                AggregateDataStructure.Artifacts.Add(addedArtifact);
            }
        }

        public void Apply(DocumentApproved @event)
        {
            AggregateDataStructure.DateRejected = null;
            AggregateDataStructure.RejectedBy = null;
            AggregateDataStructure.DateApproved = @event.DateApproved;
            AggregateDataStructure.ApprovedBy = @event.ApprovedBy;
        }

        public void Apply(DocumentRejected @event)
        {
            AggregateDataStructure.DateRejected = @event.DateRejected;
            AggregateDataStructure.RejectedBy = @event.RejectedBy;
            AggregateDataStructure.DateApproved = null;
            AggregateDataStructure.ApprovedBy = null;
        }
    }
}