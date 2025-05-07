using MediatR; 
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Features.Industry.Commands;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Features.Industry.Queris;
using TimeFlow.Application.Features.User.Command;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : DefaultController
    {
        public IndustryController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> IndustryCreate([FromBody] CreateIndustryCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPut("update")]
        public async Task<GeneralResponse<int>> IndustryUpdate([FromBody] UpdateIndustryCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<IndustryModel>> GetIndustryById(int id)
        {
            var query = new IndustryByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPut("activete")]
        public async Task<GeneralResponse<int>> IndustryActive([FromBody] DeleteIndustryCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<PagedResult<IndustryModel>>> GetIndustry([FromQuery] IndustryListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<IndustrySelectListModel>>> GetIndustrySelectList()
        {
            var result = await Mediator.Send(new IndustrySelectListQuery());
            return Ok(result);
        }
    }
}