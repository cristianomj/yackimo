using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    public class Author
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }

    public class Book
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}