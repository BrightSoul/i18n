using i18n.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace i18n.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureFilterAttribute());
        }
    }
}
