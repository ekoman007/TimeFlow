using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

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
                 request.ServiceId,request.AppointmentDate, 
                 request.StartTime, request.EndTime, request.Notes);


            await _appointmentRepository.Add(appointments, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Appointments created successfully.",
                Result = appointments.Id
            };
        }
    }
}