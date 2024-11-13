namespace Library.Application.DTOs.AuthorDTOs
{
    public class AuthorUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Country { get; set; }
    }
}
