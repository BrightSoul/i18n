using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Text.RegularExpressions;
using i18n.Web.App_GlobalResources;

namespace i18n.Web.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly IEnumerable<string> supportedLanguages;

        public LocalizationController()
        {
            supportedLanguages = GetSupportedLanguages(ConfigurationManager.AppSettings["SupportedLanguages"]);
        }

        internal static IEnumerable<string> GetSupportedLanguages(string rawValue)
        {
          return rawValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        
        internal static IEnumerable<string> GetPreferredLanguages(string rawValue)
        {
            if (string.IsNullOrEmpty(rawValue)) yield break;

            //Esempio di valore: it-IT,it;q=0.8,en-US;q=0.6,en;q=0.4
            var regexp = new Regex("[a-z]{2}(-[a-z]{2})?", RegexOptions.IgnoreCase);
            foreach (Match match in regexp.Matches(rawValue))
            {
                yield return match.Value;
            }
        }

        internal static string SuggestLanguage(IEnumerable<string> supportedLanguages, IEnumerable<string> preferredLanguages) {
            var intersection = preferredLanguages.Intersect(supportedLanguages);

            if (intersection.Any())
                return intersection.First();

            return supportedLanguages.First();
        }

        [ChildActionOnly] //Vary by custom URL
        public ActionResult LanguageSelection()
        {
            
            //var items = GetItems(Strings.ResourceManager, ControllerContext.ParentActionViewContext.RequestContext, supportedLanguages);
            return View(supportedLanguages);
        }

        public ActionResult RedirectToLanguage()
        {
            var preferredLanguages = GetPreferredLanguages(Request.Headers["Accept-Language"]);
            var suggestedLanguage = SuggestLanguage(supportedLanguages, preferredLanguages);
            return RedirectToRoute("Default", new { language = suggestedLanguage, action = default(string), controller = default(string) });
        }

        public ActionResult HrefLangs()
        {
            //var items = GetItems(Strings.ResourceManager, ControllerContext.ParentActionViewContext.RequestContext, supportedLanguages);
            return View(supportedLanguages);
        }


    }


}