namespace AwawaTech.Mecanaut.API.Shared.Domain.Services;

public interface IImageStorageService
{
    Task<string> UploadImageAsync(IFormFile file);
}