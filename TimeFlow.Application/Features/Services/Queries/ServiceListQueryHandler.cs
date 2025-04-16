using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Services.Queries
{
    public class ServiceListQueryHandler : IRequestHandler<ServiceListQuery, GeneralResponse<IEnumerable<ServiceModel>>>
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceListQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<GeneralResponse<IEnumerable<ServiceModel>>> Handle(ServiceListQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Aggregates.UsersAggregates.Service> queryable = _serviceRepository.Get(cancellationToken: cancellationToken);
 
            var totalCount = await queryable.CountAsync(cancellationToken);
            var address = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var serviceModel = address.Select(x =>
                new ServiceModel
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
                });

            return new GeneralResponse<IEnumerable<ServiceModel>>
            {
                Success = true,
                Message = "Service list.",
                Result = serviceModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}