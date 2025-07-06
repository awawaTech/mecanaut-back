namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Storage;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using AwawaTech.Mecanaut.API.Shared.Domain.Services;

public class CloudinaryService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration config)
    {
        var settings = config.GetSection("CloudinarySettings");
        var account = new Account(
            settings["CloudName"],
            settings["ApiKey"],
            settings["ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file.Length <= 0) return null;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "mecanaut" // Opcional: folder en Cloudinary
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.AbsoluteUri;
    }
}
