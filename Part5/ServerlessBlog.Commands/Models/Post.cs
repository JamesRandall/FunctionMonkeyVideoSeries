using System;

namespace ServerlessBlog.Commands.Models
{
    public class Post : AbstractPost
    {
        public Guid Id { get; set; }

        public DateTime PostedAtUtc { get; set; }

        public string CreatedByUserId { get; set; }
    }
}
