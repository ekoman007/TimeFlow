using MediatR;  
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Services.Commands
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;

        public CreateServiceCommandHandler(IUnitOfWork unitOfWork, IServiceRepository serviceRepository)
        {
            _unitOfWork = unitOfWork;
            _serviceRepository = serviceRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            Domain.Aggregates.UsersAggregates.Service address = Domain.Aggregates.UsersAggregates.Service.Create(
                request.Name, 
                request.Description,
                request.Price,
                request.DurationInMinutes,
                request.ServiceType,
                request.Tags,
                request.MaxBookingsPerDay,
                request.AdditionalInfo,
                request.DiscountPrice,
                request.Availability,
                request.RequiredMaterials,
                request.ServiceCode,
                request.Currency,
                request.ImageUrl,
                request.BusinessProfileId);


            await _serviceRepository.Add(address, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Address created successfully.",
                Result = address.Id
            };
        }
    }
}