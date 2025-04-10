using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Features.Login.Commands;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : DefaultController
    {
        public LoginController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("login")]
        public async Task<GeneralResponse<string>> PostCreate([FromBody] LoginCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
              

             
        }
    }

}
