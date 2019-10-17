using System;

namespace POC.Documents.Commands
{
    public class ApproveDocument : BaseCommand
    {
        public string Approver { get; set; }
    }
}