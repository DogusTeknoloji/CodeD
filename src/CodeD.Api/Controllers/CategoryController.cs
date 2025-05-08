using CodeD.Api.Dto.Categories;
using CodeD.Api.Dto.Products;
using CodeD.Application.Commands.Categories;
using CodeD.Application.Queries;
using CodeD.Application.Queries.Product;
using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;
using CodeD.Domain.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeD.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(ILogger<CategoryController> _logger)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<CategoryListResponse>> GetAsync(int pageIndex, int pageSize, [FromServices] IMediator mediator)
        {
            var req = new CategoryListQuery(new PagableRequest(pageIndex, pageSize));

            var res = await mediator.Send(req);

            return res.Value;
        }

        [HttpGet("{id}")]
        public Task<Post?> GetByIdAsync(int id)
        {
            _logger.LogDebug($"{nameof(CategoryController)}.{nameof(GetByIdAsync)} is started.");
            //return _productService.GetProductByIdAsync(id);
            throw new NotImplementedException();
        }

        [HttpPost()]
        [ProducesResponseType<CreateCategoryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateCategoryRequest category,
            [FromServices] IMediator mediator)
        {
            var command = new CreateCategoryCommand
            {
                Key = category.Key,
                Title = category.Title,
                SourceProviderKey = category.SourceProviderKey ?? string.Empty,
                SourceItemId = category.SourceItemId ?? string.Empty,
                SourceVersion = category.SourceVersion
            };

            var categoryResult = await mediator.Send(command);

            if (categoryResult.IsSuccess)
            {
                var c = categoryResult.Value!;
                return Ok(new CreateCategoryResponse(c.Id.Value, c.Key.Value, c.Title.Value, c.ExternalReference.ProviderKey, c.ExternalReference.ItemId, c.ExternalReference.Version));
            }

            return BadRequest(categoryResult);
        }

        [HttpPut("{idOrKey}")] // PUT /category/30FFD9E8-C9F8-4149-8C02-CED70A7AC684 or PUT /category/A
        [HttpPut()]
        public async Task<IActionResult> PutAsync(
            [FromRoute] string? idOrKey,
            [FromBody] UpdateCategoryRequest categoryRequest,
            [FromServices] IMediator mediator)
        {
            var command = new UpdateCategoryCommand
            {
                Id = categoryRequest.Id,
                Key = categoryRequest.Key,
                Title = categoryRequest.Title,
                SourceProviderKey = categoryRequest.SourceProviderKey ?? string.Empty,
                SourceItemId = categoryRequest.SourceItemId ?? string.Empty,
                SourceVersion = categoryRequest.SourceVersion
            };

            if (Guid.TryParse(idOrKey, out Guid id))
            {
                command.Id = id;
            }
            else if (!string.IsNullOrEmpty(idOrKey))
            {
                command.Id = null;
                command.Key = idOrKey;
            }

            var categoryResult = await mediator.Send(command);

            if (categoryResult is { IsSuccess: false, Value: not null } && (categoryResult.Value.Id.Value == Guid.Empty || categoryResult.Value.Id.Value == Guid.Parse("")))
            {
                return BadRequest(categoryResult);
            }

            if (categoryResult is { IsSuccess: true, Value: not null })
            {
                return Ok(new CreateCategoryResponse(categoryResult.Value.Id.Value,
                                                     categoryResult.Value.Key.Value,
                                                     categoryResult.Value.Title.Value,
                                                     categoryResult.Value.ExternalReference.ProviderKey,
                                                     categoryResult.Value.ExternalReference.ItemId,
                                                     categoryResult.Value.ExternalReference.Version));
            }

            return BadRequest(categoryResult);
        }
    }
}