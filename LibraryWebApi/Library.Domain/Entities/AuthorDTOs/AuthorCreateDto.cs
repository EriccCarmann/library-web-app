namespace Library.Domain.Entities.AuthorDTOs
{
    public class AuthorCreateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Country { get; set; }

    }
}
