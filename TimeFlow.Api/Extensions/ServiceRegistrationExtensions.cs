using MediatR;
using TimeFlow.Application.Features.Login.Commands;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Repositories;
using TimeFlow.Infrastructure.Security;

namespace TimeFlow.Api.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Regjistro shërbimet e aplikacionit
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
            services.AddScoped<IIndustryRepository, IndustryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBussinesProfileRepository, BussinesProfileRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();


            // Regjistro AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Regjistro MediatR
            services.AddMediatR(typeof(LoginCommand).Assembly);
        }
    }
}
