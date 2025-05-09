using System;
using System.Collections.Generic;
using TimeFlow.Domain.Aggregates.Enums;

namespace TimeFlow.Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        public int? StaffId { get; set; }
        public UserDto Customer { get; set; }
        public int GuestId { get; set; }
        public ServiceDto Service { get; set; }
        public int ApplicationUserDetailsId { get; set; }
        public StaffMemberDto StaffMember { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? Notes { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public string FormattedDuration => Duration.ToString(@"hh\:mm");
        public AppointmentStatus AppointmentStatus
        {
            get => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), Status);
            set => Status = value.ToString();
        }
        public string StatusName => AppointmentStatus.ToString();
        public string FormattedStartTime => StartTime.ToString("yyyy-MM-dd HH:mm");
        public string FormattedEndTime => EndTime.ToString("HH:mm");
    }
} 
