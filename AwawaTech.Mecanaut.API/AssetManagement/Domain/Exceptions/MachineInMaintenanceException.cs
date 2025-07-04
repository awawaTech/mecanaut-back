namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class MachineInMaintenanceException : InvalidOperationException
{
    public MachineInMaintenanceException(string? message=null) : base(message) {}
} 