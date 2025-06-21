using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Infrastructure.OutboundServices;

/// <summary>
/// ACL que utiliza el repositorio de AssetManagement dentro de la misma solución.
/// </summary>
public class MachineCatalogAcl : IMachineCatalogAcl
{
    private readonly IMachineRepository machineRepository;

    public MachineCatalogAcl(IMachineRepository machineRepository)
    {
        this.machineRepository = machineRepository;
    }

    public bool MachineExists(long machineId, long tenantId)
    {
        var machine = machineRepository.FindByIdAsync(machineId).Result;
        return machine != null && machine.TenantId.Value == tenantId;
    }
} 