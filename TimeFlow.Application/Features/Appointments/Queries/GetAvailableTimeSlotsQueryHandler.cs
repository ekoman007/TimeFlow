using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.Appointments.Queries
{
    public class GetAvailableTimeSlotsQueryHandler : IRequestHandler<GetAvailableTimeSlotsQuery, GeneralResponse<List<TimeSlotDto>>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStaffRepository _staffRepository;

        public GetAvailableTimeSlotsQueryHandler(
            IServiceRepository serviceRepository,
            IAppointmentRepository appointmentRepository,
            IStaffRepository staffRepository)
        {
            _serviceRepository = serviceRepository;
            _appointmentRepository = appointmentRepository;
            _staffRepository = staffRepository;
        }

        public async Task<GeneralResponse<List<TimeSlotDto>>> Handle(GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            // Get the service to know the duration
            var service = await _serviceRepository.GetByIdAsync(request.ServiceId, cancellationToken);
            if (service == null)
            {
                return new GeneralResponse<List<TimeSlotDto>>
                {
                    Success = false,
                    Message = "Service not found",
                    Result = new List<TimeSlotDto>()
                };
            }

            // Define business hours (this could come from configuration or database)
            TimeSpan startOfBusinessDay = new TimeSpan(9, 0, 0); // 9:00 AM
            TimeSpan endOfBusinessDay = new TimeSpan(18, 0, 0);  // 6:00 PM

            // Get existing appointments for that date
            IEnumerable<Domain.Aggregates.UsersAggregates.Appointment> existingAppointments;

            // Check if staff exists
            var staff = await _staffRepository.GetByIdAsync(request.StaffId, cancellationToken);
            if (staff == null)
            {
                return new GeneralResponse<List<TimeSlotDto>>
                {
                    Success = false,
                    Message = "Staff member not found",
                    Result = new List<TimeSlotDto>()
                };
            }

            // Get appointments for specified staff on the date
            existingAppointments = await _appointmentRepository.GetByStaffIdAsync(
                request.StaffId, 
                request.Date,
                request.Date,
                cancellationToken);

            // Calculate duration from the service
            int durationMinutes = service.DurationInMinutes;
            TimeSpan serviceDuration = TimeSpan.FromMinutes(durationMinutes);

            // Generate available time slots
            var availableTimeSlots = new List<TimeSlotDto>();
            
            for (TimeSpan currentTime = startOfBusinessDay; 
                 currentTime.Add(serviceDuration) <= endOfBusinessDay; 
                 currentTime = currentTime.Add(TimeSpan.FromMinutes(30)))
            {
                TimeSpan endTime = currentTime.Add(serviceDuration);

                // Check for conflicts with existing appointments
                bool isAvailable = !existingAppointments.Any(a => 
                    (currentTime >= a.StartTime && currentTime < a.EndTime) ||
                    (endTime > a.StartTime && endTime <= a.EndTime) ||
                    (currentTime <= a.StartTime && endTime >= a.EndTime));

                availableTimeSlots.Add(new TimeSlotDto
                {
                    Date = request.Date,
                    StartTime = currentTime,
                    EndTime = endTime,
                    IsAvailable = isAvailable
                });
            }

            return new GeneralResponse<List<TimeSlotDto>>
            {
                Success = true,
                Message = "Available time slots retrieved successfully",
                Result = availableTimeSlots
            };
        }
    }
} 
