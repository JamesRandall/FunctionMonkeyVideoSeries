using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunctionMonkey.Commanding.Abstractions.Validation;
using FunctionMonkey.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServerlessBlog.Application.Models.Document;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Commands;
using ServerlessBlog.Commands.Models;
using Xbehave;
using Xbehave.Execution;
using Xunit;

namespace ServerlessBlog.Tests.Acceptance
{
    public class AddPostFunctionShould : AbstractAcceptanceTest
    {
        [Scenario]
        public void ReturnInsertedPost(NewPost newPost, HttpResponse response, Post savedPost)
        {
            DateTime utcStart = DateTime.UtcNow;
            
            "Given a new post".x(() => newPost = new NewPost
            {
                Body = "my new post",
                Title = "my title"
            });

            "When I submit the post".x(async () => response = await ExecuteHttpAsync(new AddPostCommand
            {
                Post = newPost
            }));

            "Then I receive an OK result and a post".x(() =>
            {
                Assert.Equal(200, response.StatusCode);
                savedPost = response.GetJson<Post>();
                Assert.NotNull(savedPost);
            });

            "And the returned post has an ID".x(() => Assert.NotEqual(Guid.Empty, savedPost.Id));
            "And it has its post date set".x(() => Assert.True(savedPost.PostedAtUtc >= utcStart));
            "And the body and title match the submitted post".x(() =>
            {
                Assert.Equal("my new post", savedPost.Body);
                Assert.Equal("my title", savedPost.Title);
            });
        }

        [Scenario]
        public void ReceiveBadRequestOnInvalidPost(NewPost newPost, HttpResponse response)
        {
            "Given a new post with an invalid title".x(() => newPost = new NewPost
            {
                Body = "my new post",
                Title = ""
            });

            "When I submit the post".x(async () => response = await ExecuteHttpAsync(new AddPostCommand
            {
                Post = newPost
            }));

            "Then I receive a bad request status code".x(() => Assert.Equal(400, response.StatusCode));
            "And a validation failure has been set for the title property".x(() =>
            {
                ValidationResult validationResult = response.GetJson<ValidationResult>();
                Assert.NotNull(validationResult);
                Assert.NotNull(validationResult.Errors.SingleOrDefault(x => x.Property == "Post.Title"));
            });
        }

        [Scenario]
        public void InsertIntoPostRepository(NewPost newPost, HttpResponse response)
        {
            PostDocument postDocument = null;

            "Given a new post".x(() => newPost = new NewPost
            {
                Body = "my new post",
                Title = "my title"
            });

            "When I submit the post".x(async () => response = await ExecuteHttpAsync(new AddPostCommand
            {
                Post = newPost
            }));

            "Then the post is available in the repository".x(async () =>
            {
                Post returnedPost = response.GetJson<Post>();
                Guid postId = returnedPost.Id;
                IPostRepository postRepository = ServiceProvider.GetRequiredService<IPostRepository>();
                postDocument = await postRepository.Get(postId);
            });

            "And the body and title match the submitted post".x(() =>
            {
                Assert.Equal("my new post", postDocument.Body);
                Assert.Equal("my title", postDocument.Title);
            });
        }
    }
}
