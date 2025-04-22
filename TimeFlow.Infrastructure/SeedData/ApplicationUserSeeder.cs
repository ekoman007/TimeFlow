using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.SeedData
{
    public static class ApplicationUserSeeder
    {
        public static async Task SeedApplicationUserAsync(TimeFlowDbContext context)
        {
            if (await context.ApplicationUsers.AnyAsync())
                return;

            var users = new List<ApplicationUser>
            {
                ApplicationUser.Create("admin", "erhan@msn.com","203925",true),
                ApplicationUser.Create("bussinessAdmin", "erhan@msn.com","203925",true),
                ApplicationUser.Create("User", "erhan@msn.com","203925",true)
            };

            context.ApplicationUsers.AddRange(users);
            await context.SaveChangesAsync();
        }
    } 
} 
