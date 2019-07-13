using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using ServerlessBlog.Application.Models.Document;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Commands;
using ServerlessBlog.Commands.Models;
using Xbehave;
using Xunit;

namespace ServerlessBlog.Tests.Acceptance
{
    public class GetPostFunctionShould : AbstractAcceptanceTest
    {
        public override void BeforeServiceProviderBuild(
            IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            base.BeforeServiceProviderBuild(serviceCollection, commandRegistry);
            IPostRepository postRepository = Substitute.For<IPostRepository>();
            postRepository.Get(Arg.Any<Guid>()).Returns(callInfo => Task.FromResult(new PostDocument
            {
                Id = callInfo.Arg<Guid>()
            }));
            serviceCollection.Replace(new ServiceDescriptor(typeof(IPostRepository), postRepository));
        }

        [Scenario]
        public void ReturnAnExistingPost(Guid postId, HttpResponse response)
        {
            "Given a post ID".x(() => postId = Guid.NewGuid());

            "When I request a post".x(async () => response = await ExecuteHttpAsync(new GetPostQuery
            {
                PostId = postId
            }));

            "Then I receive the expected post".x(() =>
            {
                Assert.Equal(200, response.StatusCode);
                Post post = response.GetJson<Post>();
                Assert.Equal(postId, post.Id);
            });
        }

        [Scenario]
        public void ReturnNotFoundWhenPostDoesNotExist(Guid postId, HttpResponse response)
        {
            AcceptanceTestScaffold scaffold = new AcceptanceTestScaffold();
            scaffold.Setup();

            "Given a post ID for a post that does not exist".x(() => postId = Guid.NewGuid());

            "When I request a post".x(async () =>
                response = await scaffold.ExecuteHttpAsync(new GetPostQuery {PostId = postId})
            );

            "Then I receive a not found status code".x(() => Assert.Equal(404, response.StatusCode));
        }
    }
}
