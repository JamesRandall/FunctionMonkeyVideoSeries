using System;
using System.Threading.Tasks;
using AutoMapper;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Application.Models.Document;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Commands;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Application.Handlers
{
    internal class AddPostCommandHandler : ICommandHandler<AddPostCommand, Post>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public AddPostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<Post> ExecuteAsync(AddPostCommand command, Post previousResult)
        {
            PostDocument postDocument = CreatePostDocument(command);

            await AddPostDocumentToRepository(postDocument);

            return CreatePostFromDocument(postDocument);
        }

        private async Task AddPostDocumentToRepository(PostDocument postDocument)
        {
            await _postRepository.Add(postDocument);
        }

        private PostDocument CreatePostDocument(AddPostCommand command)
        {
            return new PostDocument
            {
                Body = command.Post.Body,
                CreatedByUserId = command.UserId,
                Id = Guid.NewGuid(),
                PostedAtUtc = DateTime.UtcNow,
                Title = command.Post.Title,
                Version = Constants.Posts.CurrentDocumentVersion
            };
        }

        private Post CreatePostFromDocument(PostDocument postDocument)
        {
            return _mapper.Map<Post>(postDocument);
        }
    }
}
