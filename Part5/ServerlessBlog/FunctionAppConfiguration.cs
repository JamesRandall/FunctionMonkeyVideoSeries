using System.Net.Http;
using AutoMapper;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServerlessBlog.Application;
using ServerlessBlog.Commands;
using ServerlessBlog.CrossCuttingConcerns;

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
                        .Replace(new ServiceDescriptor(
                            typeof(ICommandDispatcher),
                            typeof(CustomDispatcher),
                            ServiceLifetime.Transient))
                        ;
                })
                .OutputAuthoredSource("/Users/jamesrandall/code/authoredSource")
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
