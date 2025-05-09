using System;
using System.Collections.Generic;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.BusinessAggregates
{
    public class Service : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public TimeSpan Duration { get; private set; }
        public Guid BusinessId { get; private set; }
        public Business Business { get; private set; }
        public ICollection<StaffService> StaffServices { get; private set; }
        public bool IsActive { get; private set; }

        // Private constructor for EF Core
        private Service() 
        {
            StaffServices = new List<StaffService>();
        }

        public Service(string name, string description, decimal price, TimeSpan duration, Guid businessId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            if (price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(price));
            Price = price;
            if (duration.TotalMinutes <= 0)
                throw new ArgumentException("Duration must be positive", nameof(duration));
            Duration = duration;
            BusinessId = businessId;
            IsActive = true;
            StaffServices = new List<StaffService>();
        }

        public void UpdateDetails(string name, string description, decimal price, TimeSpan duration)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            if (price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(price));
            Price = price;
            if (duration.TotalMinutes <= 0)
                throw new ArgumentException("Duration must be positive", nameof(duration));
            Duration = duration;
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