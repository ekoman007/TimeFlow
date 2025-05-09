using MediatR; 
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.Appointments.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository)
        {
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            Appointment appointments = Appointment.Create(
                request.BusinessProfileId, 
                request.StaffId, request.GuestId,
                request.ApplicationUserDetailsId, 
                request.ServiceId, request.AppointmentDate, 
                request.StartTime, request.EndTime, request.Notes);

            await _appointmentRepository.AddAsync(appointments, cancellationToken);
            await _unitOfWork.Save(cancellationToken);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Appointment created successfully.",
                Result = appointments.Id
            };
        }
    }
}
