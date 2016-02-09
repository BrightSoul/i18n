using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace i18n.Web.Models
{
    [ComplexType]
    public class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public override string ToString()
        {
            return $"{Amount:G} {Currency}";
        }
    }
}