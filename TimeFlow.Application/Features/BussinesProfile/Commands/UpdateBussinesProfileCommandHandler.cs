using MediatR; 
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.BussinesProfile.Commands
{
    public class UpdateBussinesProfileCommandHandler : IRequestHandler<UpdateBussinesProfileCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBussinesProfileRepository _bussinesProfileRepository;

        public UpdateBussinesProfileCommandHandler(IUnitOfWork unitOfWork, IBussinesProfileRepository bussinesProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _bussinesProfileRepository = bussinesProfileRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateBussinesProfileCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var bussinesProfileExists = await _bussinesProfileRepository.GetById(request.Id, cancellationToken: cancellationToken);

            bussinesProfileExists.ChangeBussinesName(request.BusinessName);
            bussinesProfileExists.ChangeDescription(request.Description);
            bussinesProfileExists.ChangeEmail(request.Email);
            bussinesProfileExists.ChangePhoneNumber(request.PhoneNumber);
            bussinesProfileExists.ChangeLogoUrl(request.LogoUrl);
            bussinesProfileExists.ChangeWebsite(request.Website);
            bussinesProfileExists.ChangeIndustry(request.IndustryId);
            bussinesProfileExists.ChangeUserDetails(request.UserDetailsId);

            await _bussinesProfileRepository.Update(bussinesProfileExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Bussines Profile updated successfully.",
                Result = bussinesProfileExists.Id
            };
        }
    }
}