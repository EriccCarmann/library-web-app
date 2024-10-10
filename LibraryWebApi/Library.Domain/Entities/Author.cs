namespace Library.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Country { get; set; }

        public List<Book> Books { get; } = new List<Book>();
    }
}
