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

namespace TimeFlow.Application.Features.Address.Commands
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressRepository _addressRepository;

        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, IAddressRepository addressRepository)
        {
            _unitOfWork = unitOfWork;
            _addressRepository = addressRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var addressExisrts = await _addressRepository.GetById(request.Id, cancellationToken: cancellationToken);

            if (addressExisrts.IsActive == false)
            {
                addressExisrts.ChangeToActive();
            }
            else
            {
                addressExisrts.ChangeToDeActive();
            }


            await _addressRepository.Update(addressExisrts, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Address updated successfully.",
                Result = addressExisrts.Id
            };
        }
    }
}