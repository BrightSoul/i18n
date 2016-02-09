using i18n.Web.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace i18n.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { language = new LocalizationConstraint() }
            );

            routes.MapRoute(
                name: "LanguageSelection",
                url: "",
                defaults: new { controller = "Localization", action = "RedirectToLanguage" }
            );

        }
    }
}
