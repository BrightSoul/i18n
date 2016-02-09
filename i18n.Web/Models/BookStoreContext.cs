using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace i18n.Web.Models
{
    public class BookStoreContext : DbContext
    {
        static BookStoreContext()
        {
            Database.SetInitializer<BookStoreContext>(new BookStoreInitializer());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLocalization> BookLocalizations { get; set; }
    }
}