using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class CompleteAppointmentCommand : IRequest<GeneralResponse<AppointmentDto>>
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        
        public CompleteAppointmentCommand(int id)
        {
            Id = id;
        }
    }
} 
