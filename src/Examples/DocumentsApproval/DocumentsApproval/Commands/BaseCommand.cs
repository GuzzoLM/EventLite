using System;

namespace DocumentsApproval.Commands
{
    public abstract class BaseCommand
    {
        public Guid StreamId { get; set; }
    }
}