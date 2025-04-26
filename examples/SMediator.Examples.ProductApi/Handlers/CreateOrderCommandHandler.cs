using SMediator.Core.Abstractions;
using SMediator.Examples.ProductApi.Commands;

namespace SMediator.Examples.ProductApi.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private static int _nextOrderId = 1000;

        public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Persist order to database; here we simulate
            int orderId = Interlocked.Increment(ref _nextOrderId);
            // After saving, publish notification
            // Note: Notifications are typically published by calling mediator inside handler or via pipeline
            return Task.FromResult(orderId);
        }
    }
}