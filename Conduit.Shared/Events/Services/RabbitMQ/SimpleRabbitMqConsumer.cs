using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services.RabbitMQ
{
    public class
        SimpleRabbitMqConsumer<T, THandler> : BaseRabbitMqEventConsumer<T>
        where THandler : IEventConsumer<T>
    {
        private readonly IServiceProvider _serviceProvider;

        public SimpleRabbitMqConsumer(
            ConnectionFactory connectionFactory,
            RabbitMqSettings<T> settings,
            IServiceProvider serviceProvider) : base(connectionFactory,
            settings)
        {
            _serviceProvider = serviceProvider;
        }


        protected override async Task ConsumeAsync(
            T message)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var eventHandler =
                scope.ServiceProvider.GetRequiredService<THandler>();
            await eventHandler.ConsumeAsync(message);
        }
    }
}
