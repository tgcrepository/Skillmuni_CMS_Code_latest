﻿// Decompiled with JetBrains decompiler
// Type: m2ostnext.Startup
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;

namespace m2ostnext
{
  public class Startup
  {
    public void ConfigureAuth(IAppBuilder app)
    {
      app.CreatePerOwinContext<ApplicationDbContext>(new Func<ApplicationDbContext>(ApplicationDbContext.Create));
      app.CreatePerOwinContext<ApplicationUserManager>(new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>(ApplicationUserManager.Create));
      app.CreatePerOwinContext<ApplicationSignInManager>(new Func<IdentityFactoryOptions<ApplicationSignInManager>, IOwinContext, ApplicationSignInManager>(ApplicationSignInManager.Create));
      IAppBuilder app1 = app;
      CookieAuthenticationOptions options = new CookieAuthenticationOptions();
      options.AuthenticationType = "ApplicationCookie";
      options.LoginPath = new PathString("/Account/Login");
      options.Provider = (ICookieAuthenticationProvider) new CookieAuthenticationProvider()
      {
        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(TimeSpan.FromMinutes(30.0), (Func<ApplicationUserManager, ApplicationUser, Task<ClaimsIdentity>>) ((manager, user) => user.GenerateUserIdentityAsync((UserManager<ApplicationUser>) manager)))
      };
      app1.UseCookieAuthentication(options);
      app.UseExternalSignInCookie("ExternalCookie");
      app.UseTwoFactorSignInCookie("TwoFactorCookie", TimeSpan.FromMinutes(5.0));
      app.UseTwoFactorRememberBrowserCookie("TwoFactorRememberBrowser");
    }

    public void Configuration(IAppBuilder app) => this.ConfigureAuth(app);
  }
}
