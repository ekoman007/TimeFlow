using Microsoft.EntityFrameworkCore; 
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.SeedData
{ 
    public static class RoleSeeder
    {
        public static async Task SeedRoleAsync(TimeFlowDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var role = new List<Role>
            {
                Role.Create("Admin", "Admin main"),
                Role.Create("BussinesAdmin", "Bussines admin")
            };

            context.Roles.AddRange(role);
            await context.SaveChangesAsync();
        }
    }
}

