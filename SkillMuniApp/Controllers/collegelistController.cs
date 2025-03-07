// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.collegelistController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
    public class collegelistController : Controller
    {
        public ActionResult Index() => (ActionResult)this.View();

        public ActionResult CollegeDasboard()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            collegeListmodel.CityModelNew cityModel = new collegeListmodel.CityModelNew();
            m2ostnext.Models.State state2;
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ////cityModel = JsonConvert.DeserializeObject<collegeListmodel.CityModel>(Encoding.UTF8.GetString(webClient.DownloadData("https://coroebus.net/crm/api.php?type=getCities&stateId=4121")));
                cityModel = JsonConvert.DeserializeObject<collegeListmodel.CityModelNew>(Encoding.UTF8.GetString(webClient.DownloadData("https://www.skillmuni.in/sulapiproduction/api/getCities?stateId=4121")));
            }
            List<tbl_college_list> tblCollegeListList = new List<tbl_college_list>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblCollegeListList = m2ostDbContext.Database.SqlQuery<tbl_college_list>("select * from tbl_college_list where status = 'A' and id_organization={0}", (object)int32).ToList<tbl_college_list>();

            state2 = JsonConvert.DeserializeObject<m2ostnext.Models.State>(new SULFestBusinessLogic().getApiResponseString("http://www.skillmuni.in/sulapiproduction/api/getStates?countryId=101"));
            this.ViewData["tcd"] = (object)tblCollegeListList;
            this.ViewData["Cm"] = (object)cityModel;
            this.ViewData["stateList"] = (object)state2;
            return (ActionResult)this.View();
        }

        public ActionResult add_new_college()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            tbl_college_list tblCollegeList = new tbl_college_list();
            tblCollegeList.college_name = this.Request.Form["c-name"].ToString();
            tblCollegeList.clg_city = this.Request.Form["city"].ToString();
            tblCollegeList.clg_state = this.Request.Form["state"].ToString();
            tblCollegeList.clg_phone = this.Request.Form["c-phone"].ToString();
            tblCollegeList.status = "A";
            tblCollegeList.id_organization = int32_1;
            tblCollegeList.id_user = int32_2;
            tblCollegeList.updated_datetime = DateTime.Now;
            string[] strArray = new string[0];
            if (!string.IsNullOrEmpty(tblCollegeList.clg_city))
                strArray = tblCollegeList.clg_city.Split(',');
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_college_list` (`college_name`,`clg_city`,`clg_state`,`clg_phone`,`status`,`id_city`,`id_organization`,`id_user`,`updated_datetime`) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8})", (object)tblCollegeList.college_name, (object)strArray[1], (object)tblCollegeList.clg_state, (object)tblCollegeList.clg_phone, (object)tblCollegeList.status, (object)Convert.ToInt32(strArray[0]), (object)tblCollegeList.id_organization, (object)tblCollegeList.id_user, (object)tblCollegeList.updated_datetime);
            return (ActionResult)this.RedirectToAction("CollegeDasboard");
        }

        public ActionResult Edit_colleg_details()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            Convert.ToInt32(content.id_ORGANIZATION);
            int int32 = Convert.ToInt32(content.ID_USER);
            tbl_college_list tblCollegeList = new tbl_college_list();
            tblCollegeList.id_college = Convert.ToInt32(this.Request.Form["ColgID"].ToString());
            tblCollegeList.college_name = this.Request.Form["editClgName"].ToString();
            tblCollegeList.clg_city = this.Request.Form["editClgCity"].ToString();
            tblCollegeList.clg_state = this.Request.Form["editClgState"].ToString();
            tblCollegeList.clg_phone = this.Request.Form["editClgPhone"].ToString();
            tblCollegeList.id_user = int32;
            tblCollegeList.updated_datetime = DateTime.Now;
            string[] strArray = new string[0];
            if (!string.IsNullOrEmpty(tblCollegeList.clg_city))
                strArray = tblCollegeList.clg_city.Split(',');
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_college_list` SET `college_name` = {0},`clg_city` = {1},`clg_state` = {2},`clg_phone` = {3},`id_city`={7},`id_user` = {4},`updated_datetime` = {5} WHERE `id_college` = {6}", (object)tblCollegeList.college_name, (object)strArray[1].ToString(), (object)tblCollegeList.clg_state, (object)tblCollegeList.clg_phone, (object)tblCollegeList.id_user, (object)tblCollegeList.updated_datetime, (object)tblCollegeList.id_college, (object)Convert.ToInt32(strArray[0]));
            return (ActionResult)this.RedirectToAction("CollegeDasboard");
        }

        public ActionResult delete_collage_details()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            int int32 = Convert.ToInt32(this.Request.Form["DelMetricID"].ToString());
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_college_list` SET `status` = 'D' WHERE `id_college` ={0}", (object)int32);
            return (ActionResult)this.RedirectToAction("CollegeDasboard");
        }

        public ActionResult referral_code_dashboard()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            List<collegeListmodel.tbl_referral_code_master1> referralCodeMasterList = new List<collegeListmodel.tbl_referral_code_master1>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                referralCodeMasterList = m2ostDbContext.Database.SqlQuery<collegeListmodel.tbl_referral_code_master1>("select * from tbl_referral_code_master where status='A' and id_organization={0}", (object)int32).ToList<collegeListmodel.tbl_referral_code_master1>();
            this.ViewData["trcm"] = (object)referralCodeMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult add_new_referral_code()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            collegeListmodel.tbl_referral_code_master1 referralCodeMaster = new collegeListmodel.tbl_referral_code_master1();
            referralCodeMaster.referral_code = this.Request.Form["r-code"].ToString();
            referralCodeMaster.referral_name = this.Request.Form["r-name"].ToString();
            referralCodeMaster.id_organization = int32_1;
            referralCodeMaster.id_user = new int?(int32_2);
            referralCodeMaster.status = "A";
            referralCodeMaster.updated_date_time = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_referral_code_master` (`referral_code`,`referral_name`,`id_organization`,`id_user`,`status`,`updated_date_time`) VALUES({0},{1},{2},{3},{4},{5})", (object)referralCodeMaster.referral_code, (object)referralCodeMaster.referral_name, (object)referralCodeMaster.id_organization, (object)referralCodeMaster.id_user, (object)referralCodeMaster.status, (object)referralCodeMaster.updated_date_time);
            return (ActionResult)this.RedirectToAction("referral_code_dashboard");
        }

        public ActionResult Edit_referral_code()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            Convert.ToInt32(content.id_ORGANIZATION);
            int int32 = Convert.ToInt32(content.ID_USER);
            collegeListmodel.tbl_referral_code_master1 referralCodeMaster = new collegeListmodel.tbl_referral_code_master1();
            referralCodeMaster.id_referral_code = Convert.ToInt32(this.Request.Form["refID"].ToString());
            referralCodeMaster.referral_code = this.Request.Form["editRefCode"].ToString();
            referralCodeMaster.referral_name = this.Request.Form["editRefName"].ToString();
            referralCodeMaster.id_user = new int?(int32);
            referralCodeMaster.updated_date_time = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_referral_code_master` SET `referral_code` = {0},`referral_name` = {1},`id_user` ={2} ,`updated_date_time` ={3} WHERE `id_referral_code` ={4}", (object)referralCodeMaster.referral_code, (object)referralCodeMaster.referral_name, (object)referralCodeMaster.id_user, (object)referralCodeMaster.updated_date_time, (object)referralCodeMaster.id_referral_code);
            return (ActionResult)this.RedirectToAction("referral_code_dashboard");
        }

        public ActionResult delete_referral_code()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            int int32 = Convert.ToInt32(this.Request.Form["DelrefID"].ToString());
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_referral_code_master` SET `status` = 'D' WHERE `id_referral_code` = {0}", (object)int32);
            return (ActionResult)this.RedirectToAction("referral_code_dashboard");
        }

        public string collegeCityVal(int cid)
        {
            tbl_college_list tblCollegeList = new tbl_college_list();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblCollegeList = m2ostDbContext.Database.SqlQuery<tbl_college_list>("select * from tbl_college_list where id_college={0}", (object)cid).FirstOrDefault<tbl_college_list>();
            return tblCollegeList.id_city.ToString() + "," + tblCollegeList.clg_city;
        }
    }
}
