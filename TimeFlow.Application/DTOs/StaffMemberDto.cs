using System;
using System.Collections.Generic;

namespace TimeFlow.Application.DTOs
{
    public class StaffMemberDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int BusinessId { get; set; }
        public string Title { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ServiceDto> Services { get; set; }
        public IEnumerable<StaffAvailabilityDto> Availabilities { get; set; }
    }
} 
