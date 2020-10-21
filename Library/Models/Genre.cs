using System.Collections.Generic;

namespace Library.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Books = new HashSet<BookGenre>();
            this.Authors = new HashSet<AuthorGenre>();
        }

        public int GenreId { get; set; }
        public string GenreName {get; set;}
        public string GenreDescription { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<BookGenre> Books { get; set; }
        public ICollection<AuthorGenre> Authors { get; set; }
    }
}