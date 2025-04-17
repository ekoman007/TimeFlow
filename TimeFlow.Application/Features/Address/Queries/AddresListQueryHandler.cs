using MediatR;
using TimeFlow.Application.Features.Address.DTOs;
using TimeFlow.Application.Features.Address.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class AddressListQueryHandler : IRequestHandler<AddresListQuery, GeneralResponse<PagedResult<AddressModel>>>
{
    private readonly IAddressRepository _addressRepository;

    public AddressListQueryHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<GeneralResponse<PagedResult<AddressModel>>> Handle(AddresListQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<Address> queryable = _addressRepository.Get(cancellationToken: cancellationToken);

        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new AddressModel
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
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<AddressModel>>
        {
            Success = true,
            Result = pagedResult, 
            Message = "Address profile list.",
        };
    }
}
