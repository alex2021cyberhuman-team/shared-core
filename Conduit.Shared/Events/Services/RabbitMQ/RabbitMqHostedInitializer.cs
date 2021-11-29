using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ
{
    public class RabbitMqHostedInitializer : IHostedService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IEnumerable<IRabbitMqSettings> _settingsEnumerable;

        public RabbitMqHostedInitializer(
            ConnectionFactory connectionFactory,
            IEnumerable<IRabbitMqSettings> settingsEnumerable)
        {
            _connectionFactory = connectionFactory;
            _settingsEnumerable = settingsEnumerable;
        }

        public Task StartAsync(
            CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            foreach (var initializer in _settingsEnumerable)
            {
                initializer.Initialize(channel);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
