namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class ProductionLineAlreadyRunningException : InvalidOperationException
{
    public ProductionLineAlreadyRunningException(string? message = null) : base(message) { }
} 