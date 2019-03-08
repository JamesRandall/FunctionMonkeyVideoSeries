using System.Net.Http;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
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
                    serviceCollection.AddApplication(commandRegistry);
                })
                .Functions(functions => functions
                    .HttpRoute("/api/v1/post", route => route
                        .HttpFunction<AddPostCommand>(HttpMethod.Post)
                    )
                );
        }
    }
}
