﻿namespace SMediator.Core.Abstractions
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);

        Task Publish<TNotification>(TNotification notification, CancellationToken ct = default)
            where TNotification : INotification;
    }
}