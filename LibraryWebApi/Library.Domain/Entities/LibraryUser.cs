using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Entities
{
    public class LibraryUser : IdentityUser
    {
        public List<Book>? Books { get; } = null;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
