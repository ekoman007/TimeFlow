using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Queries
{
    public class GetAppointmentByIdQuery : IRequest<GeneralResponse<AppointmentDto>>
    {
        public int Id { get; set; }
    }
} 
