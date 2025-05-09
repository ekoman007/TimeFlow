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
    public class GetUserAppointmentsQueryHandler : IRequestHandler<GetUserAppointmentsQuery, GeneralResponse<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IServiceRepository _serviceRepository;

        public GetUserAppointmentsQueryHandler(
            IAppointmentRepository appointmentRepository,
            IUserDetailsRepository userDetailsRepository,
            IServiceRepository serviceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userDetailsRepository = userDetailsRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<GeneralResponse<List<AppointmentDto>>> Handle(GetUserAppointmentsQuery request, CancellationToken cancellationToken)
        {
            // Get the user details ID from the user ID
            var userDetails = await _userDetailsRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if (userDetails == null)
            {
                return new GeneralResponse<List<AppointmentDto>>
                {
                    Success = false,
                    Message = "User details not found",
                    Result = new List<AppointmentDto>()
                };
            }

            // Get appointments for this user
            var appointments = await _appointmentRepository.GetByUserDetailsIdAsync(
                userDetails.Id, 
                request.StartDate, 
                request.EndDate, 
                cancellationToken);

            var appointmentDtos = new List<AppointmentDto>();

            foreach (var a in appointments)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = a.Id,
                    BusinessProfileId = a.BusinessProfileId,
                    StaffId = a.StaffId,
                    GuestId = a.GuestId > 0 ? a.GuestId : 0,
                    ApplicationUserDetailsId = a.ApplicationUserDetailsId ?? 0,
                    AppointmentDate = a.AppointmentDate,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Notes = a.Notes,
                    Status = a.Status.ToString(),
                    CreatedOn = a.CreatedOn,
                    LastModifiedOn = a.ModifiedOn
                };

                // Get and set the service information if available
                if (a.ServiceId > 0)
                {
                    var service = await _serviceRepository.GetByIdAsync(a.ServiceId, cancellationToken);
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

                appointmentDtos.Add(appointmentDto);
            }

            return new GeneralResponse<List<AppointmentDto>>
            {
                Success = true,
                Message = "User appointments retrieved successfully",
                Result = appointmentDtos
            };
        }
    }
} 
