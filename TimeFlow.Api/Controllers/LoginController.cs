using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> PostCreate([FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command).ConfigureAwait(false);

            // Nëse login nuk është i suksesshëm
            if (!result.Success)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = result.Message
                });
            }
            //var expiryMinutes = _jwtSettings.Value.ExpiryMinutes;
            //var refreshTokenExpiryDays = _jwtSettings.Value.RefreshTokenExpiryDays;

            //// Vendosja e AccessToken në cookie
            //Response.Cookies.Append("access_token", result.Result.AccessToken, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = DateTimeOffset.UtcNow.AddHours(expiryMinutes)  // ose periudha që dëshiron
            //});

            //// Vendosja e RefreshToken në cookie
            //Response.Cookies.Append("refresh_token", result.Result.RefreshToken, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = DateTimeOffset.UtcNow.AddHours(expiryMinutes)   // ose periudha që dëshiron
            //});

            //// Kthe përgjigjen që është marrë nga handler, tani përfshin edhe AccessToken dhe RefreshToken
            //return Ok(new
            //{
            //    success = true,
            //    message = "Login successful",
            //    accessToken = result.Result.AccessToken,  // Kthejmë AccessToken
            //    refreshToken = result.Result.RefreshToken  // Kthejmë RefreshToken
            //});

            return Ok(result);
        }
         

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenRequest request)
        {
            var command = new RefreshAccessTokenCommand(request.RefreshToken);
            var result = await Mediator.Send(command).ConfigureAwait(false);

            if (!result.Success)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = result.Message
                });
            }

            // Vendosja e access token të ri në cookie
            Response.Cookies.Append("token", result.Result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            return Ok(new
            {
                success = true,
                accessToken = result.Result
            });
        }
    }

    public class RefreshAccessTokenRequest
    {
        public string RefreshToken { get; set; }
    }


}
 
