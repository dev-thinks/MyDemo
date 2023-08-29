using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDemo.Core.Data;

namespace MyDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadFileController : ControllerBase
    {
         protected readonly ILogger<UploadFileController> _logger;

        public UploadFileController(ILogger<UploadFileController> logger)
        {

        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            var apiResult = new ApiResult<Object>();

            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var pathToSave = "UploadFiles/";
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = new FileInfo(pathToSave + fileName);
                    using (var stream = new FileStream(fullPath.FullName, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                else
                {
                    apiResult.Success = false;
                    apiResult.Message = "The file size must be greater than 0 !";
                }
                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadFile Error");
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }
    }
}