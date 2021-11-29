using System;
using Conduit.Shared.Events.Services.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services
{
    public static class RabbitMqRegistrationExtensions
    {
        public static IServiceCollection RegisterRabbitMqConnection(
            this IServiceCollection services,
            Action<ConnectionFactory> configureFactory)
        {
            var connectionFactory = new ConnectionFactory();
            configureFactory(connectionFactory);
            return services.AddSingleton<ConnectionFactory>()
                .AddHostedService<RabbitMqHostedInitializer>();
        }

        public static IServiceCollection RegisterProducer<T>(
            this IServiceCollection services)
        {
            return services
                .AddTransient<IRabbitMqSettings, RabbitMqSettings<T>>()
                .AddScoped<RabbitMqSettings<T>>()
                .AddScoped<IEventProducer<T>, SimpleRabbitMqProducer<T>>();
        }

        public static IServiceCollection RegisterConsumer<T, TEventConsumer>(
            this IServiceCollection services)
            where TEventConsumer : class, IEventConsumer<T>
        {
            return services
                .AddTransient<IRabbitMqSettings, RabbitMqSettings<T>>()
                .AddScoped<RabbitMqSettings<T>>()
                .AddScoped<IEventConsumer<T>, TEventConsumer>()
                .AddHostedService<SimpleRabbitMqConsumer<T, TEventConsumer>>();
        }
    }
}
