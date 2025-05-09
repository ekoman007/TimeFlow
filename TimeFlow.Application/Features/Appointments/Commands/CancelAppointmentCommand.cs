using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class CancelAppointmentCommand : IRequest<GeneralResponse<int>>
    {
        public int AppointmentId { get; set; }
        public string? CancellationReason { get; set; }

        public CancelAppointmentCommand(int appointmentId, string? cancellationReason = null)
        {
            AppointmentId = appointmentId;
            CancellationReason = cancellationReason;
        }
    }
} 
