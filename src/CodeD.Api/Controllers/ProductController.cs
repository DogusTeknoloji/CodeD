using CodeD.Api.Dto.Categories;
using CodeD.Api.Dto.Products;
using CodeD.Application.Commands.Categories;
using CodeD.Application.Queries;
using CodeD.Application.Queries.Product;
using CodeD.Domain.Abstractions;
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest category, [FromServices] IMediator mediator)
        {
            var command = new CreateCategoryCommand
            {
                Key = category.Key,
                Title = category.Title,
                SourceProviderKey = category.SourceProviderKey ?? string.Empty,
                SourceItemId = category.SourceItemId ?? string.Empty,
                SourceVersion = category.SourceVersion
            };

            var categoryIdResult = await mediator.Send(command);

            if (categoryIdResult.IsSuccess)
            {
                return Ok(new CreateCategoryResponse(categoryIdResult.Value, category.Key, category.Title, category.SourceProviderKey, category.SourceItemId, category.SourceVersion));
            }

            return BadRequest(categoryIdResult);
        }

        [HttpPut("{id}")] // PUT /product/1
        public Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateProductRequest product, [FromServices] IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
            //var p = await _productService.GetProductByIdAsync(id);

            //if (p == null)
            //{
            //    return NotFound();
            //}

            //p.UpdateKey(product.Key);
            //p.UpdateName(product.Name);
            //p.UpdateDescription(product.Description);

            ////_productService.

            //await unitOfWork.CommitAsync();

            //return Json(new CreateProductResponse(
            //    Id: p.Id,
            //    Key: p.Key,
            //    Name: p.Name,
            //    Description: p.Description,
            //    Price: p.Price,
            //    Stock: p.Stock,
            //    Image: p.Image,
            //    CategoryId: p.CategoryId
            //));
        }
    }
}