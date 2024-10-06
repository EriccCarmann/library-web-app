using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities
{
    [Table("AuthorBook")]
    public class AuthorBook
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
