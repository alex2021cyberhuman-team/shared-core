using System;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ
{
    public abstract class BaseRabbitMqEventProducer<T> : IEventProducer<T>
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMqSettings<T> _settings;
        private IConnection? _connection;

        protected BaseRabbitMqEventProducer(
            ConnectionFactory connectionFactory,
            RabbitMqSettings<T> settings)
        {
            _connectionFactory = connectionFactory;
            _settings = settings;
        }

        public Task ProduceEventAsync(
            T message)
        {
            _connection ??= _connectionFactory.CreateConnection();
            using var channel = _connection.CreateModel();
            var memory = Bytorize(message);
            channel.BasicPublish(_settings.Exchange, _settings.RoutingKey, null,
                memory);
            return Task.CompletedTask;
        }

        protected virtual ReadOnlyMemory<byte> Bytorize(
            T message)
        {
            return message.BytorizeAsJson();
        }
    }
}
