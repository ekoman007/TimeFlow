using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.Address.DTOs; 
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresListQueryHandler : IRequestHandler<AddresListQuery, GeneralResponse<IEnumerable<AddressModel>>>
    {
        private readonly IAddressRepository _addressRepository;

        public AddresListQueryHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<GeneralResponse<IEnumerable<AddressModel>>> Handle(AddresListQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Aggregates.UsersAggregates.Address> queryable = _addressRepository.Get(cancellationToken: cancellationToken);

            // Për aplikimin e paginimit
            var totalCount = await queryable.CountAsync(cancellationToken);
            var bussines = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var addressModel = bussines.Select(x =>
                new AddressModel
                {
                    Id = x.Id,
                    Street = x.Street,
                    City = x.City,
                    Country = x.Country,
                    ZipCode = x.ZipCode,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    ApplicationUserDetailsId = x.ApplicationUserDetailsId,
                    BusinessProfileId = x.BusinessProfileId,
                    IsPrimary = x.IsPrimary,
                });

            return new GeneralResponse<IEnumerable<AddressModel>>
            {
                Success = true,
                Message = "Address Profil list.",
                Result = addressModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}