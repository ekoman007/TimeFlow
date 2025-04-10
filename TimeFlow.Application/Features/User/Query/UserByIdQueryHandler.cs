using MediatR;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.User.Query
{
    public class UserByIdQueryHandler : IRequestHandler<UserByIdQuery, GeneralResponse<UserModel>>
    {
        private readonly IUserRepository _userRepository;

        public UserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GeneralResponse<UserModel>> Handle(UserByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni përdoruesin me ID nga repository
            var user = await _userRepository.GetById(query.Id, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (user == null)
            {
                return new GeneralResponse<UserModel>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Kthejeni në modelin e duhur
            var userModel = new UserModel
            {
                Id = user.Id,
                Username = user.Username,  
                Email = user.Email,        
                IsActive = user.IsActive, 
                LastLogin = user.LastLogin,  
                RoleId = user.RoleId,  
            };

            return new GeneralResponse<UserModel>
            {
                Success = true,
                Message = "User found successfully", // Mesazhi i saktë për përdoruesin
                Result = userModel
            };
        }
    }
}
