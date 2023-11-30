using System.ComponentModel.DataAnnotations;

namespace Renovation.API.Models.DTO
{
    public class MeRequestDto
    {
        [Required]
        public string Token { get; set; }
    }
}
