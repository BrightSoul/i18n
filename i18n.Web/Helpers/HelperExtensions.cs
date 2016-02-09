using i18n.Web.App_GlobalResources;
using i18n.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace i18n.Web.Helpers
{
    public static class HelperExtensions
    {
        public static string LocalizedAction(this UrlHelper helper, string action = null, string controller = null, string language = null, object routeData = null)
        {
            RouteValueDictionary values = new RouteValueDictionary(routeData);
            
            var request = helper.RequestContext.HttpContext.Request;
            var data = request.RequestContext.RouteData;
            foreach (string key in request.QueryString)
            {
                values[key] = request.QueryString[key];
            }
            
            language = language ?? (data.Values["language"] as string);
            var resourceManager = Strings.ResourceManager;
            var culture = CultureInfo.GetCultureInfo(language);
            action = action ?? (data.Values["action"] as string);
            controller = controller ?? (data.Values["controller"] as string);
            var localizedControllerName = resourceManager.GetString($"{controller}Controller", culture);
            var localizedActionName = resourceManager.GetString($"{action}Action", culture);
            values["language"] = language;
            values["action"] = localizedActionName ?? action;
            values["controller"] = localizedControllerName ?? controller;
            foreach (var key in data.Values.Keys)
            {
                if (!values.ContainsKey(key))
                    values[key] = data.Values[key];
            }

            return helper.RouteUrl("Default", values);
        }
    }
}