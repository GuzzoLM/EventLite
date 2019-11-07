using System.Collections.Generic;

namespace DocumentsApproval.Commands
{
    public class CreateDocument : BaseCommand
    {
        public string Name { get; set; }

        public IDictionary<string, string> Artifacts { get; set; }
    }
}