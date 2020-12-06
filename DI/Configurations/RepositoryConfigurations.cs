using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Configutarions
{
    public static class RepositoryConfigurations
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IArticleRep, ArticleRep>();
            services.AddScoped<ICategoryRep, CategoryRep>();
            services.AddScoped<IProfileRep, ProfileRep>();
            services.AddScoped<IUserRep, UserRep>();
        }
    }
}
