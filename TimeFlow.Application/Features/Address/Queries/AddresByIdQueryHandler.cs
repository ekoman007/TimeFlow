using MediatR; 
using TimeFlow.Application.Features.Address.DTOs;
using TimeFlow.Application.Features.BussinesProfile.DTOs; 
using TimeFlow.Application.Responses; 
using TimeFlow.Infrastructure.Contracts; 

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresByIdQueryHandler : IRequestHandler<AddresByIdQuery, GeneralResponse<AddressModel>>
    { 
        private readonly IAddressRepository _addressRepository;

        public AddresByIdQueryHandler(IAddressRepository addressRepository)
        { 
            _addressRepository = addressRepository;
        }

        public async Task<GeneralResponse<AddressModel>> Handle(AddresByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var address = await _addressRepository.GetByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);


            if (address == null)
            {
                return new GeneralResponse<AddressModel>
                {
                    Success = false,
                    Message = "Address not found"
                };
            }

            // Kthejeni në modelin e duhur
            var addressModel = new AddressModel
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                Country = address.Country,
                ZipCode = address.ZipCode,
                ApplicationUserDetailsId = address.ApplicationUserDetailsId,
                BusinessProfileId = address.BusinessProfileId,
                IsPrimary = address.IsPrimary,
                Latitude = address.Latitude,
                Longitude = address.Longitude,
                
            };

            return new GeneralResponse<AddressModel>
            {
                Success = true,
                Message = "Address  found successfully",
                Result = addressModel
            };
        }
    }
}
