using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Infrastructure.Persistence.EFC.Repositories;

public class MetricDefinitionRepository : BaseRepository<MetricDefinition>, IMetricDefinitionRepository
{
    public MetricDefinitionRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsByNameAsync(string name)
        => await Context.Set<MetricDefinition>().AnyAsync(md => md.Name == name);
} 