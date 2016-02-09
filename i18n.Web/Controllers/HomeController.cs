using i18n.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace i18n.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(string language)
        {
            List<BookLocalization> books;
            using (var context = new BookStoreContext())
            {
                books = await context.BookLocalizations.Include(Localization => Localization.Book).Where(localization => localization.Language == language).ToListAsync();
            }
            return View(books);
        }

        public async Task<ActionResult> Detail(Guid id, string language)
        {
            BookLocalization book;
            using (var context = new BookStoreContext())
            {
                book = await context.BookLocalizations.Include(Localization => Localization.Book).Where(localization => localization.BookId == id && localization.Language == language).FirstOrDefaultAsync();
            }
            return View(book);
        }
    }
}