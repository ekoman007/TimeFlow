using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Category.Queries;
using TimeFlow.Application.Features.Services.Commands;
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Features.Services.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : DefaultController
    {
        public ServicesController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> CategoryService([FromBody] CreateServiceCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPost("update")]
        public async Task<GeneralResponse<int>> ServiceUpdate([FromBody] UpdateServiceCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<ServiceModel>> GetServiceyById(int id)
        {
            var query = new ServiceByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPost("activete")]
        public async Task<GeneralResponse<int>> ServiceActive([FromBody] DeleteServiceCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<PagedResult<ServiceModel>>> GetServices([FromQuery] ServiceListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<ServiceSelectListModel>>> GetServiceSelectList()
        {
            var result = await Mediator.Send(new ServiceSelectListQuery());
            return Ok(result);
        }
    }
}