using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace i18n.Web.Models
{
    public class BookLocalization
    {
        [Key, Column(Order = 1)]
        public string Language { get; set; }
        [Key, Column(Order = 2)]
        public Guid BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(255), Index(IsUnique = true)]
        public string Slug { get; set; }

        public Money Price { get; set; }

        public string CoverPath
        {
            get { return $"/Covers/{Language}/{BookId}.jpg"; }
        }
    }
}