using MediatR;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Staffs.Commands
{
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;

        public UpdateStaffCommandHandler(IUnitOfWork unitOfWork, IStaffRepository staffRepository)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
        }


        public async Task<GeneralResponse<int>> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var staffExists = await _staffRepository.GetByIdAsync(request.Id, cancellationToken);

            staffExists.ChangeStaff(request.BusinessProfileId, request.FullName, request.PhoneNumber, request.Email,
                                        request.RoleId);

            await _staffRepository.UpdateAsync(staffExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Staff updated successfully.",
                Result = staffExists.Id
            };
        }
    }
}
