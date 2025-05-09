using System;

namespace TimeFlow.Application.DTOs
{
    public class StaffAvailabilityDto
    {
        public Guid Id { get; set; }
        public Guid StaffMemberId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string DayName => DayOfWeek.ToString();
        public TimeSpan StartTime { get; set; }
        public string FormattedStartTime => StartTime.ToString(@"hh\:mm");
        public TimeSpan EndTime { get; set; }
        public string FormattedEndTime => EndTime.ToString(@"hh\:mm");
        public bool IsRecurring { get; set; }
        public DateTime? SpecificDate { get; set; }
        public string FormattedSpecificDate => SpecificDate?.ToString("yyyy-MM-dd");
    }
} 
