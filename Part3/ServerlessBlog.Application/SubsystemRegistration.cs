using AzureFromTheTrenches.Commanding.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Application.Repositories.Implementation;
using ServerlessBlog.Application.Validators;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application
{
    public static class SubsystemRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, ICommandRegistry commandRegistry)
        {
            serviceCollection
                .AddSingleton<IPostRepository, PostRepository>()
                .AddTransient<IValidator<AddPostCommand>, AddPostCommandValidator>()
                .AddTransient<IValidator<GetPostQuery>, GetPostQueryValidator>()
                ;

            commandRegistry.Discover(typeof(SubsystemRegistration).Assembly);

            return serviceCollection;
        }
    }
}
