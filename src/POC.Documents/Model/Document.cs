using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Documents.Model
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
