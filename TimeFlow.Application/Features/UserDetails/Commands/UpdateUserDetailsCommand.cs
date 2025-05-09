﻿using MediatR;
 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.UserDetails.Commands
{
    public class UpdateUserDetailsCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
        public string FullName { get; set; } 
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePicture { get; set; } 
    }
}