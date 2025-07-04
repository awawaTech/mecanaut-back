namespace AwawaTech.Mecanaut.API.Subscription.Domain.Model.ValueObjects;

public record PlanLimits
{
    public int MaxUsers { get; private set; }
    public int MaxMachines { get; private set; }
    public int MaxPlants { get; private set; }
    public int MaxProductionLines { get; private set; }

    public PlanLimits(int maxUsers, int maxMachines, int maxPlants, int maxProductionLines)
    {
        if (maxUsers <= 0) throw new ArgumentException("MaxUsers must be greater than 0");
        if (maxMachines <= 0) throw new ArgumentException("MaxMachines must be greater than 0");
        if (maxPlants <= 0) throw new ArgumentException("MaxPlants must be greater than 0");
        if (maxProductionLines <= 0) throw new ArgumentException("MaxProductionLines must be greater than 0");

        MaxUsers = maxUsers;
        MaxMachines = maxMachines;
        MaxPlants = maxPlants;
        MaxProductionLines = maxProductionLines;
    }
}
