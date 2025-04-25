using SMediator.Core.Abstractions;

namespace SMediator.Tests
{
    public record MyNotification(string Text) : INotification;
}