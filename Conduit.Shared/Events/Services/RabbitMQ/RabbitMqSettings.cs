using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public class RabbitMqSettings<T> : IRabbitMqSettings
{
    private readonly string _lowerName;

    public RabbitMqSettings()
    {
        _lowerName = typeof(T).Name.ToLower();
        Exchange = $"exchange-{_lowerName}";
        Queue = $"queue-{_lowerName}";
        RoutingKey = $"routing-{_lowerName}";
        ExchangeType = "direct";
        Durable = true;
        Exclusive = false;
        AutoDelete = false;
        AutoAck = true;
    }

    public string Exchange  { get; set; }

    public string Queue { get; set; }

    public string RoutingKey  { get; set; }

    public string ExchangeType  { get; set; }

    public bool Durable { get; set; }

    public bool Exclusive { get; set; }

    public bool AutoDelete { get; set; }

    public bool AutoAck { get; set; }

    public void Initialize(
        IModel channel)
    {
        channel.ExchangeDeclare(Exchange, ExchangeType, Durable, AutoDelete);
        _ = channel.QueueDeclare(Queue, Durable, Exclusive, AutoDelete);
        channel.QueueBind(Queue, Exchange, RoutingKey);
    }
}
