using SMediator.Core.Abstractions;

namespace SMediator.Examples.ProductApi.Notifications
{
    public record OrderCreatedNotification(int OrderId, int CustomerId) : INotification;
}