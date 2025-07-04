namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Application.Internal.OutboundServices;

/// <summary>
/// Anti-corruption layer hacia el BC AssetManagement para verificar existencia de máquinas.
/// </summary>
public interface IMachineCatalogAcl
{
    bool MachineExists(long machineId, long tenantId);
} 