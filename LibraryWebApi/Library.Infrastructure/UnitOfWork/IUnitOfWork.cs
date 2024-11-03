using Library.Domain.Interfaces;

namespace Library.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        IAccountRepository Account { get; }

        Task SaveChangesAsync();
    }
}
