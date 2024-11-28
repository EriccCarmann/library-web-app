using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<LibraryUser>
    {
        private readonly DBConfig _dbConfig;

        public ApplicationDBContext(DbContextOptions dbContextOptions, DBConfig dbConfig)
            : base(dbContextOptions)
        {
            _dbConfig = dbConfig;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>() {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            _dbConfig.Seed(builder);
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}
