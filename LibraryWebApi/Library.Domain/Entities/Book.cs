namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        public int? AuthorId { get; set; }
        public bool? IsTaken { get; set; } = false;
        public Nullable<DateTime> TakeDateTime { get; set; } = null;

        public Author? Author { get; set; }
    }
}
