using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts; 

namespace TimeFlow.Application.Features.User.Command
{
    public class UserActiveCommandHandler : IRequestHandler<UserActiveCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserActiveCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UserActiveCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);
            if (user == null)
            {
                return new GeneralResponse<int>
                {
                    Success = true,
                    Message = "User not found"
                };
            }

            if(user.IsActive == true) { user.DeActivate(); } else { user.Activate(); }

           

            await _userRepository.Update(user, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "User has been Actived successfully.",
                Result = user.Id
            };
        }
    }
}