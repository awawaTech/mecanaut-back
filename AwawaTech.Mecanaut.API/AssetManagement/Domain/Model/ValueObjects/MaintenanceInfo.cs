namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public sealed class MaintenanceInfo
{
    public DateTime? LastMaintenance { get; private set; }
    public DateTime? NextMaintenance { get; private set; }

    private MaintenanceInfo(DateTime? last, DateTime? next)
    {
        LastMaintenance = last;
        NextMaintenance = next;
    }

    public static MaintenanceInfo CreateNew() => new(null, DateTime.UtcNow.AddMonths(6));

    public MaintenanceInfo WithLastMaintenanceDate(DateTime date)
    {
        // schedule next six months later
        return new MaintenanceInfo(date, date.AddMonths(6));
    }

    public MaintenanceInfo WithNextMaintenanceDate()
    {
        return new MaintenanceInfo(LastMaintenance, DateTime.UtcNow.AddMonths(6));
    }

    public bool IsMaintenanceDue() => NextMaintenance.HasValue && DateTime.UtcNow >= NextMaintenance.Value;

    public override string ToString() => $"Last: {LastMaintenance?.ToShortDateString() ?? "-"}, Next: {NextMaintenance?.ToShortDateString() ?? "-"}";

    protected MaintenanceInfo() { }
} 