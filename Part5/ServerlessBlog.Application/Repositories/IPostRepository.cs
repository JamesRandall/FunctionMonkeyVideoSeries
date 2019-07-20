using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerlessBlog.Application.Models.Document;

namespace ServerlessBlog.Application.Repositories
{
    interface IPostRepository
    {
        Task Add(PostDocument postDocument);
        Task<PostDocument> Get(Guid postId);
    }
}
