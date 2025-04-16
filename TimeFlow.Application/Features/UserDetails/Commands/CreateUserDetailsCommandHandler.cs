using AutoMapper;
using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.UserDetails.Commands
{
    public class CreateUserDetailsCommandHandler : IRequestHandler<CreateUserDetailsCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IUserDetailsRepository _userDetailsRepository; 
        private readonly IUserRepository _userRepository; 

        public CreateUserDetailsCommandHandler(IUnitOfWork unitOfWork, 
            IUserDetailsRepository userDetailsRepository,
            IUserRepository userRepository

            )
        {
            _unitOfWork = unitOfWork; 
            _userDetailsRepository = userDetailsRepository; 
            _userRepository = userRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            // ketu duhet nje check qe nese user nuk eshte active mos me leju mu regjistru userdetails
            var checkUserId = await _userRepository.ExistsByIDAsync(request.UserId, cancellationToken: cancellationToken);
            if (!checkUserId)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"User with this ID {request.UserId} must exists on Application User."
                };
            }

            //KEtu duhet me check me id a ekziston jo me email ose me email
            var userExists = await _userDetailsRepository.GetUserDetailsByNameAsync(request.FullName, cancellationToken);
            if (userExists)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"User with this ID {request.UserId} already exists."
                };
            }

            ApplicationUserDetails userDetails = ApplicationUserDetails.Create(request.FullName, 
                request.PhoneNumber, request.DateOfBirth, request.ProfilePicture, request.UserId);

            await _userDetailsRepository.Add(userDetails, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "UserDetails created successfully.",
                Result = userDetails.Id
            };
        }
    }
}
