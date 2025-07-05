namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Interfaces.REST
{
    public class ErrorResource
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ErrorResource(string message)
        {
            Success = false;
            Message = message;
        }
    }
} 