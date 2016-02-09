using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace i18n.Web.Models
{
    public class BookStoreInitializer : DropCreateDatabaseAlways<BookStoreContext>
    {
        protected override void Seed(BookStoreContext context)
        {
            var book = new Book
            {
                BookId = Guid.Parse("35eaf338-6161-452f-9623-e5f50b4e23c0"),
                Author = "Antoine De Saint-Exupery",
                PublishingDate = new DateTime(1943, 4, 6),
                Localizations = new HashSet<BookLocalization>
                {
                    new BookLocalization {
                        Language = "it",
                        Title = "Il piccolo principe",
                        Slug = "il-piccolo-principe",
                        Description = "",
                        Price = new Money { Amount = 5.00m, Currency = "EUR" }
                    },
                    new BookLocalization {
                        Language = "en",
                        Title = "The little prince",
                        Slug = "the-little-prince",
                        Description = "",
                        Price = new Money { Amount = 6.00m, Currency = "USD" }
                    },
                    new BookLocalization
                    {
                        Language = "ar-SA",
                        Title = "الأمير الصغير",
                        Slug = "الأمير-الصغير",
                        Description = "",
                        Price = new Money { Amount = 20.00m, Currency = "AED" }
                    }
                }
            };

            context.Books.Add(book);
            context.SaveChanges();
        }
    }
}