using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using TimeFlow.Application.Features.Guests.Commands;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : DefaultController
    {
        public GuestController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> GuestCreate([FromBody] CreateGuestCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }
    }
}