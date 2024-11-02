using Library.Infrastructure.Persistence;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDBContext _context;

        public AuthorRepository(ApplicationDBContext context) : base(context)
        { 
            _context = context;
        }

        public async Task<Author?> FindAuthorByName(string name)
        {
            var author = await _context.Author
                .FirstOrDefaultAsync(a => (a.FirstName.ToLower() + " " + a.LastName.ToLower()).Contains(name));

            if (author == null) return null;

            return author;
        }
    }
}
