using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Security;

namespace TimeFlow.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _secretKey;
        private readonly int _expiryMinutes;
        private readonly int _refreshTokenExpiryDays;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenGenerator(IConfiguration config)
        {
            _secretKey = config["JwtSettings:SecretKey"];
            _expiryMinutes = int.Parse(config["JwtSettings:ExpiryMinutes"]);
            _refreshTokenExpiryDays = int.Parse(config["JwtSettings:RefreshTokenExpiryDays"]);
            _issuer = config["JwtSettings:Issuer"] ?? "YourApp";
            _audience = config["JwtSettings:Audience"] ?? "YourApp";
        }

        public string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expiryMinutes), // preferohet UTC
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public int GetRefreshTokenExpiryDays() => _refreshTokenExpiryDays;
    }
}
