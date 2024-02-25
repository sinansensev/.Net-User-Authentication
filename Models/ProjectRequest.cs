using System;

namespace PayosferIdentity.Models
{
    public class ProjectRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string RequestType { get; set; }
        public string PriorityLevel { get; set; }
        public DateTime CreateTime { get; set; }
        public string LegalStatus { get; set; }
        public string LegalDate { get; set; }
        public string Notes { get; set; }
        

        public byte[]? PdfContent { get; set; } 
    }
}
