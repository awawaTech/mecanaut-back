using System;
using System.Threading.Tasks;
namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

public class TenantContextHelper
{
    public long GetCurrentTenantId() => TenantContext.CurrentTenantId;
    public bool HasValidTenantContext() => TenantContext.HasTenant;

    public T ExecuteInTenantContext<T>(long tenantId, Func<T> operation)
    {
        var previous = TenantContext.CurrentTenantId;
        try
        {
            TenantContext.SetTenantId(tenantId);
            return operation();
        }
        finally
        {
            TenantContext.SetTenantId(previous);
        }
    }

    public async Task<TResult> ExecuteInTenantContextAsync<TResult>(long tenantId, Func<Task<TResult>> operation)
    {
        var previous = TenantContext.CurrentTenantId;
        try
        {
            TenantContext.SetTenantId(tenantId);
            return await operation().ConfigureAwait(false);
        }
        finally
        {
            TenantContext.SetTenantId(previous);
        }
    }

    public async Task ExecuteInTenantContextAsync(long tenantId, Func<Task> operation)
    {
        var previous = TenantContext.CurrentTenantId;
        try
        {
            TenantContext.SetTenantId(tenantId);
            await operation().ConfigureAwait(false);
        }
        finally
        {
            TenantContext.SetTenantId(previous);
        }
    }
}