using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, GeneralResponse<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RescheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResponse<AppointmentDto>> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
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

            // Check if the appointment can be rescheduled
            if (appointment.Status.ToString() == "Completed" || 
                appointment.Status.ToString() == "Cancelled" || 
                appointment.Status.ToString() == "NoShow")
            {
                return new GeneralResponse<AppointmentDto>
                {
                    Success = false,
                    Message = $"Cannot reschedule appointment with status: {appointment.Status}"
                };
            }

            // Check for overlapping appointments if a staff member is assigned
            if (appointment.StaffId.HasValue)
            {
                bool hasOverlap = await _appointmentRepository.HasOverlappingAppointmentAsync(
                    appointment.StaffId.Value,
                    request.NewAppointmentDate,
                    request.NewStartTime,
                    request.NewEndTime,
                    appointment.Id,
                    cancellationToken);

                if (hasOverlap)
                {
                    return new GeneralResponse<AppointmentDto>
                    {
                        Success = false,
                        Message = "The requested time slot overlaps with another appointment"
                    };
                }
            }

            // Update appointment details
            appointment.AppointmentDate = request.NewAppointmentDate;
            appointment.StartTime = request.NewStartTime;
            appointment.EndTime = request.NewEndTime;
            
            if (!string.IsNullOrEmpty(request.Notes))
            {
                appointment.Notes = (appointment.Notes ?? "") + $" Rescheduled: {request.Notes}";
            }

            await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
            await _unitOfWork.Save(cancellationToken);

            // Create a DTO to return
            var appointmentDto = new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes
                // Fill in other properties as needed
            };

            return new GeneralResponse<AppointmentDto>
            {
                Success = true,
                Message = "Appointment rescheduled successfully",
                Result = appointmentDto
            };
        }
    }
} 