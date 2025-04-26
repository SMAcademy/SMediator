using SMediator.Core.Abstractions;

namespace SMediator.Tests.Models
{
    public class MyNotificationHandler : INotificationHandler<MyNotification>
    {
        public Task Handle(MyNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received notification: {notification.Text}");
            return Task.CompletedTask;
        }
    }
}