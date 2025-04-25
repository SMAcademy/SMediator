using Microsoft.Extensions.DependencyInjection;
using SMediator.Core.Abstractions;

namespace SMediator.Core
{
    internal class SMediator(IServiceProvider provider) : IMediator
    {
        private readonly IServiceProvider _provider = provider;

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
        {
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _provider.GetRequiredService(handlerType);

            return await handler.Handle((dynamic)request, ct);
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken ct = default)
            where TNotification : INotification
        {
            var handlerType = typeof(INotificationHandler<>)
                .MakeGenericType(notification.GetType());

            var handlers = _provider.GetServices(handlerType) ?? [];

            foreach (object? handler in handlers)
            {
                if (handler is null)
                {
                    continue;
                }
                if (handler is INotificationHandler<TNotification> genericHandler)
                {
                    await genericHandler.Handle(notification, ct);
                    continue;
                }
                await ((dynamic)handler)?.Handle((dynamic)notification, ct);
            }
        }
    }
}