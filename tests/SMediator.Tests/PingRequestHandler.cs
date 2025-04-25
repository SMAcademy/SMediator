using SMediator.Core.Abstractions;

namespace SMediator.Tests
{
    public class PingRequestHandler : IRequestHandler<PingRequest, string>
    {
        public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
            => Task.FromResult($"Pong: {request.Message}");
    }
}