namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

public record PlanFeatures
{
    public bool HasPredictiveMaintenance { get; private set; }
    public bool HasPrioritySupport { get; private set; }
    public bool HasAdvancedAnalytics { get; private set; }
    public bool HasCustomReports { get; private set; }

    public PlanFeatures(
        bool hasPredictiveMaintenance,
        bool hasPrioritySupport,
        bool hasAdvancedAnalytics,
        bool hasCustomReports)
    {
        HasPredictiveMaintenance = hasPredictiveMaintenance;
        HasPrioritySupport = hasPrioritySupport;
        HasAdvancedAnalytics = hasAdvancedAnalytics;
        HasCustomReports = hasCustomReports;
    }
}
