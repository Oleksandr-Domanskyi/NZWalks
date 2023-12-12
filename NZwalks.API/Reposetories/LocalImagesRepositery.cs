using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Reposetories
{
    public class LocalImagesRepositery : IimageRepositery
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccesso;
        private readonly NZWalksDbContext dbContext;

        public LocalImagesRepositery(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccesso,
            NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccesso = httpContextAccesso;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",
                $"{image.FileName}{image.FileExtentions}");
            
            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccesso.HttpContext.Request.Scheme}://{httpContextAccesso.HttpContext.Request.Host}{httpContextAccesso.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtentions}";

            image.FilePath = urlFilePath;

            //Add Image to Images Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
