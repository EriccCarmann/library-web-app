using Library.Domain.Interfaces;

namespace Library.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }

        Task SaveChangesAsync();
    }
}
