using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;
using SMediator.Examples.ProductApi.Commands;
using SMediator.Examples.ProductApi.Notifications;

namespace SMediator.Examples.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            await _mediator.Publish(new OrderCreatedNotification(orderId, command.CustomerId));
            return CreatedAtAction(nameof(Create), new { id = orderId }, orderId);
        }
    }
}