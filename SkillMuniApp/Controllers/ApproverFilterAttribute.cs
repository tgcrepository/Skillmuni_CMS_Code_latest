// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.ApproverFilterAttribute
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace m2ostnext.Controllers
{
  public class ApproverFilterAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      UserSession content = (UserSession) filterContext.HttpContext.Session.Contents["UserSession"];
      int num = Convert.ToInt32(content.Roleid) % 2;
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
      else if (content.Roleid.Equals("2") || num == 0)
      {
        if (content.id_ORGANIZATION == "")
          filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary()
          {
            {
              "Controller",
              (object) "dashboard"
            },
            {
              "Action",
              (object) "Index"
            }
          });
        else
          base.OnActionExecuting(filterContext);
      }
      else
        filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary()
        {
          {
            "Controller",
            (object) "dashboard"
          },
          {
            "Action",
            (object) "ApprovalDashboard"
          }
        });
    }
  }
}
