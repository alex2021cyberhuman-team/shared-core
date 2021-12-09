using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public class SimpleRabbitMqConsumer<T> : BaseRabbitMqEventConsumer<T>
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleRabbitMqConsumer(
        ConnectionFactory connectionFactory,
        RabbitMqSettings<T> settings,
        IServiceProvider serviceProvider) : base(connectionFactory, settings)
    {
        _serviceProvider = serviceProvider;
    }


    protected override async Task ConsumeAsync(
        T message)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var eventHandler = scope.ServiceProvider.GetRequiredService<IEventConsumer<T>>();
        await eventHandler.ConsumeAsync(message);
    }
}
