namespace Library.Domain.Entities.BookDTOs
{
    public class BookReadDto
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        public bool? IsTaken { get; set; } = false;
        public int? AuthorId { get; set; }
    }
}
