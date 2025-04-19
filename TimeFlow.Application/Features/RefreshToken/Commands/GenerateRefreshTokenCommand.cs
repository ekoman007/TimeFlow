using MediatR;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.RefreshToken.Commands
{
    public class GenerateRefreshTokenCommand : IRequest<GeneralResponse<string>>
    {
        public int UserId { get; set; }

        public GenerateRefreshTokenCommand(int userId)
        {
            if (userId ==null)
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            UserId = userId;
        }
    }
}
