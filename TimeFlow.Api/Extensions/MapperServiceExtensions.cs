using MediatR;
using TimeFlow.Application.Features.Login.Commands;

namespace TimeFlow.Api.Extensions
{
    public static class MapperServiceExtensions
    {
        public static IServiceCollection AddMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(LoginCommand).Assembly);

            return services;
        }
    }
}