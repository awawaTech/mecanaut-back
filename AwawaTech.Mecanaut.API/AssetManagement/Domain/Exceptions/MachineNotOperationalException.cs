namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Exceptions;

public class MachineNotOperationalException : InvalidOperationException
{
    public MachineNotOperationalException(string? message=null) : base(message) {}
} 