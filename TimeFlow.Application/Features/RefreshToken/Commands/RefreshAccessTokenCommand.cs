using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.RefreshToken.Commands
{
    public class RefreshAccessTokenCommand : IRequest<GeneralResponse<string>>
    {
        public string RefreshToken { get; set; }

        public RefreshAccessTokenCommand(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", nameof(refreshToken));

            RefreshToken = refreshToken;
        }
    }
}