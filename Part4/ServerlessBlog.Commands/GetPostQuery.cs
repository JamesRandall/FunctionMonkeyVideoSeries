using System;
using System.Collections.Generic;
using System.Text;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Commands
{
    public class GetPostQuery : ICommand<Post>
    {
        public Guid PostId { get; set; }
    }
}
