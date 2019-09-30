using System;
using POC.Documents.Model;

namespace POC.Documents.Commands
{
    public class CreateDocument
    {
        public Guid StreamId { get; set; }
        public Document Document { get; set; }
    }
}