using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AwawaTech.Mecanaut.API.Subscription.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Subscription.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace AwawaTech.Mecanaut.API.Subscription.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionPlanRepository : BaseRepository<SubscriptionPlan>, ISubscriptionPlanRepository
{
    private readonly AppDbContext _context;

    public SubscriptionPlanRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SubscriptionPlan> GetByIdAsync(long id)
        => await _context.Set<SubscriptionPlan>().FindAsync(id);

    public async Task<IEnumerable<SubscriptionPlan>> GetAllAsync()
        => await _context.Set<SubscriptionPlan>().ToListAsync();

    public async Task AddAsync(SubscriptionPlan plan)
    {
        await _context.Set<SubscriptionPlan>().AddAsync(plan);
        await _context.SaveChangesAsync();  // Guarda los cambios después de agregar
    }

    public async Task UpdateAsync(SubscriptionPlan plan)
    {
        _context.Entry(plan).State = EntityState.Modified;
        await _context.SaveChangesAsync();  // Guarda los cambios después de actualizar
    }

    public async Task DeleteAsync(long id)
    {
        var plan = await GetByIdAsync(id);
        if (plan != null)
        {
            _context.Set<SubscriptionPlan>().Remove(plan);
            await _context.SaveChangesAsync();  // Guarda los cambios después de eliminar
        }
    }
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Set<SubscriptionPlan>().AnyAsync(sp => sp.Name == name);
    }
}
