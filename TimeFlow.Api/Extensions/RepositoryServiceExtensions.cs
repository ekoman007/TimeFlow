using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Repositories;

namespace TimeFlow.Api.Extensions
{
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}