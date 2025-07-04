namespace AwawaTech.Mecanaut.API.Subscription.Application.Internal.EventHandlers;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.Subscription.Domain.Services;
using AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Resources;
using AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Transform;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

public class SubscriptionPlansSeedHostedService : IHostedService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<SubscriptionPlansSeedHostedService> _logger;

    public SubscriptionPlansSeedHostedService(IServiceProvider services, ILogger<SubscriptionPlansSeedHostedService> logger)
    {
        _services = services;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var svc = scope.ServiceProvider.GetRequiredService<ISubscriptionPlanCommandService>();

        _logger.LogInformation("Seeding default subscription plans...");

        // Ejecutar el comando de seed para suscripciones
        await svc.HandleAsync(new SeedSubscriptionPlansCommand());

        _logger.LogInformation("Subscription plans seeding finished.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
