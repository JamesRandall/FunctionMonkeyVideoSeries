using System.Net.Http;
using AutoMapper;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.FluentValidation;
using ServerlessBlog.Application;
using ServerlessBlog.Commands;

namespace ServerlessBlog
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            builder
                .Setup((serviceCollection, commandRegistry) =>
                {
                    serviceCollection
                        .AddApplication(commandRegistry)
                        .AddAutoMapper(typeof(SubsystemRegistration))
                        ;
                })
                .AddFluentValidation()
                .DefaultHttpResponseHandler<HttpResponseHandler>()
                .Functions(functions => functions
                    .HttpRoute("/api/v1/post", route => route
                        .HttpFunction<AddPostCommand>(HttpMethod.Post)
                        .HttpFunction<GetPostQuery>("/{postId}", HttpMethod.Get)
                    )
                );
        }
    }
}
