using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using TimeFlow.Application.Features.User.Command;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Features.User.Queries;
using TimeFlow.Application.Features.User.Query;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Queries.Roles.TimeFlow.Application.Queries.Roles;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 

    public class ApplicationUserController : DefaultController
    {
        public ApplicationUserController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> UserCreate([FromBody] CreateUserCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<ApplicationUserModel>> GetUserById(int id)
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
        public async Task<GeneralResponse<PagedResult<ApplicationUserModel>>> GetUsers([FromQuery] UserListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<ApplicationUserModel>>> GetSelectList()
        {
            var result = await Mediator.Send(new UserSelectListQuery());
            return Ok(result);
        }
    }
}
