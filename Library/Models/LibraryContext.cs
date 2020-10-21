using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Author> Authors { get; set; }

    public DbSet<Genre> Genres {get; set;}
    public DbSet<Book> Books {get; set;}
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Copy> Copies { get; set; }

    public DbSet<AuthorBook> AuthorBook {get; set;}
    public DbSet<AuthorGenre> AuthorGenre {get; set;}
    public DbSet<BookGenre> BookGenre {get; set;}

    public DbSet<CopyPatron> CopyPatron {get; set;}
    public DbSet<BookCopy> BookCopy { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { } 
  }
}