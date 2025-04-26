using Microsoft.Extensions.DependencyInjection;
using SMediator.Core;
using SMediator.Core.Abstractions;
using SMediator.Tests.Models;

namespace SMediator.Tests
{
    public class MediatorTests
    {
        private readonly IMediator _mediator;

        public MediatorTests()
        {
            var services = new ServiceCollection();

            services.AddSMediator(false)
                .AddSingleton(typeof(PingRequestHandler))
                .AddSingleton(typeof(MyNotificationHandler))
                .AddSingleton<IRequestHandler<PingRequest, string>, PingRequestHandler>()
                .AddSingleton<INotificationHandler<MyNotification>, MyNotificationHandler>();

            var provider = services.BuildServiceProvider();
            _mediator = provider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Send_PingRequest_ReturnsPong()
        {
            // Arrange
            var request = new PingRequest("Hello");

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.Equal("Pong: Hello", response);
        }

        [Fact]
        public async Task Publish_MyNotification_WritesToConsole()
        {
            // Arrange
            var originalOut = Console.Out;
            var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                await _mediator.Publish(new MyNotification("TestNotify"));

                // Assert
                var output = writer.ToString().Trim();
                Assert.Equal("Received notification: TestNotify", output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}