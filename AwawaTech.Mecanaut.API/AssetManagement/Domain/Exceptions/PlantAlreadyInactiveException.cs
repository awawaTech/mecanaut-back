namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class PlantAlreadyInactiveException : InvalidOperationException
{
    public PlantAlreadyInactiveException(string? message = null) : base(message) { }
} 