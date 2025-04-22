using MediatR; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using TimeFlow.Api.Extensions;
using TimeFlow.Application.Features.Login.Commands;
using TimeFlow.Application.Features.RefreshToken.Commands;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : DefaultController
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        public LoginController(IMediator mediator, IOptions<JwtSettings> jwtSettings)
           : base(mediator)
        {
            _jwtSettings = jwtSettings;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginCreate([FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command).ConfigureAwait(false);

            if (!result.Success)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = result.Message
                });
            } 
            // Vendos refresh token si HttpOnly cookie
            Response.Cookies.Append("refreshToken", result.Result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // për HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(_jwtSettings.Value.RefreshTokenExpiryDays)
            });

            // Kthe access token në response body
            return Ok(new
            {
                success = true,
                message = result.Message,
                result = new
                {
                    accessToken = result.Result.AccessToken,
                    roleName = result.Result.RoleName,
                }
            });
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "No refresh token found."
                });
            }

            var command = new RefreshAccessTokenCommand(refreshToken);
            var result = await Mediator.Send(command).ConfigureAwait(false);

            if (!result.Success)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return Ok(new
            {
                success = true,
                accessToken = result.Result
            });
        }


        public class RefreshAccessTokenRequest
        {
            public string RefreshToken { get; set; }
        }


    }
}
 
