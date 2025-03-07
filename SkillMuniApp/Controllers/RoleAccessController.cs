// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.RoleAccessController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace m2ostnext.Controllers
{
  public class RoleAccessController : ActionFilterAttribute
  {
    public int KEY { get; set; }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      UserSession content = (UserSession) filterContext.HttpContext.Session.Contents["UserSession"];
      if (content == null)
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
      else if (new RoleBasedAccess().checkAccess(content.action, this.KEY))
        base.OnActionExecuting(filterContext);
      else
        filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary()
        {
          {
            "Controller",
            (object) "Information"
          },
          {
            "Action",
            (object) "Forbidden"
          }
        });
    }
  }
}
