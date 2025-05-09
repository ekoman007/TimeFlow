using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;

namespace TimeFlow.Application.Features.StaffMembers.Commands
{
    public class UpdateStaffAvailabilityCommand : IRequest<List<StaffAvailabilityDto>>
    {
        public Guid StaffId { get; set; }
        public List<StaffAvailabilityRequest> Availabilities { get; set; } = new List<StaffAvailabilityRequest>();
    }

    public class StaffAvailabilityRequest
    {
        public Guid? Id { get; set; } // Null for new, non-null for existing
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? SpecificDate { get; set; }
    }

    public class UpdateStaffAvailabilityCommandHandler : IRequestHandler<UpdateStaffAvailabilityCommand, List<StaffAvailabilityDto>>
    {
        // Implementation will be added later
        public Task<List<StaffAvailabilityDto>> Handle(UpdateStaffAvailabilityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
} 
