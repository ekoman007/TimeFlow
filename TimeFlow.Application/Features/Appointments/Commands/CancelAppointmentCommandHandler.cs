using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, GeneralResponse<int>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResponse<int>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId, cancellationToken);

            if (appointment == null)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"Appointment with ID {request.AppointmentId} not found"
                };
            }

            // Check if the appointment can be cancelled
            if (appointment.Status.ToString() == "Completed" || 
                appointment.Status.ToString() == "Cancelled" || 
                appointment.Status.ToString() == "NoShow")
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"Cannot cancel appointment with status: {appointment.Status}"
                };
            }

            // Update appointment status to cancelled
            appointment.Cancel();
            if (!string.IsNullOrEmpty(request.CancellationReason))
            {
                appointment.Notes = (appointment.Notes ?? "") + $" Cancellation reason: {request.CancellationReason}";
            }

            await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
            await _unitOfWork.Save(cancellationToken);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Appointment cancelled successfully",
                Result = appointment.Id
            };
        }
    }
} 
