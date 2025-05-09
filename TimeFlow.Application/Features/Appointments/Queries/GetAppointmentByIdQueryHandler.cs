using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.Appointments.Queries
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, GeneralResponse<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;

        public GetAppointmentByIdQueryHandler(
            IAppointmentRepository appointmentRepository,
            IServiceRepository serviceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<GeneralResponse<AppointmentDto>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (appointment == null)
            {
                return new GeneralResponse<AppointmentDto>
                {
                    Success = false,
                    Message = $"Appointment with ID {request.Id} not found"
                };
            }

            // Map the appointment entity to AppointmentDto
            var appointmentDto = new AppointmentDto
            {
                Id = appointment.Id,
                BusinessProfileId = appointment.BusinessProfileId,
                StaffId = appointment.StaffId,
                GuestId = appointment.GuestId > 0 ? appointment.GuestId : 0,
                ApplicationUserDetailsId = appointment.ApplicationUserDetailsId ?? 0,
                AppointmentDate = appointment.AppointmentDate,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Notes = appointment.Notes,
                Status = appointment.Status.ToString(),
                CreatedOn = appointment.CreatedOn,
                LastModifiedOn = appointment.ModifiedOn
            };

            // Populate service details
            if (appointment.ServiceId > 0)
            {
                var service = await _serviceRepository.GetByIdAsync(appointment.ServiceId, cancellationToken);
                if (service != null)
                {
                    appointmentDto.Service = new ServiceDto
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Description = service.Description,
                        Price = service.Price,
                        DurationInMinutes = service.DurationInMinutes,
                        BusinessId = service.BusinessProfileId
                    };
                }
            }

            return new GeneralResponse<AppointmentDto>
            {
                Success = true,
                Message = "Appointment retrieved successfully",
                Result = appointmentDto
            };
        }
    }
} 
