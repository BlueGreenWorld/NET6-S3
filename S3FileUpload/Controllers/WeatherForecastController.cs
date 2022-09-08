using AwsS3.Models;
using AwsS3.Services;
using Microsoft.AspNetCore.Mvc;

namespace S3FileUpload.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _config;
    private readonly IStorageService _storageService;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IConfiguration config,
        IStorageService storageService)
    {
        _logger = logger;
        _storageService = storageService;
        _config = config;
    }

    [HttpPost(Name = "UploadFile")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        // Process the file
        await using var memoryStr = new MemoryStream();
        await file.CopyToAsync(memoryStr);

        var fileExt = Path.GetExtension(file.FileName);
        var objName = $"{Guid.NewGuid()}.{fileExt}";

        var s3Obj = new S3Object() {
            BucketName = "demo-youtube-ml-1",
            InputStream = memoryStr,
            Name = objName
        };

        var cred = new AwsCredentials()
        {
            AwsKey = _config["AwsConfiguration:AWSAccessKey"],
            AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
        };

        var result = await _storageService.UploadFileAsync(s3Obj, cred);

        return Ok(result);
    }
}
