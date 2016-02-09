using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace i18n.Web.ViewEngines
{
public class LocalizedRazorViewEngine : RazorViewEngine
{
    public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
    {
        var language = controllerContext.HttpContext.Request.RequestContext.RouteData.Values["language"] as string;
        ViewEngineResult result = null;
        //Cerchiamo la view in una sottodirectory che porti il nome della lingua selezionata
        if (language != null)
            result = base.FindView(controllerContext, $"{language}/{viewName}", masterName, useCache);

        if (result == null || result.View == null)
            result = base.FindView(controllerContext, viewName, masterName, useCache);

        return result;
    }
}
}
