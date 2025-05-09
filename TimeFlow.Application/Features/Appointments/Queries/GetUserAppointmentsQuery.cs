using System;
using System.Collections.Generic;
using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Queries
{
    public class GetUserAppointmentsQuery : IRequest<GeneralResponse<List<AppointmentDto>>>
    {
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
        
        public GetUserAppointmentsQuery(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
} 
