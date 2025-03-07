// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.industryjobController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  [UserFilter]
  public class industryjobController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_ce_evaluation_jobindustry> evaluationJobindustryList = new List<tbl_ce_evaluation_jobindustry>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql = "SELECT * FROM tbl_ce_evaluation_jobindustry WHERE id_organization = " + (object) int32 + " AND status = 'A'";
        evaluationJobindustryList = m2ostDbContext.Database.SqlQuery<tbl_ce_evaluation_jobindustry>(sql).ToList<tbl_ce_evaluation_jobindustry>();
      }
      this.ViewData["role"] = (object) evaluationJobindustryList;
      return (ActionResult) this.View();
    }

    public ActionResult add_industry_job() => (ActionResult) this.View();

    public ActionResult add_industry_job_action()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      string str1 = (string) null;
      string str2 = this.Request.Form["Industry"];
      string str3 = str2.Replace(" ", "").Replace("&", "");
      string str4 = this.Request.Form["code"];
      string str5 = this.Request.Form["red_url"];
      string str6 = this.Request.Form["Description"];
      int int32_2 = Convert.ToInt32(this.Request.Form["position"]);
      string str7 = this.Request.Form["buttontext"];
      tbl_ce_evaluation_jobindustry evaluationJobindustry = new tbl_ce_evaluation_jobindustry();
      try
      {
        if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
        {
          HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["uploadBtn"];
          str1 = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["uploadBtn"].FileName);
          string str8 = System.Web.HttpContext.Current.Request["id"];
          if (file != null && file.ContentLength > 0)
          {
            if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/CECATEGORY_IMAGE/" + content.id_ORGANIZATION + "/")))
              Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/CECATEGORY_IMAGE/" + content.id_ORGANIZATION + "/"));
            string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/CECATEGORY_IMAGE/" + content.id_ORGANIZATION + "/"), str3 + str1);
            file.SaveAs(filename);
          }
        }
        evaluationJobindustry.ce_job_industry = str2;
        evaluationJobindustry.ce_industry_code = str4;
        evaluationJobindustry.description = str6;
        evaluationJobindustry.IMAGE_PATH = str3 + str1;
        evaluationJobindustry.IMAGE_URL = str5;
        evaluationJobindustry.status = "A";
        evaluationJobindustry.updated_date_time = DateTime.Now;
        evaluationJobindustry.tile_position = int32_2;
        evaluationJobindustry.buttontext = str7;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_jobindustry(id_organization,ce_job_industry,ce_industry_code,description,status,updated_date_time,tile_image,tile_position,buttontext) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8});", (object) int32_1, (object) evaluationJobindustry.ce_job_industry, (object) evaluationJobindustry.ce_industry_code, (object) evaluationJobindustry.description, (object) evaluationJobindustry.status, (object) evaluationJobindustry.updated_date_time, (object) evaluationJobindustry.IMAGE_PATH, (object) evaluationJobindustry.tile_position, (object) evaluationJobindustry.buttontext);
      }
      catch (Exception ex)
      {
        new contentDashboardModel().exception_log(ex);
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult edit_industry_job(string cij)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      return (ActionResult) this.View((object) this.db.Database.SqlQuery<tbl_ce_evaluation_jobindustry>("SELECT * FROM tbl_ce_evaluation_jobindustry WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_industry_code = '" + cij + "'").FirstOrDefault<tbl_ce_evaluation_jobindustry>());
    }

    public ActionResult edit_industry_job_action(int id_evalution)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      string str1 = (string) null;
      string str2 = this.Request.Form["Industry"];
      string str3 = str2.Replace(" ", "").Replace("&", "");
      string str4 = this.Request.Form["code"];
      string str5 = this.Request.Form["red_url"];
      string str6 = this.Request.Form["Description"];
      int int32 = Convert.ToInt32(this.Request.Form["position"]);
      string str7 = this.Request.Form["buttontext"];
      tbl_ce_evaluation_jobindustry evaluationJobindustry = new tbl_ce_evaluation_jobindustry();
      try
      {
        if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
        {
          HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["uploadBtn"];
          str1 = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["uploadBtn"].FileName);
          string str8 = System.Web.HttpContext.Current.Request["id"];
          if (file != null && file.ContentLength > 0)
          {
            if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/CECATEGORY_IMAGE/" + content.id_ORGANIZATION + "/")))
              Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/CECATEGORY_IMAGE/" + content.id_ORGANIZATION + "/"));
            string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/CECATEGORY_IMAGE/" + content.id_ORGANIZATION + "/"), str3 + str1);
            file.SaveAs(filename);
          }
        }
        evaluationJobindustry.ce_job_industry = str2;
        evaluationJobindustry.ce_industry_code = str4;
        evaluationJobindustry.description = str6;
        evaluationJobindustry.id_ce_evaluation_jobindustry = id_evalution;
        evaluationJobindustry.IMAGE_PATH = str3 + str1;
        evaluationJobindustry.IMAGE_URL = str5;
        evaluationJobindustry.status = "A";
        evaluationJobindustry.updated_date_time = DateTime.Now;
        evaluationJobindustry.tile_position = int32;
        evaluationJobindustry.buttontext = str7;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_evaluation_jobindustry SET  ce_job_industry = {0}, description = {1}, status = {2}, updated_date_time = {3}, tile_image = {4}, tile_position = {5}, buttontext = {6} WHERE  id_ce_evaluation_jobindustry = {7}", (object) evaluationJobindustry.ce_job_industry, (object) evaluationJobindustry.description, (object) evaluationJobindustry.status, (object) evaluationJobindustry.updated_date_time, (object) evaluationJobindustry.IMAGE_PATH, (object) evaluationJobindustry.tile_position, (object) evaluationJobindustry.buttontext, (object) evaluationJobindustry.id_ce_evaluation_jobindustry);
      }
      catch (Exception ex)
      {
        new contentDashboardModel().exception_log(ex);
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult delete_industry_job(string cij)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      this.ViewData["industry"] = (object) this.db.Database.SqlQuery<tbl_ce_evaluation_jobindustry>("SELECT * FROM tbl_ce_evaluation_jobindustry WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_industry_code = '" + cij + "'").FirstOrDefault<tbl_ce_evaluation_jobindustry>();
      return (ActionResult) this.View();
    }

    public ActionResult delete_industry_job_action()
    {
      DateTime now = DateTime.Now;
      string str = this.Request.Form["id_evalution"];
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_evaluation_jobindustry  SET  status = 'D',updated_date_time={0} WHERE id_ce_evaluation_jobindustry ={1}", (object) now, (object) str);
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult delete_industry_benchmark_action(string ids)
    {
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_evaluation_jobindustry_tgc_setup  SET  status = 'D' WHERE id_ce_evaluation_jobindustry_tgc_setup ={0}", (object) ids);
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult industryDetail(string cij)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      tbl_ce_evaluation_jobindustry evaluationJobindustry = this.db.Database.SqlQuery<tbl_ce_evaluation_jobindustry>("SELECT * FROM tbl_ce_evaluation_jobindustry WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_industry_code = '" + cij + "'").FirstOrDefault<tbl_ce_evaluation_jobindustry>();
      if (evaluationJobindustry == null)
        return (ActionResult) this.RedirectToAction("Index");
      this.ViewData["industry"] = (object) evaluationJobindustry;
      this.ViewData["orglist"] = (object) this.db.Database.SqlQuery<IndustryCompanyTGC>("SELECT a.id_ce_evaluation_jobindustry_tgc_setup, b.ID_ORGANIZATION ID_JOB_COMAPANY, c.id_ce_evaluation_jobindustry, b.COMPANY_NAME, a.benchmark_jobpoint, c.ce_industry_code, c.ce_job_industry FROM tbl_ce_evaluation_jobindustry_tgc_setup a, job_organization b, tbl_ce_evaluation_jobindustry c WHERE a.id_job_organization = b.ID_ORGANIZATION AND a.id_ce_evaluation_jobindustry = c.id_ce_evaluation_jobindustry AND a.status = 'A' AND b.STATUS = 'A' AND c.STATUS = 'A' AND c.ce_industry_code = '" + cij + "'").ToList<IndustryCompanyTGC>();
      List<jobOrganization> jobOrganizationList = new List<jobOrganization>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql = "SELECT ID_ORGANIZATION, COMPANY_NAME FROM job_organization WHERE ID_ORGANIZATION NOT IN (SELECT DISTINCT id_job_organization FROM tbl_ce_evaluation_jobindustry_tgc_setup WHERE status='A' AND id_ce_evaluation_jobindustry = " + (object) evaluationJobindustry.id_ce_evaluation_jobindustry + ") ORDER BY COMPANY_NAME";
        jobOrganizationList = m2ostDbContext.Database.SqlQuery<jobOrganization>(sql).ToList<jobOrganization>();
      }
      this.ViewData["joblist"] = (object) jobOrganizationList;
      return (ActionResult) this.View();
    }

    public string addIndustryBenchmarkAction(int ids, int cid, int bmark)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      tbl_ce_evaluation_jobindustry_tgc_setup jobindustryTgcSetup = new tbl_ce_evaluation_jobindustry_tgc_setup();
      jobindustryTgcSetup.id_job_organization = cid;
      jobindustryTgcSetup.id_ce_evaluation_jobindustry = ids;
      jobindustryTgcSetup.benchmark_jobpoint = bmark;
      jobindustryTgcSetup.status = "A";
      jobindustryTgcSetup.updated_date_time = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_jobindustry_tgc_setup(id_job_organization,id_ce_evaluation_jobindustry,benchmark_jobpoint,status,updated_date_time) VALUES ({0},{1},{2},{3},{4});", (object) jobindustryTgcSetup.id_job_organization, (object) jobindustryTgcSetup.id_ce_evaluation_jobindustry, (object) jobindustryTgcSetup.benchmark_jobpoint, (object) jobindustryTgcSetup.status, (object) jobindustryTgcSetup.updated_date_time);
        return "1";
      }
    }
  }
}
