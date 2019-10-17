using System.Collections.Generic;

namespace DocumentsApproval.Commands
{
    public class UpdateArtifacts : BaseCommand
    {
        public IDictionary<string, string> AddArtifacts { get; set; }

        public IEnumerable<string> RemoveArtifacts { get; set; }
    }
}