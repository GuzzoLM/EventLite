using System;

namespace POC.Documents.Commands
{
    public abstract class BaseCommand
    {
        public Guid StreamId { get; set; }
    }
}