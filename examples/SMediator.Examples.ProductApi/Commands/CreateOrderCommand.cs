using SMediator.Core.Abstractions;
using SMediator.Examples.ProductApi.Models;

namespace SMediator.Examples.ProductApi.Commands
{
    public record CreateOrderCommand(int CustomerId, List<OrderItemDto> Items) : IRequest<int>;
}