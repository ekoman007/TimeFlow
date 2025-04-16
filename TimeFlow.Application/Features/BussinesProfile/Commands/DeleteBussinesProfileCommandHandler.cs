using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.BussinesProfile.Commands
{
    public class DeleteBussinesProfileCommandHandler : IRequestHandler<DeleteBussinesProfileCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBussinesProfileRepository _bussinesProfileRepository;

        public DeleteBussinesProfileCommandHandler(IUnitOfWork unitOfWork, IBussinesProfileRepository bussinesProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _bussinesProfileRepository = bussinesProfileRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteBussinesProfileCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var bussinesProfileExisrts = await _bussinesProfileRepository.GetById(request.Id, cancellationToken: cancellationToken);

            if (bussinesProfileExisrts.IsActive == false)
            {
                bussinesProfileExisrts.ChangeToActive();
            }
            else
            {
                bussinesProfileExisrts.ChangeToDeActive();
            }


            await _bussinesProfileRepository.Update(bussinesProfileExisrts, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Category updated successfully.",
                Result = bussinesProfileExisrts.Id
            };
        }
    }
}