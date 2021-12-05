using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ;

public class SimpleRabbitMqProducer<T> : BaseRabbitMqEventProducer<T>
{
    public SimpleRabbitMqProducer(
        ConnectionFactory connectionFactory,
        RabbitMqSettings<T> settings) : base(connectionFactory, settings)
    {
    }
}
