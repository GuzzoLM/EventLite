using System;
using System.Collections.Generic;
using EventLite;
using POC.Documents.Commands;
using POC.Documents.Events;

namespace POC.Documents.Model
{
    public class DocumentAggregate : AggregateBase<List<Document>>,
        IEventHandler<DocumentCreated>,
        IEventHandler<DocumentUpdated>,
        IEventHandler<DocumentDeleted>
    {
        public DocumentAggregate() : base()
        {
            AggregateDataStructure = new List<Document>();
        }

        public override List<Document> AggregateDataStructure { get; set; }

        protected override int CommitsBeforeSnapshot => 10;

        public void Apply(DocumentCreated @event)
        {
            AggregateDataStructure.Add(@event.Document);
        }

        public void Apply(DocumentUpdated @event)
        {
            var oldDoc = AggregateDataStructure.Find(x => x.Id == @event.Document.Id);
            AggregateDataStructure.Remove(oldDoc);
            AggregateDataStructure.Add(@event.Document);
        }

        public void Apply(DocumentDeleted @event)
        {
            var oldDoc = AggregateDataStructure.Find(x => x.Id == @event.DocumentId);
            AggregateDataStructure.Remove(oldDoc);
        }

        public DocumentCreated CreateDocument(CreateDocument command)
        {
            return new DocumentCreated
            {
                Document = command.Document,
                EventType = typeof(DocumentCreated).Name,
                Timstamp = DateTime.Now
            };
        }

        public DocumentUpdated UpdateDocument(UpdateDocument command)
        {
            return new DocumentUpdated
            {
                Document = command.Document,
                EventType = typeof(DocumentUpdated).Name,
                Timstamp = DateTime.Now
            };
        }

        public DocumentDeleted DeleteDocument(DeleteDocument command)
        {
            return new DocumentDeleted
            {
                DocumentId = command.DocumentId,
                EventType = typeof(DocumentDeleted).Name,
                Timstamp = DateTime.Now
            };
        }
    }
}