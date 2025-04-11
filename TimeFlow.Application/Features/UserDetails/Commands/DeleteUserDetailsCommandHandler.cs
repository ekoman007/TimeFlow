

using MediatR;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.UserDetails.Commands
{
    public class DeleteUserDetailsCommandHandler : IRequestHandler<DeleteUserDetailsCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public DeleteUserDetailsCommandHandler(IUnitOfWork unitOfWork, IUserDetailsRepository userDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteUserDetailsCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var userDetailsExists = await _userDetailsRepository.GetById(request.Id, cancellationToken: cancellationToken);
            if(userDetailsExists == null)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = "This user Id does not exists.",
                    Result = request.Id
                };
            }
            if(!userDetailsExists.IsActive) { userDetailsExists.ChangeToActive(); } else { userDetailsExists.ChangeToDeActive(); }
             

            await _userDetailsRepository.Update(userDetailsExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "User details updated successfully.",
                Result = userDetailsExists.Id
            };
        }
    }
}