using SMediator.Core.Abstractions;
using SMediator.Examples.ProductApi.Notifications;

namespace SMediator.Examples.ProductApi.Handlers
{
    public class OrderCreatedNotificationHandler : INotificationHandler<OrderCreatedNotification>
    {
        public Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Notification] Order {notification.OrderId} created for customer {notification.CustomerId}.");
            return Task.CompletedTask;
        }
    }
}