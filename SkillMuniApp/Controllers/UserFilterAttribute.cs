// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.UserFilterAttribute
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace m2ostnext.Controllers
{
  public class UserFilterAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if ((UserSession) filterContext.HttpContext.Session.Contents["UserSession"] == null)
        filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary()
        {
          {
            "Controller",
            (object) "Home"
          },
          {
            "Action",
            (object) "Index"
          }
        });
      else
        base.OnActionExecuting(filterContext);
    }
  }
}
