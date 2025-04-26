using Microsoft.Extensions.DependencyInjection;
using SMediator.Core.Abstractions;
using System.Reflection;

namespace SMediator.Core
{
    public static class SMediatorExtensions
    {
        public static IServiceCollection AddSMediator(this IServiceCollection services, params Assembly[] assembliesToScan)
        {
            // 1. Register the Mediator itself
            services.AddTransient<IMediator, SMediator>();

            // 2. Register the Request and Notification handlers in the provided assemblies
            foreach (var asm in assembliesToScan)
            {
                // find IRequestHandler<,>
                var requestHandlers = asm.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                    .Where(x => x.Interface.IsGenericType
                                && x.Interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                foreach (var rh in requestHandlers)
                    services.AddTransient(rh.Interface, rh.Type);

                // find INotificationHandler<>
                var notifHandlers = asm.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                    .Where(x => x.Interface.IsGenericType
                                && x.Interface.GetGenericTypeDefinition() == typeof(INotificationHandler<>));

                foreach (var nh in notifHandlers)
                    services.AddTransient(nh.Interface, nh.Type);
            }

            return services;
        }

        public static IServiceCollection AddSMediator(this IServiceCollection services, bool scanAllAssemblies)
        {
            // 1. Register the Mediator itself
            services.AddTransient<IMediator, SMediator>();

            if (!scanAllAssemblies)
            {
                return services;
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var asm in assemblies)
            {
                // find IRequestHandler<,>
                var requestHandlers = asm.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                    .Where(x => x.Interface.IsGenericType
                                && x.Interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                foreach (var rh in requestHandlers)
                {
                    services.AddTransient(rh.Interface, rh.Type);
                }

                // find INotificationHandler<>
                var notifHandlers = asm.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                    .Where(x => x.Interface.IsGenericType
                                && x.Interface.GetGenericTypeDefinition() == typeof(INotificationHandler<>));

                foreach (var nh in notifHandlers)
                {
                    services.AddTransient(nh.Interface, nh.Type);
                }
            }

            return services;
        }
    }
}