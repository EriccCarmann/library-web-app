using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<LibraryUser> _userManager;
        private readonly SignInManager<LibraryUser> _signInManager;

        private IAuthorRepository _author;
        private IBookRepository _book;
        private IAccountRepository _account;

        private bool _isDisposed;

        public UnitOfWork(
            ApplicationDBContext context,
            SignInManager<LibraryUser> signInManager,
            UserManager<LibraryUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IAuthorRepository Author
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

        public IBookRepository Book
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

        public IAccountRepository Account
        {
            get
            {
                if (_account is null)
                {
                    _account = new AccountRepository(_context);
                }

                return _account;
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
