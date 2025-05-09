 

namespace TimeFlow.Application.Features.Login.Dtos
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RoleName { get; set; }
    }
}
