using System;
using MediatR;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class CreateAppointmentCommand : IRequest<GeneralResponse<int>>
    {
        public int BusinessProfileId { get; set; }
        public int? StaffId { get; set; }
        public int GuestId { get; set; }
        public int ApplicationUserDetailsId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Notes { get; set; }
    }
}
