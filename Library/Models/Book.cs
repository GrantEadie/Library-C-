using System.Collections.Generic;

namespace Library.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<AuthorBook>();
            this.Genres = new HashSet<BookGenre>();
            this.Copies = new HashSet<BookCopy>();
        }

        public int BookId { get; set; }
        public string BookName {get; set; }
        public string BookDescription { get; set; }
        public int NumberOfBooks {get; set; }
        public virtual ApplicationUser User { get; set; }

        public ICollection<AuthorBook> Authors { get;}
        public ICollection<BookGenre> Genres { get;}
        public ICollection<BookCopy> Copies {get; }
    }
}