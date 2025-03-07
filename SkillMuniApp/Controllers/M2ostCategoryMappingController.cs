// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.M2ostCategoryMappingController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class M2ostCategoryMappingController : Controller
  {
    private string m2ostoid = ConfigurationManager.AppSettings["skillmuni_org_id"].ToString();
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult M2ost_category_mapping(int flag = 0)
    {
      List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
      List<tbl_category_heading> tblCategoryHeadingList = new List<tbl_category_heading>();
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblBriefMasterList = m2ostDbContext.Database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where id_organization = " + (object) int32 + " AND status = 'A' AND id_brief_master not in (SELECT id_brief FROM tbl_brief_m2ost_category_mapping WHERE id_org = " + (object) int32 + ") ORDER BY brief_title").ToList<tbl_brief_master>();
      using (m2ostDBContextNew m2ostDbContextNew = new m2ostDBContextNew())
        tblCategoryHeadingList = m2ostDbContextNew.Database.SqlQuery<tbl_category_heading>("SELECT * FROM tbl_category_heading AS t1 INNER JOIN tbl_category_tiles AS t2 ON t2.id_category_tiles = t1.id_category_tiles AND t2.id_organization IN (" + this.m2ostoid + ") AND t2.status = 'A' WHERE t1.status = 'A' ORDER BY t1.heading_order").ToList<tbl_category_heading>();
      this.ViewData["brief"] = (object) tblBriefMasterList;
      this.ViewData["header"] = (object) tblCategoryHeadingList;
      this.ViewData[nameof (flag)] = (object) flag;
      return (ActionResult) this.View();
    }

    public ActionResult M2ost_category_mapping_action()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_brief_m2ost_category_mapping m2ostCategoryMapping = new tbl_brief_m2ost_category_mapping();
      if (Convert.ToInt32(this.Request.Form["t2-select-order"]) == 1)
      {
        m2ostCategoryMapping.id_brief = Convert.ToInt32(this.Request.Form["select-category"]);
        m2ostCategoryMapping.type = 1;
        m2ostCategoryMapping.id_org = int32_1;
        m2ostCategoryMapping.URL = this.Request.Form["typeurl"];
        m2ostCategoryMapping.status = "A";
        m2ostCategoryMapping.updated_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_brief_m2ost_category_mapping(id_brief,id_category,id_org,status,type,URL,id_category_heading,updated_date_time)values({0},{1},{2},{3},{4},{5},{6},{7})", (object) m2ostCategoryMapping.id_brief, (object) m2ostCategoryMapping.id_category, (object) m2ostCategoryMapping.id_org, (object) m2ostCategoryMapping.status, (object) m2ostCategoryMapping.type, (object) m2ostCategoryMapping.URL, (object) m2ostCategoryMapping.id_category_heading, (object) m2ostCategoryMapping.updated_date_time);
      }
      else
      {
        for (int index = 1; index <= 10; ++index)
        {
          if (this.Request.Form["row-qtn-list-" + (object) index] != null)
          {
            int int32_2 = Convert.ToInt32(this.Request.Form["row-qtn-list-" + (object) index]);
            m2ostCategoryMapping.id_brief = Convert.ToInt32(this.Request.Form["select-category"]);
            m2ostCategoryMapping.id_category_heading = Convert.ToInt32(this.Request.Form["t3-select-order"]);
            m2ostCategoryMapping.type = 2;
            m2ostCategoryMapping.id_category = int32_2;
            m2ostCategoryMapping.id_org = int32_1;
            m2ostCategoryMapping.status = "A";
            m2ostCategoryMapping.updated_date_time = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
              m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_brief_m2ost_category_mapping(id_brief,id_category,id_org,status,type,URL,id_category_heading,updated_date_time)values({0},{1},{2},{3},{4},{5},{6},{7})", (object) m2ostCategoryMapping.id_brief, (object) m2ostCategoryMapping.id_category, (object) m2ostCategoryMapping.id_org, (object) m2ostCategoryMapping.status, (object) m2ostCategoryMapping.type, (object) m2ostCategoryMapping.URL, (object) m2ostCategoryMapping.id_category_heading, (object) m2ostCategoryMapping.updated_date_time);
          }
        }
      }
      return (ActionResult) this.RedirectToAction("M2ost_category_mapping_dashbord", "M2ostCategoryMapping", (object) new
      {
        flag = 1
      });
    }

    public string getCategoryList(int cid)
    {
      Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      string str = "";
      List<tbl_m2ost_mapping> tblM2ostMappingList = new List<tbl_m2ost_mapping>();
      using (m2ostDBContextNew m2ostDbContextNew = new m2ostDBContextNew())
        tblM2ostMappingList = m2ostDbContextNew.Database.SqlQuery<tbl_m2ost_mapping>("select t1.id_category, t3.CATEGORYNAME from tbl_category_associantion as t1 inner join tbl_category_heading as t2 on t2.id_category_heading=t1.id_category_heading inner join tbl_category as t3 on t3.ID_CATEGORY=t1.id_category where t1.id_category_heading=" + (object) cid ?? "").ToList<tbl_m2ost_mapping>();
      foreach (tbl_m2ost_mapping tblM2ostMapping in tblM2ostMappingList)
      {
        str = str + "<td>" + tblM2ostMapping.CATEGORYNAME + "</td>";
        str = str + "<td><a id=\"plus-qtn-brief-" + (object) tblM2ostMapping.id_category + "\" href=\"javascript:void(0)\" onclick=\"addQuestionToBrief('" + (object) tblM2ostMapping.id_category + "')\"><i class=\"glyphicon glyphicon-plus\"></i></a><i style=\"display:none;\" id=\"plus-ok-" + (object) tblM2ostMapping.id_category + "\" class=\"glyphicon glyphicon-ok\"></i></td></tr>";
      }
      return "<table id=\"brief-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>" + "<tr><th>Category Name</th>" + "<th></th></tr></thead><tbody>" + str + "</tbody></table>";
    }

    public ActionResult M2ost_category_mapping_dashbord()
    {
      List<m2ost_category_mapping_dashboard> mappingDashboardList = new List<m2ost_category_mapping_dashboard>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        mappingDashboardList = m2ostDbContext.Database.SqlQuery<m2ost_category_mapping_dashboard>("SELECT t1.brief_title, t2.type, t2.URL, t3.CATEGORYNAME, t2.status , t2.id_brief FROM tbl_brief_master AS t1 INNER JOIN tbl_brief_m2ost_category_mapping AS t2 ON t2.id_brief = t1.id_brief_master left JOIN tbl_category AS t3 ON t3.ID_CATEGORY = t2.id_category WHERE t2.id_brief = t1.id_brief_master And t2.status='A' order by t1.brief_title").ToList<m2ost_category_mapping_dashboard>();
      this.ViewData["mapping"] = (object) mappingDashboardList;
      return (ActionResult) this.View();
    }

    public ActionResult M2ost_category_mapping_edit(int id_brief)
    {
      List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
      tbl_brief_master tblBriefMaster = new tbl_brief_master();
      tbl_brief_m2ost_category_mapping model = new tbl_brief_m2ost_category_mapping();
      List<tbl_category_heading> tblCategoryHeadingList = new List<tbl_category_heading>();
      tbl_category_heading tblCategoryHeading = new tbl_category_heading();
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        model = m2ostDbContext.Database.SqlQuery<tbl_brief_m2ost_category_mapping>("SELECT * FROM tbl_brief_m2ost_category_mapping WHERE id_brief =" + (object) id_brief + " AND status='A'").FirstOrDefault<tbl_brief_m2ost_category_mapping>();
        tblBriefMaster = m2ostDbContext.Database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master as t1 inner join tbl_brief_m2ost_category_mapping as t2 where t1.id_brief_master = t2.id_brief and t2.id_brief =" + (object) id_brief ?? "").FirstOrDefault<tbl_brief_master>();
        tblBriefMasterList = m2ostDbContext.Database.SqlQuery<tbl_brief_master>("SELECT * FROM tbl_brief_master where  ID_ORGANIZATION=" + (object) int32 + " AND status='A'  order by brief_title ").ToList<tbl_brief_master>();
      }
      using (m2ostDBContextNew m2ostDbContextNew = new m2ostDBContextNew())
      {
        tblCategoryHeadingList = m2ostDbContextNew.Database.SqlQuery<tbl_category_heading>("SELECT * FROM tbl_category_heading AS t1 INNER JOIN tbl_category_tiles AS t2 ON t2.id_category_tiles = t1.id_category_tiles AND t2.id_organization IN (" + this.m2ostoid + ") AND t2.status = 'A' WHERE t1.status = 'A' ORDER BY t1.heading_order").ToList<tbl_category_heading>();
        tblCategoryHeading = m2ostDbContextNew.Database.SqlQuery<tbl_category_heading>("select * from  tbl_category_heading as t1 inner join tbl_brief_m2ost_category_mapping as t2 where t1.id_category_heading = t2.id_category_heading and t2.id_brief =" + (object) id_brief ?? "").FirstOrDefault<tbl_category_heading>();
      }
      this.ViewData["brief"] = (object) tblBriefMasterList;
      this.ViewData["header"] = (object) tblCategoryHeadingList;
      this.ViewData["briefdata"] = (object) tblBriefMaster;
      this.ViewData["header1"] = (object) tblCategoryHeading;
      this.ViewData["map"] = (object) model;
      return (ActionResult) this.View((object) model);
    }

    public ActionResult M2ost_category_mapping_edit_action()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(this.Request.Form["id_brief"]);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_brief_m2ost_category_mapping SET status = 'D' WHERE id_brief =" + (object) int32_2 ?? "");
      tbl_brief_m2ost_category_mapping m2ostCategoryMapping = new tbl_brief_m2ost_category_mapping();
      if (Convert.ToInt32(this.Request.Form["t2-select-order"]) == 1)
      {
        m2ostCategoryMapping.id_brief = Convert.ToInt32(this.Request.Form["select-category"]);
        m2ostCategoryMapping.type = 1;
        m2ostCategoryMapping.id_org = int32_1;
        m2ostCategoryMapping.URL = this.Request.Form["typeurl"];
        m2ostCategoryMapping.status = "A";
        m2ostCategoryMapping.updated_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_brief_m2ost_category_mapping(id_brief,id_category,id_org,status,type,URL,id_category_heading,updated_date_time)values({0},{1},{2},{3},{4},{5},{6},{7})", (object) m2ostCategoryMapping.id_brief, (object) m2ostCategoryMapping.id_category, (object) m2ostCategoryMapping.id_org, (object) m2ostCategoryMapping.status, (object) m2ostCategoryMapping.type, (object) m2ostCategoryMapping.URL, (object) m2ostCategoryMapping.id_category_heading, (object) m2ostCategoryMapping.updated_date_time);
      }
      else
      {
        for (int index = 1; index <= 10; ++index)
        {
          if (this.Request.Form["row-qtn-list-" + (object) index] != null)
          {
            int int32_3 = Convert.ToInt32(this.Request.Form["row-qtn-list-" + (object) index]);
            m2ostCategoryMapping.id_brief = Convert.ToInt32(this.Request.Form["select-category"]);
            m2ostCategoryMapping.id_category_heading = Convert.ToInt32(this.Request.Form["t3-select-order"]);
            m2ostCategoryMapping.type = 2;
            m2ostCategoryMapping.id_category = int32_3;
            m2ostCategoryMapping.id_org = int32_1;
            m2ostCategoryMapping.status = "A";
            m2ostCategoryMapping.updated_date_time = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
              m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_brief_m2ost_category_mapping(id_brief,id_category,id_org,status,type,URL,id_category_heading,updated_date_time)values({0},{1},{2},{3},{4},{5},{6},{7})", (object) m2ostCategoryMapping.id_brief, (object) m2ostCategoryMapping.id_category, (object) m2ostCategoryMapping.id_org, (object) m2ostCategoryMapping.status, (object) m2ostCategoryMapping.type, (object) m2ostCategoryMapping.URL, (object) m2ostCategoryMapping.id_category_heading, (object) m2ostCategoryMapping.updated_date_time);
          }
        }
      }
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM tbl_brief_m2ost_category_mapping WHERE status='D'");
      return (ActionResult) this.RedirectToAction("M2ost_category_mapping_dashbord");
    }

    public string deactivateSeminar(string id_brief)
    {
      int int32 = Convert.ToInt32(id_brief);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("update tbl_brief_m2ost_category_mapping set status='D' where id_brief={0}", (object) int32);
      return "";
    }
  }
}
