using TimeFlow.Infrastructure.Database;
using TimeFlow.Infrastructure.SeedData;

namespace TimeFlow.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TimeFlowDbContext>();

                await RoleSeeder.SeedRoleAsync(context); 
                //await UserSeeder.SeedUsersAsync(context);
                //await ServiceSeeder.SeedServicesAsync(context);
                //await BusinessProfileSeeder.SeedBusinessProfilesAsync(context);
            }
        }
    }
}