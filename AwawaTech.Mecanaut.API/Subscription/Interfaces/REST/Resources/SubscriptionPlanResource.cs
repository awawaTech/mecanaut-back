using System;

namespace AwawaTech.Mecanaut.API.Subscription.Interfaces.REST.Resources;

public class SubscriptionPlanResource
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public int MaxMachines { get; set; }
    public int MaxUsers { get; set; }
    public bool SupportPriority { get; set; }
    public bool PredictiveMaintenance { get; set; }
    public bool AdvancedAnalytics { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

