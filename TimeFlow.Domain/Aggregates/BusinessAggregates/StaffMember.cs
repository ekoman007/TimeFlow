using System;
using System.Collections.Generic;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.BusinessAggregates
{
    public class StaffMember : BaseEntity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid BusinessId { get; private set; }
        public Business Business { get; private set; }
        public string Title { get; private set; }
        public string? Bio { get; private set; }
        public string? ProfileImageUrl { get; private set; }
        public ICollection<StaffService> Services { get; private set; }
        public ICollection<StaffAvailability> Availabilities { get; private set; }
        public ICollection<Appointment> Appointments { get; private set; }
        public bool IsActive { get; private set; }

        // Private constructor for EF Core
        private StaffMember() 
        {
            Services = new List<StaffService>();
            Availabilities = new List<StaffAvailability>();
            Appointments = new List<Appointment>();
        }

        public StaffMember(Guid userId, Guid businessId, string title, string? bio = null, string? profileImageUrl = null)
        {
            UserId = userId;
            BusinessId = businessId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Bio = bio;
            ProfileImageUrl = profileImageUrl;
            IsActive = true;
            Services = new List<StaffService>();
            Availabilities = new List<StaffAvailability>();
            Appointments = new List<Appointment>();
        }

        public void UpdateDetails(string title, string? bio, string? profileImageUrl)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Bio = bio;
            ProfileImageUrl = profileImageUrl;
        }

        public void AddService(StaffService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            
            Services.Add(service);
        }

        public void AddAvailability(StaffAvailability availability)
        {
            if (availability == null)
                throw new ArgumentNullException(nameof(availability));
            
            Availabilities.Add(availability);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
} 