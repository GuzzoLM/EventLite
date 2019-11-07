using System;
using System.Collections.Generic;
using System.Text;
using DocumentsApproval.Events;
using DocumentsApproval.Commands;

namespace DocumentsApproval.Model
{
    public partial class DocumentAggregate
    {
        public DocumentCreated CreateDocument(CreateDocument command)
        {
            return new DocumentCreated
            {
                EventType = nameof(DocumentCreated),
                Timstamp = DateTime.UtcNow,
                Document = new Document
                {
                    Id = command.StreamId,
                    DateCreated = DateTime.UtcNow,
                    Name = command.Name,
                    Artifacts = command.Artifacts
                }
            };
        }

        public DocumentRenamed RenameDocument(RenameDocument command)
        {
            return new DocumentRenamed
            {
                EventType = nameof(DocumentRenamed),
                Timstamp = DateTime.UtcNow,
                Name = command.Name
            };
        }

        public ArtifactsUpdated UpdateArtifacts(UpdateArtifacts command)
        {
            return new ArtifactsUpdated
            {
                EventType = nameof(ArtifactsUpdated),
                Timstamp = DateTime.UtcNow,
                ArtifactsAdded = command.AddArtifacts,
                ArtifactsRemoved = command.RemoveArtifacts
            };
        }

        public DocumentDeleted DeleteDocument(DeleteDocument command)
        {
            return new DocumentDeleted
            {
                EventType = typeof(DocumentDeleted).Name,
                Timstamp = DateTime.UtcNow
            };
        }

        public DocumentApproved ApproveDocument(ApproveDocument command)
        {
            return new DocumentApproved
            {
                EventType = nameof(DocumentApproved),
                Timstamp = DateTime.UtcNow,
                DateApproved = DateTime.UtcNow,
                ApprovedBy = command.Approver
            };
        }

        public DocumentRejected RejectDocument(RejectDocument command)
        {
            return new DocumentRejected
            {
                EventType = nameof(DocumentRejected),
                Timstamp = DateTime.UtcNow,
                DateRejected = DateTime.UtcNow,
                RejectedBy = command.Rejecter
            };
        }
    }
}
