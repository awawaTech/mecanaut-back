using System.Threading;

namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

public static class TenantContext
{
    private static readonly AsyncLocal<long?> _currentTenantId = new();

    public static long CurrentTenantId => _currentTenantId.Value ?? 1L;

    public static void SetTenantId(long? tenantId) => _currentTenantId.Value = tenantId ?? 1L;

    public static bool HasTenant => _currentTenantId.Value.HasValue;

    public static void Clear() => _currentTenantId.Value = null;
}