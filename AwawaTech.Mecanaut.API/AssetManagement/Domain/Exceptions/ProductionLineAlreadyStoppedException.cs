namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class ProductionLineAlreadyStoppedException : InvalidOperationException
{
    public ProductionLineAlreadyStoppedException(string? message=null) : base(message) {}
} 