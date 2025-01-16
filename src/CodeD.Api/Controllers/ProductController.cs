using CodeD.Api.Dto.Products;
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
    public class PostController(ILogger<WeatherForecastController> _logger)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ProductListResponse>> GetAsync(int pageIndex, int pageSize, [FromServices] IMediator mediator)
        {
            var req = new ProductListQuery(new PagableRequest(pageIndex, pageSize));

            var res = await mediator.Send(req);

            return res.Value;
        }

        [HttpGet("{id}")]
        public Task<Post?> GetByIdAsync(int id)
        {
            _logger.LogDebug($"{nameof(PostController)}.{nameof(GetByIdAsync)} is started.");
            //return _productService.GetProductByIdAsync(id);
            throw new NotImplementedException();
        }

        [HttpPost()]
        public Task<CreateProductResponse?> PostAsync([FromBody] CreateProductRequest product, [FromServices] IUnitOfWork unitOfWork)
        {
            //var p = new Product(0, product.Key, product.Name, product.Description, product.Price, product.Stock, product.Image, product.CategoryId);

            //_productService.AddProduct(p);

            //await unitOfWork.CommitAsync();

            //return new CreateProductResponse(
            //    Id: p.Id,
            //    Key: p.Key,
            //    Name: p.Name,
            //    Description: p.Description,
            //    Price: p.Price,
            //    Stock: p.Stock,
            //    Image: p.Image,
            //    CategoryId: p.CategoryId
            //);
            throw new NotImplementedException();
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