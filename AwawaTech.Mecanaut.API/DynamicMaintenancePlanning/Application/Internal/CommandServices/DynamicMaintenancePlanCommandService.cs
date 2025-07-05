using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Repositories;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Application.Internal.CommandServices;

public class DynamicMaintenancePlanCommandService : IDynamicMaintenancePlanCommandService
{
    private readonly IDynamicMaintenancePlanRepository planRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly TenantContextHelper tenantHelper;

    public DynamicMaintenancePlanCommandService(
        IDynamicMaintenancePlanRepository repo,
        IUnitOfWork uow,
        TenantContextHelper helper)
    {
        planRepository = repo;
        unitOfWork = uow;
        tenantHelper = helper;
    }

    public async Task<DynamicMaintenancePlan> CreateAsync(CreateDynamicMaintenancePlanCommand command)
    {
        var tenantId = tenantHelper.GetCurrentTenantId();

        // Verifica duplicados
        if (await planRepository.ExistsByNameAsync(command.Name, tenantId))
            throw new InvalidOperationException("A maintenance plan with this name already exists.");

        // 1️⃣ Crea el plan base
        var plan = DynamicMaintenancePlan.Create(
            command.Name,
            long.Parse(command.MetricId),
            command.Amount,
            long.Parse(command.ProductionLineId),
            long.Parse(command.PlantLineId),
            new TenantId(tenantId));

        await planRepository.AddAsync(plan);
        await unitOfWork.CompleteAsync(); // Persiste y genera ID

        if (plan.Id <= 0)
            throw new InvalidOperationException("The plan ID was not generated correctly.");

        // 2️⃣ Relaciona máquinas
        foreach (var machineId in command.Machines)
        {
            var machineEntity = new DynamicMaintenancePlanMachine(plan.Id, machineId);
            await planRepository.AddEntityAsync(machineEntity);
        }

        // 3️⃣ Relaciona tareas
        foreach (var taskDescription in command.Tasks)
        {
            var taskEntity = new DynamicMaintenancePlanTask(plan.Id, taskDescription);
            await planRepository.AddEntityAsync(taskEntity);
        }

        // 4️⃣ Confirma todo junto
        await unitOfWork.CompleteAsync();

        return plan;
    }
}
