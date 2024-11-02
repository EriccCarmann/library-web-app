using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repository;

namespace Library.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        private IAuthorRepository _author;
        private IBookRepository _book;

        private bool _isDisposed;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;     
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (_author is null)
                {
                    _author = new AuthorRepository(_context);
                }

                return _author;
            }
        }

        public IBookRepository BookRepository
        {
            get
            {
                if (_book is null)
                {
                    _book = new BookRepository(_context);
                }

                return _book;
            }
        }

        public void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
