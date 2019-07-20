using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Application.Models.Document;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Commands;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Application.Handlers
{
    internal class GetPostQueryHandler : ICommandHandler<GetPostQuery, Post>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<Post> ExecuteAsync(GetPostQuery command, Post previousResult)
        {
            PostDocument postDocument = await _postRepository.Get(command.PostId);
            Post post = _mapper.Map<Post>(postDocument);
            return post;
        }
    }
}
