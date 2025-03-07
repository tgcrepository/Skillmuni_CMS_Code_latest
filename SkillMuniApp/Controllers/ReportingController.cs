using m2ostnext;
using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
    public class ReportingController : Controller
    {
        private db_m2ostEntities db = new db_m2ostEntities();

        private string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

        public ReportingController()
        {
        }

        public ActionResult ContentAccess()
        {
            UserSession item = (UserSession)base.HttpContext.Session.Contents["UserSession"];
            int num = Convert.ToInt32(item.id_ORGANIZATION);
            List<string> list = (
                from t in this.db.tbl_user
                where t.ID_ORGANIZATION == (int?)num
                select t into p
                select p.user_function).Distinct<string>().ToList<string>();
            list = (
                from s in list
                where !string.IsNullOrWhiteSpace(s)
                select s).ToList<string>();
            List<tbl_csst_role> tblCsstRoles = (
                from t in this.db.tbl_csst_role
                where t.id_organization == (int?)num
                select t).ToList<tbl_csst_role>();
            base.ViewData["functions"] = list;
            base.ViewData["roleList"] = tblCsstRoles;
            return base.View();
        }

        public ActionResult DesignationReport()
        {
            UserSession item = (UserSession)base.HttpContext.Session.Contents["UserSession"];
            int num = Convert.ToInt32(item.id_ORGANIZATION);
            List<string> list = (
                from t in this.db.tbl_user
                where t.ID_ORGANIZATION == (int?)num
                select t into p
                select p.user_function).Distinct<string>().ToList<string>();
            list = (
                from s in list
                where !string.IsNullOrWhiteSpace(s)
                select s).ToList<string>();
            List<tbl_csst_role> tblCsstRoles = (
                from t in this.db.tbl_csst_role
                where t.id_organization == (int?)num
                select t).ToList<tbl_csst_role>();
            List<string> strs = (
                from t in this.db.tbl_user
                where t.ID_ORGANIZATION == (int?)num
                select t into p
                select p.user_designation).Distinct<string>().ToList<string>();
            base.ViewData["functions"] = list;
            base.ViewData["roleList"] = tblCsstRoles;
            base.ViewData["designation"] = strs;
            return base.View();
        }

        public ActionResult GenderReport()
        {
            UserSession item = (UserSession)base.HttpContext.Session.Contents["UserSession"];
            int num = Convert.ToInt32(item.id_ORGANIZATION);
            List<string> list = (
                from t in this.db.tbl_user
                where t.ID_ORGANIZATION == (int?)num
                select t into p
                select p.user_function).Distinct<string>().ToList<string>();
            list = (
                from s in list
                where !string.IsNullOrWhiteSpace(s)
                select s).ToList<string>();
            List<tbl_csst_role> tblCsstRoles = (
                from t in this.db.tbl_csst_role
                where t.id_organization == (int?)num
                select t).ToList<tbl_csst_role>();
            string str = string.Concat("select * from tbl_profile where id_user in (select id_user from tbl_user where id_organization=", num, ") group by location");
            List<string> strs = (
                from p in this.db.tbl_profile.SqlQuery(str, new object[0]).ToList<tbl_profile>()
                select p.LOCATION).Distinct<string>().ToList<string>();
            strs = (
                from s in strs
                where !string.IsNullOrWhiteSpace(s)
                select s).ToList<string>();
            base.ViewData["functions"] = list;
            base.ViewData["roleList"] = tblCsstRoles;
            base.ViewData["location"] = strs;
            return base.View();
        }

        public string getContentAccessReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and a.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and a.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and a.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT count(a.id_content) COUNTER,a.id_content,date(b.EXPIRY_DATE) EXPIRY_DATE, b.CONTENT_QUESTION,MAX(a.UPDATED_DATE_TIME) LASTACCESS from tbl_content_counters a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
            str4 = string.Concat(string.Concat(new string[] { str4, str, str1, str2, str3 }), " group by a.id_content order by COUNTER desc limit 100");
            List<ContentLike> contentAccess = (new ContentReportModel()).getContentAccess(str4);
            string str5 = "";
            if (contentAccess.Count > 0)
            {
                foreach (ContentLike contentLike in contentAccess)
                {
                    str5 = string.Concat(str5, "<tr>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.ID_CONTENT, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.CONTENT, "</td>");
                    str5 = string.Concat(str5, "<td>", contentLike.ENDDATE, "</td>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.CONTENTACCESS, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.LASTACCESS, "</td>");
                    str5 = string.Concat(str5, "</tr>");
                }
            }
            return string.Concat(string.Concat("<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>", "<tr><td>Content No</td><td>Content</td><td>Expirt Date</td><td># of Times Accessed</td><td>Last Activity</td></tr>"), "</thead><tbody>", str5, "</tbody></table>");
        }

        public string getContentDesignationReport(string rid, string fid, string lid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (rid != "0")
            {
                str = string.Concat(" and c.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (lid != "ALL")
            {
                str2 = string.Concat(" and lower(B.DESIGNATION) like lower('", lid, "') ");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str3 = string.Concat(" and C.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str4 = string.Concat(" and C.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str5 = string.Concat("SELECT d.id_content,d.CONTENT_QUESTION,A.user_designation DESIGNATION,COUNT(C.id_content) AS Contentaccess FROM tbl_user A,tbl_profile B,tbl_content_counters C,tbl_content D WHERE c.id_content = d.id_content AND A.id_user = B.id_user AND A.id_user = C.id_user AND A.id_organization = ", num, " ");
            str5 = string.Concat(string.Concat(new string[] { str5, str, str1, str2, str3, str4 }), " group by A.user_designation,C.id_content order by id_content ,Contentaccess desc limit 20");
            List<ContentLocationGenderWise> designationAccess = (new ContentReportModel()).getDesignationAccess(str5);
            string str6 = "";
            if (designationAccess.Count > 0)
            {
                foreach (ContentLocationGenderWise contentLocationGenderWise in designationAccess)
                {
                    str6 = string.Concat(str6, "<tr>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.ID_CONTENT, "</td>" });
                    str6 = string.Concat(str6, "<td>", contentLocationGenderWise.CONTENT_QUESTION, "</td>");
                    str6 = string.Concat(str6, "<td>", contentLocationGenderWise.DESIGNATION, "</td>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.CONTENTACCESS, "</td>" });
                    str6 = string.Concat(str6, "</tr>");
                }
            }
            return string.Concat(string.Concat("<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>", "<tr><td>Content No</td><td>Content Name</td><td>Designation</td><td># of Content Accessed</td></tr>"), "</thead><tbody>", str6, "</tbody></table>");
        }

        public string getContentLikeReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and a.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and a.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i') ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and a.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT count(a.id_content) COUNTER,a.id_content,date(b.EXPIRY_DATE) EXPIRY_DATE, b.CONTENT_QUESTION,sum(case when choice = 1 then 1 else 0 end) LikeCount,sum(case when choice = 0 then 1 else 0 end) DisLikeCount,MAX(a.UPDATED_DATE_TIME) LASTACCESS from tbl_report_content a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
            str4 = string.Concat(string.Concat(new string[] { str4, str, str1, str2, str3 }), " group by a.id_content order by COUNTER desc limit 100");
            List<ContentLike> contentLikes = (new ContentReportModel()).getContentLikes(str4);
            string str5 = "";
            if (contentLikes.Count > 0)
            {
                foreach (ContentLike contentLike in contentLikes)
                {
                    str5 = string.Concat(str5, "<tr>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.ID_CONTENT, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.CONTENT, "</td>");
                    str5 = string.Concat(str5, "<td>", contentLike.ENDDATE, "</td>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.LIKES, "</td>" });
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.DISLIKES, "</td>" });
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.CONTENTACCESS, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.LASTACCESS, "</td>");
                    str5 = string.Concat(str5, "</tr>");
                }
            }
            return string.Concat(string.Concat("<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>", "<tr><td>Content No</td><td>Content</td><td>Expirt Date</td><td>Like</td><td>Dislike</td><td>Totle Count</td><td>Last Activity</td></tr>"), "</thead><tbody>", str5, "</tbody></table>");
        }

        public string getContentLocationGenderReport(string rid, string fid, string lid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (rid != "0")
            {
                str = string.Concat(" and c.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower ('", fid, "')");
            }
            if (lid != "ALL")
            {
                str2 = string.Concat(" and lower(B.LOCATION) like lower ('", lid, "') ");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str3 = string.Concat(" and C.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str4 = string.Concat(" and C.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str5 = string.Concat("SELECT d.id_content,d.CONTENT_QUESTION,SUM(CASE WHEN B.gender = 'M' THEN 1 ELSE 0 END) MALE, SUM(CASE  WHEN B.gender = 'F' THEN 1    ELSE 0  END) FEMALE,COUNT(C.id_content) AS Contentaccess FROM tbl_user A,tbl_profile B,tbl_content_counters C,tbl_content D WHERE c.id_content = d.id_content AND A.id_user = B.id_user AND A.id_user = C.id_user AND A.id_organization = ", num, " ");
            str5 = string.Concat(string.Concat(new string[] { str5, str, str1, str2, str3, str4 }), "group by C.id_content order by Contentaccess desc limit 20");
            List<ContentLocationGenderWise> contentLocationGender = (new ContentReportModel()).getContentLocationGender(str5);
            string str6 = "";
            if (contentLocationGender.Count > 0)
            {
                foreach (ContentLocationGenderWise contentLocationGenderWise in contentLocationGender)
                {
                    str6 = string.Concat(str6, "<tr>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.ID_CONTENT, "</td>" });
                    str6 = string.Concat(str6, "<td>", contentLocationGenderWise.CONTENT_QUESTION, "</td>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.MALE, "</td>" });
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.FEMALE, "</td>" });
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.CONTENTACCESS, "</td>" });
                    str6 = string.Concat(str6, "</tr>");
                }
            }
            return string.Concat(string.Concat("<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>", "<tr><td>Content No</td><td>Content Name</td><td>Male</td><td>Female</td><td># of Content Accessed</td></tr>"), "</thead><tbody>", str6, "</tbody></table>");
        }

        public string getContentLocationReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and c.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and C.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and C.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT A.id_user,B.FIRSTNAME,B.LASTNAME, A.USERID, count(C.id_content_counters) as Contentaccess ,B.LOCATION FROM tbl_user A, tbl_profile B, tbl_content_counters C WHERE A.id_user = B.id_user AND A.id_user = C.id_user and A.id_organization = ", num, " ");
            str4 = string.Concat(string.Concat(new string[] { str4, str, str1, str2, str3 }), "group by A.id_user order by B.LOCATION, Contentaccess desc limit 100");
            List<ContentLocationWise> contentLocation = (new ContentReportModel()).getContentLocation(str4);
            string str5 = "";
            if (contentLocation.Count > 0)
            {
                foreach (ContentLocationWise contentLocationWise in contentLocation)
                {
                    str5 = string.Concat(str5, "<tr>");
                    str5 = string.Concat(str5, "<td>", contentLocationWise.LOCATION, "</td>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLocationWise.FIRSTNAME, " ", contentLocationWise.LASTNAME, "(", contentLocationWise.USERID, "-", contentLocationWise.ID_USER, ")</td>" });
                    str5 = string.Concat(new object[] { str5, "<td>", contentLocationWise.CONTENTACCESS, "</td>" });
                    str5 = string.Concat(str5, "</tr>");
                }
            }
            return string.Concat(string.Concat("<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>", "<tr><td>Location</td><td>User</td><td># of Content Accessed</td></tr>"), "</thead><tbody>", str5, "</tbody></table>");
        }

        public string getLocationWiseContentAccessReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and a.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and a.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and a.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT count(a.id_content) COUNTER,a.id_content,date(b.EXPIRY_DATE) EXPIRY_DATE, b.CONTENT_QUESTION,MAX(a.UPDATED_DATE_TIME) LASTACCESS from tbl_content_counters a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
            str4 = string.Concat(str4, str, str1);
            str4 = string.Concat(str4, str2, str3);
            str4 = string.Concat(str4, " group by a.id_content order by COUNTER desc limit 100");
            List<List<int>> lists = new List<List<int>>();
            string str5 = "";
            if (rid != "0")
            {
                str5 = string.Concat(" and id_csst_role=", rid, " ");
            }
            string.Concat(new object[] { "select Distinct LOCATION from tbl_profile where id_user in (select id_user from tbl_role_user_mapping where id_organization=", num, str5, ")" });
            List<string> locationList = (new ContentReportModel()).getLocationList(num, str5);
            string str6 = "";
            if (locationList.Count > 0)
            {
                foreach (string str7 in locationList)
                {
                    string str8 = string.Concat(new object[] { "select * from tbl_user where id_user in (select id_user from tbl_role_user_mapping where id_organization=", num, str5, ") and id_user in (select id_user from tbl_profile where lower(location) like lower('", str7, "'))" });
                    foreach (tbl_user list in this.db.tbl_user.SqlQuery(str8, new object[0]).ToList<tbl_user>())
                    {
                    }
                }
                str6 = string.Concat(str6, "<tr>");
            }
            List<ContentLike> contentAccess = (new ContentReportModel()).getContentAccess(str4);
            if (contentAccess.Count > 0)
            {
                foreach (ContentLike contentLike in contentAccess)
                {
                    str6 = string.Concat(str6, "<tr>");
                    str6 = string.Concat(str6, "<td>", contentLike.CONTENT, "</td>");
                    str6 = string.Concat(str6, "<td>", contentLike.ENDDATE, "</td>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLike.CONTENTACCESS, "</td>" });
                    str6 = string.Concat(str6, "<td>", contentLike.LASTACCESS, "</td>");
                    str6 = string.Concat(str6, "</tr>");
                }
            }
            return string.Concat(string.Concat(string.Concat("<table id=\"report-table-month\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>", "<tr><td>Content</td><td>Expirt Date</td><td># of Times Accessed</td><td>Last Activity</td>"), "</tr>"), "</thead><tbody>", str6, "</tbody></table>");
        }

        public string getMonthWiseContentAccessReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and a.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and a.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and a.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT count(a.id_content) COUNTER,a.id_content,date(b.EXPIRY_DATE) EXPIRY_DATE, b.CONTENT_QUESTION,MAX(a.UPDATED_DATE_TIME) LASTACCESS from tbl_content_counters a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
            str4 = string.Concat(str4, str, str1);
            str4 = string.Concat(str4, str2, str3);
            str4 = string.Concat(str4, " group by a.id_content order by COUNTER desc limit 100");
            List<List<int>> lists = new List<List<int>>();
            DateTime datetime = (new Utility()).StringToDatetime(stdate);
            DateTime dateTime = (new Utility()).StringToDatetime(endate);
            int day = datetime.Day;
            int num1 = 0;
            while (datetime < dateTime)
            {
                List<int> nums = new List<int>();
                int num2 = Convert.ToInt32(datetime.ToString("MM"));
                int num3 = Convert.ToInt32(datetime.ToString("yyyy"));
                num1 = DateTime.DaysInMonth(num3, num2);
                nums.Add(num2);
                nums.Add(num3);
                nums.Add(day);
                nums.Add(num1);
                lists.Add(nums);
                day = 1;
                datetime = new DateTime(datetime.Year, datetime.Month, num1);
                datetime = datetime.AddMonths(1);
            }
            List<int> nums1 = new List<int>()
            {
                dateTime.Month,
                dateTime.Year,
                1,
                dateTime.Day
            };
            lists.Add(nums1);
            List<ContentLike> contentAccess = (new ContentReportModel()).getContentAccess(str4);
            string str5 = "";
            if (contentAccess.Count > 0)
            {
                foreach (ContentLike contentLike in contentAccess)
                {
                    str5 = string.Concat(str5, "<tr>");
                    str5 = string.Concat(str5, "<td>", contentLike.CONTENT, "</td>");
                    str5 = string.Concat(str5, "<td>", contentLike.ENDDATE, "</td>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.CONTENTACCESS, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.LASTACCESS, "</td>");
                    foreach (List<int> list in lists)
                    {
                        int item = list[3];
                        string str6 = string.Concat("SELECT count(a.id_content) LIKES,'0' DISLIKES from tbl_content_counters a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
                        str6 = string.Concat(str6, str, str1);
                        str6 = string.Concat(string.Concat(new object[] { str6, " and a.id_content=", contentLike.ID_CONTENT, " and  a.UPDATED_DATE_TIME >= STR_TO_DATE('", list[1], "-", list[2], "-", list[0], " 00,01', '%Y-%d-%m %H,%i') and  a.UPDATED_DATE_TIME <= STR_TO_DATE('", list[1], "-", list[0], "-", item, " 23,59', '%Y-%m-%d %H,%i')" }), " group by a.id_content order by LIKES desc limit 100");
                        MonthData contentCount = (new ContentReportModel()).getContentCount(str6);
                        str5 = string.Concat(new object[] { str5, "<td>", contentCount.LIKES, "</td>" });
                    }
                    str5 = string.Concat(str5, "</tr>");
                }
            }
            string str7 = "<table id=\"report-table-month\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>";
            str7 = string.Concat(str7, "<tr><td>Content</td><td>Expirt Date</td><td># of Times Accessed</td><td>Last Activity</td>");
            foreach (List<int> list1 in lists)
            {
                str7 = string.Concat(new object[] { str7, "<td>", this.monthNames[list1[0] - 1], "-", list1[1], "</td>" });
            }
            str7 = string.Concat(str7, "</tr>");
            str7 = string.Concat(str7, "</thead><tbody>", str5, "</tbody></table>");
            return str7;
        }

        public string getMonthWiseContentDesignationReport(string rid, string fid, string lid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (rid != "0")
            {
                str = string.Concat(" and c.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (lid != "ALL")
            {
                str2 = string.Concat(" and lower(B.DESIGNATION) = lower('", lid, "') ");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str3 = string.Concat(" and C.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str4 = string.Concat(" and C.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str5 = string.Concat("SELECT d.id_content,d.CONTENT_QUESTION,COUNT(C.id_content) AS ContentAccess,A.user_designation DESIGNATION FROM tbl_user A,tbl_profile B,tbl_content_counters C,tbl_content D WHERE c.id_content = d.id_content AND A.id_user = B.id_user AND A.id_user = C.id_user AND A.id_organization = ", num, " ");
            str5 = string.Concat(str5, str, str1, str2);
            str5 = string.Concat(str5, str3, str4);
            str5 = string.Concat(str5, " group by A.user_designation,C.id_content order by id_content ,Contentaccess desc ");
            List<List<int>> lists = new List<List<int>>();
            DateTime datetime = (new Utility()).StringToDatetime(stdate);
            DateTime dateTime = (new Utility()).StringToDatetime(endate);
            int day = datetime.Day;
            int num1 = 0;
            while (datetime < dateTime)
            {
                List<int> nums = new List<int>();
                int num2 = Convert.ToInt32(datetime.ToString("MM"));
                int num3 = Convert.ToInt32(datetime.ToString("yyyy"));
                num1 = DateTime.DaysInMonth(num3, num2);
                nums.Add(num2);
                nums.Add(num3);
                nums.Add(day);
                nums.Add(num1);
                lists.Add(nums);
                day = 1;
                datetime = new DateTime(datetime.Year, datetime.Month, num1);
                datetime = datetime.AddMonths(1);
            }
            List<int> nums1 = new List<int>()
            {
                dateTime.Month,
                dateTime.Year,
                1,
                dateTime.Day
            };
            lists.Add(nums1);
            List<ContentLocationGenderWise> designationAccess = (new ContentReportModel()).getDesignationAccess(str5);
            string str6 = "";
            if (designationAccess.Count > 0)
            {
                foreach (ContentLocationGenderWise contentLocationGenderWise in designationAccess)
                {
                    str6 = string.Concat(str6, "<tr>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.ID_CONTENT, "</td>" });
                    str6 = string.Concat(str6, "<td>", contentLocationGenderWise.CONTENT_QUESTION, "</td>");
                    str6 = string.Concat(str6, "<td>", contentLocationGenderWise.DESIGNATION, "</td>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.CONTENTACCESS, "</td>" });
                    foreach (List<int> list in lists)
                    {
                        int item = list[3];
                        string str7 = string.Concat("SELECT d.id_content,d.CONTENT_QUESTION,COUNT(C.id_content) AS ContentAccess,A.user_designation DESIGNATION FROM tbl_user A,tbl_profile B,tbl_content_counters C,tbl_content D WHERE c.id_content = d.id_content AND A.id_user = B.id_user AND A.id_user = C.id_user AND A.id_organization = ", num, " ");
                        str7 = string.Concat(str7, str, str1, str2);
                        str7 = string.Concat(new object[] { str7, " and upper(A.user_designation) ='", contentLocationGenderWise.DESIGNATION.ToUpper(), "' and c.id_content=", contentLocationGenderWise.ID_CONTENT, " and  C.UPDATED_DATE_TIME >= STR_TO_DATE('", list[1], "-", list[2], "-", list[0], " 00,01', '%Y-%d-%m %H,%i') and  C.UPDATED_DATE_TIME <= STR_TO_DATE('", list[1], "-", list[0], "-", item, " 23,59', '%Y-%m-%d %H,%i')" });
                        MonthData locationCount = (new ContentReportModel()).getLocationCount(str7);
                        str6 = string.Concat(new object[] { str6, "<td>", locationCount.LIKES, "</td>" });
                    }
                    str6 = string.Concat(str6, "</tr>");
                }
            }
            string str8 = "<table id=\"report-table-month\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>";
            str8 = string.Concat(str8, "<tr><td>Content No</td><td>Content Name</td><td>Designation</td><td># of Times Accessed</td>");
            foreach (List<int> list1 in lists)
            {
                str8 = string.Concat(new object[] { str8, "<td>", this.monthNames[list1[0] - 1], "-", list1[1], "</td>" });
            }
            str8 = string.Concat(str8, "</tr>");
            str8 = string.Concat(str8, "</thead><tbody>", str6, "</tbody></table>");
            return str8;
        }

        public string getMonthWiseContentLikeReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and a.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and a.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and a.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT count(a.id_content) COUNTER,a.id_content,date(b.EXPIRY_DATE)EXPIRY_DATE, b.CONTENT_QUESTION,sum(case when choice = 1 then 1 else 0 end) LikeCount,sum(case when choice = 0 then 1 else 0 end) DisLikeCount,MAX(a.UPDATED_DATE_TIME) LASTACCESS from tbl_report_content a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
            str4 = string.Concat(str4, str, str1);
            str4 = string.Concat(str4, str2, str3);
            str4 = string.Concat(str4, " group by a.id_content order by COUNTER desc limit 100");
            List<List<int>> lists = new List<List<int>>();
            DateTime datetime = (new Utility()).StringToDatetime(stdate);
            DateTime dateTime = (new Utility()).StringToDatetime(endate);
            int day = datetime.Day;
            int num1 = 0;
            while (datetime < dateTime)
            {
                List<int> nums = new List<int>();
                int num2 = Convert.ToInt32(datetime.ToString("MM"));
                int num3 = Convert.ToInt32(datetime.ToString("yyyy"));
                num1 = DateTime.DaysInMonth(num3, num2);
                nums.Add(num2);
                nums.Add(num3);
                nums.Add(day);
                nums.Add(num1);
                lists.Add(nums);
                day = 1;
                datetime = new DateTime(datetime.Year, datetime.Month, num1);
                datetime = datetime.AddMonths(1);
            }
            List<int> nums1 = new List<int>()
            {
                dateTime.Month,
                dateTime.Year,
                1,
                dateTime.Day
            };
            lists.Add(nums1);
            List<ContentLike> contentAccess = (new ContentReportModel()).getContentAccess(str4);
            string str5 = "";
            if (contentAccess.Count > 0)
            {
                foreach (ContentLike contentLike in contentAccess)
                {
                    str5 = string.Concat(str5, "<tr>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.ID_CONTENT, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.CONTENT, "</td>");
                    str5 = string.Concat(str5, "<td>", contentLike.ENDDATE, "</td>");
                    str5 = string.Concat(new object[] { str5, "<td>", contentLike.CONTENTACCESS, "</td>" });
                    str5 = string.Concat(str5, "<td>", contentLike.LASTACCESS, "</td>");
                    foreach (List<int> list in lists)
                    {
                        int item = list[3];
                        string str6 = string.Concat("SELECT sum(case when choice = 1 then 1 else 0 end) LIKES,sum(case when choice = 0 then 1 else 0 end) DISLIKES from tbl_report_content a,tbl_content b,tbl_user c,tbl_profile d,tbl_csst_role e,tbl_role_user_mapping f where a.id_content=b.id_content and a.id_user=c.id_user and c.id_user=d.id_user  and e.id_csst_role = f.id_csst_role and f.id_user = c.ID_USER  and e.id_organization = ", num, " ");
                        str6 = string.Concat(str6, str, str1);
                        str6 = string.Concat(new object[] { str6, " and a.id_content=", contentLike.ID_CONTENT, " and  a.UPDATED_DATE_TIME >= STR_TO_DATE('", list[1], "-", list[2], "-", list[0], " 00,01', '%Y-%d-%m %H,%i') and  a.UPDATED_DATE_TIME <= STR_TO_DATE('", list[1], "-", list[0], "-", item, " 23,59', '%Y-%m-%d %H,%i')" });
                        MonthData contentCount = (new ContentReportModel()).getContentCount(str6);
                        str5 = string.Concat(new object[] { str5, "<td>", contentCount.LIKES, "/", contentCount.DISLIKES, "</td>" });
                    }
                    str5 = string.Concat(str5, "</tr>");
                }
            }
            string str7 = "<table id=\"report-table-month\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>";
            str7 = string.Concat(str7, "<tr><td>Content No</td><td>Content</td><td>Expirt Date</td><td># of Times Accessed</td><td>Last Activity</td>");
            foreach (List<int> list1 in lists)
            {
                str7 = string.Concat(new object[] { str7, "<td>", this.monthNames[list1[0] - 1], "-", list1[1], "</td>" });
            }
            str7 = string.Concat(str7, "</tr>");
            str7 = string.Concat(str7, "</thead><tbody>", str5, "</tbody></table>");
            return str7;
        }

        public string getMonthWiseContentLocationGenderReport(string rid, string fid, string lid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (rid != "0")
            {
                str = string.Concat(" and c.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (lid != "ALL")
            {
                str2 = string.Concat(" and lower(B.LOCATION) like lower('", lid, "') ");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str3 = string.Concat(" and C.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str4 = string.Concat(" and C.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str5 = string.Concat("SELECT d.id_content,d.CONTENT_QUESTION,SUM(CASE WHEN B.gender = 'M' THEN 1 ELSE 0 END) MALE, SUM(CASE  WHEN B.gender = 'F' THEN 1    ELSE 0  END) FEMALE,COUNT(C.id_content) AS Contentaccess FROM tbl_user A,tbl_profile B,tbl_content_counters C,tbl_content D WHERE c.id_content = d.id_content AND A.id_user = B.id_user AND A.id_user = C.id_user AND A.id_organization = ", num, " ");
            str5 = string.Concat(str5, str, str1, str2);
            str5 = string.Concat(str5, str3, str4);
            str5 = string.Concat(str5, "group by C.id_content order by Contentaccess desc limit 20");
            List<List<int>> lists = new List<List<int>>();
            DateTime datetime = (new Utility()).StringToDatetime(stdate);
            DateTime dateTime = (new Utility()).StringToDatetime(endate);
            int day = datetime.Day;
            int num1 = 0;
            while (datetime < dateTime)
            {
                List<int> nums = new List<int>();
                int num2 = Convert.ToInt32(datetime.ToString("MM"));
                int num3 = Convert.ToInt32(datetime.ToString("yyyy"));
                num1 = DateTime.DaysInMonth(num3, num2);
                nums.Add(num2);
                nums.Add(num3);
                nums.Add(day);
                nums.Add(num1);
                lists.Add(nums);
                day = 1;
                datetime = new DateTime(datetime.Year, datetime.Month, num1);
                datetime = datetime.AddMonths(1);
            }
            List<int> nums1 = new List<int>()
            {
                dateTime.Month,
                dateTime.Year,
                1,
                dateTime.Day
            };
            lists.Add(nums1);
            List<ContentLocationGenderWise> locationGenderAccess = (new ContentReportModel()).getLocationGenderAccess(str5);
            string str6 = "";
            if (locationGenderAccess.Count > 0)
            {
                foreach (ContentLocationGenderWise contentLocationGenderWise in locationGenderAccess)
                {
                    str6 = string.Concat(str6, "<tr>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.ID_CONTENT, "</td>" });
                    str6 = string.Concat(str6, "<td>", contentLocationGenderWise.CONTENT_QUESTION, "</td>");
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.MALE, "</td>" });
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.FEMALE, "</td>" });
                    str6 = string.Concat(new object[] { str6, "<td>", contentLocationGenderWise.CONTENTACCESS, "</td>" });
                    foreach (List<int> list in lists)
                    {
                        int item = list[3];
                        string str7 = string.Concat("SELECT d.id_content,d.CONTENT_QUESTION,SUM(CASE WHEN B.gender = 'M' THEN 1 ELSE 0 END) MALE,SUM(CASE WHEN B.gender='F' THEN 1 ELSE 0 END) FEMALE,COUNT(C.id_content) AS Contentaccess FROM tbl_user A, tbl_profile B, tbl_content_counters C, tbl_content D WHERE c.id_content = d.id_content AND A.id_user = B.id_user AND A.id_user = C.id_user AND A.id_organization = ", num, " ");
                        str7 = string.Concat(str7, str, str1, str2);
                        str7 = string.Concat(new object[] { str7, " and c.id_content=", contentLocationGenderWise.ID_CONTENT, " and  C.UPDATED_DATE_TIME >= STR_TO_DATE('", list[1], "-", list[2], "-", list[0], " 00,01', '%Y-%d-%m %H,%i') and  C.UPDATED_DATE_TIME <= STR_TO_DATE('", list[1], "-", list[0], "-", item, " 23,59', '%Y-%m-%d %H,%i')" });
                        MonthData locationGenderCount = (new ContentReportModel()).getLocationGenderCount(str7);
                        str6 = string.Concat(new object[] { str6, "<td>", locationGenderCount.LIKES, "/", locationGenderCount.DISLIKES, "</td>" });
                    }
                    str6 = string.Concat(str6, "</tr>");
                }
            }
            string str8 = "<table id=\"report-table-month\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>";
            str8 = string.Concat(str8, "<tr><td>Content No</td><td>Content Name</td><td>Male</td><td>Female</td><td># of Times Accessed</td>");
            foreach (List<int> list1 in lists)
            {
                str8 = string.Concat(new object[] { str8, "<td>", this.monthNames[list1[0] - 1], "-", list1[1], "</td>" });
            }
            str8 = string.Concat(str8, "</tr>");
            str8 = string.Concat(str8, "</thead><tbody>", str6, "</tbody></table>");
            return str8;
        }

        public string getMonthWiseContentLocationReport(string rid, string fid, string stdate, string endate)
        {
            int num = Convert.ToInt32(((UserSession)base.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (rid != "0")
            {
                str = string.Concat(" and c.id_user in (select id_user from tbl_role_user_mapping where id_csst_role=", rid, ") ");
            }
            if (fid != "ALL")
            {
                str1 = string.Concat(" and lower(user_function) like lower('", fid, "')");
            }
            if (!string.IsNullOrEmpty(stdate))
            {
                str2 = string.Concat(" and C.UPDATED_DATE_TIME >= STR_TO_DATE('", stdate, " 00,01', '%d-%m-%Y %H,%i')  ");
            }
            if (!string.IsNullOrEmpty(endate))
            {
                str3 = string.Concat(" and C.UPDATED_DATE_TIME <= STR_TO_DATE('", endate, " 23,59', '%d-%m-%Y %H,%i') ");
            }
            string str4 = string.Concat("SELECT A.id_user,B.FIRSTNAME,B.LASTNAME, A.USERID, count(C.id_content_counters) as Contentaccess ,B.LOCATION FROM tbl_user A, tbl_profile B, tbl_content_counters C WHERE A.id_user = B.id_user AND A.id_user = C.id_user and A.id_organization = ", num, " ");
            str4 = string.Concat(str4, str, str1);
            str4 = string.Concat(str4, str2, str3);
            str4 = string.Concat(str4, " group by A.id_user order by B.LOCATION, Contentaccess desc limit 100");
            List<List<int>> lists = new List<List<int>>();
            DateTime datetime = (new Utility()).StringToDatetime(stdate);
            DateTime dateTime = (new Utility()).StringToDatetime(endate);
            int day = datetime.Day;
            int num1 = 0;
            while (datetime < dateTime)
            {
                List<int> nums = new List<int>();
                int num2 = Convert.ToInt32(datetime.ToString("MM"));
                int num3 = Convert.ToInt32(datetime.ToString("yyyy"));
                num1 = DateTime.DaysInMonth(num3, num2);
                nums.Add(num2);
                nums.Add(num3);
                nums.Add(day);
                nums.Add(num1);
                lists.Add(nums);
                day = 1;
                datetime = new DateTime(datetime.Year, datetime.Month, num1);
                datetime = datetime.AddMonths(1);
            }
            List<int> nums1 = new List<int>()
            {
                dateTime.Month,
                dateTime.Year,
                1,
                dateTime.Day
            };
            lists.Add(nums1);
            List<ContentLocationWise> locationAccess = (new ContentReportModel()).getLocationAccess(str4);
            string str5 = "";
            if (locationAccess.Count > 0)
            {
                foreach (ContentLocationWise contentLocationWise in locationAccess)
                {
                    str5 = string.Concat(str5, "<tr>");
                    str5 = string.Concat(str5, "<td>", contentLocationWise.LOCATION, "</td>");
                    str5 = string.Concat(new string[] { str5, "<td>", contentLocationWise.FIRSTNAME, " ", contentLocationWise.LASTNAME, "(", contentLocationWise.USERID, ")</td>" });
                    str5 = string.Concat(new object[] { str5, "<td>", contentLocationWise.CONTENTACCESS, "</td>" });
                    foreach (List<int> list in lists)
                    {
                        int item = list[3];
                        string str6 = string.Concat("SELECT count(C.id_content_counters) ContentAccess FROM tbl_user A, tbl_profile B, tbl_content_counters C WHERE A.id_user = B.id_user AND A.id_user = C.id_user and A.id_organization = ", num, " ");
                        str6 = string.Concat(str6, str, str1);
                        str6 = string.Concat(new object[] { str6, " and C.id_user=", contentLocationWise.ID_USER, " and  C.UPDATED_DATE_TIME >= STR_TO_DATE('", list[1], "-", list[2], "-", list[0], " 00,01', '%Y-%d-%m %H,%i') and  C.UPDATED_DATE_TIME <= STR_TO_DATE('", list[1], "-", list[0], "-", item, " 23,59', '%Y-%m-%d %H,%i')" });
                        MonthData locationCount = (new ContentReportModel()).getLocationCount(str6);
                        str5 = string.Concat(new object[] { str5, "<td>", locationCount.LIKES, "</td>" });
                    }
                    str5 = string.Concat(str5, "</tr>");
                }
            }
            string str7 = "<table id=\"report-table-month\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>";
            str7 = string.Concat(str7, "<tr><td>Location</td><td>User</td><td># of Content Accessed</td>");
            foreach (List<int> list1 in lists)
            {
                str7 = string.Concat(new object[] { str7, "<td>", this.monthNames[list1[0] - 1], "-", list1[1], "</td>" });
            }
            str7 = string.Concat(str7, "</tr>");
            str7 = string.Concat(str7, "</thead><tbody>", str5, "</tbody></table>");
            return str7;
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult Likes()
        {
            UserSession item = (UserSession)base.HttpContext.Session.Contents["UserSession"];
            int num = Convert.ToInt32(item.id_ORGANIZATION);
            List<string> list = (
                from t in this.db.tbl_user
                where t.ID_ORGANIZATION == (int?)num
                select t into p
                select p.user_function).Distinct<string>().ToList<string>();
            list = (
                from s in list
                where !string.IsNullOrWhiteSpace(s)
                select s).ToList<string>();
            List<tbl_csst_role> tblCsstRoles = (
                from t in this.db.tbl_csst_role
                where t.id_organization == (int?)num
                select t).ToList<tbl_csst_role>();
            base.ViewData["functions"] = list;
            base.ViewData["roleList"] = tblCsstRoles;
            return base.View();
        }

        public ActionResult LocationReport()
        {
            UserSession item = (UserSession)base.HttpContext.Session.Contents["UserSession"];
            int num = Convert.ToInt32(item.id_ORGANIZATION);
            List<string> list = (
                from t in this.db.tbl_user
                where t.ID_ORGANIZATION == (int?)num
                select t into p
                select p.user_function).Distinct<string>().ToList<string>();
            list = (
                from s in list
                where !string.IsNullOrWhiteSpace(s)
                select s).ToList<string>();
            List<tbl_csst_role> tblCsstRoles = (
                from t in this.db.tbl_csst_role
                where t.id_organization == (int?)num
                select t).ToList<tbl_csst_role>();
            base.ViewData["functions"] = list;
            base.ViewData["roleList"] = tblCsstRoles;
            return base.View();
        }
    }
}