using System.Collections.Generic;

namespace Library.Models
{
  public class Author
    {
        public Author()
        {
            this.Books = new HashSet<AuthorBook>();
            this.Genres = new HashSet<AuthorGenre>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<AuthorBook> Books { get; set; }
        public virtual ICollection<AuthorGenre> Genres { get; set; }
    }
}