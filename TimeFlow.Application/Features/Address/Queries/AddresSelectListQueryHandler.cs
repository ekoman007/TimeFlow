using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.Address.DTOs; 
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresSelectListQueryHandler : IRequestHandler<AddresSelectListQuery, List<AddresSelectListModal>>
    {
        private readonly IAddressRepository _addressRepository;

        public AddresSelectListQueryHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<AddresSelectListModal>> Handle(AddresSelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var addressList = await _addressRepository
                .Get(cancellationToken: cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return addressList.Select(x => new AddresSelectListModal
            {
                Id = x.Id,
                Street = x.Street
            }).ToList();
        }
    }
}