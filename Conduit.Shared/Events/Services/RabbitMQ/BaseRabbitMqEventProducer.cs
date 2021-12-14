using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public abstract class BaseRabbitMqEventProducer<T> : IEventProducer<T>, IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqSettings<T> _settings;
    private IModel? _channel;
    private IConnection? _connection;

    protected BaseRabbitMqEventProducer(
        ConnectionFactory connectionFactory,
        RabbitMqSettings<T> settings)
    {
        _connectionFactory = connectionFactory;
        _settings = settings;
    }

    protected IConnection Connection =>
        _connection ??= _connectionFactory.CreateConnection();

    protected IModel Channel => _channel ??= Connection.CreateModel();

    public Task ProduceEventAsync(
        T message)
    {
        var memory = Bytorize(message);
        Channel.BasicPublish(_settings.Exchange, _settings.RoutingKey,
            body: memory);
        return Task.CompletedTask;
    }

    protected virtual ReadOnlyMemory<byte> Bytorize(
        T message)
    {
        return message.BytorizeAsJson();
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
