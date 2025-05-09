using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class CompleteAppointmentCommandHandler : IRequestHandler<CompleteAppointmentCommand, GeneralResponse<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResponse<AppointmentDto>> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
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

            // Complete the appointment
            appointment.Complete();

            if (!string.IsNullOrEmpty(request.Notes))
            {
                appointment.Notes = (appointment.Notes ?? "") + $" Completion note: {request.Notes}";
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
                Message = "Appointment marked as completed successfully",
                Result = appointmentDto
            };
        }
    }
} 
