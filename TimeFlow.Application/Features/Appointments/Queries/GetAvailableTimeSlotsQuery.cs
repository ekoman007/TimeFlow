using MediatR;
using System;
using System.Collections.Generic;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Appointments.Queries
{
    public class GetAvailableTimeSlotsQuery : IRequest<GeneralResponse<List<TimeSlotDto>>>
    {
        public int ServiceId { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        
        public GetAvailableTimeSlotsQuery(int serviceId, int staffId, DateTime date)
        {
            ServiceId = serviceId;
            StaffId = staffId;
            Date = date;
        }
    }
} 
