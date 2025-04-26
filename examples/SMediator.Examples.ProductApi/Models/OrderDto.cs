namespace SMediator.Examples.ProductApi.Models
{
    public record OrderDto(int OrderId, int CustomerId, List<OrderItemDto> Items);

    public record OrderItemDto(int ProductId, int Quantity);
}