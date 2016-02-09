using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace i18n.Web.Filters
{
public class CultureFilterAttribute : ActionFilterAttribute
{
    //Dobbiamo impostare la Culture ad ogni richiesta.
    //Un action filter come questo ci consente di farlo
    //subito prima che l'action vada in esecuzione
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var language = filterContext.RequestContext.RouteData.Values["language"];
        if (language == null) return;

        //Otteniamo un riferimento alla Culture leggendo il valore
        //di language ottenuto dai dati di routing
        var culture = CultureInfo.GetCultureInfo(language.ToString());
        //...e la assegnamo al thread corrente
        Thread.CurrentThread.CurrentCulture =
        Thread.CurrentThread.CurrentUICulture = culture;

        //Già che ci siamo, impostiamo anche un proprietà del ViewBag
        //che ci segnalerà se il senso di lettura di questa lingua è
        //da sinistra verso destra (es. per le lingue occidentali)
        //o da destra verso sinistra (es. lingue arabe)
        filterContext.Controller.ViewBag.Direction = culture.TextInfo.IsRightToLeft ? "rtl" : "ltr";

        base.OnActionExecuting(filterContext);
    }
}
}