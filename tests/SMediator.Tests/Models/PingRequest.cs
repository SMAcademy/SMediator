using SMediator.Core.Abstractions;

namespace SMediator.Tests.Models
{
    public record PingRequest(string Message) : IRequest<string>;
}