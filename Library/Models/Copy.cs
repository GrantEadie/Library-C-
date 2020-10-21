using System.Collections.Generic;
using System;

namespace Library.Models
{
  public class Copy
    {
        public Copy()
        {
            this.Books = new HashSet<BookCopy>();
            this.Patrons = new HashSet<CopyPatron>();
        }

        public int CopyId { get; set; }
        public string CopyName { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<BookCopy> Books { get; set; }
        public virtual ICollection<CopyPatron> Patrons { get; set; }
    }
}