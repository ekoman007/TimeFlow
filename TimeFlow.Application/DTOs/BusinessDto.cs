using System;
using TimeFlow.Domain.Aggregates.Enums;

namespace TimeFlow.Application.DTOs
{
    public class BusinessDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BusinessCategory Category { get; set; }
        public string CategoryName => Category.ToString();
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string TimeZone { get; set; }
        public bool IsActive { get; set; }
    }
} 
