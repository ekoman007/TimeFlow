using System;

namespace TimeFlow.Application.DTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice => Price.ToString("C");
        public TimeSpan Duration { get; set; }
        public string FormattedDuration => Duration.ToString(@"hh\:mm");
        public int BusinessId { get; set; }
        public bool IsActive { get; set; }
        public int DurationInMinutes { get; set; }
    }
} 
