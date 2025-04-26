using SMediator.Core.Abstractions;

namespace SMediator.Tests.Models
{
    public class PingRequestHandler : IRequestHandler<PingRequest, string>
    {
        public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
            => Task.FromResult($"Pong: {request.Message}");
    }
}