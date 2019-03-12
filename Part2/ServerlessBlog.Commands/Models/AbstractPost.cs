using System;
using System.Collections.Generic;
using System.Text;

namespace ServerlessBlog.Commands.Models
{
    public abstract class AbstractPost
    {
        public string Title { get; set; }

        public string Body { get; set; }
    }
}
