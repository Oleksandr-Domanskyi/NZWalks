using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.Dtos
{
    public class ImageUlpoadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}
