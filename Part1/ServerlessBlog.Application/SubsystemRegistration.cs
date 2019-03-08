using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Application.Repositories.Implementation;

namespace ServerlessBlog.Application
{
    public static class SubsystemRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, ICommandRegistry commandRegistry)
        {
            serviceCollection
                .AddTransient<IPostRepository, PostRepository>()
                ;

            commandRegistry.Discover(typeof(SubsystemRegistration).Assembly);

            return serviceCollection;
        }
    }
}
