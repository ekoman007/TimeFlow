using MediatR; 
using TimeFlow.Application.Features.Address.DTOs;
using TimeFlow.Application.Features.Address.Queries;
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Services.Queries
{
    public class ServiceByIdQueryHandler : IRequestHandler<ServiceByIdQuery, GeneralResponse<ServiceModel>>
    { 
        private readonly IServiceRepository _serviceRepository;

        public ServiceByIdQueryHandler(IServiceRepository serviceRepository)
        { 
            _serviceRepository = serviceRepository;
        }

        public async Task<GeneralResponse<ServiceModel>> Handle(ServiceByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var service = await _serviceRepository.GetById(query.Id, cancellationToken: cancellationToken).ConfigureAwait(false);


            if (service == null)
            {
                return new GeneralResponse<ServiceModel>
                {
                    Success = false,
                    Message = "Address not found"
                };
            }

            // Kthejeni në modelin e duhur
            var serviceModel = new ServiceModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes,
                ServiceType = service.ServiceType, 
                Tags = service.Tags,
                MaxBookingsPerDay = service.MaxBookingsPerDay,
                AdditionalInfo = service.AdditionalInfo,
                DiscountPrice = service.DiscountPrice,
                Availability = service.Availability,
                RequiredMaterials = service.RequiredMaterials,
                ServiceCode = service.ServiceCode,
                Currency = service.Currency,
                ImageUrl = service.ImageUrl,
                BusinessProfileId = service.BusinessProfileId,

            };

            return new GeneralResponse<ServiceModel>
            {
                Success = true,
                Message = "Service found successfully",
                Result = serviceModel
            };
        }
    }
}