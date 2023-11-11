using System.ComponentModel.DataAnnotations;

namespace Renovation.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "min value is 3") ]
        [MaxLength(3, ErrorMessage = "max value is 3") ]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
