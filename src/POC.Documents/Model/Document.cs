using System;
using System.Collections.Generic;

namespace POC.Documents.Model
{
    public class Document
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ApprovedBy { get; set; }

        public string RejectedBy { get; set; }

        public IDictionary<string, string> Artifacts { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime? DateApproved { get; set; }

        public DateTime? DateRejected { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}