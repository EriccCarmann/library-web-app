using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs.LibraryUserDTOs
{
    public class LoginDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
