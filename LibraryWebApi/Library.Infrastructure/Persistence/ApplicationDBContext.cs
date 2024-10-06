using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<LibraryUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        { }

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

            builder.Entity<AuthorBook>(
                x => x.HasKey(p => new { p.AuthorId, p.BookId }));

            builder.Entity<AuthorBook>()
                .HasOne(u => u.Author)
                .WithMany(u => u.AuthorBook)
                .HasForeignKey(u => u.AuthorId);

            builder.Entity<AuthorBook>()
                .HasOne(u => u.Book)
                .WithMany(u => u.AuthorBook)
                .HasForeignKey(u => u.BookId);
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<AuthorBook> authorBooks { get; set; }
    }
}
