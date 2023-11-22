using System.ComponentModel.DataAnnotations;

namespace Renovation.API.Models.DTO
{
    public class LogitRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
