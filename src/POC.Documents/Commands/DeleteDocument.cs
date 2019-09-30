using System;

namespace POC.Documents.Commands
{
    public class DeleteDocument
    {
        public Guid StreamId { get; set; }

        public Guid DocumentId { get; set; }
    }
}