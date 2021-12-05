using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public class RabbitMqSettings<T> : IRabbitMqSettings
{
    private readonly string _lowerName;

    public RabbitMqSettings()
    {
        _lowerName = typeof(T).Name.ToLower();
    }

    public string Exchange =>
        $"exchange-{_lowerName}";

    public string Queue => $"queue-{_lowerName}";

    public string RoutingKey =>
        $"routing-{_lowerName}";

    public string ExchangeType => "direct";

    public bool Durable => true;

    public bool Exclusive => false;

    public bool AutoDelete => false;

    public void Initialize(
        IModel channel)
    {
        channel.ExchangeDeclare(Exchange, ExchangeType, Durable, AutoDelete);
        _ = channel.QueueDeclare(Queue, Durable, Exclusive, AutoDelete);
        channel.QueueBind(Queue, Exchange, RoutingKey);
    }
}
