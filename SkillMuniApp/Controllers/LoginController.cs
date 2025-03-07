// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.LoginController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class LoginController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult loginPost(FormCollection collection)
    {
      try
      {
        System.Web.HttpContext.Current.Session["UserSession"] = (object) null;
        Login login = new contentDashboardModel().checkUser(new Login()
        {
          Username = this.Request.Form["UI"],
          Password = this.Request.Form["PD"]
        });
        if (login == null)
          return (ActionResult) this.RedirectToAction("Index", "Home");
        UserSession orgStatus = new addCMS_CategoryModel().get_org_status(new UserSession()
        {
          Username = login.Username,
          Roleid = login.Roleid,
          ID_USER = login.ID_USER,
          id_ORGANIZATION = login.ID_ORG,
          //id_M2ost_ORGANIZATION = new addCMS_CategoryModel().getM2ostOrg(login.ID_ORG)
        });
        int uid = Convert.ToInt32(login.ID_USER);
        int rid = Convert.ToInt32(login.Roleid);
        int oid = Convert.ToInt32(login.ID_ORG);
        tbl_organization tblOrganization = this.db.tbl_organization.Where<tbl_organization>((Expression<Func<tbl_organization, bool>>) (t => t.ID_ORGANIZATION == oid && t.STATUS == "A")).FirstOrDefault<tbl_organization>();
        orgStatus.org_name = tblOrganization.ORGANIZATION_NAME;
        orgStatus.org_logo = tblOrganization.LOGO;
        tbl_cms_users tblCmsUsers = this.db.tbl_cms_users.Where<tbl_cms_users>((Expression<Func<tbl_cms_users, bool>>) (t => t.ID_USER == uid && t.STATUS == "A" || t.STATUS == "S" || t.STATUS == "F")).FirstOrDefault<tbl_cms_users>();
        orgStatus.USER = tblCmsUsers;
        List<tbl_cms_role_action_mapping> list = this.db.tbl_cms_role_action_mapping.Where<tbl_cms_role_action_mapping>((Expression<Func<tbl_cms_role_action_mapping, bool>>) (t => t.id_cms_role == (int?) rid && t.id_organization == (int?) oid)).ToList<tbl_cms_role_action_mapping>();
        orgStatus.action = list;
        System.Web.HttpContext.Current.Session["UserSession"] = (object) orgStatus;
        if (orgStatus.org_status == "S" || orgStatus.org_status == "F" || orgStatus.org_status == "H")
        {
          if (orgStatus.exp_date < DateTime.Now)
            return (ActionResult) this.RedirectToAction("index", "Home");
          DateTime expDate = orgStatus.exp_date;
        }
        return (ActionResult) this.RedirectToAction("Index", "Dashboard");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string getSessionStatus() => (UserSession) this.HttpContext.Session.Contents["UserSession"] == null ? "0" : "1";

    public string setSessionStatus(string uname, string password)
    {
      System.Web.HttpContext.Current.Session["UserSession"] = (object) null;
      Login login = new contentDashboardModel().checkUser(new Login()
      {
        Username = uname,
        Password = password
      });
      if (login == null)
        return "0";
      UserSession userSession = new UserSession();
      userSession.Username = login.Username;
      userSession.Roleid = login.Roleid;
      userSession.ID_USER = login.ID_USER;
      userSession.id_ORGANIZATION = login.ID_ORG;
      int uid = Convert.ToInt32(login.ID_USER);
      int rid = Convert.ToInt32(login.Roleid);
      int oid = Convert.ToInt32(login.ID_ORG);
      tbl_cms_users tblCmsUsers = this.db.tbl_cms_users.Where<tbl_cms_users>((Expression<Func<tbl_cms_users, bool>>) (t => t.ID_USER == uid && t.STATUS == "A")).FirstOrDefault<tbl_cms_users>();
      userSession.USER = tblCmsUsers;
      List<tbl_cms_role_action_mapping> list = this.db.tbl_cms_role_action_mapping.Where<tbl_cms_role_action_mapping>((Expression<Func<tbl_cms_role_action_mapping, bool>>) (t => t.id_cms_role == (int?) rid && t.id_organization == (int?) oid)).ToList<tbl_cms_role_action_mapping>();
      userSession.action = list;
      System.Web.HttpContext.Current.Session["UserSession"] = (object) userSession;
      return "1";
    }

    public ActionResult Logout()
    {
      System.Web.HttpContext.Current.Session["UserSession"] = (object) null;
      return (ActionResult) this.RedirectToAction(nameof (Logout), "Home", (object) new
      {
        sessoin = "1"
      });
    }

    public ActionResult ChangePassword() => (ActionResult) this.View();

    public JsonResult change_password(FormCollection collection)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      string str1 = this.Request.Form["current_password"].ToString();
      string str2 = this.Request.Form["new_password"].ToString();
      string str3 = this.Request.Form["confirm_password"].ToString();
      int id_user = Convert.ToInt32(content.ID_USER);
      tbl_cms_users tblCmsUsers = this.db.tbl_cms_users.Where<tbl_cms_users>((Expression<Func<tbl_cms_users, bool>>) (t => t.ID_USER == id_user)).FirstOrDefault<tbl_cms_users>();
      if (tblCmsUsers == null)
        return this.Json((object) new
        {
          flag = true,
          msg = "Invalid User.Please login again.",
          redirectUrl = this.Url.Action("Logout", "login")
        });
      string str4 = str1.Trim();
      if (!(tblCmsUsers.PASSWORD == str4))
        return this.Json((object) new
        {
          flag = false,
          msg = "current password doesnot match ,please try again."
        });
      tblCmsUsers.PASSWORD = str2;
      tblCmsUsers.UPDATED_DATE_TIME = DateTime.Now;
      this.db.SaveChanges();
      return tblCmsUsers.PASSWORD == str3 ? this.Json((object) new
      {
        flag = true,
        msg = "Your password updated successfully",
        redirectUrl = this.Url.Action("Index", "dashboard")
      }) : this.Json((object) new
      {
        flag = true,
        msg = "Your password not updated ,please try again."
      });
    }
  }
}
