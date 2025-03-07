// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.UniversityAdminController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
    public class UniversityAdminController : Controller
    {
        private db_m2ostEntities db = new db_m2ostEntities();

        public ActionResult Index() => (ActionResult)this.View();

        public ActionResult ThemeDashboard()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_theme_master> tblThemeMasterList = new List<tbl_theme_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblThemeMasterList = m2ostDbContext.Database.SqlQuery<tbl_theme_master>("Select * from tbl_theme_master").ToList<tbl_theme_master>();
            this.ViewData["theme"] = (object)tblThemeMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult AddTheme()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            this.ViewData["SpMetDrop"] = (object)new RoadmapLogic().getSpMetric(int32);
            return (ActionResult)this.View();
        }

        public ActionResult AddThemeAction()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            tbl_theme_master theme = new tbl_theme_master();
            theme.name = this.Request.Form["theme-name"].ToString();
            ////theme.id_creator = Convert.ToInt32(content.ID_USER);
            theme.id_creator = content.ID_USER.ToString();
            theme.status = "D";
            theme.updated_datetime = DateTime.Now;
            theme.description = this.Request.Form["theme-desc"].ToString();
            int num = new UniversityAdminLogic().insertTheme(theme);
            string str1 = "";
            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["Theme-Logo-btn"];
                str1 = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["Theme-Logo-btn"].FileName);
                string str2 = System.Web.HttpContext.Current.Request["id"];
                if (file != null)
                {
                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Theme_Logo/")))
                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Theme_Logo/"));
                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Theme_Logo/"), num.ToString() + "logo" + str1);
                    file.SaveAs(filename);
                    theme.theme_logo = num.ToString() + "logo" + str1;
                }
                theme.id_theme = num;
                new UniversityAdminLogic().updatethemelogo(theme);
            }
            List<tbl_theme_metric> metric = new List<tbl_theme_metric>();
            int int32_3 = Convert.ToInt32(this.Request.Form["metriccount"].ToString());
            for (int index = 1; index <= int32_3; ++index)
                metric.Add(new tbl_theme_metric()
                {
                    id_theme = num,
                    metric_value = this.Request.Form["theme-metric" + (object)index].ToString(),
                    status = "A",
                    updated_datetime = DateTime.Now
                });
            new UniversityAdminLogic().insertMetric(metric);
            List<tbl_theme_leagues> league = new List<tbl_theme_leagues>();
            int int32_4 = Convert.ToInt32(this.Request.Form["leaguecount"].ToString());
            for (int index = 1; index <= int32_4; ++index)
            {
                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn" + (object)index];
                    Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn" + (object)index].FileName);
                    string str3 = System.Web.HttpContext.Current.Request["id"];
                    if (file != null)
                    {

                        tbl_theme_leagues tblThemeLeagues = new tbl_theme_leagues();
                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/league/")))
                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/league/"));
                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/league/"), num.ToString() + "league" + (object)index + str1);
                        file.SaveAs(filename);
                        tblThemeLeagues.league_logo = num.ToString() + "league" + (object)index + str1;
                        tblThemeLeagues.id_theme = num;
                        tblThemeLeagues.league_name = this.Request.Form["theme-league" + (object)index].ToString();
                        tblThemeLeagues.level = index;
                        tblThemeLeagues.status = "A";
                        tblThemeLeagues.updated_date_time = DateTime.Now;
                        league.Add(tblThemeLeagues);
                    }
                }
            }
            new UniversityAdminLogic().insertLeague(league);
            List<tbl_badge_master> badge = new List<tbl_badge_master>();
            int int32_5 = Convert.ToInt32(this.Request.Form["badgecount"].ToString());
            for (int index = 1; index <= int32_5; ++index)
            {
                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["badge-btn" + (object)index];
                    Path.GetExtension(System.Web.HttpContext.Current.Request.Files["badge-btn" + (object)index].FileName);
                    string str4 = System.Web.HttpContext.Current.Request["id"];
                    if (file != null)
                    {
                        tbl_badge_master tblBadgeMaster = new tbl_badge_master();
                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/badge/")))
                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/badge/"));
                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/badge/"), num.ToString() + "badge" + (object)index + str1);
                        file.SaveAs(filename);
                        tblBadgeMaster.badge_logo = num.ToString() + "badge" + (object)index + str1;
                        tblBadgeMaster.id_theme = num;
                        tblBadgeMaster.badge_name = this.Request.Form["theme-badge" + (object)index].ToString();
                        tblBadgeMaster.status = "A";
                        tblBadgeMaster.updated_datetime = DateTime.Now;
                        badge.Add(tblBadgeMaster);
                    }
                }
            }
            new UniversityAdminLogic().insertBadge(badge);
            List<tbl_crrency_points> currency = new List<tbl_crrency_points>();
            int int32_6 = Convert.ToInt32(this.Request.Form["currencycount"].ToString());
            for (int index = 1; index <= int32_6; ++index)
            {
                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["curr-btn" + (object)index];
                    Path.GetExtension(System.Web.HttpContext.Current.Request.Files["curr-btn" + (object)index].FileName);
                    string str5 = System.Web.HttpContext.Current.Request["id"];
                    if (file != null)
                    {
                        tbl_crrency_points tblCrrencyPoints = new tbl_crrency_points();
                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/currency/")))
                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/currency/"));
                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/currency/"), num.ToString() + "currency" + (object)index + str1);
                        file.SaveAs(filename);
                        tblCrrencyPoints.currency_logo = num.ToString() + "currency" + (object)index + str1;
                        tblCrrencyPoints.id_theme = num;
                        tblCrrencyPoints.currency_value = this.Request.Form["theme-currency" + (object)index].ToString();
                        tblCrrencyPoints.status = "A";
                        tblCrrencyPoints.updated_datetime = DateTime.Now;
                        currency.Add(tblCrrencyPoints);
                    }
                }
            }
            new UniversityAdminLogic().insertCurrency(currency);
            if (num != 0)
            {
                int id_creator = int32_2;
                int id_organization = int32_1;
                string kpi_name = "";
                string kpi_description = "";
                int Id_them = num;
                string KPIID = "MKPI" + DateTime.Now.ToString("yyyyMMddHHmmss");
                int int32_7 = Convert.ToInt32(this.Request.Form["SpMet-type"].ToString());
                DateTime now1 = DateTime.Now;
                DateTime now2 = DateTime.Now;
                string stat = "A";
                DateTime now3 = DateTime.Now;
                new RoadmapLogic().createSpecial_Points(id_creator, id_organization, kpi_name, kpi_description, KPIID, int32_7, now1, now2, stat, now3, Id_them);
            }
            return (ActionResult)this.RedirectToAction("AddTheme");
        }

        public ActionResult EditTheme(int id)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_theme_master tblThemeMaster = new tbl_theme_master();
            List<tbl_theme_metric> tblThemeMetricList = new List<tbl_theme_metric>();
            List<tbl_theme_leagues> tblThemeLeaguesList = new List<tbl_theme_leagues>();
            List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
            List<tbl_crrency_points> tblCrrencyPointsList = new List<tbl_crrency_points>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                tblThemeMaster = m2ostDbContext.Database.SqlQuery<tbl_theme_master>("Select * from tbl_theme_master where id_theme={0}", (object)id).FirstOrDefault<tbl_theme_master>();
                tblThemeMetricList = m2ostDbContext.Database.SqlQuery<tbl_theme_metric>("Select * from tbl_theme_metric where id_theme={0}", (object)id).ToList<tbl_theme_metric>();
                tblThemeLeaguesList = m2ostDbContext.Database.SqlQuery<tbl_theme_leagues>("Select * from tbl_theme_leagues where id_theme={0}", (object)id).ToList<tbl_theme_leagues>();
                tblBadgeMasterList = m2ostDbContext.Database.SqlQuery<tbl_badge_master>("Select * from tbl_badge_master where id_theme={0}", (object)id).ToList<tbl_badge_master>();
                tblCrrencyPointsList = m2ostDbContext.Database.SqlQuery<tbl_crrency_points>("Select * from tbl_currency_points where id_theme={0}", (object)id).ToList<tbl_crrency_points>();
            }
            this.ViewData["theme"] = (object)tblThemeMaster;
            this.ViewData["metric"] = (object)tblThemeMetricList;
            this.ViewData["league"] = (object)tblThemeLeaguesList;
            this.ViewData["badge"] = (object)tblBadgeMasterList;
            this.ViewData["currency"] = (object)tblCrrencyPointsList;
            return (ActionResult)this.View();
        }

        public ActionResult EditThemeAction()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            Convert.ToInt32(content.id_ORGANIZATION);
            tbl_theme_master theme = new tbl_theme_master();
            theme.name = this.Request.Form["theme-name"].ToString();
            ////theme.id_creator = Convert.ToInt32(content.ID_USER);
            theme.id_creator = content.ID_USER;
            theme.status = "D";
            theme.updated_datetime = DateTime.Now;
            theme.description = this.Request.Form["theme-desc"].ToString();
            int int32_1 = Convert.ToInt32(this.Request.Form["themeId"].ToString());
            theme.id_theme = int32_1;
            new UniversityAdminLogic().updateTheme(theme);
            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["Theme-Logo-btn"];
                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["Theme-Logo-btn"].FileName);
                string str = System.Web.HttpContext.Current.Request["id"];
                if (file != null)
                {
                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Theme_Logo/")))
                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Theme_Logo/"));
                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Theme_Logo/"), int32_1.ToString() + "logo" + extension);
                    file.SaveAs(filename);
                    theme.theme_logo = int32_1.ToString() + "logo" + extension;
                    new UniversityAdminLogic().updatethemelogo(theme);
                }
            }
            List<tbl_theme_metric> metric1 = new List<tbl_theme_metric>();
            List<tbl_theme_metric> metric2 = new List<tbl_theme_metric>();
            int int32_2 = Convert.ToInt32(this.Request.Form["metriccount"].ToString());
            int int32_3 = Convert.ToInt32(this.Request.Form["oldmetriccount"].ToString());
            for (int index = 1; index <= int32_2; ++index)
            {
                tbl_theme_metric tblThemeMetric = new tbl_theme_metric();
                tblThemeMetric.id_theme = int32_1;
                tblThemeMetric.metric_value = this.Request.Form["theme-metric" + (object)index].ToString();
                tblThemeMetric.status = "A";
                tblThemeMetric.updated_datetime = DateTime.Now;
                if (index <= int32_3)
                {
                    tblThemeMetric.id_metric = Convert.ToInt32(this.Request.Form["theme-metric-id" + (object)index].ToString());
                    metric1.Add(tblThemeMetric);
                }
                else
                    metric2.Add(tblThemeMetric);
            }
            if (metric2.Count != 0)
                new UniversityAdminLogic().insertMetric(metric2);
            new UniversityAdminLogic().UpdateMetric(metric1);
            List<tbl_theme_leagues> league1 = new List<tbl_theme_leagues>();
            List<tbl_theme_leagues> league2 = new List<tbl_theme_leagues>();
            int int32_4 = Convert.ToInt32(this.Request.Form["leaguecount"].ToString());
            int int32_5 = Convert.ToInt32(this.Request.Form["oldleaguecount"].ToString());
            for (int index = 1; index <= int32_4; ++index)
            {
                tbl_theme_leagues tblThemeLeagues = new tbl_theme_leagues();
                tblThemeLeagues.id_theme = int32_1;
                tblThemeLeagues.league_name = this.Request.Form["theme-league" + (object)index].ToString();
                tblThemeLeagues.level = index;
                tblThemeLeagues.status = "A";
                tblThemeLeagues.updated_date_time = DateTime.Now;
                if (index <= int32_5)
                {
                    tblThemeLeagues.id_league = Convert.ToInt32(this.Request.Form["theme-league-Id" + (object)index].ToString());
                    league1.Add(tblThemeLeagues);
                }
                else
                    league2.Add(tblThemeLeagues);
            }
            if (league2.Count != 0)
                new UniversityAdminLogic().insertLeague(league2);
            new UniversityAdminLogic().UpdateLeague(league1);
            List<tbl_badge_master> badge1 = new List<tbl_badge_master>();
            List<tbl_badge_master> badge2 = new List<tbl_badge_master>();
            int int32_6 = Convert.ToInt32(this.Request.Form["badgecount"].ToString());
            int int32_7 = Convert.ToInt32(this.Request.Form["oldbadgecount"].ToString());
            for (int index = 1; index <= int32_6; ++index)
            {
                tbl_badge_master tblBadgeMaster = new tbl_badge_master();
                tblBadgeMaster.id_theme = int32_1;
                tblBadgeMaster.badge_name = this.Request.Form["theme-badge" + (object)index].ToString();
                tblBadgeMaster.status = "A";
                tblBadgeMaster.updated_datetime = DateTime.Now;
                if (index <= int32_7)
                {
                    tblBadgeMaster.id_badge = Convert.ToInt32(this.Request.Form["theme-badge-Id" + (object)index].ToString());
                    badge1.Add(tblBadgeMaster);
                }
                else
                    badge2.Add(tblBadgeMaster);
            }
            if (badge2.Count != 0)
                new UniversityAdminLogic().insertBadge(badge2);
            new UniversityAdminLogic().UpdateBadge(badge1);
            List<tbl_crrency_points> currency1 = new List<tbl_crrency_points>();
            List<tbl_crrency_points> currency2 = new List<tbl_crrency_points>();
            int int32_8 = Convert.ToInt32(this.Request.Form["currencycount"].ToString());
            int int32_9 = Convert.ToInt32(this.Request.Form["oldcurrencycount"].ToString());
            for (int index = 1; index <= int32_8; ++index)
            {
                tbl_crrency_points tblCrrencyPoints = new tbl_crrency_points();
                tblCrrencyPoints.id_theme = int32_1;
                tblCrrencyPoints.currency_value = this.Request.Form["theme-currency" + (object)index].ToString();
                tblCrrencyPoints.status = "A";
                tblCrrencyPoints.updated_datetime = DateTime.Now;
                if (index <= int32_9)
                {
                    tblCrrencyPoints.id_currency = Convert.ToInt32(this.Request.Form["theme-currency-Id" + (object)index].ToString());
                    currency1.Add(tblCrrencyPoints);
                }
                else
                    currency2.Add(tblCrrencyPoints);
            }
            if (badge2.Count != 0)
                new UniversityAdminLogic().insertCurrency(currency2);
            new UniversityAdminLogic().updateCurrency(currency1);
            return (ActionResult)this.RedirectToAction("ThemeDashboard");
        }

        public ActionResult ThemeApprovalDashboard()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_theme_master> tblThemeMasterList = new List<tbl_theme_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblThemeMasterList = m2ostDbContext.Database.SqlQuery<tbl_theme_master>("Select * from tbl_theme_master where status='D'").ToList<tbl_theme_master>();
            List<theme_preview> themePreviewList = new List<theme_preview>();
            foreach (tbl_theme_master tblThemeMaster in tblThemeMasterList)
            {
                theme_preview themePreview1 = new theme_preview();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    theme_preview themePreview2 = m2ostDbContext.Database.SqlQuery<theme_preview>("Select * from tbl_theme_master where id_theme={0}", (object)tblThemeMaster.id_theme).FirstOrDefault<theme_preview>();
                    themePreview2.metric = m2ostDbContext.Database.SqlQuery<tbl_theme_metric>("Select * from tbl_theme_metric where id_theme={0}", (object)tblThemeMaster.id_theme).ToList<tbl_theme_metric>();
                    themePreview2.league = m2ostDbContext.Database.SqlQuery<tbl_theme_leagues>("Select * from tbl_theme_leagues where id_theme={0}", (object)tblThemeMaster.id_theme).ToList<tbl_theme_leagues>();
                    themePreview2.badge = m2ostDbContext.Database.SqlQuery<tbl_badge_master>("Select * from tbl_badge_master where id_theme={0}", (object)tblThemeMaster.id_theme).ToList<tbl_badge_master>();
                    themePreview2.currency = m2ostDbContext.Database.SqlQuery<tbl_crrency_points>("Select * from tbl_currency_points where id_theme={0}", (object)tblThemeMaster.id_theme).ToList<tbl_crrency_points>();
                    themePreviewList.Add(themePreview2);
                }
            }
            this.ViewData["preview"] = (object)themePreviewList;
            this.ViewData["theme"] = (object)tblThemeMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult ApproveThemeAction(int id)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_theme_master set status='A' where id_theme={0}", (object)id);
            return (ActionResult)this.RedirectToAction("ThemeApprovalDashboard");
        }

        public ActionResult MappingThemes()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_organization> tblOrganizationList = new List<tbl_organization>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblOrganizationList = m2ostDbContext.Database.SqlQuery<tbl_organization>("Select * from tbl_organization where STATUS='A'").ToList<tbl_organization>();
            this.ViewData["org"] = (object)tblOrganizationList;
            return (ActionResult)this.View();
        }

        public string GetThemesforOrg(int orgid)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<theme_map> themeMapList = new List<theme_map>();
            List<tbl_theme_master> tblThemeMasterList = new List<tbl_theme_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblThemeMasterList = m2ostDbContext.Database.SqlQuery<tbl_theme_master>("Select * from tbl_theme_master where status='A'").ToList<tbl_theme_master>();
            foreach (tbl_theme_master tblThemeMaster1 in tblThemeMasterList)
            {
                tbl_theme_master tblThemeMaster2 = new tbl_theme_master();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    tblThemeMaster2 = m2ostDbContext.Database.SqlQuery<tbl_theme_master>("Select * from tbl_theme_master where id_theme={0}", (object)tblThemeMaster1.id_theme).FirstOrDefault<tbl_theme_master>();
                if (tblThemeMaster2 != null)
                {
                    tbl_theme_organisation_mapping organisationMapping = new tbl_theme_organisation_mapping();
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        organisationMapping = m2ostDbContext.Database.SqlQuery<tbl_theme_organisation_mapping>("Select * from tbl_theme_organisation_mapping where id_theme={0} and id_org={1}", (object)tblThemeMaster2.id_theme, (object)orgid).FirstOrDefault<tbl_theme_organisation_mapping>();
                    themeMapList.Add(new theme_map()
                    {
                        id_theme = tblThemeMaster1.id_theme,
                        description = tblThemeMaster1.description,
                        name = tblThemeMaster1.name,
                        flag = organisationMapping != null
                    });
                }
            }
            string str = "";
            foreach (theme_map themeMap in themeMapList)
            {
                str = str + " <tr><td>" + themeMap.name + "</td>";
                str = str + "<td>" + themeMap.description + "</td>";
                str += " <td>";
                if (themeMap.flag)
                {
                    str = str + " <a  style=\"display:none;\" id=\"add_" + (object)themeMap.id_theme + "\" href=\"#\" onclick=\"addThemeToOrg('" + (object)themeMap.id_theme + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
                    str = str + " <i id=\"done_" + (object)themeMap.id_theme + "\" class=\"glyphicon glyphicon-ok\"></i>";
                    str = str + " <a id=\"link_" + (object)themeMap.id_theme + "\" href=\"#\" onclick=\"removeThemeFromOrg('" + (object)themeMap.id_theme + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
                }
                else
                {
                    str = str + " <a id=\"add_" + (object)themeMap.id_theme + "\" href=\"#\" onclick=\"addThemeToOrg('" + (object)themeMap.id_theme + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
                    str = str + " <i style=\"display:none;\"  id=\"done_" + (object)themeMap.id_theme + "\" class=\"glyphicon glyphicon-ok\"></i>";
                    str = str + " <a style=\"display:none;\"  id=\"link_" + (object)themeMap.id_theme + "\" href=\"#\" onclick=\"removeThemeFromOrg('" + (object)themeMap.id_theme + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
                }
                str += " </td></tr>";
            }
            return "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"> <thead>  <tr><th width=\"50%\">Theme Name</th><th width=\"50%\">Description</th><th width=\"5%\">Action</th> </tr></thead> <tbody>" + str + " </tbody></table>";
        }

        public string addThemesToOrg(int id_theme, int id_org)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_theme_organisation_mapping (id_theme,id_org,status,updated_datetime) values({0},{1},{2},{3})", (object)id_theme, (object)id_org, (object)'A', (object)DateTime.Now);
                return "1";
            }
        }

        public string removeThemeFromOrg(int id_theme, int id_org)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("delete  from tbl_theme_organisation_mapping where id_theme={0} and id_org={1}", (object)id_theme, (object)id_org);
                return "1";
            }
        }

        public ActionResult CreateGameAction()
        {
            try
            {
                int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
                tbl_game_master tblGameMaster = new tbl_game_master();
                tblGameMaster.name = this.Request.Form["game_name"].ToString();
                tblGameMaster.description = this.Request.Form["game_description"].ToString();
                tblGameMaster.id_theme = Convert.ToInt32(this.Request.Form["themeid"].ToString());
                tblGameMaster.id_metric = 0;
                tblGameMaster.id_kpi = 0;
                tblGameMaster.id_org = int32_1;
                tblGameMaster.game_type = 1;
                tblGameMaster.status = "D";
                string dateString1 = this.Request.Form["start_date"].ToString();
                string dateString2 = this.Request.Form["end_date"].ToString();
                tblGameMaster.start_date = new Utility().StringToDatetime(dateString1);
                tblGameMaster.end_date = new Utility().StringToDatetime(dateString2);
                tblGameMaster.updated_date_time = DateTime.Now;
                tblGameMaster.relegation_switch = 0;
                int int32_2 = Convert.ToInt32(this.Request.Form["specialMetricflag"].ToString());
                tblGameMaster.id_special_metric = int32_2;
                tblGameMaster.assignment_flag = this.Request.Form["userassign"].ToString();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    tblGameMaster.id_game = m2ostDbContext.Database.SqlQuery<int>("insert into tbl_game_master (name,description,id_kpi,id_theme,id_org,game_type,status,updated_date_time,assignment_flag,id_metric,relegation_switch,id_special_metric,start_date,end_date) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});SELECT LAST_INSERT_ID();", (object)tblGameMaster.name, (object)tblGameMaster.description, (object)tblGameMaster.id_kpi, (object)tblGameMaster.id_theme, (object)tblGameMaster.id_org, (object)tblGameMaster.game_type, (object)tblGameMaster.status, (object)tblGameMaster.updated_date_time, (object)tblGameMaster.assignment_flag, (object)tblGameMaster.id_metric, (object)tblGameMaster.relegation_switch, (object)tblGameMaster.id_special_metric, (object)tblGameMaster.start_date, (object)tblGameMaster.end_date).FirstOrDefault<int>();
                if (tblGameMaster.assignment_flag == "U")
                {
                    int[] source = Array.ConvertAll<string, int>(this.Request.Form["chk_user"].ToString().Split(','), new Converter<string, int>(int.Parse));
                    List<tbl_user_game_assignment> userGameAssignmentList = new List<tbl_user_game_assignment>();
                    for (int index = 0; index < ((IEnumerable<int>)source).Count<int>(); ++index)
                        userGameAssignmentList.Add(new tbl_user_game_assignment()
                        {
                            id_game = tblGameMaster.id_game,
                            id_org = int32_1,
                            id_user = source[index],
                            status = "A",
                            updated_datetime = DateTime.Now.ToString()
                        });
                    foreach (tbl_user_game_assignment userGameAssignment in userGameAssignmentList)
                    {
                        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_user_game_assignment (id_game,id_org,id_user,status,updated_datetime) values({0},{1},{2},{3},{4})", (object)userGameAssignment.id_game, (object)userGameAssignment.id_org, (object)userGameAssignment.id_user, (object)userGameAssignment.status, (object)userGameAssignment.updated_datetime);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("GameDashboard");
        }

        public ActionResult GameDashboard()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_game_master> tblGameMasterList = new List<tbl_game_master>();
            List<tbl_game_metric_kpi_mapping> metricKpiMappingList = new List<tbl_game_metric_kpi_mapping>();
            List<int> intList = new List<int>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                tblGameMasterList = m2ostDbContext.Database.SqlQuery<tbl_game_master>("select * from tbl_game_master").ToList<tbl_game_master>();
                foreach (tbl_game_master tblGameMaster1 in tblGameMasterList)
                {
                    List<tbl_theme_metric> tblThemeMetricList = new List<tbl_theme_metric>();
                    List<tbl_theme_metric> list1 = m2ostDbContext.Database.SqlQuery<tbl_theme_metric>("select * from tbl_theme_metric where id_theme={0}", (object)tblGameMaster1.id_theme).ToList<tbl_theme_metric>();
                    List<int> list2 = m2ostDbContext.Database.SqlQuery<int>("select distinct id_metric from tbl_game_metric_kpi_mapping where id_game={0}", (object)tblGameMaster1.id_game).ToList<int>();
                    if (list1.Count<tbl_theme_metric>() == list2.Count<int>())
                    {
                        tblGameMaster1.added_metric_count = list2.Count<int>();
                        tblGameMaster1.metrics_completion_flag = 1;
                    }
                    else
                    {
                        tblGameMaster1.added_metric_count = list2.Count<int>();
                        tblGameMaster1.metrics_completion_flag = 0;
                    }
                    tblGameMaster1.kpiids = m2ostDbContext.Database.SqlQuery<int>("select distinct id_metric from tbl_game_metric_kpi_mapping where id_game={0} ", (object)tblGameMaster1.id_game).ToList<int>();
                    List<string> stringList = new List<string>();
                    foreach (int kpiid in tblGameMaster1.kpiids)
                    {
                        string str = m2ostDbContext.Database.SqlQuery<string>("select  metric_value from tbl_theme_metric where id_metric={0} ", (object)kpiid).FirstOrDefault<string>();
                        tbl_game_master tblGameMaster2 = tblGameMaster1;
                        tblGameMaster2.metricname = tblGameMaster2.metricname + str + ",";
                    }
                }
            }
            this.ViewData["game"] = (object)tblGameMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult editGame(int id)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int OID = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            List<tbl_university_kpi_master> kpiName = new RoadmapLogic().Get_KPi_Name(OID);
            List<tbl_special_metric_master> specialMet = new RoadmapLogic().Get_SpecialMet(OID);
            List<tbl_theme_master> tblThemeMasterList = new RoadmapLogic().themeOrg(OID);
            List<tbl_user> list = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>)(t => t.ID_ORGANIZATION == (int?)OID && t.STATUS == "A")).ToList<tbl_user>();
            List<tbl_profile> tblProfileList = new List<tbl_profile>();
            List<tbl_game_master> tblGameMas = new RoadmapLogic().getTblGameMas(id);
            List<tbl_leagues_data> tblLeagueData = new RoadmapLogic().getTblLeagueData(id);
            List<tbl_badge_data> tblBadgeData = new RoadmapLogic().getTblBadgeData(id);
            List<tbl_relegtion_data> tblRelegData = new RoadmapLogic().getTblRelegData(id);
            List<tbl_currency_data> tblCurrData = new RoadmapLogic().getTblCurrData(id);
            List<tbl_user_game_assignment> tblUsrAss = new RoadmapLogic().getTblUsrAss(id);
            foreach (tbl_user tblUser in list)
            {
                tbl_user itm = tblUser;
                tbl_profile tblProfile = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>)(t => t.ID_USER == itm.ID_USER)).FirstOrDefault<tbl_profile>();
                tblProfileList.Add(tblProfile);
            }
            this.ViewData["tblGame"] = (object)tblGameMas;
            this.ViewData["tblLeague"] = (object)tblLeagueData;
            this.ViewData["tblBadgData"] = (object)tblBadgeData;
            this.ViewData["tblRelegData"] = (object)tblRelegData;
            this.ViewData["tblCurrData"] = (object)tblCurrData;
            this.ViewData["tblUsrAssDat"] = (object)tblUsrAss;
            this.ViewData["users"] = (object)tblProfileList;
            this.ViewData["specialMet"] = (object)specialMet;
            this.ViewData["kpiName"] = (object)kpiName;
            this.ViewData["themeOrg"] = (object)tblThemeMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult EditGameAction()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(this.Request.Form["idGamMas"].ToString());
            tbl_game_master tblGameMaster = new tbl_game_master();
            tblGameMaster.id_game = int32_2;
            tblGameMaster.name = this.Request.Form["game_name"].ToString();
            tblGameMaster.description = this.Request.Form["game_description"].ToString();
            tblGameMaster.id_kpi = 0;
            tblGameMaster.id_theme = Convert.ToInt32(this.Request.Form["themeid"].ToString());
            tblGameMaster.id_metric = 0;
            tblGameMaster.id_org = int32_1;
            tblGameMaster.game_type = 1;
            tblGameMaster.status = "D";
            tblGameMaster.updated_date_time = DateTime.Now;
            tblGameMaster.relegation_switch = 0;
            int int32_3 = Convert.ToInt32(this.Request.Form["specialMetricflag"].ToString());
            tblGameMaster.assignment_flag = this.Request.Form["userassign"].ToString();
            tblGameMaster.id_special_metric = int32_3;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblGameMaster.id_game = m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_game_master` SET name={0},description={1},id_kpi={2},id_theme={3},id_metric={4},id_org={5},game_type={6},status={7},updated_date_time={8},id_special_metric={9},relegation_switch={10},assignment_flag={11} where id_game={12}", (object)tblGameMaster.name, (object)tblGameMaster.description, (object)tblGameMaster.id_kpi, (object)tblGameMaster.id_theme, (object)tblGameMaster.id_metric, (object)tblGameMaster.id_org, (object)tblGameMaster.game_type, (object)tblGameMaster.status, (object)tblGameMaster.updated_date_time, (object)tblGameMaster.id_special_metric, (object)tblGameMaster.relegation_switch, (object)tblGameMaster.assignment_flag, (object)int32_2);
            if (tblGameMaster.assignment_flag == "U")
            {
                int[] source = Array.ConvertAll<string, int>(this.Request.Form["chk_user"].ToString().Split(','), new Converter<string, int>(int.Parse));
                List<tbl_user_game_assignment> userGameAssignmentList = new List<tbl_user_game_assignment>();
                for (int index = 0; index < ((IEnumerable<int>)source).Count<int>(); ++index)
                    userGameAssignmentList.Add(new tbl_user_game_assignment()
                    {
                        id_game = tblGameMaster.id_game,
                        id_org = int32_1,
                        id_user = source[index],
                        status = "A",
                        updated_datetime = DateTime.Now.ToString()
                    });
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM `tbl_user_game_assignment` WHERE id_game={0}", (object)int32_2);
                foreach (tbl_user_game_assignment userGameAssignment in userGameAssignmentList)
                {
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_user_game_assignment (id_game,id_org,id_user,status,updated_datetime) values({0},{1},{2},{3},{4})", (object)int32_2, (object)userGameAssignment.id_org, (object)userGameAssignment.id_user, (object)userGameAssignment.status, (object)userGameAssignment.updated_datetime);
                }
            }
            return (ActionResult)this.RedirectToAction("GameDashboard");
        }

        public ActionResult ActivateGame(int id)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            DateTime now = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("update tbl_game_master set status='A',updated_date_time={0} where id_game={1}", (object)now, (object)id);
            return (ActionResult)this.RedirectToAction("GameDashboard");
        }

        public ActionResult DeActivateGame(int id)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("update tbl_game_master set status='S' where id_game={0}", (object)id);
            return (ActionResult)this.RedirectToAction("GameDashboard");
        }

        public ActionResult RewardsConfig()
        {
            this.ViewData["theme"] = (object)new RoadmapLogic().themeOrg(Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION));
            return (ActionResult)this.View();
        }

        public ActionResult RewardsDashboard()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_rewards_master> tblRewardsMasterList = new List<tbl_rewards_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblRewardsMasterList = m2ostDbContext.Database.SqlQuery<tbl_rewards_master>("select * from tbl_rewards_master where id_org={0}", (object)int32).ToList<tbl_rewards_master>();
            this.ViewData["rewards"] = (object)tblRewardsMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult RewardAdd()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_rewards_master tblRewardsMaster = new tbl_rewards_master();
            tblRewardsMaster.reward_value = this.Request.Form["RewardTitle"].ToString();
            tblRewardsMaster.description = this.Request.Form["RewardDescription"].ToString();
            tblRewardsMaster.id_org = int32;
            tblRewardsMaster.status = "A";
            tblRewardsMaster.updated_datetime = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_rewards_master (reward_value,description,id_org,status,updated_datetime) values({0},{1},{2},{3},{4})", (object)tblRewardsMaster.reward_value, (object)tblRewardsMaster.description, (object)tblRewardsMaster.id_org, (object)tblRewardsMaster.status, (object)tblRewardsMaster.updated_datetime);
            return (ActionResult)this.RedirectToAction("RewardsDashboard");
        }

        public ActionResult RewardsUpdateAction()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_rewards_master tblRewardsMaster = new tbl_rewards_master();
            tblRewardsMaster.reward_value = this.Request.Form["editrewardName"].ToString();
            tblRewardsMaster.description = this.Request.Form["editrewardDesc"].ToString();
            tblRewardsMaster.id_org = int32;
            tblRewardsMaster.status = "A";
            tblRewardsMaster.updated_datetime = DateTime.Now;
            tblRewardsMaster.id_reward = Convert.ToInt32(this.Request.Form["editrewardid"].ToString());
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_rewards_master set reward_value={0},description={1},id_org={2},status={3},updated_datetime={4} where id_reward={5}", (object)tblRewardsMaster.reward_value, (object)tblRewardsMaster.description, (object)tblRewardsMaster.id_org, (object)tblRewardsMaster.status, (object)tblRewardsMaster.updated_datetime, (object)tblRewardsMaster.id_reward);
            return (ActionResult)this.RedirectToAction("RewardsDashboard");
        }

        public ActionResult RewardDelete()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            int int32 = Convert.ToInt32(this.Request.Form["DeleterewardId"].ToString());
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("delete from tbl_rewards_master where id_reward={0}", (object)int32);
            return (ActionResult)this.RedirectToAction("RewardsDashboard");
        }

        public string getBadgeWithRewards(int id)
        {
            string str = "";
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
            List<tbl_rewards_master> tblRewardsMasterList = new List<tbl_rewards_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                tblBadgeMasterList = m2ostDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object)id).ToList<tbl_badge_master>();
                tblRewardsMasterList = m2ostDbContext.Database.SqlQuery<tbl_rewards_master>("select * from tbl_rewards_master where id_org={0}", (object)int32).ToList<tbl_rewards_master>();
            }
            int num = 0;
            foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
            {
                str = str + "<tr class='drg1' style='background-color:rgb(41, 148, 211); color: white;'><td> " + tblBadgeMaster.badge_name + " <input type='hidden' name='currbadid" + (object)num + "'  value='" + (object)tblBadgeMaster.id_badge + "'></td><td><select id='currnDrop' name='currnDrop" + (object)num + "' class='form-control'><option value=''>select</option>";
                foreach (tbl_rewards_master tblRewardsMaster in tblRewardsMasterList)
                    str = str + "<option value='" + (object)tblRewardsMaster.id_reward + "' > " + tblRewardsMaster.reward_value + "</option>";
                str += "</select></td></tr>";
                ++num;
            }
            return str + "<input type='hidden' value='" + (object)tblBadgeMasterList.Count + "' name='badgecount'/>";
        }

        public ActionResult RewardsConfigAction()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(this.Request.Form["badgecount"].ToString());
            List<tbl_badge_reward_mapping> badgeRewardMappingList = new List<tbl_badge_reward_mapping>();
            for (int index = 0; index < int32_2; ++index)
                badgeRewardMappingList.Add(new tbl_badge_reward_mapping()
                {
                    id_badge = Convert.ToInt32(this.Request.Form["currbadid" + (object)index].ToString()),
                    id_reward = Convert.ToInt32(this.Request.Form["currnDrop" + (object)index].ToString()),
                    id_org = int32_1,
                    id_theme = Convert.ToInt32(this.Request.Form["theme"].ToString()),
                    status = "S",
                    updated_datetime = DateTime.Now
                });
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                foreach (tbl_badge_reward_mapping badgeRewardMapping in badgeRewardMappingList)
                    m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_badge_reward_mapping (id_badge,id_org,id_theme,status,updated_datetime,id_reward) values({0},{1},{2},{3},{4},{5})", (object)badgeRewardMapping.id_badge, (object)badgeRewardMapping.id_org, (object)badgeRewardMapping.id_theme, (object)badgeRewardMapping.status, (object)badgeRewardMapping.updated_datetime, (object)badgeRewardMapping.id_reward);
            }
            return (ActionResult)this.RedirectToAction("RewardsConfigDashboard");
        }

        public ActionResult RewardsConfigDashboard()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<badge_reward_dashboard> badgeRewardDashboardList = new List<badge_reward_dashboard>();
            List<tbl_theme_master> tblThemeMasterList = new List<tbl_theme_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                Database database = m2ostDbContext.Database;
                object[] objArray = new object[1] { (object)int32 };
                foreach (badge_reward_dashboard badgeRewardDashboard in database.SqlQuery<badge_reward_dashboard>("SELECT distinct id_theme FROM tbl_badge_reward_mapping where id_org={0}", objArray).ToList<badge_reward_dashboard>())
                {
                    tbl_theme_master tblThemeMaster1 = new tbl_theme_master();
                    tbl_theme_master tblThemeMaster2 = m2ostDbContext.Database.SqlQuery<tbl_theme_master>("SELECT * FROM tbl_theme_master where id_theme={0}", (object)badgeRewardDashboard.id_theme).FirstOrDefault<tbl_theme_master>();
                    tblThemeMasterList.Add(tblThemeMaster2);
                }
            }
            this.ViewData["theme"] = (object)tblThemeMasterList;
            return (ActionResult)this.View();
        }

        public string RewardPreview(int themeid)
        {
            string str1 = "";
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<reward_preview> rewardPreviewList = new List<reward_preview>();
            List<tbl_badge_reward_mapping> badgeRewardMappingList = new List<tbl_badge_reward_mapping>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                badgeRewardMappingList = m2ostDbContext.Database.SqlQuery<tbl_badge_reward_mapping>("SELECT  * FROM tbl_badge_reward_mapping where  id_org={0} and id_theme={1}", (object)int32, (object)themeid).ToList<tbl_badge_reward_mapping>();
                foreach (tbl_badge_reward_mapping badgeRewardMapping in badgeRewardMappingList)
                {
                    badgeRewardMapping.reward_name = m2ostDbContext.Database.SqlQuery<string>("SELECT reward_value FROM tbl_rewards_master where id_reward={0}", (object)badgeRewardMapping.id_reward).FirstOrDefault<string>();
                    badgeRewardMapping.name = m2ostDbContext.Database.SqlQuery<string>("SELECT name FROM tbl_theme_master where id_theme={0}", (object)badgeRewardMapping.id_theme).FirstOrDefault<string>();
                    badgeRewardMapping.description = m2ostDbContext.Database.SqlQuery<string>("SELECT description FROM tbl_theme_master where id_theme={0}", (object)badgeRewardMapping.id_theme).FirstOrDefault<string>();
                    badgeRewardMapping.badge_name = m2ostDbContext.Database.SqlQuery<string>("SELECT badge_name from tbl_badge_master where id_badge={0}", (object)badgeRewardMapping.id_badge).FirstOrDefault<string>();
                }
            }
            string str2 = str1 + "<div class='form-group'> <label for='recipien-name' class='col-form-label'>Theme:&nbsp;</label><span>" + badgeRewardMappingList[0].name + "</span></div> <div class='form-group'> <label for='message-text' class='col-form-label'>Description:&nbsp;</label> <span>" + badgeRewardMappingList[0].description + "</span> </div> <div class='form-group'> <label  class='col-form-label'>Rewards:</label> </br>";
            foreach (tbl_badge_reward_mapping badgeRewardMapping in badgeRewardMappingList)
                str2 = str2 + "<label class='col-form-label'>" + badgeRewardMapping.badge_name + "-</label> <span>" + badgeRewardMapping.reward_name + "</span></br> ";
            return str2 + "</div>";
        }

        public ActionResult DeleteRewardsConfig(int id)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("delete from  tbl_badge_reward_mapping where  id_org={0} and id_theme={1}", (object)int32, (object)id);
            return (ActionResult)this.RedirectToAction("RewardsConfigDashboard");
        }

        public string CheckRewardConfig(int id)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "N";
            List<tbl_badge_reward_mapping> badgeRewardMappingList = new List<tbl_badge_reward_mapping>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                if (m2ostDbContext.Database.SqlQuery<tbl_badge_reward_mapping>("select * from  tbl_badge_reward_mapping where  id_org={0} and id_theme={1}", (object)int32, (object)id).ToList<tbl_badge_reward_mapping>().Count > 0)
                    str = "A";
            }
            return str;
        }

        public ActionResult AddGameAttribute(int id_game, int id_theme)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_game_metric_kpi_mapping> metricKpiMappingList = new List<tbl_game_metric_kpi_mapping>();
            List<int> intList = new List<int>();
            List<tbl_university_kpi_master> universityKpiMasterList1 = new List<tbl_university_kpi_master>();
            List<tbl_university_kpi_master> universityKpiMasterList2 = new List<tbl_university_kpi_master>();
            List<tbl_university_kpi_master> universityKpiMasterList3 = new List<tbl_university_kpi_master>();
            List<tbl_theme_metric> tblThemeMetricList = new List<tbl_theme_metric>();
            List<tbl_special_metric_master> specialMetricMasterList = new List<tbl_special_metric_master>();
            int num1 = 0;
            tbl_university_special_points_master specialPointsMaster = new tbl_university_special_points_master();
            special_metric_master specialMetricMaster = new special_metric_master();
            tbl_game_master tblGameMaster = new tbl_game_master();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                List<int> list1 = m2ostDbContext.Database.SqlQuery<int>("SELECT distinct id_kpi FROM tbl_game_metric_kpi_mapping where id_game={0}", (object)id_game).ToList<int>();
                List<tbl_university_kpi_master> list2 = m2ostDbContext.Database.SqlQuery<tbl_university_kpi_master>("SELECT * FROM tbl_university_kpi_master where id_organization={0}", (object)int32).ToList<tbl_university_kpi_master>();
                tblThemeMetricList = m2ostDbContext.Database.SqlQuery<tbl_theme_metric>("SELECT * FROM tbl_theme_metric where id_theme={0}", (object)id_theme).ToList<tbl_theme_metric>();
                tblGameMaster = m2ostDbContext.Database.SqlQuery<tbl_game_master>("SELECT id_special_metric FROM tbl_game_master where id_game={0}", (object)id_game).FirstOrDefault<tbl_game_master>();
                specialMetricMasterList = m2ostDbContext.Database.SqlQuery<tbl_special_metric_master>("select * from tbl_special_metric_master where id_org={0}", (object)int32).ToList<tbl_special_metric_master>();
                num1 = m2ostDbContext.Database.SqlQuery<int>("select count(*) from tbl_university_special_points_master t1 inner join tbl_university_special_point_grid t2 on t1.id_special_points=t2.id_special_points where t1.id_theme={0} and t2.id_game={1}", (object)id_theme, (object)id_game).FirstOrDefault<int>();
                specialPointsMaster = m2ostDbContext.Database.SqlQuery<tbl_university_special_points_master>("SELECT * FROM tbl_university_special_points_master where id_theme={0}", (object)id_theme).FirstOrDefault<tbl_university_special_points_master>();
                foreach (tbl_university_kpi_master universityKpiMaster in list2)
                {
                    int num2 = 0;
                    foreach (int num3 in list1)
                    {
                        if (num3 == universityKpiMaster.id_kpi_master)
                            num2 = 1;
                    }
                    if (num2 == 0)
                    {
                        universityKpiMasterList2.Add(universityKpiMaster);
                    }
                    else
                    {
                        int num4 = m2ostDbContext.Database.SqlQuery<int>("SELECT  id_metric FROM tbl_game_metric_kpi_mapping where id_game={0}  and id_kpi={1}", (object)id_game, (object)universityKpiMaster.id_kpi_master).FirstOrDefault<int>();
                        universityKpiMaster.metric_name = m2ostDbContext.Database.SqlQuery<string>("SELECT  metric_value FROM tbl_theme_metric where id_metric={0}", (object)num4).FirstOrDefault<string>();
                        universityKpiMasterList3.Add(universityKpiMaster);
                    }
                }
            }
            this.ViewData["specialMetFlag"] = (object)tblGameMaster;
            this.ViewData["specialmetric"] = (object)specialMetricMasterList;
            this.ViewData["themeid"] = (object)id_theme;
            this.ViewData["kpi"] = (object)universityKpiMasterList2;
            this.ViewData["kpiaddedd"] = (object)universityKpiMasterList3;
            this.ViewData["metric"] = (object)tblThemeMetricList;
            this.ViewData[nameof(id_game)] = (object)id_game;
            this.ViewData["tbl_univerSP"] = (object)specialPointsMaster;
            this.ViewData["SpMasCount"] = (object)num1;
            return (ActionResult)this.View();
        }

        public ActionResult AddGameAttributeAction()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(this.Request.Form["id_game"].ToString());
            int int32_3 = Convert.ToInt32(this.Request.Form["themVal"].ToString());
            int int32_4 = Convert.ToInt32(this.Request.Form["idmetric"].ToString());
            int int32_5 = Convert.ToInt32(this.Request.Form["kpiid"].ToString());
            int int32_6 = Convert.ToInt32(this.Request.Form["kpi_type_val"].ToString());
            int int32_7 = Convert.ToInt32(this.Request.Form["smswitch"].ToString());
            int int32_8 = Convert.ToInt32(this.Request.Form["sm_type_hid"].ToString());
            Convert.ToInt32(this.Request.Form["id_sm_hid"].ToString());
            int int32_9 = Convert.ToInt32(this.Request.Form["SpecialMet"].ToString());
            tbl_game_metric_kpi_mapping metricKpiMapping = new tbl_game_metric_kpi_mapping();
            metricKpiMapping.id_game = int32_2;
            metricKpiMapping.id_kpi = int32_5;
            metricKpiMapping.id_metric = int32_4;
            metricKpiMapping.id_org = int32_1;
            metricKpiMapping.id_theme = int32_3;
            metricKpiMapping.status = "A";
            metricKpiMapping.updated_date_time = DateTime.Now;
            metricKpiMapping.special_metric_flag = Convert.ToInt32(this.Request.Form["smswitch"].ToString());
            switch (int32_6)
            {
                case 1:
                    new RoadmapLogic().Kpi_grid_insert(0, "", Convert.ToDouble(this.Request.Form["OneCorrect"].ToString()), 0.0, "A", DateTime.Now, int32_5, int32_2, int32_4);
                    break;
                case 2:
                    int int32_10 = Convert.ToInt32(this.Request.Form["kpi-grid-count"].ToString());
                    for (int index = 1; index <= int32_10; ++index)
                    {
                        int id_kpi_master = int32_5;
                        new RoadmapLogic().Kpi_grid_insert(Convert.ToInt32(this.Request.Form["kpi-value-" + (object)index].ToString()), this.Request.Form["kpi-text-" + (object)index].ToString(), Convert.ToDouble(this.Request.Form["kpi-start-" + (object)index].ToString()), Convert.ToDouble(this.Request.Form["kpi-end-" + (object)index].ToString()), "A", DateTime.Now, id_kpi_master, int32_2, int32_4);
                    }
                    break;
            }
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_game_metric_kpi_mapping (id_game,id_kpi,id_metric,id_org,id_theme,status,updated_date_time,special_metric_flag,id_special_metric) values({0},{1},{2},{3},{4},{5},{6},{7},{8})", (object)metricKpiMapping.id_game, (object)metricKpiMapping.id_kpi, (object)metricKpiMapping.id_metric, (object)metricKpiMapping.id_org, (object)metricKpiMapping.id_theme, (object)metricKpiMapping.status, (object)metricKpiMapping.updated_date_time, (object)metricKpiMapping.special_metric_flag, (object)int32_9);
                m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_game_master set  id_kpi=1 where id_game={0}", (object)int32_2);
            }
            List<tbl_leagues_data> tblLeaguesDataList = new List<tbl_leagues_data>();
            int int32_11 = Convert.ToInt32(this.Request.Form["leaguecount"].ToString());
            for (int index = 0; index < int32_11; ++index)
                tblLeaguesDataList.Add(new tbl_leagues_data()
                {
                    id_game = int32_2,
                    evaluation_type = Convert.ToInt32(this.Request.Form["evaluation_type" + (object)index].ToString()),
                    expression_type = Convert.ToInt32(this.Request.Form["comparileague" + (object)index].ToString()),
                    id_league = Convert.ToInt32(this.Request.Form["leagueid_" + (object)index].ToString()),
                    id_org = int32_1,
                    id_theme = int32_3,
                    level = index + 1,
                    minscore = (double)Convert.ToInt32(this.Request.Form["eligiblity_" + (object)index].ToString()),
                    movement_number = Convert.ToInt32(this.Request.Form["movement_number_" + (object)index].ToString()),
                    status = "A",
                    updated_date_time = DateTime.Now,
                    id_metric = int32_4
                });
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                foreach (tbl_leagues_data tblLeaguesData in tblLeaguesDataList)
                    m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_leagues_data (id_game,evaluation_type,expression_type,id_league,id_org,id_theme,level,minscore,movement_number,status,updated_date_time,id_metric) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11});", (object)tblLeaguesData.id_game, (object)tblLeaguesData.evaluation_type, (object)tblLeaguesData.expression_type, (object)tblLeaguesData.id_league, (object)tblLeaguesData.id_org, (object)tblLeaguesData.id_theme, (object)tblLeaguesData.level, (object)tblLeaguesData.minscore, (object)tblLeaguesData.movement_number, (object)tblLeaguesData.status, (object)tblLeaguesData.updated_date_time, (object)tblLeaguesData.id_metric);
            }
            int int32_12 = Convert.ToInt32(this.Request.Form["relegationswitch"].ToString());
            List<tbl_relegtion_data> tblRelegtionDataList = new List<tbl_relegtion_data>();
            if (int32_12 == 1)
            {
                for (int index = 0; index < int32_11; ++index)
                    tblRelegtionDataList.Add(new tbl_relegtion_data()
                    {
                        id_game = int32_2,
                        frequency_type = this.Request.Form["freqType" + (object)index].ToString(),
                        id_league = Convert.ToInt32(this.Request.Form["rellegid_" + (object)index].ToString()),
                        id_org = int32_1,
                        level = index + 1,
                        min_score = Convert.ToInt32(this.Request.Form["minscore" + (object)index].ToString()),
                        relegation_expression = Convert.ToInt32(this.Request.Form["compari" + (object)index].ToString()),
                        relegation_frequency = Convert.ToInt32(this.Request.Form["freq_" + (object)index].ToString()),
                        relegation_method = Convert.ToInt32(this.Request.Form["relmethod" + (object)index].ToString()),
                        status = "A",
                        updated_datetime = DateTime.Now,
                        id_metric = int32_4
                    });
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    foreach (tbl_relegtion_data tblRelegtionData in tblRelegtionDataList)
                        m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_relegtion_data  (id_game,frequency_type,id_league,id_org,level,min_score,relegation_expression,relegation_frequency,relegation_method,status,updated_datetime,id_metric) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11});", (object)tblRelegtionData.id_game, (object)tblRelegtionData.frequency_type, (object)tblRelegtionData.id_league, (object)tblRelegtionData.id_org, (object)tblRelegtionData.level, (object)tblRelegtionData.min_score, (object)tblRelegtionData.relegation_expression, (object)tblRelegtionData.relegation_frequency, (object)tblRelegtionData.relegation_method, (object)tblRelegtionData.status, (object)tblRelegtionData.updated_datetime, (object)tblRelegtionData.id_metric);
                }
            }
            int[] source = Array.ConvertAll<string, int>(this.Request.Form["chk_badge"].ToString().Split(','), new Converter<string, int>(int.Parse));
            List<tbl_badge_data> tblBadgeDataList = new List<tbl_badge_data>();
            Convert.ToInt32(this.Request.Form["badgecount"].ToString());
            for (int index = 0; index < ((IEnumerable<int>)source).Count<int>(); ++index)
                tblBadgeDataList.Add(new tbl_badge_data()
                {
                    id_game = int32_2,
                    id_badge = Convert.ToInt32(this.Request.Form["badgId" + (object)source[index]].ToString()),
                    id_org = int32_1,
                    required_score = Convert.ToInt32(this.Request.Form["badgVal" + (object)source[index]].ToString()),
                    status = "A",
                    updated_datetime = DateTime.Now,
                    id_metric = int32_4
                });
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                foreach (tbl_badge_data tblBadgeData in tblBadgeDataList)
                    m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_badge_data (id_game,id_org,required_score,status,updated_datetime,id_metric,id_badge) values({0},{1},{2},{3},{4},{5},{6});", (object)tblBadgeData.id_game, (object)tblBadgeData.id_org, (object)tblBadgeData.required_score, (object)tblBadgeData.status, (object)tblBadgeData.updated_datetime, (object)tblBadgeData.id_metric, (object)tblBadgeData.id_badge);
            }
            List<tbl_currency_data> tblCurrencyDataList = new List<tbl_currency_data>();
            for (int index = 0; index < ((IEnumerable<int>)source).Count<int>(); ++index)
                tblCurrencyDataList.Add(new tbl_currency_data()
                {
                    id_game = int32_2,
                    id_badge = Convert.ToInt32(this.Request.Form["currbadid" + (object)source[index]].ToString()),
                    id_org = int32_1,
                    currency_value = Convert.ToInt32(this.Request.Form["CurrncyVal" + (object)source[index]].ToString()),
                    id_currency = Convert.ToInt32(this.Request.Form["currnDrop" + (object)source[index]].ToString()),
                    status = "A",
                    updated_datetime = DateTime.Now,
                    id_metric = int32_4
                });
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                foreach (tbl_currency_data tblCurrencyData in tblCurrencyDataList)
                    m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_currency_data (id_game,id_badge,currency_value,id_currency,status,updated_datetime,id_org,id_metric) values({0},{1},{2},{3},{4},{5},{6},{7});", (object)tblCurrencyData.id_game, (object)tblCurrencyData.id_badge, (object)tblCurrencyData.currency_value, (object)tblCurrencyData.id_currency, (object)tblCurrencyData.status, (object)tblCurrencyData.updated_datetime, (object)tblCurrencyData.id_org, (object)tblCurrencyData.id_metric);
            }
            if (int32_7 == 1)
            {
                int idSpoints = new RoadmapLogic().getIdSpoints(int32_3);
                if (int32_8 == 1)
                {
                    double startVal = Convert.ToDouble(this.Request.Form["sm_value"].ToString());
                    int endVal = 0;
                    int spVal = 0;
                    string spTxt = "";
                    int spMetr = 0;
                    string status = "A";
                    DateTime now = DateTime.Now;
                    int idGam = int32_2;
                    new RoadmapLogic().insertSpGrid(idSpoints, startVal, endVal, spVal, spTxt, spMetr, status, now, int32_9, idGam);
                }
                else
                {
                    int int32_13 = Convert.ToInt32(this.Request.Form["smgridcnt"].ToString());
                    for (int index = 1; index <= int32_13; ++index)
                    {
                        double startVal = Convert.ToDouble(this.Request.Form["smrng-start" + (object)index].ToString());
                        int int32_14 = Convert.ToInt32(this.Request.Form["smrng-end" + (object)index].ToString());
                        int int32_15 = Convert.ToInt32(this.Request.Form["smrng-val" + (object)index].ToString());
                        string spTxt = "";
                        int spMetr = 0;
                        string status = "A";
                        DateTime now = DateTime.Now;
                        int idGam = int32_2;
                        new RoadmapLogic().insertSpGrid(idSpoints, startVal, int32_14, int32_15, spTxt, spMetr, status, now, int32_9, idGam);
                    }
                }
            }
            return (ActionResult)this.RedirectToAction("GameDashboard");
        }

        public string CheckmetricAvailability(int id_metric, int id_game)
        {
            string str = "0";
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_game_metric_kpi_mapping> metricKpiMappingList = new List<tbl_game_metric_kpi_mapping>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                if (m2ostDbContext.Database.SqlQuery<tbl_game_metric_kpi_mapping>("select * from  tbl_game_metric_kpi_mapping where id_game={0} and id_metric={1}", (object)id_game, (object)id_metric).ToList<tbl_game_metric_kpi_mapping>().Count > 0)
                    str = "1";
            }
            return str;
        }

        public ActionResult GameMappingDashboard()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_game_academic_mapping> gameAcademicMappingList = new List<tbl_game_academic_mapping>();
            return (ActionResult)this.View();
        }

        public ActionResult GameMapping()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_game_master> tblGameMasterList = new List<tbl_game_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblGameMasterList = m2ostDbContext.Database.SqlQuery<tbl_game_master>("select * from  tbl_game_master where id_org={0} and status={1}", (object)int32, (object)"A").ToList<tbl_game_master>();
            this.ViewData["game"] = (object)tblGameMasterList;
            return (ActionResult)this.View();
        }

        public string getAcademicTilesforGame(int id_game)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<academic_map> academicMapList = new List<academic_map>();
            List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblAcademicTilesList = m2ostDbContext.Database.SqlQuery<tbl_academic_tiles>("Select * from tbl_academic_tiles where status='A' and id_org={0}", (object)int32).ToList<tbl_academic_tiles>();
            foreach (tbl_academic_tiles tblAcademicTiles1 in tblAcademicTilesList)
            {
                tbl_academic_tiles tblAcademicTiles2 = new tbl_academic_tiles();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    tblAcademicTiles2 = m2ostDbContext.Database.SqlQuery<tbl_academic_tiles>("Select * from tbl_academic_tiles where id_academic_tile={0}", (object)tblAcademicTiles1.id_academic_tile).FirstOrDefault<tbl_academic_tiles>();
                if (tblAcademicTiles2 != null)
                {
                    tbl_game_academic_mapping gameAcademicMapping = new tbl_game_academic_mapping();
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        gameAcademicMapping = m2ostDbContext.Database.SqlQuery<tbl_game_academic_mapping>("Select * from tbl_game_academic_mapping where id_academic_tile={0} and id_org={1} and id_game={2}", (object)tblAcademicTiles2.id_academic_tile, (object)int32, (object)id_game).FirstOrDefault<tbl_game_academic_mapping>();
                    academicMapList.Add(new academic_map()
                    {
                        id_academic_tile = tblAcademicTiles1.id_academic_tile,
                        description = tblAcademicTiles1.tile_description,
                        name = tblAcademicTiles1.tile_name,
                        flag = gameAcademicMapping != null
                    });
                }
            }
            string str = "";
            foreach (academic_map academicMap in academicMapList)
            {
                str = str + " <tr><td>" + academicMap.name + "</td>";
                str = str + "<td>" + academicMap.description + "</td>";
                str += " <td>";
                if (academicMap.flag)
                {
                    str = str + " <a  style=\"display:none;\" id=\"add_" + (object)academicMap.id_academic_tile + "\" href=\"#\" onclick=\"addAcademyToGame('" + (object)academicMap.id_academic_tile + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
                    str = str + " <i id=\"done_" + (object)academicMap.id_academic_tile + "\" class=\"glyphicon glyphicon-ok\"></i>";
                    str = str + " <a id=\"link_" + (object)academicMap.id_academic_tile + "\" href=\"#\" onclick=\"removeAcademyfromGame('" + (object)academicMap.id_academic_tile + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
                }
                else
                {
                    str = str + " <a id=\"add_" + (object)academicMap.id_academic_tile + "\" href=\"#\" onclick=\"addAcademyToGame('" + (object)academicMap.id_academic_tile + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
                    str = str + " <i style=\"display:none;\"  id=\"done_" + (object)academicMap.id_academic_tile + "\" class=\"glyphicon glyphicon-ok\"></i>";
                    str = str + " <a style=\"display:none;\"  id=\"link_" + (object)academicMap.id_academic_tile + "\" href=\"#\" onclick=\"removeAcademyfromGame('" + (object)academicMap.id_academic_tile + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
                }
                str += " </td></tr>";
            }
            return "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"> <thead>  <tr><th width=\"50%\">Academy</th><th width=\"50%\">Description</th><th width=\"5%\">Action</th> </tr></thead> <tbody>" + str + " </tbody></table>";
        }

        public string addAcademyToGame(int id_game, int id_academic_tile)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_game_academic_mapping (id_org,id_game,id_academic_tile,user_assign_flag,status,updated_date_time) values({0},{1},{2},{3},{4},{5})", (object)int32, (object)id_game, (object)id_academic_tile, (object)"N", (object)'A', (object)DateTime.Now);
                return "1";
            }
        }

        public string removeAcademyfromGame(int id_game, int id_academic_tile)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("delete  from tbl_game_academic_mapping where id_academic_tile={0} and id_game={1}", (object)id_academic_tile, (object)id_game);
                return "1";
            }
        }

        public string getKPIType(int id_kpi)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                return Convert.ToString(m2ostDbContext.Database.SqlQuery<int>("select kpi_type from  tbl_university_kpi_master where id_kpi_master={0}", (object)id_kpi).FirstOrDefault<int>());
        }

        public ActionResult EditGameAttribute(int id_game, int id_theme)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_theme_metric> tblThemeMetricList = new List<tbl_theme_metric>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblThemeMetricList = m2ostDbContext.Database.SqlQuery<tbl_theme_metric>("select * from tbl_game_metric_kpi_mapping t1 inner join tbl_theme_metric t2 on t1.id_metric=t2.id_metric where t1.id_game={0} and t1.id_theme={1}", (object)id_game, (object)id_theme).ToList<tbl_theme_metric>();
            this.ViewData[nameof(id_game)] = (object)id_game;
            this.ViewData[nameof(id_theme)] = (object)id_theme;
            this.ViewData["metThem"] = (object)tblThemeMetricList;
            return (ActionResult)this.View();
        }

        public ActionResult editGameAttributeAction(int id_game, int id_theme, int idMet)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_game_metric_kpi_mapping> metricKpiMappingList1 = new List<tbl_game_metric_kpi_mapping>();
            List<int> intList = new List<int>();
            List<tbl_university_kpi_master> universityKpiMasterList1 = new List<tbl_university_kpi_master>();
            List<tbl_university_kpi_master> universityKpiMasterList2 = new List<tbl_university_kpi_master>();
            List<tbl_university_kpi_master> universityKpiMasterList3 = new List<tbl_university_kpi_master>();
            List<tbl_theme_metric> tblThemeMetricList = new List<tbl_theme_metric>();
            List<tbl_special_metric_master> specialMetricMasterList = new List<tbl_special_metric_master>();
            int num1 = 0;
            tbl_university_special_points_master specialPointsMaster = new tbl_university_special_points_master();
            special_metric_master specialMetricMaster = new special_metric_master();
            tbl_game_master tblGameMaster = new tbl_game_master();
            List<tbl_game_master> tblGameMasterList = new List<tbl_game_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.SqlQuery<int>("SELECT distinct id_kpi FROM tbl_game_metric_kpi_mapping where id_game={0}", (object)id_game).ToList<int>();
                List<tbl_university_kpi_master> list = m2ostDbContext.Database.SqlQuery<tbl_university_kpi_master>("SELECT * FROM tbl_university_kpi_master where id_organization={0}", (object)int32).ToList<tbl_university_kpi_master>();
                tblThemeMetricList = m2ostDbContext.Database.SqlQuery<tbl_theme_metric>("SELECT * FROM tbl_theme_metric where id_theme={0}", (object)id_theme).ToList<tbl_theme_metric>();
                tblGameMaster = m2ostDbContext.Database.SqlQuery<tbl_game_master>("SELECT id_special_metric FROM tbl_game_master where id_game={0}", (object)id_game).FirstOrDefault<tbl_game_master>();
                specialMetricMasterList = m2ostDbContext.Database.SqlQuery<tbl_special_metric_master>("select * from tbl_special_metric_master where id_org={0}", (object)int32).ToList<tbl_special_metric_master>();
                num1 = m2ostDbContext.Database.SqlQuery<int>("select count(*) from tbl_university_special_points_master t1 inner join tbl_university_special_point_grid t2 on t1.id_special_points=t2.id_special_points where id_theme={0}", (object)id_theme).FirstOrDefault<int>();
                specialPointsMaster = m2ostDbContext.Database.SqlQuery<tbl_university_special_points_master>("SELECT * FROM tbl_university_special_points_master where id_theme={0}", (object)id_theme).FirstOrDefault<tbl_university_special_points_master>();
                tblGameMasterList = m2ostDbContext.Database.SqlQuery<tbl_game_master>("SELECT * FROM tbl_game_master where id_game={0}", (object)id_game).ToList<tbl_game_master>();
                foreach (tbl_university_kpi_master universityKpiMaster in list)
                {
                    if (true)
                    {
                        universityKpiMasterList2.Add(universityKpiMaster);
                    }
                    else
                    {
                        int num2 = m2ostDbContext.Database.SqlQuery<int>("SELECT  id_metric FROM tbl_game_metric_kpi_mapping where id_game={0}  and id_kpi={1}", (object)id_game, (object)universityKpiMaster.id_kpi_master).FirstOrDefault<int>();
                        universityKpiMaster.metric_name = m2ostDbContext.Database.SqlQuery<string>("SELECT  metric_value FROM tbl_theme_metric where id_metric={0}", (object)num2).FirstOrDefault<string>();
                        universityKpiMasterList3.Add(universityKpiMaster);
                    }
                }
            }
            List<tbl_game_metric_kpi_mapping> metricKpiMappingList2 = new List<tbl_game_metric_kpi_mapping>();
            List<tbl_leagues_data> tblLeaguesDataList = new List<tbl_leagues_data>();
            List<tbl_badge_data> tblBadgeDataList = new List<tbl_badge_data>();
            List<tbl_currency_data> tblCurrencyDataList = new List<tbl_currency_data>();
            List<tbl_relegtion_data> tblRelegtionDataList = new List<tbl_relegtion_data>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                metricKpiMappingList2 = m2ostDbContext.Database.SqlQuery<tbl_game_metric_kpi_mapping>("SELECT * FROM tbl_game_metric_kpi_mapping where id_game={0} and id_theme={1}", (object)id_game, (object)id_theme).ToList<tbl_game_metric_kpi_mapping>();
                tblLeaguesDataList = m2ostDbContext.Database.SqlQuery<tbl_leagues_data>("select t2.* from tbl_theme_leagues t1 inner join tbl_leagues_data t2 on t1.id_league=t2.id_league where t1.id_theme={0}", (object)id_theme).ToList<tbl_leagues_data>();
                tblBadgeDataList = m2ostDbContext.Database.SqlQuery<tbl_badge_data>("select t2.* from tbl_badge_master t1 inner join tbl_badge_data t2 on t1.id_badge=t2.id_badge where t1.id_theme={0} and t2.id_metric={1}", (object)id_theme, (object)idMet).ToList<tbl_badge_data>();
                tblCurrencyDataList = m2ostDbContext.Database.SqlQuery<tbl_currency_data>("SELECT t2.* FROM tbl_currency_points t1 inner join tbl_currency_data t2 on t1.id_currency=t2.id_currency where id_theme={0} and id_metric={1}", (object)id_theme, (object)idMet).ToList<tbl_currency_data>();
                if (tblGameMasterList[0].relegation_switch == 1)
                    tblRelegtionDataList = m2ostDbContext.Database.SqlQuery<tbl_relegtion_data>("SELECT * FROM tbl_relegtion_data where id_metric={0}", (object)idMet).ToList<tbl_relegtion_data>();
                else
                    tblRelegtionDataList = (List<tbl_relegtion_data>)null;
            }
            this.ViewData["specialMetFlag"] = (object)tblGameMaster;
            this.ViewData["specialmetric"] = (object)specialMetricMasterList;
            this.ViewData["themeid"] = (object)id_theme;
            this.ViewData["kpi"] = (object)universityKpiMasterList2;
            this.ViewData["kpiaddedd"] = (object)universityKpiMasterList3;
            this.ViewData["metric"] = (object)tblThemeMetricList;
            this.ViewData[nameof(id_game)] = (object)id_game;
            this.ViewData["tblGameMas"] = (object)tblGameMasterList;
            this.ViewData["tbl_univerSP"] = (object)specialPointsMaster;
            this.ViewData["SpMasCount"] = (object)num1;
            this.ViewData["tblGameKpiMap"] = (object)metricKpiMappingList2;
            this.ViewData["tblLeagDat"] = (object)tblLeaguesDataList;
            this.ViewData["tblBadgDat"] = (object)tblBadgeDataList;
            this.ViewData["tblcurrDat"] = (object)tblCurrencyDataList;
            this.ViewData["tblRelegData"] = (object)tblRelegtionDataList;
            return (ActionResult)this.View();
        }

        public ActionResult signup_config()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_signup_config> tblSignupConfigList = new List<tbl_signup_config>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblSignupConfigList = m2ostDbContext.Database.SqlQuery<tbl_signup_config>("SELECT * FROM tbl_signup_config where status='A'").ToList<tbl_signup_config>();
            this.ViewData["tbl_signup"] = (object)tblSignupConfigList;
            return (ActionResult)this.View();
        }

        public ActionResult update_signup_config()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            int int32_1 = Convert.ToInt32(this.Request.Form["totalCount"].ToString());
            for (int index = 1; index < int32_1; ++index)
            {
                int int32_2 = Convert.ToInt32(this.Request.Form["signupId-" + (object)index].ToString());
                int int32_3 = Convert.ToInt32(this.Request.Form["check-box-option-" + (object)index].ToString());
                DateTime now = DateTime.Now;
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_signup_config` SET `mandatory` = {0}, `updated_datetime` = {1} WHERE `id_signup_config` = {2}", (object)int32_3, (object)now, (object)int32_2);
            }
            return (ActionResult)this.RedirectToAction("signup_config");
        }

        public ActionResult DisplayFeedback()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_feedback_master> tblFeedbackMasterList = new List<tbl_feedback_master>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblFeedbackMasterList = m2ostDbContext.Database.SqlQuery<tbl_feedback_master>("Select * from tbl_feedback_master where Issues='1'").ToList<tbl_feedback_master>();
            List<feedback_preview> feedbackPreviewList = new List<feedback_preview>();
            foreach (tbl_feedback_master tblFeedbackMaster in tblFeedbackMasterList)
            {
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    tblFeedbackMaster.FIRSTNAME = m2ostDbContext.Database.SqlQuery<string>("SELECT concat(FIRSTNAME,' ',LASTNAME) as Name FROM tbl_profile where ID_USER={0}", (object)tblFeedbackMaster.UID).FirstOrDefault<string>();
                    if (tblFeedbackMaster.MediaFlag == 1)
                        tblFeedbackMaster.file = m2ostDbContext.Database.SqlQuery<string>("SELECT media FROM tbl_feedback_media where id_feedback={0}", (object)tblFeedbackMaster.id_feedback).ToList<string>();
                }
            }
            this.ViewData["preview"] = (object)feedbackPreviewList;
            this.ViewData["feedback"] = (object)tblFeedbackMasterList;
            return (ActionResult)this.View();
        }
    }
}
