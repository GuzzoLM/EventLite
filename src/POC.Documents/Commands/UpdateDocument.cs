using System;
using POC.Documents.Model;

namespace POC.Documents.Commands
{
    public class UpdateDocument
    {
        public Guid StreamId { get; set; }

        public Document Document { get; set; }
    }
}