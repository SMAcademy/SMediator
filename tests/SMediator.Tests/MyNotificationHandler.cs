using SMediator.Core.Abstractions;

namespace SMediator.Tests
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