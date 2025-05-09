using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class RescheduleAppointmentCommand : IRequest<GeneralResponse<AppointmentDto>>
    {
        public int Id { get; set; }
        public DateTime NewAppointmentDate { get; set; }
        public TimeSpan NewStartTime { get; set; }
        public TimeSpan NewEndTime { get; set; }
        public string? Notes { get; set; }
    }
} 