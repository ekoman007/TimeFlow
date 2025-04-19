using MediatR;
using TimeFlow.Application.Features.RefreshToken.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.RefreshToken.Queries
{
    public class GetRefreshTokenByValueQuery : IRequest<GeneralResponse<RefreshTokenModel>>
    {
        public string Token { get; set; }

        public GetRefreshTokenByValueQuery(string token)
        {
            Token = token;
        }
    }
}
