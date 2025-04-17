using MediatR; 
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Features.BussinesProfile.Commands;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.BussinesProfile.Queries;
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BussinesProfilesController : DefaultController
    {
        public BussinesProfilesController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> BussinesProfileCreate([FromBody] CreateBussinesProfileCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPost("update")]
        public async Task<GeneralResponse<int>> BussinesProfileUpdate([FromBody] UpdateBussinesProfileCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<BussinesProfileModel>> GetBussinesProfileById(int id)
        {
            var query = new BusinessProfileGetByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPost("activete")]
        public async Task<GeneralResponse<int>> BussinesProfileActive([FromBody] DeleteBussinesProfileCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<PagedResult<BussinesProfileModel>>> GetCategories([FromQuery] BusinessProfileListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<BussinesProfileSelectListModel>>> GetCategorySelectList()
        {
            var result = await Mediator.Send(new BusinessProfileSelectListQuery());
            return Ok(result);
        }
    }
}