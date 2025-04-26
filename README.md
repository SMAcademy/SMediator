# SMediator



A lightweight, zero-dependency **Mediator** implementation for .NET 8+ that follows the [Mediator pattern](https://en.wikipedia.org/wiki/Mediator_pattern). SMediator makes it easy to send requests (commands/queries) and publish notifications in your application via a simple API and dependency injection.

---

## Features

- **Zero dependencies** beyond .NET 8+ and `Microsoft.Extensions.DependencyInjection`.
- **Requests & Responses** (commands & queries) with `IRequest<TResponse>`.
- **Notifications** (fire-and-forget) with `INotification`.
- **Automatic handler discovery** via assembly scanning.
- **Simple DI integration** with `IServiceCollection.AddSMediator()` extension.
- **Extensible**: add pipeline behaviors, scoped/singleton lifetimes, caching, etc.

---

## Installation

Install via NuGet (not ready yet):

```bash
dotnet add package SMediator
```

Your project will reference **SMediator** and bring in the required abstractions.

---

## Quick Start

1. **Register SMediator** in `Program.cs`:

   ```csharp
   using SMediator;
   using Microsoft.Extensions.DependencyInjection;

   var builder = WebApplication.CreateBuilder(args);

   // Add SMediator and scan this assembly for handlers
   builder.Services.AddSMediator(typeof(Program).Assembly);

   // ... add controllers, Swagger, etc.
   ```

2. **Define a request and handler**:

   ```csharp
   // IRequest<T>
   public record GetProductsQuery() : IRequest<List<ProductDto>>;

   public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
   {
       public Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct)
       {
           // return list of products
       }
   }
   ```

3. **Define a notification and handler** (optional):

   ```csharp
   public record OrderCreatedNotification(int OrderId, int CustomerId) : INotification;

   public class OrderCreatedNotificationHandler : INotificationHandler<OrderCreatedNotification>
   {
       public Task Handle(OrderCreatedNotification notification, CancellationToken ct)
       {
           Console.WriteLine($"Order {notification.OrderId} created.");
           return Task.CompletedTask;
       }
   }
   ```

4. **Send requests / publish notifications** anywhere DI is available:

   ```csharp
   public class MyService
   {
       private readonly IMediator _mediator;
       public MyService(IMediator mediator) => _mediator = mediator;

       public async Task PlaceOrderAsync()
       {
           int newOrderId = await _mediator.Send(new CreateOrderCommand(customerId, items));
           await _mediator.Publish(new OrderCreatedNotification(newOrderId, customerId));
       }
   }
   ```

---

## Examples

A concrete **ASP.NET Core** sample lives in the [`Examples`](./examples) folder:

- `ProductsController` (GET `/api/products`)
- `OrdersController` (POST `/api/orders`)
- `requests.http` for VS Code REST Client

Refer to that folder for a full end-to-end demo.

---

## Extension Points

- **Pipeline behaviors**: intercept requests (logging, validation, caching).
- **Lifetime management**: register handlers as scoped/singleton.
- **Custom conventions**: filter which assemblies/types to scan.

### Custom `AddSMediator` overload

```csharp
public static IServiceCollection AddSMediator(
    this IServiceCollection services,
    params Assembly[] assembliesToScan);
```

Pass specific assemblies for performance.

---

## Contributing

1. Fork the repo
2. Create your feature branch (`git checkout -b feature/foo`)
3. Commit your changes (`git commit -am 'Add foo'`)
4. Push to the branch (`git push origin feature/foo`)
5. Open a Pull Request

Please ensure all new code is covered by unit tests.

---

## License

This project is licensed under the [MIT License](LICENSE).

