namespace TimeFlow.Application.Features.RefreshToken.DTOs
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
    }
}
