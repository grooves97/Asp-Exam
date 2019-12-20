using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class AuthDTO
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
