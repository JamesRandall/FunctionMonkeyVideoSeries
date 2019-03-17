using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerlessBlog.Application.Exceptions;
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

        public Task<PostDocument> Get(Guid postId)
        {
            PostDocument postDocument = _documents.SingleOrDefault(x => x.Id == postId);
            if (postDocument == null)
            {
                throw new PostNotFoundException();
            }

            return Task.FromResult(postDocument);
        }
    }
}
