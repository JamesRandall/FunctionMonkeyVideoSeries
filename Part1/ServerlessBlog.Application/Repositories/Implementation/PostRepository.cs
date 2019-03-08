using System.Collections.Generic;
using System.Threading.Tasks;
using ServerlessBlog.Application.Models.Document;

namespace ServerlessBlog.Application.Repositories.Implementation
{
    internal class PostRepository : IPostRepository
    {
        private readonly List<PostDocument> _documents = new List<PostDocument>();

        public Task Add(PostDocument postDocument)
        {
            _documents.Add(postDocument);
            return Task.CompletedTask;
        }
    }
}
