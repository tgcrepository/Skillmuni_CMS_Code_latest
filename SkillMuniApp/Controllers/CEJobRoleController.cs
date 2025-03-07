// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.CEJobRoleController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  [UserFilter]
  public class CEJobRoleController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<IndustryRole> industryRoleList = new List<IndustryRole>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql = "SELECT * FROM tbl_ce_evaluation_jobrole WHERE id_organization = " + (object) int32 + " AND status = 'A'";
        industryRoleList = m2ostDbContext.Database.SqlQuery<IndustryRole>(sql).ToList<IndustryRole>();
      }
      this.ViewData["jobrole"] = (object) industryRoleList;
      return (ActionResult) this.View();
    }

    public ActionResult add_job_role() => (ActionResult) this.View();

    public ActionResult add_job_role_action()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      string str1 = this.Request.Form["Industryrole"];
      string str2 = this.Request.Form["rolecode"];
      string str3 = this.Request.Form["Description"];
      IndustryRole industryRole = new IndustryRole();
      industryRole.ce_industry_role = str1;
      industryRole.ce_role_code = str2;
      industryRole.description = str3;
      industryRole.status = "A";
      industryRole.updated_date_time = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_jobrole(id_organization,ce_industry_role,ce_role_code,description,status,updated_date_time) VALUES ({0},{1},{2},{3},{4},{5});", (object) int32, (object) industryRole.ce_industry_role, (object) industryRole.ce_role_code, (object) industryRole.description, (object) industryRole.status, (object) industryRole.updated_date_time);
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult edit_job_role(string cij)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      return (ActionResult) this.View((object) this.db.Database.SqlQuery<IndustryRole>("SELECT * FROM tbl_ce_evaluation_jobrole WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_role_code = '" + cij + "'").FirstOrDefault<IndustryRole>());
    }

    public ActionResult edit_job_role_action(int id_evalution_role)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      string str1 = this.Request.Form["Industryrole"];
      string str2 = this.Request.Form["rolecode"];
      string str3 = this.Request.Form["Description"];
      IndustryRole industryRole = new IndustryRole();
      industryRole.id_ce_evaluation_jobrole = id_evalution_role;
      industryRole.ce_industry_role = str1;
      industryRole.ce_role_code = str2;
      industryRole.description = str3;
      industryRole.status = "A";
      industryRole.updated_date_time = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_evaluation_jobrole SET  ce_industry_role= {0}, ce_role_code= {1}, description= {2}, status= {3}, updated_date_time= {4}  WHERE  id_ce_evaluation_jobrole= {5}", (object) industryRole.ce_industry_role, (object) industryRole.ce_role_code, (object) industryRole.description, (object) industryRole.status, (object) industryRole.updated_date_time, (object) industryRole.id_ce_evaluation_jobrole);
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult delete_job_role(string cij)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      this.ViewData["role"] = (object) this.db.Database.SqlQuery<IndustryRole>("SELECT * FROM tbl_ce_evaluation_jobrole WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_role_code = '" + cij + "'").FirstOrDefault<IndustryRole>();
      return (ActionResult) this.View();
    }

    public ActionResult delete_job_role_action()
    {
      DateTime now = DateTime.Now;
      string str = this.Request.Form["id_evalution_job"];
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_evaluation_jobrole  SET  status = 'D',updated_date_time={0} WHERE id_ce_evaluation_jobrole ={1}", (object) now, (object) str);
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult delete_job_benchmark_action(string ids)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      DateTime now = DateTime.Now;
      tbl_ce_evaluation_job_organization_setup organizationSetup = this.db.Database.SqlQuery<tbl_ce_evaluation_job_organization_setup>("select * from tbl_ce_evaluation_job_organization_setup where id_ce_evaluation_job_organization_setup=" + ids ?? "").FirstOrDefault<tbl_ce_evaluation_job_organization_setup>();
      if (organizationSetup != null)
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_evaluation_job_organization_setup  SET  status = 'D',updated_date_time={0} WHERE id_ce_evaluation_job_organization_setup ={1}", (object) now, (object) ids);
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_ce_organization_ce_setup SET status = 'D',updated_date_time={0} WHERE id_job_organization ={1} and id_ce_evaluation_jobrole ={2}", (object) now, (object) organizationSetup.id_job_organization, (object) organizationSetup.id_ce_evaluation_jobrole);
        }
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult jobRoleDetail(string cij)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      IndustryRole industryRole = this.db.Database.SqlQuery<IndustryRole>("SELECT * FROM tbl_ce_evaluation_jobrole WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_role_code = '" + cij + "'").FirstOrDefault<IndustryRole>();
      if (industryRole == null)
        return (ActionResult) this.RedirectToAction("Index");
      this.ViewData["role"] = (object) industryRole;
      List<CEJobRoleTGC> list = this.db.Database.SqlQuery<CEJobRoleTGC>("SELECT a.id_ce_evaluation_job_organization_setup, b.ID_ORGANIZATION ID_JOB_COMAPANY, c.id_ce_evaluation_jobrole, b.COMPANY_NAME, a.organization_benchmark_jobpoint, c.ce_role_code, c.ce_industry_role FROM tbl_ce_evaluation_job_organization_setup a, job_organization b, tbl_ce_evaluation_jobrole c WHERE a.id_job_organization = b.ID_ORGANIZATION AND a.id_ce_evaluation_jobrole = c.id_ce_evaluation_jobrole AND a.status = 'A' AND b.STATUS = 'A' AND c.STATUS = 'A' AND c.ce_role_code ='" + cij + "'").ToList<CEJobRoleTGC>();
      foreach (CEJobRoleTGC ceJobRoleTgc in list)
      {
        string str = this.db.Database.SqlQuery<string>("SELECT GROUP_CONCAT('[', b.career_evaluation_title, ':', a.ce_benchmark_jobpoint, ']') asssessment_data FROM tbl_ce_organization_ce_setup a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master  AND a.id_job_organization = " + (object) ceJobRoleTgc.ID_JOB_COMAPANY + " AND a.id_ce_evaluation_jobrole = " + (object) ceJobRoleTgc.id_ce_evaluation_jobrole ?? "").FirstOrDefault<string>();
        ceJobRoleTgc.career_evaluation_data = str == null ? "" : str;
      }
      this.ViewData["jobRoleList"] = (object) list;
      List<jobOrganization> jobOrganizationList = new List<jobOrganization>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql = "SELECT ID_ORGANIZATION, COMPANY_NAME FROM job_organization WHERE ID_ORGANIZATION NOT IN (SELECT DISTINCT id_job_organization FROM tbl_ce_evaluation_job_organization_setup WHERE status='A' AND id_ce_evaluation_jobrole = " + (object) industryRole.id_ce_evaluation_jobrole + ") ORDER BY COMPANY_NAME";
        jobOrganizationList = m2ostDbContext.Database.SqlQuery<jobOrganization>(sql).ToList<jobOrganization>();
      }
      this.ViewData["rolelist"] = (object) jobOrganizationList;
      this.ViewData["astlist"] = (object) this.db.Database.SqlQuery<ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + (object) int32 + " AND status = 'A' AND ce_assessment_type = 1").ToList<ce_career_evaluation_master>();
      return (ActionResult) this.View();
    }

    public ActionResult addIndustryRoleBenchmarkAction()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int int32_2 = Convert.ToInt32(this.Request.Form["idsrole"]);
      tbl_ce_evaluation_job_organization_setup organizationSetup = new tbl_ce_evaluation_job_organization_setup();
      organizationSetup.id_job_organization = Convert.ToInt32(this.Request.Form["rolename"]);
      organizationSetup.id_ce_evaluation_jobrole = int32_2;
      organizationSetup.job_setup_type = 0;
      organizationSetup.organization_benchmark_jobpoint = Convert.ToInt32(this.Request.Form["bmark"]);
      organizationSetup.status = "A";
      organizationSetup.updated_date_time = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_job_organization_setup(id_job_organization,id_ce_evaluation_jobrole,job_setup_type,organization_benchmark_jobpoint,status,updated_date_time) VALUES ({0},{1},{2},{3},{4},{5});", (object) organizationSetup.id_job_organization, (object) organizationSetup.id_ce_evaluation_jobrole, (object) organizationSetup.job_setup_type, (object) organizationSetup.organization_benchmark_jobpoint, (object) organizationSetup.status, (object) organizationSetup.updated_date_time);
      string ceRoleCode = this.db.Database.SqlQuery<IndustryRole>("SELECT * FROM tbl_ce_evaluation_jobrole WHERE id_organization = " + (object) int32_1 + " AND status = 'A' AND id_ce_evaluation_jobrole=" + (object) int32_2 ?? "").FirstOrDefault<IndustryRole>().ce_role_code;
      tbl_ce_organization_ce_setup organizationCeSetup = new tbl_ce_organization_ce_setup();
      for (int index = 1; index <= 5; ++index)
      {
        if (this.Request.Form["ces-" + (object) index] != null)
        {
          int int32_3 = Convert.ToInt32(this.Request.Form["ces-" + (object) index]);
          int int32_4 = Convert.ToInt32(this.Request.Form["abenchid-" + (object) index]);
          organizationCeSetup.id_ce_career_evaluation_master = int32_3;
          organizationCeSetup.ce_benchmark_jobpoint = int32_4;
          organizationCeSetup.ce_role_code = ceRoleCode;
          organizationCeSetup.ce_setup_type = 0;
          organizationCeSetup.status = "A";
          organizationCeSetup.updated_date_time = DateTime.Now;
          organizationCeSetup.id_job_organization = Convert.ToInt32(this.Request.Form["rolename"]);
          organizationCeSetup.id_ce_evaluation_jobrole = int32_2;
        }
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_organization_ce_setup(id_job_organization,id_ce_evaluation_jobrole, ce_role_code, id_ce_career_evaluation_master, ce_setup_type, ce_benchmark_jobpoint, status, updated_date_time)VALUES ({0},{1},{2},{3},{4},{5},{6},{7});", (object) organizationCeSetup.id_job_organization, (object) organizationCeSetup.id_ce_evaluation_jobrole, (object) organizationCeSetup.ce_role_code, (object) organizationCeSetup.id_ce_career_evaluation_master, (object) organizationCeSetup.ce_setup_type, (object) organizationCeSetup.ce_benchmark_jobpoint, (object) organizationCeSetup.status, (object) organizationCeSetup.updated_date_time);
      }
      return (ActionResult) this.RedirectToAction("jobRoleDetail", "CEJobRole", (object) new
      {
        cij = organizationCeSetup.ce_role_code
      });
    }
  }
}
