using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;

public class ProductionLineCommandService : IProductionLineCommandService
{
    private readonly IProductionLineRepository lineRepo;
    private readonly IPlantRepository plantRepo;
    private readonly IUnitOfWork uow;
    private readonly TenantContextHelper tenantHelper;

    public ProductionLineCommandService(IProductionLineRepository lr, IPlantRepository pr, IUnitOfWork u, TenantContextHelper th)
    {
        lineRepo = lr; plantRepo = pr; uow = u; tenantHelper = th;
    }

    public async Task<ProductionLine> Handle(CreateProductionLineCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        // ensure plant exists
        var plant = await plantRepo.FindByIdAndTenantAsync(command.PlantId, tenantId)
                    ?? throw new KeyNotFoundException("Plant not found");
        if (!plant.IsActive()) throw new InvalidOperationException("Plant inactive");
        if (!plant.CanAddProductionLine()) throw new InvalidOperationException("Plant cannot add production line");
        if (await lineRepo.ExistsByCodeAsync(command.Code, command.PlantId, tenantId))
            throw new InvalidOperationException("Code already exists");

        var line = ProductionLine.Create(command.Name, command.Code, command.Capacity, command.PlantId, new TenantId(tenantId));
        await lineRepo.AddAsync(line);
        await uow.CompleteAsync();
        return line;
    }

    public async Task<ProductionLine> Handle(StartProductionCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var line = await lineRepo.FindByIdAndTenantAsync(command.ProductionLineId, tenantId)
                   ?? throw new KeyNotFoundException("Line not found");
        line.StartProduction();
        await uow.CompleteAsync();
        return line;
    }

    public async Task<ProductionLine> Handle(StopProductionCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();
        var line = await lineRepo.FindByIdAndTenantAsync(command.ProductionLineId, tenantId)
                   ?? throw new KeyNotFoundException("Line not found");
        line.StopProduction(command.Reason);
        await uow.CompleteAsync();
        return line;
    }
} 