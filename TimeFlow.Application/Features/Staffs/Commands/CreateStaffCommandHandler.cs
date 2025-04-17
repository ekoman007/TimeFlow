using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Staffs.Commands
{
    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;

        public CreateStaffCommandHandler(IUnitOfWork unitOfWork, IStaffRepository staffRepository)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            Staff staff = Staff.Create(
                request.BusinessProfileId,
                request.FullName,
                request.PhoneNumber,
                request.Email, 
                request.RoleId );


            await _staffRepository.Add(staff, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Staff created successfully.",
                Result = staff.Id
            };
        }
    }
}