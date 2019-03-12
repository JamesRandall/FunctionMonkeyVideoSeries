using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Commands
{
    public class AddPostCommand : ICommand<Post>
    {
        public Guid UserId { get; set; }

        public NewPost Post { get; set; }
    }
}
