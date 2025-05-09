using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Services.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Staffs.Commands
{
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteServiceCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;

        public DeleteStaffCommandHandler(IUnitOfWork unitOfWork, IStaffRepository staffRepository)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var staffExists = await _staffRepository.GetByIdAsync(request.Id, cancellationToken);

            if (staffExists.IsActive == false)
            {
                staffExists.ChangeToActive();
            }
            else
            {
                staffExists.ChangeToDelete();
            }


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
