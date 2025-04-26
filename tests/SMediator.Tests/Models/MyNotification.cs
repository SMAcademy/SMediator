using SMediator.Core.Abstractions;

namespace SMediator.Tests.Models
{
    public record MyNotification(string Text) : INotification;
}