using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using Microsoft.Extensions.Hosting;

namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.EventHandlers;

public class SeedRolesHostedService(IServiceProvider services) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = services.CreateScope();
        var commandService = scope.ServiceProvider.GetRequiredService<IRoleCommandService>();
        await commandService.Handle(new SeedRolesCommand());
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
} 