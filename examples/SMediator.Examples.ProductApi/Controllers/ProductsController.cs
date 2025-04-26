using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;
using SMediator.Examples.ProductApi.Models;
using SMediator.Examples.ProductApi.Queries;

namespace SMediator.Examples.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }
    }
}