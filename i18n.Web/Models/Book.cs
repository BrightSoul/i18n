using i18n.Web.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace i18n.Web.Models
{
    public class Book
    {
        public Book()
        {
            Localizations = new HashSet<BookLocalization>();
        }
        [Key]
        public Guid BookId { get; set; }
        [Display(Name = "Author", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Strings))]
        public string Author { get; set; }
        public DateTime PublishingDate { get; set; }
        public virtual ICollection<BookLocalization> Localizations { get; set; }
    }
}