using Conduit.Shared.Events.Services.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Conduit.Shared.Events.Services;

public static class RabbitMqRegistrationExtensions
{
    public static async Task InitializeQueuesAsync(
        this IServiceScope scope)
    {
        var rabbitMqInitializer = scope.ServiceProvider
            .GetRequiredService<RabbitMqInitializer>();
        await rabbitMqInitializer.StartAsync();
    }

    public static IServiceCollection RegisterRabbitMqWithHealthCheck(
        this IServiceCollection services,
        Action<ConnectionFactory> configureFactory)
    {
        var connectionFactory = new ConnectionFactory();
        configureFactory(connectionFactory);
        return services.AddSingleton(connectionFactory)
            .AddScoped<RabbitMqInitializer>().AddHealthChecks()
            .AddRabbitMQ(_ => connectionFactory).Services;
    }

    public static IServiceCollection RegisterProducer<T>(
        this IServiceCollection services)
    {
        return services.AddTransient<IRabbitMqSettings, RabbitMqSettings<T>>()
            .AddScoped<RabbitMqSettings<T>>()
            .AddScoped<IEventProducer<T>, SimpleRabbitMqProducer<T>>();
    }

    public static IServiceCollection RegisterConsumer<T, TEventConsumer>(
        this IServiceCollection services)
        where TEventConsumer : class, IEventConsumer<T>
    {
        return services.AddTransient<IRabbitMqSettings, RabbitMqSettings<T>>()
            .AddScoped<RabbitMqSettings<T>>()
            .AddScoped<IEventConsumer<T>, TEventConsumer>()
            .AddHostedService<SimpleRabbitMqConsumer<T, TEventConsumer>>();
    }
}
