using MediatR;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Address.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressRepository _addressRepository;

        public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IAddressRepository addressRepository)
        {
            _unitOfWork = unitOfWork;
            _addressRepository = addressRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var addressExists = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

            addressExists.ChangeStreet(request.Street);
            addressExists.ChangeCity(request.City);
            addressExists.ChangeCountry(request.Country);
            addressExists.ChangeZipCode(request.ZipCode); 

            await _addressRepository.UpdateAsync(addressExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Address updated successfully.",
                Result = addressExists.Id
            };
        }
    }
}
