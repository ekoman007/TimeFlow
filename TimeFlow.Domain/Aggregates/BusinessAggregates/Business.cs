using System;
using System.Collections.Generic;
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.BusinessAggregates
{
    public class Business : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public BusinessCategory Category { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string? LogoUrl { get; private set; }
        public string? Website { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string TimeZone { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<Service> Services { get; private set; }
        public ICollection<StaffMember> Staff { get; private set; }

        // Private constructor for EF Core
        private Business() 
        {
            Services = new List<Service>();
            Staff = new List<StaffMember>();
        }

        public Business(string name, string description, BusinessCategory category, 
            string address, string city, string country, string timeZone,
            string? logoUrl = null, string? website = null, string? phoneNumber = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Category = category;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            TimeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));
            LogoUrl = logoUrl;
            Website = website;
            PhoneNumber = phoneNumber;
            IsActive = true;
            Services = new List<Service>();
            Staff = new List<StaffMember>();
        }

        public void UpdateDetails(string name, string description, BusinessCategory category,
            string address, string city, string country, string timeZone,
            string? logoUrl, string? website, string? phoneNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Category = category;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            TimeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));
            LogoUrl = logoUrl;
            Website = website;
            PhoneNumber = phoneNumber;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void AddService(Service service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            Services.Add(service);
        }

        public void AddStaffMember(StaffMember staffMember)
        {
            if (staffMember == null)
                throw new ArgumentNullException(nameof(staffMember));

            Staff.Add(staffMember);
        }
    }
} 