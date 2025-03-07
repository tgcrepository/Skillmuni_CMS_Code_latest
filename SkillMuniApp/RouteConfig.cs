// Decompiled with JetBrains decompiler
// Type: m2ostnext.RouteConfig
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Web.Mvc;
using System.Web.Routing;

namespace m2ostnext
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.MapRoute("Default", "{controller}/{action}/{id}", (object) new
      {
        controller = "Home",
        action = "Index",
        id = UrlParameter.Optional
      });
    }
  }
}
