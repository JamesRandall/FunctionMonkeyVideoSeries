using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Commands
{
    public class AddPostCommand : ICommand<Post>
    {
        [SecurityProperty]
        public string UserId { get; set; }

        public NewPost Post { get; set; }
    }
}
