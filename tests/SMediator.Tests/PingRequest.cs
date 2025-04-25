using SMediator.Core.Abstractions;

namespace SMediator.Tests
{
    public record PingRequest(string Message) : IRequest<string>;
}