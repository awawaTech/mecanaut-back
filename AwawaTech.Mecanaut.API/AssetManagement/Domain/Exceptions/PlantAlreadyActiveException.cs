using System;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class PlantAlreadyActiveException : InvalidOperationException
{
    public PlantAlreadyActiveException(string? message = null) : base(message) { }
} 