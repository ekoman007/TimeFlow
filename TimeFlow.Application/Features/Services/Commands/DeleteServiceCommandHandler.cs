using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Services.Commands
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceCommandHandler(IUnitOfWork unitOfWork, IServiceRepository serviceRepository)
        {
            _unitOfWork = unitOfWork;
            _serviceRepository = serviceRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var serviceExists = await _serviceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (serviceExists.IsActive == false)
            {
                serviceExists.ChangeToActive();
            }
            else
            {
                serviceExists.ChangeToDelete();
            }


            await _serviceRepository.UpdateAsync(serviceExists, cancellationToken).ConfigureAwait(false);
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
