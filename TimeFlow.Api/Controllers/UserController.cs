﻿using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using TimeFlow.Application.Features.User.Command;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Features.User.Query;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 

    public class UserController : DefaultController
    {
        public UserController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> PostCreate([FromBody] CreateUserCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<UserModel>> GetUserById(int id)
        {
            var query = new UserByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPost("activete")]
        public async Task<GeneralResponse<int>> UserActive([FromBody] UserActiveCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<IEnumerable<UserModel>>> GetRoles([FromQuery] UserListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }
    }
}
