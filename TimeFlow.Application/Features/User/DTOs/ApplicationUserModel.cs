 

namespace TimeFlow.Application.Features.User.DTOs
{
    public class ApplicationUserModel
    {
        public int Id { get;  set; }
        public string Username { get;  set; }
        public string Email { get;  set; } 
        public bool IsActive { get;  set; }  
        public DateTime? LastLogin { get;  set; }
        public int RoleId { get;  set; }
    }
}
