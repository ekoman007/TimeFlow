using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.Roles.Queries;
using TimeFlow.Application.Features.User.Command;
using TimeFlow.Application.Features.UserDetails.Commands;
using TimeFlow.Application.Queries.Roles.TimeFlow.Application.Queries.Roles;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserDetailsController : DefaultController
    {
        public ApplicationUserDetailsController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> RoleCreate([FromBody] CreateUserDetailsCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPost("update")]
        public async Task<GeneralResponse<int>> UserDetailsUpdate([FromBody] UpdateUserDetailsCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }


        //[HttpPost("delete")]
        //public async Task<GeneralResponse<int>> DeleteRole([FromBody] DeleteRoleCommand command)
        //{
        //    return await Mediator.Send(command).ConfigureAwait(false);
        //}

        //[HttpGet]
        //public async Task<GeneralResponse<IEnumerable<RolesModel>>> GetRoles([FromQuery] RoleListQuery query)
        //{
        //    return await Mediator.Send(query).ConfigureAwait(false);
        //}


        //[HttpGet("{id}")]
        //public async Task<GeneralResponse<RolesModel>> GetRoleById(int id)
        //{
        //    var query = new RoleByIdQuery(id);
        //    return await Mediator.Send(query).ConfigureAwait(false);
        //}

        //[HttpGet("select-list")]
        //public async Task<ActionResult<List<RoleSelectListModel>>> GetRoleSelectList()
        //{
        //    var result = await Mediator.Send(new RoleSelectListQuery());
        //    return Ok(result);
        //}
    }
}
