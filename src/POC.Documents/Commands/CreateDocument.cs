using System.Collections.Generic;

namespace POC.Documents.Commands
{
    public class CreateDocument : BaseCommand
    {
        public string Name { get; set; }

        public IDictionary<string, string> Artifacts { get; set; }
    }
}