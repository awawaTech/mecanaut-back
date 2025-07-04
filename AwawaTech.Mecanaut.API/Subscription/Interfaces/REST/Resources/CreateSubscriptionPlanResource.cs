

namespace AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Resources;

public class CreateSubscriptionPlanResource
{
    public string Name { get; set; }


    public string Description { get; set; }


    public decimal Price { get; set; }

    public string Currency { get; set; }

    public int MaxMachines { get; set; }

    public int MaxUsers { get; set; }

    public bool SupportPriority { get; set; } = false;
    public bool PredictiveMaintenance { get; set; } = false;
    public bool AdvancedAnalytics { get; set; } = false;
}
