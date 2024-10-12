using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Library.Domain.Entities.LibraryUserDTOs;
using System.Security.Claims;
using IdentityModel;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Migrations;

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

            Seed(builder);
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }

        public void Seed(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(
                   new Author() { Id = 1, FirstName = "John Ronald Reuel", LastName = "Tolkien", DateOfBirth = new DateOnly(1892, 01, 03), Country = "United Kingdom" },
                   new Author() { Id = 2, FirstName = "Terry", LastName = "Pratchett", DateOfBirth = new DateOnly(1948, 04, 28), Country = "United Kingdom" },
                   new Author() { Id = 3, FirstName = "Neil", LastName = "Gaiman", DateOfBirth = new DateOnly(1960, 11, 10), Country = "United Kingdom" }
            );

            builder.Entity<Book>().HasData(
                    new Book()
                    {
                        Id = 1,
                        Title = "Hobbit",
                        Genre = "Adventure",
                        Description = "The Hobbit, or There and Back Again is a children's fantasy novel by the English author J. R. R. Tolkien. It was published in 1937 " +
                        "to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction.",
                        ISBN = "1-34-678910-2",
                        AuthorId = 1,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },
                    new Book()
                    {
                        Id = 2,
                        Title = "Lord Of The Rings",
                        Genre = "Adventure",
                        Description = "The Lord of the Rings is an epic high fantasy novel by the English author and scholar J. R. R. Tolkien. Set in Middle-earth, " +
                        "the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
                        ISBN = "2-33-679910-2",
                        AuthorId = 1,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },
                    new Book()
                    {
                        Id = 3,
                        Title = "The Silmarillion",
                        Genre = "Adventure",
                        Description = "The Silmarillion is a book consisting of a collection of myths and stories in varying styles by the English writer J. R. R. Tolkien." +
                        " It was edited, partly written, and published posthumously by his son Christopher Tolkien in 1977, assisted by Guy Gavriel Kay, who became a fantasy author.",
                        ISBN = "2-33-679910-2",
                        AuthorId = 1,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },

                    new Book()
                    {
                        Id = 4,
                        Title = "Small Gods",
                        Genre = "Fantasy",
                        Description = "Small Gods is the thirteenth of Terry Pratchett's Discworld novels, published in 1992. It tells the origin of the god Om, and his " +
                        "relations with his prophet, the reformer Brutha. In the process, it satirises philosophy, religious institutions, people, and practices, and the " +
                        "role of religion in political life.",
                        ISBN = "0-06-017750-0",
                        AuthorId = 2,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },
                    new Book()
                    {
                        Id = 5,
                        Title = "Guards! Guards!",
                        Genre = "Fantasy",
                        Description = "Guards! Guards! is a fantasy novel by British writer Terry Pratchett, the eighth in the Discworld series, first published in 1989. It is " +
                        "the first novel about the Ankh-Morpork City Watch. The first Discworld point-and-click adventure game borrowed heavily from the plot of Guards! Guards!",
                        ISBN = "3-33-679910-2",
                        AuthorId = 2,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },
                    new Book()
                    {
                        Id = 6,
                        Title = "Night Watch",
                        Genre = "Fantasy",
                        Description = "Night Watch is a fantasy novel by British writer Terry Pratchett, the 29th book in his Discworld series, and the sixth starring the " +
                        "City Watch, published in 2002. The protagonist of the novel is Sir Samuel Vimes, commander of the Ankh-Morpork City Watch.",
                        ISBN = "3-73-674510-2",
                        AuthorId = 2,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },

                    new Book()
                    {
                        Id = 7,
                        Title = "Norse Mythology",
                        Genre = "Fantasy",
                        Description = "Norse Mythology is a 2017 book by Neil Gaiman, which retells several stories from Norse mythology. In the introduction, Gaiman " +
                        "describes where his fondness for the source material comes from. The book received positive reviews from critics.",
                        ISBN = "4-33-679910-2",
                        AuthorId = 3,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },
                    new Book()
                    {
                        Id = 8,
                        Title = "The ​Graveyard Book",
                        Genre = "Horror fiction",
                        Description = "The Graveyard Book is a young adult novel written by the English author Neil Gaiman, simultaneously published in Britain and " +
                        "America in 2008. The Graveyard Book traces the story of the boy Nobody \"Bod\" Owens, who is adopted and reared by the supernatural occupants of a" +
                        " graveyard after his family is brutally murdered.",
                        ISBN = "4-53-679910-2",
                        AuthorId = 3,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    },
                    new Book()
                    {
                        Id = 9,
                        Title = "Stardust",
                        Genre = "Adventure",
                        Description = "Stardust is a 1999 fantasy novel by British writer Neil Gaiman, usually published with illustrations by Charles Vess.",
                        ISBN = "4-39-674510-2",
                        AuthorId = 3,
                        UserId = null,
                        IsTaken = false,
                        TakeDateTime = null
                    }
                );

            var admin = new LibraryUser
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin"
            };

            PasswordHasher<LibraryUser> ph = new PasswordHasher<LibraryUser>();

            admin.PasswordHash = ph.HashPassword(admin, "123qwe123QWE*");

            builder.Entity<LibraryUser>().HasData(admin);

            builder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
            {
                Id = -1,
                ClaimType = ClaimTypes.Role,
                UserId = "b74ddd14-6340-4840-95c2-db12554843e5",
                ClaimValue = "Admin"
            });
        }
    }
}
