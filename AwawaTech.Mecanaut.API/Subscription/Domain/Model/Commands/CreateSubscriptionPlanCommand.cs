using AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.Commands;

public record CreateSubscriptionPlanCommand
{
    public string Name;
    public string Description ;
    public decimal Price ;
    public string Currency ;
    public int MaxMachines ;
    public int MaxUsers ;
    public bool SupportPriority ;
    public bool PredictiveMaintenance ;
    public bool AdvancedAnalytics ;
}
