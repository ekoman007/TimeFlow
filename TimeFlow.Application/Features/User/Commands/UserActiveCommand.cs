using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.User.Command
{
    public class UserActiveCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; } 
    }
}
