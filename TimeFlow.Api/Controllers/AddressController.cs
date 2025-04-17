using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Features.Address.Commands;
using TimeFlow.Application.Features.Address.DTOs;
using TimeFlow.Application.Features.Address.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;  

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : DefaultController
    {
        public AddressController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> AddressCreate([FromBody] CreateAddressCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPost("update")]
        public async Task<GeneralResponse<int>> AddressUpdate([FromBody] UpdateAddressCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<AddressModel>> GetAddressById(int id)
        {
            var query = new AddresByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPost("activete")]
        public async Task<GeneralResponse<int>> AddressActive([FromBody] DeleteAddressCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<PagedResult<AddressModel>>> GetAddress([FromQuery] AddresListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<AddresSelectListModal>>> GetAddressSelectList()
        {
            var result = await Mediator.Send(new AddresSelectListQuery());
            return Ok(result);
        }
    }
}