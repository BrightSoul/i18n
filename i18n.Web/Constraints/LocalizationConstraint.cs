using i18n.Web.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using i18n.Web.App_GlobalResources;

namespace i18n.Web.Constraints
{
    public class LocalizationConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var supportedLanguages = LocalizationController.GetSupportedLanguages(ConfigurationManager.AppSettings["SupportedLanguages"]);
            if (!supportedLanguages.Contains(values[parameterName]))
                return false;

            var language = values["language"] as string;
            var sets = Strings.ResourceManager.GetResourceSet(CultureInfo.GetCultureInfo(language), true, true).Cast<DictionaryEntry>();
            //Ispeziona questa cosa che implicazioni ha
            if (routeDirection == RouteDirection.IncomingRequest)
            {

                //TODO: sposta questa cosa nel file di configurazione
                


                var localizedActionName = values["action"] as string;
                var localizedControllerName = values["controller"] as string;


                const string actionSuffix = "Action";
                const string controllerSuffix = "Controller";
                var actionName = sets.Where(entry => entry.Key.ToString().EndsWith(actionSuffix, StringComparison.OrdinalIgnoreCase) && entry.Value.ToString().Equals(localizedActionName, StringComparison.OrdinalIgnoreCase)).Select(entry => entry.Key).FirstOrDefault() as string;
                var controllerName = sets.Where(entry => entry.Key.ToString().EndsWith(controllerSuffix, StringComparison.OrdinalIgnoreCase) && entry.Value.ToString().Equals(localizedControllerName, StringComparison.OrdinalIgnoreCase)).Select(entry => entry.Key).FirstOrDefault() as string;

                if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(controllerName))
                {
                    actionName = actionName.Substring(0, actionName.Length - actionSuffix.Length);
                    controllerName = controllerName.Substring(0, controllerName.Length - controllerSuffix.Length);

                    values["action"] = actionName;
                    values["controller"] = controllerName;
                }
            } else if (routeDirection == RouteDirection.UrlGeneration)
            {
                var controllerName = $"{values["controller"]}Controller";
                var actionName = $"{values["action"]}Action";

                if (sets.Any(s => s.Key.ToString() == controllerName))
                {
                    values["controller"] = sets.FirstOrDefault(s => s.Key.ToString() == controllerName).Value;
                    // = sets.FirstOrDefault(s => s.Key.ToString() == controllerName).Value;
                }
                if (sets.Any(s => s.Key.ToString() == actionName))
                {
                    values["action"] = sets.FirstOrDefault(s => s.Key.ToString() == actionName).Value;
                }
                //values["controller"] = "ZIppoi";

            }
            return true;
        }
    }
}
