// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.SULFestController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
    public class SULFestController : Controller
    {
        public ActionResult Index() => (ActionResult)this.View();

        public ActionResult CreateEventLayout()
        {
            List<tbl_event_type_master> tblEventTypeMasterList = new List<tbl_event_type_master>();
            m2ostnext.Models.State state1 = new m2ostnext.Models.State();
            List<tbl_college_list> tblCollegeListList = new List<tbl_college_list>();
            List<tbl_sub_event_type_master> subEventTypeMasterList = new List<tbl_sub_event_type_master>();
            List<tbl_event_sponsor_master> eventSponsorMasterList = new List<tbl_event_sponsor_master>();
            m2ostnext.Models.State state2;
            try
            {
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    tblEventTypeMasterList = m2ostDbContext.Database.SqlQuery<tbl_event_type_master>("select * from tbl_event_type_master where status='A' ").ToList<tbl_event_type_master>();
                    tblCollegeListList = m2ostDbContext.Database.SqlQuery<tbl_college_list>("select * from tbl_college_list where status = 'A' and id_organization =130").ToList<tbl_college_list>();
                    subEventTypeMasterList = m2ostDbContext.Database.SqlQuery<tbl_sub_event_type_master>("select * from tbl_sub_event_type_master where status='A' ").ToList<tbl_sub_event_type_master>();
                    eventSponsorMasterList = m2ostDbContext.Database.SqlQuery<tbl_event_sponsor_master>("select * from tbl_event_sponsor_master where status='A' ").ToList<tbl_event_sponsor_master>();
                }
                ////state2 = JsonConvert.DeserializeObject<m2ostnext.Models.State>(new SULFestBusinessLogic().getApiResponseString("https://coroebus.net/crm/api.php?type=getStates&countryId=101"));
                state2 = JsonConvert.DeserializeObject<m2ostnext.Models.State>(new SULFestBusinessLogic().getApiResponseString("http://www.skillmuni.in/sulapiproduction/api/getStates?countryId=101"));
                ////var ABC = new SULFestBusinessLogic().getApiResponseString("http://www.skillmuni.in/sulapiproduction/api/getStates?countryId=101");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.ViewData["event_type"] = (object)tblEventTypeMasterList;
            this.ViewData["stateList"] = (object)state2;
            this.ViewData["college"] = (object)tblCollegeListList;
            this.ViewData["sub_type"] = (object)subEventTypeMasterList;
            this.ViewData["sponsor"] = (object)eventSponsorMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult CreateEventAction()
        {
            try
            {
                tbl_sul_fest_master fes = new tbl_sul_fest_master();
                List<tbl_event_type_mapping> eventTypeMappingList1 = new List<tbl_event_type_mapping>();
                List<tbl_sub_event_type_mapping> eventTypeMappingList2 = new List<tbl_sub_event_type_mapping>();
                fes.event_title = this.Request.Form["event-title"];
                fes.event_objective = this.Request.Form["event-Objective"];
                fes.is_registration_needed = new int?(Convert.ToInt32(this.Request.Form["is_reg_needed"]));
                int? registrationNeeded = fes.is_registration_needed;
                int num1 = 1;
                if (registrationNeeded.GetValueOrDefault() == num1 & registrationNeeded.HasValue)
                {
                    fes.registration_start_date = new Utility().StringToDatetime(this.Request.Form["regis-start"].ToString());
                    fes.registration_end_date = new Utility().StringToDatetime(this.Request.Form["regis-end"].ToString());
                }
                fes.event_start_date = new Utility().StringToDatetime(this.Request.Form["event-start"].ToString());
                fes.event_end_date = new Utility().StringToDatetime(this.Request.Form["event-end"].ToString());
                fes.event_duration = this.Request.Form["event-duration"];
                fes.location_text = this.Request.Form["event-location"];
                fes.state = this.Request.Form["state"];
                fes.city = this.Request.Form["city"];
                fes.address = this.Request.Form["address"];
                fes.is_event_closed = new int?(Convert.ToInt32(this.Request.Form["closed_event"]));
                int? nullable = fes.is_event_closed;
                int num2 = 1;
                if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
                    fes.user_count = new int?(Convert.ToInt32(this.Request.Form["participants_count"]));
                fes.is_college_restricted = new int?(Convert.ToInt32(this.Request.Form["college_restriction"]));
                nullable = fes.is_college_restricted;
                int num3 = 1;
                if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
                    fes.id_college = new int?(Convert.ToInt32(this.Request.Form["college"]));
                fes.is_paid_event = new int?(Convert.ToInt32(this.Request.Form["paid"]));
                nullable = fes.is_paid_event;
                int num4 = 1;
                if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
                    fes.amount = this.Request.Form["amount"];
                fes.is_org_specified = new int?(0);
                fes.is_sponsor_available = new int?(Convert.ToInt32(this.Request.Form["sponsor"]));
                nullable = fes.is_sponsor_available;
                int num5 = 1;
                if (nullable.GetValueOrDefault() == num5 & nullable.HasValue)
                    fes.id_sponsor = new int?(Convert.ToInt32(this.Request.Form["sponsor_id"]));
                fes.event_status = "D";
                fes.status = "A";
                fes.contact_name = this.Request.Form["contact_name"];
                fes.contact_number = this.Request.Form["contact_number"];
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    fes.id_event = m2ostDbContext.Database.SqlQuery<int>("insert into tbl_sul_fest_master (event_title,event_objective,event_logo,is_registration_needed,registration_start_date,registration_end_date,event_start_date,event_end_date,event_duration,location_text,state,city,address,is_event_closed,user_count,is_college_restricted,id_college,is_paid_event,is_org_specified,id_org,is_sponsor_available,id_sponsor,sponsor_logo,event_status,status,updated_date_time,amount,contact_name,contact_number) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28});select max(id_event) from tbl_sul_fest_master", (object)fes.event_title, (object)fes.event_objective, (object)fes.event_logo, (object)fes.is_registration_needed, (object)fes.registration_start_date, (object)fes.registration_end_date, (object)fes.event_start_date, (object)fes.event_end_date, (object)fes.event_duration, (object)fes.location_text, (object)fes.state, (object)fes.city, (object)fes.address, (object)fes.is_event_closed, (object)fes.user_count, (object)fes.is_college_restricted, (object)fes.id_college, (object)fes.is_paid_event, (object)fes.is_org_specified, (object)fes.id_org, (object)fes.is_sponsor_available, (object)fes.id_sponsor, (object)fes.sponsor_logo, (object)fes.event_status, (object)fes.status, (object)DateTime.Now, (object)fes.amount, (object)fes.contact_name, (object)fes.contact_number).FirstOrDefault<int>();
                    if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                    {
                        HttpPostedFile file1 = System.Web.HttpContext.Current.Request.Files["event-logo-btn"];
                        HttpPostedFile file2 = System.Web.HttpContext.Current.Request.Files["event-logo-btn"];
                        if (file2 != null)
                        {
                            string extension = Path.GetExtension(file2.FileName);
                            if (file1.ContentLength > 0)
                            {
                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestLogo/")))
                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestLogo/"));
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestLogo/"), fes.id_event.ToString() + extension);
                                file1.SaveAs(filename);
                                fes.event_logo = fes.id_event.ToString() + extension;
                                m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_fest_master set event_logo={0} where id_event={1} ", (object)fes.event_logo, (object)fes.id_event);
                            }
                        }
                    }
                    nullable = fes.is_sponsor_available;
                    int num6 = 1;
                    if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
                    {
                        fes.id_sponsor = new int?(Convert.ToInt32(this.Request.Form["sponsor_id"]));
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file3 = System.Web.HttpContext.Current.Request.Files["sponsor-logo-btn"];
                            HttpPostedFile file4 = System.Web.HttpContext.Current.Request.Files["sponsor-logo-btn"];
                            if (file4 != null)
                            {
                                string extension = Path.GetExtension(file4.FileName);
                                if (file3.ContentLength > 0)
                                {
                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestSponsorLogo/")))
                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestSponsorLogo/"));
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestSponsorLogo/"), fes.id_event.ToString() + "sponsor" + extension);
                                    file3.SaveAs(filename);
                                    fes.sponsor_logo = fes.id_event.ToString() + "sponsor" + extension;
                                    m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_fest_master set sponsor_logo={0} where id_event={1} ", (object)fes.sponsor_logo, (object)fes.id_event);
                                }
                            }
                        }
                    }
                    if (fes.event_status == "P")
                    {
                        List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
                        new SULFestBusinessLogic().SendEventPushNotification(m2ostDbContext.Database.SqlQuery<tbl_user_gcm_log>("select * from tbl_user_gcm_log where status='A'").ToList<tbl_user_gcm_log>(), fes);
                        List<tbl_profile> tblProfileList = new List<tbl_profile>();
                        new SULFestBusinessLogic().SendEventMailNotification(m2ostDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile inner join tbl_user on tbl_user.ID_USER= tbl_profile.ID_USER where tbl_user.ID_ORGANIZATION=130 and tbl_user.STATUS='A'").ToList<tbl_profile>(), fes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("EventsDashboard");
        }

        public ActionResult EventsDashboard()
        {
            List<tbl_sul_fest_master> tblSulFestMasterList = new List<tbl_sul_fest_master>();
            try
            {
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    tblSulFestMasterList = m2ostDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from tbl_sul_fest_master where status='A'").ToList<tbl_sul_fest_master>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.ViewData["mas"] = (object)tblSulFestMasterList;
            return (ActionResult)this.View();
        }

        public ActionResult EditEventLayout(int event_id)
        {
            List<tbl_event_type_master> tblEventTypeMasterList1 = new List<tbl_event_type_master>();
            m2ostnext.Models.State state1 = new m2ostnext.Models.State();
            city city1 = new city();
            List<tbl_college_list> tblCollegeListList = new List<tbl_college_list>();
            List<tbl_sub_event_type_master> subEventTypeMasterList1 = new List<tbl_sub_event_type_master>();
            List<tbl_event_sponsor_master> eventSponsorMasterList = new List<tbl_event_sponsor_master>();
            tbl_sul_fest_master model = new tbl_sul_fest_master();
            List<tbl_event_type_master> tblEventTypeMasterList2 = new List<tbl_event_type_master>();
            List<tbl_sub_event_type_master> subEventTypeMasterList2 = new List<tbl_sub_event_type_master>();
            m2ostnext.Models.State state2;
            ////city city2;
            collegeListmodel.CityModelNew city2 = new collegeListmodel.CityModelNew();
            try
            {
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    tblEventTypeMasterList1 = m2ostDbContext.Database.SqlQuery<tbl_event_type_master>("select * from tbl_event_type_master where status='A' ").ToList<tbl_event_type_master>();
                    tblCollegeListList = m2ostDbContext.Database.SqlQuery<tbl_college_list>("select * from tbl_college_list where status = 'A' and id_organization =130").ToList<tbl_college_list>();
                    subEventTypeMasterList1 = m2ostDbContext.Database.SqlQuery<tbl_sub_event_type_master>("select * from tbl_sub_event_type_master where status='A' ").ToList<tbl_sub_event_type_master>();
                    eventSponsorMasterList = m2ostDbContext.Database.SqlQuery<tbl_event_sponsor_master>("select * from tbl_event_sponsor_master where status='A' ").ToList<tbl_event_sponsor_master>();
                    model = m2ostDbContext.Database.SqlQuery<tbl_sul_fest_master>("SELECT * FROM tbl_sul_fest_master WHERE id_event =" + (object)event_id + " AND status='A'").FirstOrDefault<tbl_sul_fest_master>();
                    tblEventTypeMasterList2 = m2ostDbContext.Database.SqlQuery<tbl_event_type_master>("SELECT * FROM db_sul_pre_prd.tbl_event_type_master WHERE status= 'A' and id_event_type in (SELECT id_event_type FROM db_sul_pre_prd.tbl_event_type_mapping WHERE status='A' and id_event='" + (object)event_id + "')").ToList<tbl_event_type_master>();
                    subEventTypeMasterList2 = m2ostDbContext.Database.SqlQuery<tbl_sub_event_type_master>("SELECT * FROM  tbl_sub_event_type_master WHERE status='A' AND id_sub_event_type IN (SELECT id_sub_event_type FROM db_sul_pre_prd.tbl_sub_event_type_mapping WHERE status = 'A' AND id_event = '" + (object)event_id + "')").ToList<tbl_sub_event_type_master>();
                    model.cityname = m2ostDbContext.Database.SqlQuery<string>("select name from cities where id={0}", (object)model.city).FirstOrDefault<string>();
                }
                //////state2 = JsonConvert.DeserializeObject<m2ostnext.Models.State>(new SULFestBusinessLogic().getApiResponseString("https://coroebus.net/crm/api.php?type=getStates&countryId=101"));
                //////city2 = JsonConvert.DeserializeObject<city>(new SULFestBusinessLogic().getApiResponseString("https://coroebus.net/crm/api.php?type=getCities&stateId=4121"));

                state2 = JsonConvert.DeserializeObject<m2ostnext.Models.State>(new SULFestBusinessLogic().getApiResponseString("http://www.skillmuni.in/sulapiproduction/api/getStates?countryId=101"));
                city2 = JsonConvert.DeserializeObject<collegeListmodel.CityModelNew>(new SULFestBusinessLogic().getApiResponseString("https://www.skillmuni.in/sulapiproduction/api/getCities?stateId=4121"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.ViewData["event_type"] = (object)tblEventTypeMasterList1;
            this.ViewData["stateList"] = (object)state2;
            this.ViewData["cityList"] = (object)city2;
            this.ViewData["college"] = (object)tblCollegeListList;
            this.ViewData["sub_type"] = (object)subEventTypeMasterList1;
            this.ViewData["sponsor"] = (object)eventSponsorMasterList;
            this.ViewData["sul_fest"] = (object)model;
            this.ViewData["map"] = (object)tblEventTypeMasterList2;
            this.ViewData["sub"] = (object)subEventTypeMasterList2;
            return (ActionResult)this.View((object)model);
        }

        public ActionResult EditEventAction()
        {
            try
            {
                int int32 = Convert.ToInt32(this.Request.Form["event_id"]);
                tbl_sul_fest_master fes = new tbl_sul_fest_master();
                List<tbl_event_type_mapping> eventTypeMappingList1 = new List<tbl_event_type_mapping>();
                List<tbl_sub_event_type_mapping> eventTypeMappingList2 = new List<tbl_sub_event_type_mapping>();
                fes.event_title = this.Request.Form["event-title"];
                fes.event_objective = this.Request.Form["event-Objective"];
                fes.event_logo = this.Request.Form["event-logo"];
                fes.is_registration_needed = new int?(Convert.ToInt32(this.Request.Form["is_reg_needed"]));
                int? registrationNeeded = fes.is_registration_needed;
                int num1 = 1;
                if (registrationNeeded.GetValueOrDefault() == num1 & registrationNeeded.HasValue)
                {
                    fes.registration_start_date = new Utility().StringToDatetime(this.Request.Form["regis-start"].ToString());
                    fes.registration_end_date = new Utility().StringToDatetime(this.Request.Form["regis-end"].ToString());
                }
                fes.event_start_date = new Utility().StringToDatetime(this.Request.Form["event-start"].ToString());
                fes.event_end_date = new Utility().StringToDatetime(this.Request.Form["event-end"].ToString());
                fes.event_duration = this.Request.Form["event-duration"];
                fes.location_text = this.Request.Form["event-location"];
                fes.state = this.Request.Form["state"];
                fes.city = this.Request.Form["city"];
                fes.address = this.Request.Form["address"];
                fes.is_event_closed = new int?(Convert.ToInt32(this.Request.Form["closed_event"]));
                int? isEventClosed = fes.is_event_closed;
                int num2 = 1;
                if (isEventClosed.GetValueOrDefault() == num2 & isEventClosed.HasValue)
                    fes.user_count = new int?(Convert.ToInt32(this.Request.Form["participants_count"]));
                fes.is_college_restricted = new int?(Convert.ToInt32(this.Request.Form["college_restriction"]));
                int? collegeRestricted = fes.is_college_restricted;
                int num3 = 1;
                if (collegeRestricted.GetValueOrDefault() == num3 & collegeRestricted.HasValue)
                    fes.id_college = new int?(Convert.ToInt32(this.Request.Form["college"]));
                fes.is_paid_event = new int?(Convert.ToInt32(this.Request.Form["paid"]));
                int? isPaidEvent = fes.is_paid_event;
                int num4 = 1;
                if (isPaidEvent.GetValueOrDefault() == num4 & isPaidEvent.HasValue)
                    fes.amount = this.Request.Form["amount"];
                fes.is_org_specified = new int?(0);
                fes.is_sponsor_available = new int?(Convert.ToInt32(this.Request.Form["sponsor"]));
                int? sponsorAvailable = fes.is_sponsor_available;
                int num5 = 1;
                if (sponsorAvailable.GetValueOrDefault() == num5 & sponsorAvailable.HasValue)
                    fes.id_sponsor = new int?(Convert.ToInt32(this.Request.Form["sponsor_id"]));
                fes.event_status = "D";
                fes.status = "A";
                fes.contact_name = this.Request.Form["contact_name"];
                fes.contact_number = this.Request.Form["contact_number"];
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    m2ostDbContext.Database.ExecuteSqlCommand("UPDATE  tbl_sul_fest_master SET event_title={0},event_objective={1},event_logo={2},is_registration_needed={3},registration_start_date={4},registration_end_date={5},event_start_date={6},event_end_date={7},event_duration={8},location_text={9},state={10},city={11},address={12},is_event_closed={13},user_count={14},is_college_restricted={15},id_college={16},is_paid_event={17},is_org_specified={18},id_org={19},is_sponsor_available={20},id_sponsor={21},sponsor_logo={22},event_status={23},status={24},updated_date_time={25},amount={26},contact_name={27},contact_number={28} WHERE id_event={29};select max(id_event) from tbl_sul_fest_master", (object)fes.event_title, (object)fes.event_objective, (object)fes.event_logo, (object)fes.is_registration_needed, (object)fes.registration_start_date, (object)fes.registration_end_date, (object)fes.event_start_date, (object)fes.event_end_date, (object)fes.event_duration, (object)fes.location_text, (object)fes.state, (object)fes.city, (object)fes.address, (object)fes.is_event_closed, (object)fes.user_count, (object)fes.is_college_restricted, (object)fes.id_college, (object)fes.is_paid_event, (object)fes.is_org_specified, (object)fes.id_org, (object)fes.is_sponsor_available, (object)fes.id_sponsor, (object)fes.sponsor_logo, (object)fes.event_status, (object)fes.status, (object)DateTime.Now, (object)fes.amount, (object)fes.contact_name, (object)fes.contact_number, (object)int32);
                    if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                    {
                        HttpPostedFile file1 = System.Web.HttpContext.Current.Request.Files["event-logo-btn"];
                        HttpPostedFile file2 = System.Web.HttpContext.Current.Request.Files["event-logo-btn"];
                        if (file2 != null)
                        {
                            string extension = Path.GetExtension(file2.FileName);
                            if (file1.ContentLength > 0)
                            {
                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestLogo/")))
                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestLogo/"));
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestLogo/"), int32.ToString() + extension);
                                file1.SaveAs(filename);
                                fes.event_logo = int32.ToString() + extension;
                                m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_fest_master set event_logo={0} where id_event={1} ", (object)fes.event_logo, (object)int32);
                            }
                        }
                    }
                    sponsorAvailable = fes.is_sponsor_available;
                    int num6 = 1;
                    if (sponsorAvailable.GetValueOrDefault() == num6 & sponsorAvailable.HasValue)
                    {
                        fes.id_sponsor = new int?(Convert.ToInt32(this.Request.Form["sponsor_id"]));
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file3 = System.Web.HttpContext.Current.Request.Files["sponsor-logo-btn"];
                            HttpPostedFile file4 = System.Web.HttpContext.Current.Request.Files["sponsor-logo-btn"];
                            if (file4 != null && file4.FileName != "")
                            {
                                string extension = Path.GetExtension(file4.FileName);
                                if (file3.ContentLength > 0)
                                {
                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestSponsorLogo/")))
                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestSponsorLogo/"));
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/SULFestSponsorLogo/"), int32.ToString() + "sponsor" + extension);
                                    file3.SaveAs(filename);
                                    fes.sponsor_logo = int32.ToString() + "sponsor" + extension;
                                    m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_fest_master set sponsor_logo={0} where id_event={1} ", (object)fes.sponsor_logo, (object)int32);
                                }
                            }
                        }
                    }
                    if (fes.event_status == "P")
                    {
                        List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
                        new SULFestBusinessLogic().SendEventPushNotification(m2ostDbContext.Database.SqlQuery<tbl_user_gcm_log>("select * from tbl_user_gcm_log where status='A'").ToList<tbl_user_gcm_log>(), fes);
                        List<tbl_profile> tblProfileList = new List<tbl_profile>();
                        new SULFestBusinessLogic().SendEventMailNotification(m2ostDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile inner join tbl_user on tbl_user.ID_USER= tbl_profile.ID_USER where tbl_user.ID_ORGANIZATION=130 and tbl_user.STATUS='A'").ToList<tbl_profile>(), fes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("EventsDashboard");
        }
    }
}
