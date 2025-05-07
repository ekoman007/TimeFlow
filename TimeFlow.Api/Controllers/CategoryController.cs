using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Category.Queries;
using TimeFlow.Application.Features.Industry.Commands;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Features.Industry.Queris;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : DefaultController
    {
        public CategoryController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> CategoryCreate([FromBody] CreateCategoryCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpPut("update")]
        public async Task<GeneralResponse<int>> CategoryUpdate([FromBody] UpdateCategoryCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<GeneralResponse<CategoryModel>> GetCategoryById(int id)
        {
            var query = new CategoryGetByIdQuery(id);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpPut("activete")]
        public async Task<GeneralResponse<int>> CategoryActive([FromBody] DeleteCategoryCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<GeneralResponse<PagedResult<CategoryModel>>> GetCategories([FromQuery] CategoryListQuery query)
        {
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        [HttpGet("select-list")]
        public async Task<ActionResult<List<CategorySelectListModel>>> GetCategorySelectList()
        {
            var result = await Mediator.Send(new CategorySelectListQuery());
            return Ok(result);
        }
    }
}