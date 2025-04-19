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
        public DbSet<Staff> Staffs { get; set; } 
        public DbSet<WorkSchedule> WorkSchedules { get; set; } 
        public DbSet<WorkShift> WorkShifts { get; set; } 
        public DbSet<Guest> Guest { get; set; } 
        public DbSet<Appointment> Appointments { get; set; } 
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; } 
        public DbSet<Payment> Payments { get; set; } 
        public DbSet<RefreshToken> RefreshTokens { get; set; } 

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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
             
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurimi i lidhjes për Appointment me Service
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany() // Kjo do të jetë e ndryshueshme nëse keni ICollection<Appointment> në Service
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.NoAction); // Siguron që nuk do të ketë fshirje automatikisht

            // Konfigurimi i lidhjes për Appointment me BusinessProfile
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.BusinessProfile)
                .WithMany()
                .HasForeignKey(a => a.BusinessProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            // Konfigurimi i lidhjes për Appointment me Staff
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany()
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.NoAction);

            // Konfigurimi i lidhjes për Appointment me Guest
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Guest)
                .WithMany()
                .HasForeignKey(a => a.GuestId)
                .OnDelete(DeleteBehavior.NoAction);

            // Konfigurimi i lidhjes për Appointment me ApplicationUserDetails
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.ApplicationUserDetails)
                .WithMany()
                .HasForeignKey(a => a.ApplicationUserDetailsId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}