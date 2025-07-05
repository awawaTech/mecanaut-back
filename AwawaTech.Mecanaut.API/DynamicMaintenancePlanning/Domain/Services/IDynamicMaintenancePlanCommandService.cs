using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Aggregates;

namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Services
{
    public interface IDynamicMaintenancePlanCommandService
    {
        Task<DynamicMaintenancePlan> CreateAsync(CreateDynamicMaintenancePlanCommand command);
    }
} 