using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.Roles.Queries;
using TimeFlow.Application.Features.Staffs.Commands;
using TimeFlow.Application.Features.Staffs.DTOs;
using TimeFlow.Application.Features.Staffs.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Queries.Roles.TimeFlow.Application.Queries.Roles;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : DefaultController
    {
        public StaffController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> StaffCreate([FromBody] CreateStaffCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPost("update")]
        public async Task<GeneralResponse<int>> StaffUpdate([FromBody] UpdateStaffCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }


        [HttpPost("delete")]
        public async Task<GeneralResponse<int>> DeleteStaff([FromBody] DeleteStaffCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<PagedResult<StaffModel>>> GetStaffs([FromQuery] StaffListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }


        [HttpGet("{id}")]
        public async Task<GeneralResponse<StaffModel>> GetStaffById(int id)
        {
            var query = new StaffByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<StaffSelectListModel>>> GetRoleSelectList()
        {
            var result = await Mediator.Send(new StaffSelectListQuery());
            return Ok(result);
        }
    }
}
