using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public class RabbitMqSettings<T> : IRabbitMqSettings
{
    public RabbitMqSettings()
    {
        var lowerName = typeof(T).Name.ToLower();
        Exchange = $"exchange-{lowerName}";
        Queue = $"queue-{lowerName}";
        RoutingKey = $"routing-{lowerName}";
        ExchangeType = "direct";
        Durable = true;
        Exclusive = false;
        AutoDelete = false;
    }

    public string Exchange { get; set; }

    public string Queue { get; set; }

    public string RoutingKey { get; set; }

    public string ExchangeType { get; set; }

    public bool Durable { get; set; }

    public bool Exclusive { get; set; }

    public bool AutoDelete { get; set; }

    public void Initialize(
        IModel channel)
    {
        channel.ExchangeDeclare(Exchange, ExchangeType, Durable, AutoDelete,
            null);
        _ = channel.QueueDeclare(Queue, Durable, Exclusive, AutoDelete, null);
        channel.QueueBind(Queue, Exchange, RoutingKey, null);
    }
}
