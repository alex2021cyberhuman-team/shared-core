using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public abstract class BaseRabbitMqEventConsumer<T> : IHostedService, IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqSettings<T> _settings;
    private IModel? _channel;
    private IConnection? _connection;
    private AsyncEventingBasicConsumer? _consumer;

    protected BaseRabbitMqEventConsumer(
        ConnectionFactory connectionFactory,
        RabbitMqSettings<T> settings)
    {
        _connectionFactory = connectionFactory;
        _settings = settings;
    }

    public void Dispose()
    {
        if (_consumer != null)
        {
            _consumer.Received -= CallConsumptionAsync;
            _consumer = null;
        }

        _connection?.Dispose();
        _channel?.Dispose();
        GC.SuppressFinalize(this);
    }

    public virtual Task StartAsync(
        CancellationToken cancellationToken)
    {
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _settings.Initialize(_channel);
        _consumer = new(_channel);
        _consumer.Received += CallConsumptionAsync;
        return Task.CompletedTask;
    }


    public virtual Task StopAsync(
        CancellationToken cancellationToken)
    {
        if (_consumer != null)
        {
            _consumer.Received -= CallConsumptionAsync;
            _consumer = null;
        }

        return Task.CompletedTask;
    }

    protected virtual async Task CallConsumptionAsync(
        object sender,
        BasicDeliverEventArgs basicDeliverEvent)
    {
        var body = basicDeliverEvent.Body;
        var message = body.DeBytorizeAsRequiredJson<T>();
        await ConsumeAsync(message);
    }

    protected virtual Task ConsumeAsync(
        T message)
    {
        return Task.CompletedTask;
    }
}
