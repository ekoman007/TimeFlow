using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;


namespace TimeFlow.Infrastructure.Database
{
    public class TimeFlowDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } 
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationUserDetails> ApplicationUserDetails { get; set; } 
        public DbSet<Industry> Industries { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Service> Services { get; set; } 
        public DbSet<Address> Addresses { get; set; } 
        public DbSet<BusinessProfile> BusinessProfiles { get; set; } 

        public TimeFlowDbContext(DbContextOptions<TimeFlowDbContext> options)
            : base(options)
        {

        }
        // Aktivizoni Lazy Loading
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Sigurohuni që të mos keni konfiguruar dy herë
            {
                optionsBuilder
                    .UseLazyLoadingProxies() // Aktivizoni Lazy Loading
                    .UseSqlServer("DefaultConnection"); // Lidhja me bazën e të dhënave
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
        }
    }
}