using MediatR;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Services.Commands
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceCommandHandler(IUnitOfWork unitOfWork, IServiceRepository serviceRepository)
        {
            _unitOfWork = unitOfWork;
            _serviceRepository = serviceRepository;
        }


        public async Task<GeneralResponse<int>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var serviceExists = await _serviceRepository.GetById(request.Id, cancellationToken: cancellationToken);

            serviceExists.ChangeAddress(request.Name, request.Description,request.Price,request.DurationInMinutes,
                                        request.ServiceType, request.Tags, request.MaxBookingsPerDay, request.AdditionalInfo
                                        , request.DiscountPrice, request.Availability, request.ServiceCode, request.RequiredMaterials
                                        , request.Currency, request.ImageUrl, request.BusinessProfileId); 

            await _serviceRepository.Update(serviceExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Service updated successfully.",
                Result = serviceExists.Id
            };
        }
    }
}