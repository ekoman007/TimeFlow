using MediatR; 
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.BussinesProfile.Commands
{
    public class CreateBussinesProfileCommandHandler : IRequestHandler<CreateBussinesProfileCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBussinesProfileRepository _bussinesProfileRepository;

        public CreateBussinesProfileCommandHandler(IUnitOfWork unitOfWork, IBussinesProfileRepository bussinesProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _bussinesProfileRepository = bussinesProfileRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateBussinesProfileCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var bussinesProfile = await _bussinesProfileRepository.GetBussinesProfileByNameAsync(request.BusinessName, cancellationToken);
            if (bussinesProfile)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"Bussines Profile with this {request.BusinessName} already exists."
                };
            }

            Domain.Aggregates.UsersAggregates.BusinessProfile bussinesProfiles = Domain.Aggregates.UsersAggregates.BusinessProfile.Create(request.BusinessName, request.Email,request.PhoneNumber, request.Website, request.Description,request.LogoUrl, request.IndustryId, request.UserDetailsId, request.NIPT);


            await _bussinesProfileRepository.Add(bussinesProfiles, cancellationToken).ConfigureAwait(false);
            try
            {
                await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex) { }
           

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Bussines Profile created successfully.",
                Result = bussinesProfiles.Id
            };
        }
    }
}