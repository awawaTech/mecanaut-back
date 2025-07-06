using Microsoft.AspNetCore.Mvc;
using AwawaTech.Mecanaut.API.Shared.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Storage.Resources;

namespace AwawaTech.Mecanaut.API.Shared.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageStorageController : ControllerBase
{
    private readonly IImageStorageService _imageStorageService;

    public ImageStorageController(IImageStorageService imageStorageService)
    {
        _imageStorageService = imageStorageService;
    }
    
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadImageResource resource)
    {
        if (resource.File == null || resource.File.Length == 0)
            return BadRequest("No file provided.");

        var url = await _imageStorageService.UploadImageAsync(resource.File);
        return Ok(new { Url = url });
    }

}