// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.UniversityKPIController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class UniversityKPIController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult KPIDashboard() => (ActionResult) this.View();

    public ActionResult AddKPI()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      return (ActionResult) this.View();
    }

    public ActionResult AddKPIAction()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      return (ActionResult) this.View();
    }

    public ActionResult Kpi_dashboard()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      this.ViewData["kpiMaster"] = (object) new RoadmapLogic().getkpiList(int32);
      return (ActionResult) this.View();
    }

    public ActionResult university_kpi_add()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(content.ID_USER);
      try
      {
        new RoadmapLogic().CreateKpi(int32_2, int32_1, this.Request.Form["kpi-title"].ToString(), this.Request.Form["kpi-desc"].ToString(), "MKPI" + DateTime.Now.ToString("yyyyMMddHHmmss"), Convert.ToInt32(this.Request.Form["kpi-type"].ToString()), DateTime.Now, DateTime.Now, "A", DateTime.Now);
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      return (ActionResult) this.RedirectToAction("Kpi_dashboard", "UniversityKPI");
    }

    public ActionResult University_KPIGrid(string kpid, int flag)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      tbl_kpi_master tblKpiMaster = this.db.tbl_kpi_master.SqlQuery("select * from tbl_kpi_master where id_organization=" + (object) int32 + " and KPIID ='" + kpid + "'").FirstOrDefault<tbl_kpi_master>();
      if (tblKpiMaster != null)
      {
        List<tbl_kpi_grid> list = this.db.tbl_kpi_grid.SqlQuery("select * from tbl_kpi_grid where id_kpi_master=" + (object) tblKpiMaster.id_kpi_master ?? "").ToList<tbl_kpi_grid>();
        this.ViewData["master"] = (object) tblKpiMaster;
        this.ViewData["grids"] = (object) list;
      }
      this.ViewData["master"] = (object) tblKpiMaster;
      this.ViewData[nameof (flag)] = (object) flag;
      return (ActionResult) this.View();
    }

    public bool CategValidation(int acad, int categ)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      return new RoadmapLogic().CheckCategValid(acad, categ, int32);
    }

    public ActionResult League_Configuration() => (ActionResult) this.View();

    public JsonResult ThemeValueGet(int id_theme) => this.Json((object) new RoadmapLogic().themdetail(id_theme), JsonRequestBehavior.AllowGet);

    public ActionResult Game_creation()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int OID = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_university_kpi_master> kpiName = new RoadmapLogic().Get_KPi_Name(OID);
      List<tbl_special_metric_master> specialMet = new RoadmapLogic().Get_SpecialMet(OID);
      List<tbl_theme_master> tblThemeMasterList = new RoadmapLogic().themeOrg(OID);
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_ORGANIZATION == (int?) OID && t.STATUS == "A")).ToList<tbl_user>();
      this.ViewData["specialMet"] = (object) specialMet;
      this.ViewData["kpiName"] = (object) kpiName;
      this.ViewData["themeOrg"] = (object) tblThemeMasterList;
      return (ActionResult) this.View();
    }

    public JsonResult metricValue(int themVal) => this.Json((object) new RoadmapLogic().getMetricName(themVal), JsonRequestBehavior.AllowGet);

    public JsonResult BadgeName(int themVal, int id_game)
    {
      List<tbl_badge_master> badgeName = new RoadmapLogic().getBadgeName(themVal);
      List<tbl_badge_master> data = new List<tbl_badge_master>();
      List<tbl_badge_data> tblBadgeDataList = new List<tbl_badge_data>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblBadgeDataList = m2ostDbContext.Database.SqlQuery<tbl_badge_data>("select * from tbl_badge_data where id_game={0}", (object) id_game).ToList<tbl_badge_data>();
      foreach (tbl_badge_master tblBadgeMaster in badgeName)
      {
        int num = 0;
        foreach (tbl_badge_data tblBadgeData in tblBadgeDataList)
        {
          if (tblBadgeData.id_badge == tblBadgeMaster.id_badge)
            num = 1;
        }
        if (num == 0)
          data.Add(tblBadgeMaster);
      }
      return this.Json((object) data, JsonRequestBehavior.AllowGet);
    }

    public JsonResult LeagueName(int themId) => this.Json((object) new RoadmapLogic().themdetail(themId), JsonRequestBehavior.AllowGet);

    public string GetCurrency(int themVal, int id_game)
    {
      new RoadmapLogic().themdetail(themVal);
      List<tbl_currency_points> tblCurrencyPointsList = new RoadmapLogic().currencyDetail(themVal);
      List<tbl_badge_master> badgeName = new RoadmapLogic().getBadgeName(themVal);
      List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
      List<tbl_currency_data> tblCurrencyDataList = new List<tbl_currency_data>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblCurrencyDataList = m2ostDbContext.Database.SqlQuery<tbl_currency_data>("select * from tbl_currency_data where id_game={0}", (object) id_game).ToList<tbl_currency_data>();
      foreach (tbl_badge_master tblBadgeMaster in badgeName)
      {
        int num = 0;
        foreach (tbl_currency_data tblCurrencyData in tblCurrencyDataList)
        {
          if (tblCurrencyData.id_badge == tblBadgeMaster.id_badge)
            num = 1;
        }
        if (num == 0)
          tblBadgeMasterList.Add(tblBadgeMaster);
      }
      string currency = "";
      int num1 = 0;
      foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
      {
        currency = currency + "<tr class='drg1' style='background-color:rgb(41, 148, 211); color: white;'><td> <input type='checkbox' style='height: 18px; width: 18px;display:none' onclick='return false' id='cr_" + (object) num1 + "' name='chk_badge_cur' value=' " + (object) num1 + "'></td><td><input type='hidden' id='idcurrDat" + (object) num1 + "' name='idcurrDat" + (object) num1 + "' value='' /> " + tblBadgeMaster.badge_name + " <input type='hidden' name='currbadid" + (object) num1 + "'  value='" + (object) tblBadgeMaster.id_badge + "'></td><td><input style='color:black;' type='number' class='form-control select-control validate[required,custom[number]]' value='' placeholder='' id='CurrTxt" + (object) num1 + "' name='CurrncyVal" + (object) num1 + "'/></td><td><select id='currnDrop" + (object) num1 + "' name='currnDrop" + (object) num1 + "' class='form-control'><option value=''>select</option>";
        foreach (tbl_currency_points tblCurrencyPoints in tblCurrencyPointsList)
          currency = currency + "<option value='" + (object) tblCurrencyPoints.id_currency + "' > " + tblCurrencyPoints.currency_value + "</option>";
        currency += "</select></td></tr>";
        ++num1;
      }
      return currency;
    }

    public ActionResult Special_Points()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      this.ViewData["SpMetDrop"] = (object) new RoadmapLogic().getSpMetric(int32);
      return (ActionResult) this.View();
    }

    public ActionResult university_Special_Points_add()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(content.ID_USER);
      try
      {
        int id_creator = int32_2;
        int id_organization = int32_1;
        string kpi_name = this.Request.Form["SpMet-title"].ToString();
        string kpi_description = this.Request.Form["SpMet-desc"].ToString();
        string KPIID = "MKPI" + DateTime.Now.ToString("yyyyMMddHHmmss");
        int int32_3 = Convert.ToInt32(this.Request.Form["SpMet-type"].ToString());
        DateTime now1 = DateTime.Now;
        DateTime now2 = DateTime.Now;
        string stat = "A";
        DateTime now3 = DateTime.Now;
        int Id_them = 0;
        if (new RoadmapLogic().createSpecial_Points(id_creator, id_organization, kpi_name, kpi_description, KPIID, int32_3, now1, now2, stat, now3, Id_them))
        {
          int int32_4 = Convert.ToInt32(this.Request.Form["grid-value"].ToString());
          if (int32_3 == 1)
          {
            double num1 = Convert.ToDouble(this.Request.Form["OneCorrect"].ToString());
            int num2 = new RoadmapLogic().Special_master_inserted_id();
            int SpMet_value = 0;
            string SpMet_text = "";
            double start_range = num1;
            double end_range = 0.0;
            string status = "A";
            int special_metric = 0;
            DateTime now4 = DateTime.Now;
            int id_kpi_master = num2;
            new RoadmapLogic().Special_grid_insert(SpMet_value, SpMet_text, start_range, end_range, status, now4, id_kpi_master, special_metric);
          }
          else
          {
            for (int index = 1; index <= int32_4; ++index)
            {
              int num = new RoadmapLogic().Special_master_inserted_id();
              int int32_5 = Convert.ToInt32(this.Request.Form["SpMet-value-" + (object) index].ToString());
              string SpMet_text = this.Request.Form["SpText-" + (object) index].ToString();
              double start_range = Convert.ToDouble(this.Request.Form["SpMet-start-" + (object) index].ToString());
              double end_range = Convert.ToDouble(this.Request.Form["SpMet-end-" + (object) index].ToString());
              int int32_6 = Convert.ToInt32(this.Request.Form["MetGridDrop-" + (object) index].ToString());
              string status = "A";
              DateTime now5 = DateTime.Now;
              int id_kpi_master = num;
              new RoadmapLogic().Special_grid_insert(int32_5, SpMet_text, start_range, end_range, status, now5, id_kpi_master, int32_6);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      return (ActionResult) this.RedirectToAction("Special_Points", "UniversityKPI");
    }

    public ActionResult Metric_Config()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      this.ViewData["SpMetDetail"] = (object) new RoadmapLogic().getSpMetric(int32);
      return (ActionResult) this.View();
    }

    public ActionResult Special_metric_Add()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      new RoadmapLogic().SpecialMetricAdd(this.Request.Form["SpecialMet"].ToString(), this.Request.Form["SpecMetDesc"].ToString(), int32, "A", DateTime.Now);
      return (ActionResult) this.RedirectToAction("Metric_Config", "UniversityKPI");
    }

    public ActionResult Special_metric_Update()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      string name = this.Request.Form["editMetName"].ToString();
      string desc = this.Request.Form["editDescName"].ToString();
      int int32_2 = Convert.ToInt32(this.Request.Form["MetricID"].ToString());
      int id_org = int32_1;
      new RoadmapLogic().SpecialMetricUpdate(name, desc, id_org, int32_2);
      return (ActionResult) this.RedirectToAction("Metric_Config", "UniversityKPI");
    }

    public ActionResult Special_metric_Delete()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      new RoadmapLogic().SpecialMetricDelete(Convert.ToInt32(this.Request.Form["DelMetricID"].ToString()));
      return (ActionResult) this.RedirectToAction("Metric_Config", "UniversityKPI");
    }

    public ActionResult EditKPI(int id_kpi)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_brief_category> list = this.db.tbl_brief_category.SqlQuery("SELECT * FROM tbl_brief_category where id_organization=" + (object) int32 + " ").ToList<tbl_brief_category>();
      string str = "SELECT * FROM tbl_brief_category where id_organization=" + (object) int32 + " ";
      List<tbl_academic_tiles> tblAcademicTilesList = new RoadmapLogic().AcademicVal(int32);
      List<tbl_university_kpi_master> universityKpiMasterList = new RoadmapLogic().editKpi_master(id_kpi, int32);
      List<tbl_university_kpi_grid> universityKpiGridList = new RoadmapLogic().editKpi_grid(id_kpi);
      int num = 0;
      foreach (tbl_university_kpi_master universityKpiMaster in universityKpiMasterList)
        num = Convert.ToInt32(universityKpiMaster.kpi_type.ToString());
      this.ViewData[nameof (id_kpi)] = (object) id_kpi;
      this.ViewData["kpiTyp"] = (object) num;
      this.ViewData["editKpiMaster"] = (object) universityKpiMasterList;
      this.ViewData["editKpiGrid"] = (object) universityKpiGridList;
      this.ViewData["detail"] = (object) list;
      this.ViewData["academi"] = (object) tblAcademicTilesList;
      return (ActionResult) this.View();
    }

    public ActionResult university_kpi_edit()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(this.Request.Form["idKpiMs"].ToString());
      int int32_2 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_3 = Convert.ToInt32(content.ID_USER);
      try
      {
        int id_creator = int32_3;
        int id_organization = int32_2;
        string kpi_name = this.Request.Form["kpi-title"].ToString();
        string kpi_description = this.Request.Form["kpi-desc"].ToString();
        string KPIID = "MKPI" + DateTime.Now.ToString("yyyyMMddHHmmss");
        int int32_4 = Convert.ToInt32(this.Request.Form["kpi-type"].ToString());
        int int32_5 = Convert.ToInt32(this.Request.Form["idKpiMa"].ToString());
        DateTime now1 = DateTime.Now;
        DateTime now2 = DateTime.Now;
        string stat = "A";
        DateTime now3 = DateTime.Now;
        new RoadmapLogic().EditKpi(id_creator, id_organization, kpi_name, kpi_description, KPIID, int32_4, now1, now2, stat, now3, int32_5);
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      return (ActionResult) this.RedirectToAction("EditKPI", "UniversityKPI", (object) new
      {
        id_kpi = int32_1
      });
    }

    public ActionResult Special_points_dashboard()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      this.ViewData["SpMaster"] = (object) new RoadmapLogic().getSp_points(int32);
      return (ActionResult) this.View();
    }

    public ActionResult Special_Points_edit(int id_sp)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_special_metric_master> spMetric = new RoadmapLogic().getSpMetric(int32);
      List<tbl_university_special_points_master> spMas = new RoadmapLogic().getSpMas(id_sp);
      List<tbl_university_special_point_grid> specialPointGridList = new RoadmapLogic().getspGrid(id_sp);
      this.ViewData["spMas"] = (object) spMas;
      this.ViewData["spGrid"] = (object) specialPointGridList;
      this.ViewData["SpMetDrop"] = (object) spMetric;
      return (ActionResult) this.View();
    }

    public ActionResult university_Special_Points_edit()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(content.ID_USER);
      int int32_3 = Convert.ToInt32(this.Request.Form["IdSp"].ToString());
      try
      {
        int id_creator = int32_2;
        int id_organization = int32_1;
        string SpMet_name = this.Request.Form["SpMet-title"].ToString();
        string SpMet_description = this.Request.Form["SpMet-desc"].ToString();
        string SPIID = "MKPI" + DateTime.Now.ToString("yyyyMMddHHmmss");
        int int32_4 = Convert.ToInt32(this.Request.Form["SpMet-type"].ToString());
        DateTime now1 = DateTime.Now;
        DateTime now2 = DateTime.Now;
        string stat = "A";
        DateTime now3 = DateTime.Now;
        if (new RoadmapLogic().UpdateSpecial_Points(id_creator, id_organization, SpMet_name, SpMet_description, SPIID, int32_4, now1, now2, stat, now3, int32_3))
        {
          int int32_5 = Convert.ToInt32(this.Request.Form["grid-value"].ToString());
          if (int32_4 == 1)
          {
            double num = Convert.ToDouble(this.Request.Form["OneCorrect"].ToString());
            int int32_6 = Convert.ToInt32(this.Request.Form["idkpiMasOne"].ToString());
            int SpMet_value = 0;
            string SpMet_text = "";
            double start_range = num;
            double end_range = 0.0;
            string status = "A";
            int special_metric = 0;
            DateTime now4 = DateTime.Now;
            int id_kpi_master = int32_3;
            new RoadmapLogic().Special_grid_update(SpMet_value, SpMet_text, start_range, end_range, status, now4, id_kpi_master, special_metric, int32_6);
          }
          else
          {
            for (int index = 1; index <= int32_5; ++index)
            {
              int num = new RoadmapLogic().checkVaidSpId(Convert.ToInt32(this.Request.Form["IdSp"].ToString()));
              if (index - 1 < num)
              {
                int int32_7 = Convert.ToInt32(this.Request.Form["idKpiGri-" + (object) index].ToString());
                int int32_8 = Convert.ToInt32(this.Request.Form["SpMet-value-" + (object) index].ToString());
                string SpMet_text = this.Request.Form["SpText-" + (object) index].ToString();
                double start_range = Convert.ToDouble(this.Request.Form["SpMet-start-" + (object) index].ToString());
                double end_range = Convert.ToDouble(this.Request.Form["SpMet-end-" + (object) index].ToString());
                int int32_9 = Convert.ToInt32(this.Request.Form["MetGridDrop-" + (object) index].ToString());
                string status = "A";
                DateTime now5 = DateTime.Now;
                int id_kpi_master = int32_3;
                new RoadmapLogic().Special_grid_update(int32_8, SpMet_text, start_range, end_range, status, now5, id_kpi_master, int32_9, int32_7);
              }
              else
              {
                int int32_10 = Convert.ToInt32(this.Request.Form["SpMet-value-" + (object) index].ToString());
                string SpMet_text = this.Request.Form["SpText-" + (object) index].ToString();
                double start_range = Convert.ToDouble(this.Request.Form["SpMet-start-" + (object) index].ToString());
                double end_range = Convert.ToDouble(this.Request.Form["SpMet-end-" + (object) index].ToString());
                int int32_11 = Convert.ToInt32(this.Request.Form["MetGridDrop-" + (object) index].ToString());
                string status = "A";
                DateTime now6 = DateTime.Now;
                int id_kpi_master = int32_3;
                new RoadmapLogic().Special_grid_insert(int32_10, SpMet_text, start_range, end_range, status, now6, id_kpi_master, int32_11);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      return (ActionResult) this.RedirectToAction("Special_Points_edit", "UniversityKPI", (object) new
      {
        id_sp = int32_3
      });
    }

    public ActionResult deleteKpi()
    {
      new RoadmapLogic().delKpirow(Convert.ToInt32(this.Request.Form["DelKpiID"].ToString()));
      return (ActionResult) this.RedirectToAction("Kpi_dashboard", "UniversityKPI");
    }

    public JsonResult EditBadgeName(int themVal, int id_game)
    {
      List<tbl_badge_master> badgeName = new RoadmapLogic().getBadgeName(themVal);
      List<tbl_badge_master> data = new List<tbl_badge_master>();
      List<tbl_badge_data> tblBadgeDataList = new List<tbl_badge_data>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.SqlQuery<tbl_badge_data>("select * from tbl_badge_data where id_game={0}", (object) id_game).ToList<tbl_badge_data>();
      foreach (tbl_badge_master tblBadgeMaster in badgeName)
      {
        if (true)
          data.Add(tblBadgeMaster);
      }
      return this.Json((object) data, JsonRequestBehavior.AllowGet);
    }

    public string EditGetCurrency(int themVal, int id_game)
    {
      new RoadmapLogic().themdetail(themVal);
      List<tbl_currency_points> tblCurrencyPointsList = new RoadmapLogic().currencyDetail(themVal);
      List<tbl_badge_master> badgeName = new RoadmapLogic().getBadgeName(themVal);
      List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
      List<tbl_currency_data> tblCurrencyDataList = new List<tbl_currency_data>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.SqlQuery<tbl_currency_data>("select * from tbl_currency_data where id_game={0}", (object) id_game).ToList<tbl_currency_data>();
      foreach (tbl_badge_master tblBadgeMaster in badgeName)
      {
        if (true)
          tblBadgeMasterList.Add(tblBadgeMaster);
      }
      string currency = "";
      int num = 0;
      foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
      {
        currency = currency + "<tr class='drg1' style='background-color:rgb(41, 148, 211); color: white;'><td> <input type='checkbox' style='height: 18px; width: 18px;display:none' onclick='return false' id='cr_" + (object) num + "' name='chk_badge_cur' value=' " + (object) num + "'></td><td><input type='hidden' id='idcurrDat" + (object) num + "' name='idcurrDat" + (object) num + "' value='' /> " + tblBadgeMaster.badge_name + " <input type='hidden' name='currbadid" + (object) num + "'  value='" + (object) tblBadgeMaster.id_badge + "'></td><td><input style='color:black;' type='number' class='form-control select-control validate[required,custom[number]]' value='' placeholder='' id='CurrTxt" + (object) num + "' name='CurrncyVal" + (object) num + "'/></td><td><select id='currnDrop" + (object) num + "' name='currnDrop" + (object) num + "' class='form-control'><option value=''>select</option>";
        foreach (tbl_currency_points tblCurrencyPoints in tblCurrencyPointsList)
          currency = currency + "<option value='" + (object) tblCurrencyPoints.id_currency + "' > " + tblCurrencyPoints.currency_value + "</option>";
        currency += "</select></td></tr>";
        ++num;
      }
      return currency;
    }

    public ActionResult Academic_level_restriction()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblAcademicTilesList = m2ostDbContext.Database.SqlQuery<tbl_academic_tiles>("select * from tbl_academic_tiles where status='A' and id_org={0}", (object) int32).ToList<tbl_academic_tiles>();
      this.ViewData["tblAcaTiles"] = (object) tblAcademicTilesList;
      return (ActionResult) this.View();
    }

    public string getacademicVal(int acadId)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblAcademicTilesList = m2ostDbContext.Database.SqlQuery<tbl_academic_tiles>("select * from tbl_academic_tiles where status='A' and id_org={0} and id_academic_tile={1}", (object) int32, (object) acadId).ToList<tbl_academic_tiles>();
      string str = "";
      if (tblAcademicTilesList.Count > 0)
      {
        List<tbl_academy_level_brief_restriction> briefRestrictionList = new List<tbl_academy_level_brief_restriction>();
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          briefRestrictionList = m2ostDbContext.Database.SqlQuery<tbl_academy_level_brief_restriction>("select * from tbl_academy_level_brief_restriction where status='A' and id_academy={0}", (object) acadId).ToList<tbl_academy_level_brief_restriction>();
        if (briefRestrictionList.Count > 0)
        {
          List<tbl_academy_level_brief_restriction_count> restrictionCountList = new List<tbl_academy_level_brief_restriction_count>();
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            restrictionCountList = m2ostDbContext.Database.SqlQuery<tbl_academy_level_brief_restriction_count>("select t1.*,t2.brief_count,t2.time from tbl_academic_tiles t1 inner join tbl_academy_level_brief_restriction t2 on t2.id_academy=t1.id_academic_tile where t1.id_academic_tile={0}", (object) acadId).ToList<tbl_academy_level_brief_restriction_count>();
          foreach (tbl_academy_level_brief_restriction_count restrictionCount in restrictionCountList)
          {
            str = str + "<input type='hidden' name='dayres' id='dayres' value='" + (object) restrictionCount.time + "' />";
            str = str + "<tr><td><input type='hidden' name='acadmHid' value='" + (object) restrictionCount.id_academic_tile + "'/>" + restrictionCount.tile_name + "</td><td><input type='text' class='form-control validate[required]' name='acadTileCouV' value='" + (object) restrictionCount.brief_count + "' /></td></tr>";
          }
        }
        else
        {
          str += "<input type='hidden' name='dayres' id='dayres' value='0' />";
          foreach (tbl_academic_tiles tblAcademicTiles in tblAcademicTilesList)
            str = str + "<tr><td><input type='hidden' name='acadmHid' value='" + (object) tblAcademicTiles.id_academic_tile + "'/>" + tblAcademicTiles.tile_name + "</td><td><input type='text' class='form-control validate[required]' name='acadTileCouV' /></td></tr>";
        }
      }
      return str;
    }

    public string getBriefTileVal(int acadId)
    {
      string briefTileVal = "";
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_brief_category_tile> briefCategoryTileList = new List<tbl_brief_category_tile>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        briefCategoryTileList = m2ostDbContext.Database.SqlQuery<tbl_brief_category_tile>("select t2.* from tbl_brief_tile_academic_mapping t1 inner join tbl_brief_category_tile t2 on t2.id_brief_category_tile=t1.id_journey_tile where t2.status='A' and t1.id_academic_tile={0} order by id_brief_category_tile", (object) acadId).ToList<tbl_brief_category_tile>();
      List<tbl_brief_tile_level_brief_restriction_count> restrictionCountList = new List<tbl_brief_tile_level_brief_restriction_count>();
      int num = 0;
      if (briefCategoryTileList.Count > 0)
      {
        briefTileVal = briefTileVal + "<input type='hidden' id='briefTileCountval' value='" + (object) briefCategoryTileList.Count + "' name='briefTileCountval' />";
        foreach (tbl_brief_category_tile briefCategoryTile in briefCategoryTileList)
        {
          List<tbl_brief_tile_level_brief_restriction> briefRestrictionList = new List<tbl_brief_tile_level_brief_restriction>();
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            briefRestrictionList = m2ostDbContext.Database.SqlQuery<tbl_brief_tile_level_brief_restriction>("select * from tbl_brief_tile_level_brief_restriction where id_academy={0} and id_brief_tile={1}", (object) acadId, (object) briefCategoryTile.id_brief_category_tile).ToList<tbl_brief_tile_level_brief_restriction>();
          if (briefRestrictionList.Count > 0)
          {
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
              restrictionCountList = m2ostDbContext.Database.SqlQuery<tbl_brief_tile_level_brief_restriction_count>("select t2.*,t1.category_tile from tbl_brief_category_tile t1 inner join tbl_brief_tile_level_brief_restriction t2 on t2.id_brief_tile=t1.id_brief_category_tile where id_academy={0} and id_brief_tile={1}", (object) acadId, (object) briefCategoryTile.id_brief_category_tile).ToList<tbl_brief_tile_level_brief_restriction_count>();
            foreach (tbl_brief_tile_level_brief_restriction_count restrictionCount in restrictionCountList)
              briefTileVal = briefTileVal + "<tr><td><input type='hidden' name='breifHid-" + (object) num + "' value='" + (object) restrictionCount.id_brief_tile + "' /> " + restrictionCount.category_tile + " </td><td><input type='text' class='form-control validate[required]' name='briefTileCouV-" + (object) num + "' value='" + (object) restrictionCount.brief_count + "' /></td></tr>";
          }
          else
            briefTileVal = briefTileVal + "<tr><td><input type='hidden' name='breifHid-" + (object) num + "' value='" + (object) briefCategoryTile.id_brief_category_tile + "' /> " + briefCategoryTile.category_tile + " </td><td><input type='text' class='form-control validate[required]' name='briefTileCouV-" + (object) num + "' /></td></tr>";
          ++num;
        }
      }
      return briefTileVal;
    }

    public ActionResult academiclevelRes()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int int32_2 = Convert.ToInt32(this.Request.Form["academicDrop"].ToString());
      int int32_3 = Convert.ToInt32(this.Request.Form["timeStam"].ToString().ToString());
      if (this.Request.Form["academCheck"] != null)
      {
        string str = this.Request.Form["acadTileCouV"].ToString();
        string status = "A";
        DateTime now = DateTime.Now;
        List<tbl_academy_level_brief_restriction> briefRestrictionList = new List<tbl_academy_level_brief_restriction>();
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          briefRestrictionList = m2ostDbContext.Database.SqlQuery<tbl_academy_level_brief_restriction>("select * from tbl_academy_level_brief_restriction where status='A' and id_academy={0}", (object) int32_2).ToList<tbl_academy_level_brief_restriction>();
        if (!string.IsNullOrEmpty(str) && str != "0")
        {
          int int32_4 = Convert.ToInt32(str);
          if (briefRestrictionList.Count > 0)
            new RoadmapLogic().academrestUpdat(int32_2, int32_1, int32_4, now, status, int32_3);
          else
            new RoadmapLogic().academrestInser(int32_2, int32_1, int32_4, now, status, int32_3);
        }
        else if (briefRestrictionList.Count > 0)
          this.db.Database.ExecuteSqlCommand("delete from tbl_academy_level_brief_restriction where id_academy={0}", (object) int32_2);
      }
      if (this.Request.Form["briefCheck"] != null)
      {
        int int32_5 = Convert.ToInt32(this.Request.Form["briefTileCountval"].ToString());
        for (int index = 0; index < int32_5; ++index)
        {
          int int32_6 = Convert.ToInt32(this.Request.Form["breifHid-" + (object) index].ToString());
          string str = this.Request.Form["briefTileCouV-" + (object) index].ToString();
          string status = "A";
          DateTime now = DateTime.Now;
          if (!string.IsNullOrEmpty(str) && str != "0")
          {
            int int32_7 = Convert.ToInt32(str);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
              int idbrief = m2ostDbContext.Database.SqlQuery<int>("select id_restriction from tbl_brief_tile_level_brief_restriction where id_brief_tile={0} and id_academy={1}", (object) int32_6, (object) int32_2).FirstOrDefault<int>();
              if (idbrief > 0)
                new RoadmapLogic().briefrestUpdat(int32_6, int32_1, int32_7, now, status, int32_3, int32_2, idbrief);
              else
                new RoadmapLogic().briefrestInser(int32_6, int32_1, int32_7, now, status, int32_3, int32_2);
            }
          }
          else
          {
            int num = 0;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
              num = m2ostDbContext.Database.SqlQuery<int>("select id_restriction from tbl_brief_tile_level_brief_restriction where id_academy={0} and id_brief_tile={1}", (object) int32_2, (object) int32_6).FirstOrDefault<int>();
            if (num > 0)
              this.db.Database.ExecuteSqlCommand("DELETE FROM tbl_brief_tile_level_brief_restriction WHERE id_restriction ={0}", (object) num);
          }
        }
      }
      return (ActionResult) this.RedirectToAction("Academic_level_restriction", "UniversityKPI");
    }

    public ActionResult academic_level_dashboard()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_academy_level_brief_restriction_count> restrictionCountList = new List<tbl_academy_level_brief_restriction_count>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        restrictionCountList = m2ostDbContext.Database.SqlQuery<tbl_academy_level_brief_restriction_count>("select t1.*,t2.brief_count,t2.time from tbl_academic_tiles t1 inner join tbl_academy_level_brief_restriction t2 on t2.id_academy=t1.id_academic_tile where t1.status='A' and t1.id_org = {0}", (object) int32).ToList<tbl_academy_level_brief_restriction_count>();
      this.ViewData["tblacaDash"] = (object) restrictionCountList;
      return (ActionResult) this.View();
    }

    public ActionResult brief_level_dashboard()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_brief_tile_level_brief_restriction_count> restrictionCountList = new List<tbl_brief_tile_level_brief_restriction_count>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        restrictionCountList = m2ostDbContext.Database.SqlQuery<tbl_brief_tile_level_brief_restriction_count>("select t2.*,t3.tile_name,t1.category_tile from tbl_brief_category_tile t1 inner join tbl_brief_tile_level_brief_restriction t2 on t2.id_brief_tile = t1.id_brief_category_tile inner join tbl_academic_tiles t3 on t2.id_academy = t3.id_academic_tile where t2.status = 'A' and t2.OID = {0}", (object) int32).ToList<tbl_brief_tile_level_brief_restriction_count>();
      this.ViewData["tblbrefdash"] = (object) restrictionCountList;
      return (ActionResult) this.View();
    }

    public ActionResult delete_academy_res(int idacad)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM tbl_academy_level_brief_restriction WHERE id_academy ={0}", (object) idacad);
      return (ActionResult) this.RedirectToAction("academic_level_dashboard", "UniversityKPI");
    }

    public ActionResult delete_Brief_res(int idacad, int idbrief)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM tbl_brief_tile_level_brief_restriction WHERE id_academy ={0} and id_brief_tile={1}", (object) idacad, (object) idbrief);
      return (ActionResult) this.RedirectToAction("brief_level_dashboard", "UniversityKPI");
    }

    public ActionResult content_notification()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblAcademicTilesList = m2ostDbContext.Database.SqlQuery<tbl_academic_tiles>("select * from tbl_academic_tiles where status='A' and id_org={0}", (object) int32).ToList<tbl_academic_tiles>();
      this.ViewData["tblAcaTiles"] = (object) tblAcademicTilesList;
      return (ActionResult) this.View();
    }

    public JsonResult getBriefCatTileVal(int acadId)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_brief_category_tile> data = new List<tbl_brief_category_tile>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        data = m2ostDbContext.Database.SqlQuery<tbl_brief_category_tile>("select t2.* from tbl_brief_tile_academic_mapping t1 inner join tbl_brief_category_tile t2 on t2.id_brief_category_tile=t1.id_journey_tile where t2.status='A' and t1.id_academic_tile={0} order by id_brief_category_tile", (object) acadId).ToList<tbl_brief_category_tile>();
      return this.Json((object) data, JsonRequestBehavior.AllowGet);
    }

    public ActionResult insert_Content_Notification()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      tbl_content_notification_master notificationMaster = new tbl_content_notification_master();
      notificationMaster.id_academic_tile = Convert.ToInt32(this.Request.Form["academicDrop"].ToString());
      notificationMaster.id_brief_category_tile = Convert.ToInt32(this.Request.Form["BriefTilDrop"].ToString());
      notificationMaster.notifcation_title = this.Request.Form["notTitle"].ToString();
      notificationMaster.notification_message = this.Request.Form["notMsg"].ToString();
      notificationMaster.status = "A";
      notificationMaster.updated_datetime = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.SqlQuery<int>("INSERT INTO `tbl_content_notification_master` (`id_academic_tile`,`id_brief_category_tile`,`notifcation_title`,`notification_message`,`status`,`updated_datetime`) VALUES ({0},{1},{2},{3},{4},{5});select last_insert_id();", (object) notificationMaster.id_academic_tile, (object) notificationMaster.id_brief_category_tile, (object) notificationMaster.notifcation_title, (object) notificationMaster.notification_message, (object) notificationMaster.status, (object) notificationMaster.updated_datetime).FirstOrDefault<int>();
      List<tbl_profile> tblProfileList = new List<tbl_profile>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblProfileList = m2ostDbContext.Database.SqlQuery<tbl_profile>("select t1.* from tbl_profile t1 inner join tbl_user t2 on t1.ID_USER=t2.ID_USER where status='A' and t2.ID_ORGANIZATION = {0} group by t1.ID_USER", (object) int32).ToList<tbl_profile>();
      foreach (tbl_profile tblProfile in tblProfileList)
      {
        List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
        string str1 = "";
        string str2 = "";
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          tblUserGcmLogList = m2ostDbContext.Database.SqlQuery<tbl_user_gcm_log>("SELECT * FROM tbl_user_gcm_log where status='A' and id_user ={0}", (object) tblProfile.ID_USER).ToList<tbl_user_gcm_log>();
          str1 = m2ostDbContext.Database.SqlQuery<string>("select tile_name from tbl_academic_tiles where id_academic_tile={0}", (object) notificationMaster.id_academic_tile).FirstOrDefault<string>();
          str2 = m2ostDbContext.Database.SqlQuery<string>("select category_tile from tbl_brief_category_tile where id_brief_category_tile = {0}", (object) notificationMaster.id_brief_category_tile).FirstOrDefault<string>();
        }
        foreach (tbl_user_gcm_log tblUserGcmLog in tblUserGcmLogList)
        {
          string url_str = " ";
          string appSetting = WebConfigurationManager.AppSettings["cont_notif"];
          string message = notificationMaster.notification_message + " You can find the brief in " + str1 + " under " + str2;
          this.SendNotification(tblUserGcmLog.GCMID, message, notificationMaster.notifcation_title, appSetting, url_str);
        }
      }
      return (ActionResult) this.RedirectToAction("content_notification");
    }

    public string SendNotification(
      string deviceRegIds,
      string message,
      string head,
      string noType,
      string url_str = " ")
    {
      string str1 = "";
      try
      {
        string str2 = "AAAAGrnsAbc:APA91bH3oHyM5R0KrFxEexkW-DmnOr5HD1oyKmsmP_nlUjNEdlmAUu1ZF7YuD3y8NGmMx_760dgynH1hYw603TN7CAnpgD4yp59dUFOMi198H42RweTvKHYEwfVzdusHMMZuKnRvjXUW";
        string str3 = "114788401591";
        WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        webRequest.Method = "post";
        webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str2));
        webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str3));
        webRequest.ContentType = "application/json";
        byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) new
        {
          to = deviceRegIds,
          priority = "high",
          content_available = true,
          notification = new
          {
            body = message,
            title = noType,
            icon = url_str,
            badge = 1,
            image = ""
          }
        }).ToString());
        webRequest.ContentLength = (long) bytes.Length;
        using (Stream requestStream = webRequest.GetRequestStream())
        {
          requestStream.Write(bytes, 0, bytes.Length);
          using (WebResponse response = webRequest.GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
            {
              if (responseStream != null)
              {
                using (StreamReader streamReader = new StreamReader(responseStream))
                  streamReader.ReadToEnd();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Stream responseStream = ((WebException) ex).Response.GetResponseStream();
        string str4 = "";
        int num;
        do
        {
          num = responseStream.ReadByte();
          str4 += ((char) num).ToString();
        }
        while (num != -1);
        responseStream.Close();
        str1 = str4;
      }
      return str1;
    }

    public ActionResult badge_won_messgage()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_game_master> tblGameMasterList = new List<tbl_game_master>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblGameMasterList = m2ostDbContext.Database.SqlQuery<tbl_game_master>("select * from tbl_game_master where status='A' and id_org={0}", (object) int32).ToList<tbl_game_master>();
      this.ViewData["tblGame"] = (object) tblGameMasterList;
      return (ActionResult) this.View();
    }

    public string getBadgeMas(int idGame)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int num1 = 0;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        num1 = m2ostDbContext.Database.SqlQuery<int>("select id_theme from tbl_game_master where id_game={0}", (object) idGame).FirstOrDefault<int>();
      List<tbl_badge_won_message_output> wonMessageOutputList = new List<tbl_badge_won_message_output>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        wonMessageOutputList = m2ostDbContext.Database.SqlQuery<tbl_badge_won_message_output>("select t1.id_badge,t1.message,t2.badge_name,t1.id_badge from tbl_badge_won_message t1 inner join tbl_badge_master t2 on t2.id_badge=t1.id_badge where t2.status='A' and t1.id_game={0}", (object) idGame).ToList<tbl_badge_won_message_output>();
      string str = "";
      int num2 = 1;
      string badgeMas;
      if (wonMessageOutputList.Count > 0)
      {
        badgeMas = str + "<input type='hidden' name='countBad' value='" + (object) wonMessageOutputList.Count + "'/>";
        foreach (tbl_badge_won_message_output wonMessageOutput in wonMessageOutputList)
        {
          badgeMas = badgeMas + "<div class='form-group'><div class='col-md-3'><label class='control-label'>" + wonMessageOutput.badge_name + "</label></div><div class='col-md-5'><input type='hidden' name='badgeID-" + (object) num2 + "' value='" + (object) wonMessageOutput.id_badge + "'/> <input class='form-control validate[required]' type='text' name='badgeName-" + (object) num2 + "' value='" + wonMessageOutput.message + "' required /></div><div class='col-md-4'></div></div>";
          ++num2;
        }
      }
      else
      {
        List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          tblBadgeMasterList = m2ostDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where status='A' and id_theme={0}", (object) num1).ToList<tbl_badge_master>();
        badgeMas = str + "<input type='hidden' name='countBad' value='" + (object) tblBadgeMasterList.Count + "'/>";
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
        {
          badgeMas = badgeMas + "<div class='form-group'><div class='col-md-3'><label class='control-label'>" + tblBadgeMaster.badge_name + "</label></div><div class='col-md-5'><input type='hidden' name='badgeID-" + (object) num2 + "' value='" + (object) tblBadgeMaster.id_badge + "'/> <input class='form-control validate[required]' type='text' name='badgeName-" + (object) num2 + "' value='' required /></div><div class='col-md-4'></div></div>";
          ++num2;
        }
      }
      return badgeMas;
    }

    public ActionResult insert_badge_won_message()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int int32_1 = Convert.ToInt32(this.Request.Form["gameName"]);
      List<tbl_badge_won_message_output> wonMessageOutputList = new List<tbl_badge_won_message_output>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        wonMessageOutputList = m2ostDbContext.Database.SqlQuery<tbl_badge_won_message_output>("select t1.id_badge,t1.id_message,t2.badge_name,t1.id_badge from tbl_badge_won_message t1 inner join tbl_badge_master t2 on t2.id_badge=t1.id_badge where t2.status='A' and t1.id_game={0}", (object) int32_1).ToList<tbl_badge_won_message_output>();
      if (wonMessageOutputList.Count > 0)
      {
        int int32_2 = Convert.ToInt32(this.Request.Form["countBad"].ToString());
        for (int index = 1; index <= int32_2; ++index)
        {
          tbl_badge_won_message tblBadgeWonMessage = new tbl_badge_won_message();
          tblBadgeWonMessage.id_game = int32_1;
          tblBadgeWonMessage.message = this.Request.Form["badgeName-" + (object) index].ToString();
          tblBadgeWonMessage.id_badge = Convert.ToInt32(this.Request.Form["badgeID-" + (object) index].ToString());
          tblBadgeWonMessage.status = "A";
          tblBadgeWonMessage.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_badge_won_message` SET `id_game` = {0},`message` = {1},`status` = {2},`updated_date_time` = {3},`id_badge` = {4} WHERE id_game ={0} and id_badge = {4}", (object) tblBadgeWonMessage.id_game, (object) tblBadgeWonMessage.message, (object) tblBadgeWonMessage.status, (object) tblBadgeWonMessage.updated_date_time, (object) tblBadgeWonMessage.id_badge);
        }
      }
      else
      {
        int int32_3 = Convert.ToInt32(this.Request.Form["countBad"].ToString());
        for (int index = 1; index <= int32_3; ++index)
        {
          tbl_badge_won_message tblBadgeWonMessage = new tbl_badge_won_message();
          tblBadgeWonMessage.id_game = int32_1;
          tblBadgeWonMessage.message = this.Request.Form["badgeName-" + (object) index].ToString();
          tblBadgeWonMessage.id_badge = Convert.ToInt32(this.Request.Form["badgeID-" + (object) index]);
          tblBadgeWonMessage.status = "A";
          tblBadgeWonMessage.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_badge_won_message` (`id_game`,`message`,`status`,`updated_date_time`,`id_badge`) VALUES ({0},{1},{2},{3},{4})", (object) tblBadgeWonMessage.id_game, (object) tblBadgeWonMessage.message, (object) tblBadgeWonMessage.status, (object) tblBadgeWonMessage.updated_date_time, (object) tblBadgeWonMessage.id_badge);
        }
      }
      return (ActionResult) this.RedirectToAction("badge_won_messgage");
    }

    public ActionResult level_up_message()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_game_master> tblGameMasterList = new List<tbl_game_master>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblGameMasterList = m2ostDbContext.Database.SqlQuery<tbl_game_master>("select * from tbl_game_master where status='A' and id_org={0}", (object) int32).ToList<tbl_game_master>();
      this.ViewData["tblGame"] = (object) tblGameMasterList;
      return (ActionResult) this.View();
    }

    public string getThemeLeaq(int idGame)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int num1 = 0;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        num1 = m2ostDbContext.Database.SqlQuery<int>("select id_theme from tbl_game_master where id_game={0}", (object) idGame).FirstOrDefault<int>();
      List<tbl_league_message_output> leagueMessageOutputList = new List<tbl_league_message_output>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        leagueMessageOutputList = m2ostDbContext.Database.SqlQuery<tbl_league_message_output>("select t1.id_league,t1.message,t2.league_name from tbl_league_message t1 inner join tbl_theme_leagues t2 on t2.id_league=t1.id_league where t2.status='A' and t1.id_game={0}", (object) idGame).ToList<tbl_league_message_output>();
      string str = "";
      int num2 = 1;
      string themeLeaq;
      if (leagueMessageOutputList.Count > 0)
      {
        themeLeaq = str + "<input type='hidden' name='countleague' value='" + (object) leagueMessageOutputList.Count + "'/>";
        foreach (tbl_league_message_output leagueMessageOutput in leagueMessageOutputList)
        {
          themeLeaq = themeLeaq + "<div class='form-group'><div class='col-md-3'><label class='control-label'>" + leagueMessageOutput.league_name + "</label></div><div class='col-md-5'><input type='hidden' name='leagueID-" + (object) num2 + "' value='" + (object) leagueMessageOutput.id_league + "'/> <input class='form-control validate[required]' type='text' name='leagueName-" + (object) num2 + "' value='" + leagueMessageOutput.message + "' required /></div><div class='col-md-4'></div></div>";
          ++num2;
        }
      }
      else
      {
        List<tbl_theme_leagues> tblThemeLeaguesList = new List<tbl_theme_leagues>();
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          tblThemeLeaguesList = m2ostDbContext.Database.SqlQuery<tbl_theme_leagues>("select * from tbl_theme_leagues where status='A' and id_theme={0}", (object) num1).ToList<tbl_theme_leagues>();
        themeLeaq = str + "<input type='hidden' name='countleague' value='" + (object) tblThemeLeaguesList.Count + "'/>";
        foreach (tbl_theme_leagues tblThemeLeagues in tblThemeLeaguesList)
        {
          themeLeaq = themeLeaq + "<div class='form-group'><div class='col-md-3'><label class='control-label'>" + tblThemeLeagues.league_name + "</label></div><div class='col-md-5'><input type='hidden' name='leagueID-" + (object) num2 + "' value='" + (object) tblThemeLeagues.id_league + "'/> <input class='form-control validate[required]' type='text' name='leagueName-" + (object) num2 + "' value='' required /></div><div class='col-md-4'></div></div>";
          ++num2;
        }
      }
      return themeLeaq;
    }

    public ActionResult insert_level_up_message()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int int32_1 = Convert.ToInt32(this.Request.Form["gameName"]);
      List<tbl_league_message_output> leagueMessageOutputList = new List<tbl_league_message_output>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        leagueMessageOutputList = m2ostDbContext.Database.SqlQuery<tbl_league_message_output>("select t1.id_league,t1.message,t2.league_name from tbl_league_message t1 inner join tbl_theme_leagues t2 on t2.id_league=t1.id_league where t2.status='A' and t1.id_game={0}", (object) int32_1).ToList<tbl_league_message_output>();
      if (leagueMessageOutputList.Count > 0)
      {
        int int32_2 = Convert.ToInt32(this.Request.Form["countleague"].ToString());
        for (int index = 1; index <= int32_2; ++index)
        {
          tbl_league_message tblLeagueMessage = new tbl_league_message();
          tblLeagueMessage.id_game = int32_1;
          tblLeagueMessage.message = this.Request.Form["leagueName-" + (object) index].ToString();
          tblLeagueMessage.id_league = Convert.ToInt32(this.Request.Form["leagueID-" + (object) index].ToString());
          tblLeagueMessage.status = "A";
          tblLeagueMessage.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_league_message` SET `id_game` = {0},`message` = {1},`status` = {2},`updated_date_time` = {3},`id_league` = {4} WHERE id_game ={0} and id_league = {4}", (object) tblLeagueMessage.id_game, (object) tblLeagueMessage.message, (object) tblLeagueMessage.status, (object) tblLeagueMessage.updated_date_time, (object) tblLeagueMessage.id_league);
        }
      }
      else
      {
        int int32_3 = Convert.ToInt32(this.Request.Form["countleague"].ToString());
        for (int index = 1; index <= int32_3; ++index)
        {
          tbl_league_message tblLeagueMessage = new tbl_league_message();
          tblLeagueMessage.id_game = int32_1;
          tblLeagueMessage.message = this.Request.Form["leagueName-" + (object) index].ToString();
          tblLeagueMessage.id_league = Convert.ToInt32(this.Request.Form["leagueID-" + (object) index]);
          tblLeagueMessage.status = "A";
          tblLeagueMessage.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_league_message` (`id_game`,`message`,`status`,`updated_date_time`,`id_league`) VALUES ({0},{1},{2},{3},{4})", (object) tblLeagueMessage.id_game, (object) tblLeagueMessage.message, (object) tblLeagueMessage.status, (object) tblLeagueMessage.updated_date_time, (object) tblLeagueMessage.id_league);
        }
      }
      return (ActionResult) this.RedirectToAction("level_up_message");
    }

    public ActionResult Job_url_create()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_joburl_master> tblJoburlMasterList = new List<tbl_joburl_master>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblJoburlMasterList = m2ostDbContext.Database.SqlQuery<tbl_joburl_master>("select * from tbl_joburl_master").ToList<tbl_joburl_master>();
      this.ViewData["tblJurl"] = (object) tblJoburlMasterList;
      return (ActionResult) this.View();
    }

    public ActionResult Job_url_Add()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      string str1 = this.Request.Form["JobUrlVal"].ToString();
      string str2 = "D";
      DateTime now = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_joburl_master` (`url`,`status`,`updated_date_time`) VALUES({0},{1},{2})", (object) str1, (object) str2, (object) now);
      return (ActionResult) this.RedirectToAction("Job_url_create");
    }

    public ActionResult del_job_url()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      int int32 = Convert.ToInt32(this.Request.Form["idJoburl"].ToString().ToString());
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM `tbl_joburl_master` WHERE id_joburl={0}", (object) int32);
      return (ActionResult) this.RedirectToAction("Job_url_create");
    }

    public ActionResult Job_Url_Update()
    {
      string str = this.Request.Form["JoburlName"].ToString();
      int int32 = Convert.ToInt32(this.Request.Form["editIdJob"].ToString());
      DateTime now = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_joburl_master` SET `url` ={0}, `updated_date_time` ={1} WHERE `id_joburl` ={2}", (object) str, (object) now, (object) int32);
      return (ActionResult) this.RedirectToAction("Job_url_create");
    }

    public ActionResult activate_job_url(int id_job)
    {
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        int num = m2ostDbContext.Database.SqlQuery<int>("select id_joburl from tbl_joburl_master where status='A'").FirstOrDefault<int>();
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_joburl_master` SET `status` = 'D'  WHERE `id_joburl` ={0}", (object) num);
      }
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_joburl_master` SET `status` = 'A'  WHERE `id_joburl` ={0}", (object) id_job);
      return (ActionResult) this.RedirectToAction("Job_url_create");
    }

    public ActionResult url_notification()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      return (ActionResult) this.View();
    }

    public ActionResult insert_Url_Notification()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      tbl_url_notification_master notificationMaster = new tbl_url_notification_master();
      notificationMaster.notifcation_title = this.Request.Form["notTitle"].ToString();
      notificationMaster.notification_message = this.Request.Form["notMsg"].ToString();
      notificationMaster.notification_url = this.Request.Form["notUrl"].ToString();
      notificationMaster.status = "A";
      notificationMaster.updated_datetime = DateTime.Now;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.SqlQuery<int>("INSERT INTO `tbl_url_notification_master` (`notifcation_title`,`notification_message`,`notification_url`,`status`,`updated_datetime`) VALUES ({0},{1},{2},{3},{4});select last_insert_id();", (object) notificationMaster.notifcation_title, (object) notificationMaster.notification_message, (object) notificationMaster.notification_url, (object) notificationMaster.status, (object) notificationMaster.updated_datetime).FirstOrDefault<int>();
      List<tbl_profile> tblProfileList = new List<tbl_profile>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        tblProfileList = m2ostDbContext.Database.SqlQuery<tbl_profile>("select t1.* from tbl_profile t1 inner join tbl_user t2 on t1.ID_USER=t2.ID_USER where status='A' and t2.ID_ORGANIZATION = {0} group by t1.ID_USER", (object) int32).ToList<tbl_profile>();
      foreach (tbl_profile tblProfile in tblProfileList)
      {
        List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          tblUserGcmLogList = m2ostDbContext.Database.SqlQuery<tbl_user_gcm_log>("SELECT * FROM tbl_user_gcm_log where status='A' and id_user ={0}", (object) tblProfile.ID_USER).ToList<tbl_user_gcm_log>();
        foreach (tbl_user_gcm_log tblUserGcmLog in tblUserGcmLogList)
        {
          string notificationUrl = notificationMaster.notification_url;
          string appSetting = WebConfigurationManager.AppSettings["genric_notif"];
          string notificationMessage = notificationMaster.notification_message;
          this.SendNotification(tblUserGcmLog.GCMID, notificationMessage, notificationMaster.notifcation_title, appSetting, notificationUrl);
        }
      }
      return (ActionResult) this.RedirectToAction("url_notification");
    }
  }
}
