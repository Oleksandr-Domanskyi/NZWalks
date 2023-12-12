using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.Dtos;
using NZwalks.API.Reposetories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IimageRepositery iimageRepositery;

        public ImagesController(IimageRepositery iimageRepositery)
        {
            this.iimageRepositery = iimageRepositery;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm]ImageUlpoadDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtentions = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                //use repository to upload the image
                await iimageRepositery.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUlpoadDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extentions");
            }
            if(request.File.Length > 10385760)
            {
                ModelState.AddModelError("file", "File size more than 10 Mb, please ulpoadsmaller size file");
            }
        }
    }
}
