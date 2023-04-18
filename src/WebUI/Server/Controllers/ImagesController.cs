using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Auction.WebUI.Server.Controllers;

[Route("api/[controller]")]
public class ImagesController : ApiControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(IWebHostEnvironment env, ILogger<ImagesController> logger)
    {
        _env = env;
        _logger = logger;
    }

    //[HttpPost]
    //public async Task<ActionResult<IList<UploadResult>>> PostFile([FromForm] IEnumerable<IFormFile> files)
    //{
    //    var maxAllowedFiles = 10;
    //    long maxFileSize = 1024 * 15;
    //    var filesProcessed = 0;
    //    var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");

    //    List<UploadResult> uploadResults = new();

    //    foreach (var file in files)
    //    {
    //        var uploadResult = new UploadResult();
    //        string secureFileName;
    //        var unsecureFileName = file.FileName;

    //        uploadResult.FileName = unsecureFileName;

    //        var displayedFileName = WebUtility.HtmlEncode(unsecureFileName);

    //        if (filesProcessed < maxAllowedFiles)
    //        {
    //            if (file.Length == 0 )
    //            {
    //                _logger.LogInformation($"{displayedFileName} length is 0 (Err: 1)");
    //                uploadResult.ErrorCode = 1;
    //            }
    //            else if (file.Length > maxFileSize)
    //            {
    //                _logger.LogInformation($"{displayedFileName} of {file.Length} bytes is" +
    //                                       $"larger than the limit of {maxFileSize} bytes (Err: 2)");
    //                uploadResult.ErrorCode = 2;
    //            }
    //            else
    //            {
    //                try
    //                {
    //                    secureFileName = Path.GetRandomFileName();
    //                    var path = Path.Combine(_env.ContentRootPath, _env.EnvironmentName, "unsafe_uploads", secureFileName);

    //                    await using FileStream fs = new(path, FileMode.Create);
    //                    await file.CopyToAsync(fs);

    //                    _logger.LogInformation($"{displayedFileName} saved at {path}");
    //                    uploadResult.Uploaded = true;
    //                    uploadResult.StoredFileName = secureFileName;
    //                }
    //                catch (IOException ex)
    //                {
    //                    _logger.LogError($"{displayedFileName} error on upload (Err: 3): {ex.Message}");
    //                    uploadResult.ErrorCode = 3;
    //                }
    //            }

    //            filesProcessed++;
    //        }
    //        else
    //        {
    //            _logger.LogInformation($"{displayedFileName} not uploaded because the request " + 
    //                                    "exceeded the allowed amount of files (Err: 4)");
    //            uploadResult.ErrorCode = 4;
    //        }

    //        uploadResults.Add(uploadResult);
    //    }

    //    return new CreatedResult(resourcePath, uploadResults);
    //}


}
