using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.BussinesProfile.Commands
{
    public class CreateBussinesProfileCommand : IRequest<GeneralResponse<int>>
    {
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string NIPT { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public int IndustryId { get; set; } 
        public int UserDetailsId { get; set; }
    }
}