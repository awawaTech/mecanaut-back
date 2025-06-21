using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Commands;
using System.Threading.Tasks;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Services;

public interface IMetricDefinitionCommandService
{
    Task Handle(SeedMetricsCommand command);
} 