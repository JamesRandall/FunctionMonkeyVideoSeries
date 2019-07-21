using System;

namespace ServerlessBlog.Application.Models.Document
{
    internal class PostDocument
    {
        public Guid Id { get; set; }

        public DateTime PostedAtUtc { get; set; }

        public string CreatedByUserId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public SemanticVersion Version { get; set; }
    }
}
