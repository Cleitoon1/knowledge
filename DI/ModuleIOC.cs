using Data.Transactions;
using DI.Configutarions;
using Microsoft.Extensions.DependencyInjection;

namespace DI
{
    public class ModuleIOC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.RegisterRepositories();
        }
    }
}
