namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class ProductionLineNotReadyException : InvalidOperationException
{
    public ProductionLineNotReadyException(string? message=null) : base(message) {}
} 