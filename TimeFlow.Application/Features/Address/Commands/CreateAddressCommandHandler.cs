using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.BussinesProfile.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Address.Commands
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressRepository _addressRepository;

        public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IAddressRepository addressRepository)
        {
            _unitOfWork = unitOfWork;
            _addressRepository = addressRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
             
            Domain.Aggregates.UsersAggregates.Address bussinesProfiles = Domain.Aggregates.UsersAggregates.Address.Create(request.Street, request.City, request.Country, request.ZipCode, request.Latitude, request.Longitude, request.IsPrimary, request.ApplicationUserDetailsId, request.BusinessProfileId);


            await _addressRepository.AddAsync(bussinesProfiles, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Bussines Profile created successfully.",
                Result = bussinesProfiles.Id
            };
        }
    }
}
