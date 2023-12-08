namespace NZwalks.API.Models.Dtos.CreateRegionDto
{
    public class AddRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string? ReoginImageURL { get; set; }
    }
}
