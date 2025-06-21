namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class ProductionLineInMaintenanceException : InvalidOperationException
{
    public ProductionLineInMaintenanceException(string? message=null) : base(message) {}
} 