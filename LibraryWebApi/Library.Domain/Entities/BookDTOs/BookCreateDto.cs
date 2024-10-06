namespace Library.Domain.Entities.BookDTOs
{
    public class BookCreateDto
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
    }
}
