// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.referral_pointsController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class referral_pointsController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index()
    {
      this.ViewData["referral"] = (object) this.db.Database.SqlQuery<tbl_referral_code_points_config>("SELECT * FROM tbl_referral_code_points_config WHERE status='A'").ToList<tbl_referral_code_points_config>();
      return (ActionResult) this.View();
    }

    public ActionResult edit_referral_points(int ref_type)
    {
      tbl_referral_code_points_config codePointsConfig = new tbl_referral_code_points_config();
      return (ActionResult) this.View((object) this.db.Database.SqlQuery<tbl_referral_code_points_config>("SELECT * FROM tbl_referral_code_points_config WHERE ref_type=" + (object) ref_type + " AND status='A'").FirstOrDefault<tbl_referral_code_points_config>());
    }

    public ActionResult edit_referral_points_action(int ref_type)
    {
      tbl_referral_code_points_config codePointsConfig = new tbl_referral_code_points_config();
      codePointsConfig.ref_points = Convert.ToInt32(this.Request.Form["points"].ToString());
      codePointsConfig.updated_date_time = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_referral_code_points_config SET ref_points={0},updated_date_time={1} WHERE ref_type={2}", (object) codePointsConfig.ref_points, (object) codePointsConfig.updated_date_time, (object) ref_type);
      return (ActionResult) this.RedirectToAction("Index");
    }
  }
}
