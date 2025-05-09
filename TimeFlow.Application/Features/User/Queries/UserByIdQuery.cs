using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.User.Query
{
    public class UserByIdQuery : IRequest<GeneralResponse<ApplicationUserModel>>
    {
        public int Id { get; set; }
        public UserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
