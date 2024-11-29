using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;

namespace Library.Infrastructure.Repository
{
    public class AccountRepository : GenericRepository<LibraryUser>, IAccountRepository
    {
        private readonly ApplicationDBContext _context;

        public AccountRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
