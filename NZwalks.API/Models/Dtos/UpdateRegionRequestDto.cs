using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.Dtos
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage = "Code has to be a minimum of 3 charakters")]
        [MaxLength(3,ErrorMessage = "Code has to be a maximum of 3 charakters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a maximum of 100 charakters")]
        public string Name { get; set; }
        public string? ReoginImageURL { get; set; }
    }
}
