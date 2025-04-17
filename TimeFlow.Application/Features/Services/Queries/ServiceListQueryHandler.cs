using MediatR;
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Features.Services.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class ServiceListQueryHandler : IRequestHandler<ServiceListQuery, GeneralResponse<PagedResult<ServiceModel>>>
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceListQueryHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<GeneralResponse<PagedResult<ServiceModel>>> Handle(ServiceListQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<Service> queryable = _serviceRepository.Get(cancellationToken: cancellationToken);

        // Paginimi dhe mapping me ToPagedResultAsync
        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new ServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DurationInMinutes = x.DurationInMinutes,
                ServiceType = x.ServiceType,
                Tags = x.Tags,
                MaxBookingsPerDay = x.MaxBookingsPerDay,
                AdditionalInfo = x.AdditionalInfo,
                DiscountPrice = x.DiscountPrice,
                Availability = x.Availability,
                RequiredMaterials = x.RequiredMaterials,
                ServiceCode = x.ServiceCode,
                Currency = x.Currency,
                ImageUrl = x.ImageUrl,
                BusinessProfileId = x.BusinessProfileId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<ServiceModel>>
        {
            Success = true,
            Message = "Service list.",
            Result = pagedResult
        };
    }
}
