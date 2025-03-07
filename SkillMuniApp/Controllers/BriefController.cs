// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.BriefController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
    [UserFilter]
    public class BriefController : Controller
    {
        private db_m2ostEntities db = new db_m2ostEntities();

        public ActionResult Index()
        {
            this.ViewData["category"] = (object)this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            return (ActionResult)this.View();
        }

        public ActionResult AddCategory()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = this.Request.Form["category-title"].ToString();
            if (!string.IsNullOrEmpty(str))
            {
                this.db.tbl_brief_category.Add(new tbl_brief_category()
                {
                    brief_category = str.Trim(),
                    id_organization = new int?(int32),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("Index");
        }

        public ActionResult AddSubCategory()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = this.Request.Form["sub-category-title"].ToString();
            if (!string.IsNullOrEmpty(str))
            {
                this.db.tbl_brief_subcategory.Add(new tbl_brief_subcategory()
                {
                    brief_subcategory = str.Trim(),
                    id_organization = new int?(int32),
                    id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"].ToString())),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("Index");
        }

        public ActionResult dashboard()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<briefView> briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code, CASE WHEN employee_name IS NULL THEN username ELSE CONCAT(USERNAME, ' [', employee_name, ']') END creator_name FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d, tbl_cms_users e " + "  WHERE a.id_organization=" + (object)int32 + "  AND a.id_user = e.ID_USER AND   a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master and d.status_code < 7 order by a.id_brief_master desc");
            List<briefView> briefView2 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.updated_date_time scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)int32 + " and  a.status = 'X' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master and d.status_code=7 order by a.id_brief_master desc");
            this.ViewData["briefmaster"] = (object)briefView1;
            this.ViewData["deletedbriefs"] = (object)briefView2;
            return (ActionResult)this.View();
        }

        public ActionResult approvalDashboard()
        {
            this.ViewData["briefmaster"] = (object)new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + " and  a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master   AND d.status_code=1");
            return (ActionResult)this.View();
        }

        public ActionResult briefDraft()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["subcategory"] = (object)list2;
            return (ActionResult)this.View();
        }

        public ActionResult briefThemeDraft()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["subcategory"] = (object)list2;
            return (ActionResult)this.View();
        }

        public ActionResult questionDashboard()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>)(t => t.status == "A")).ToList<tbl_brief_question_complexity>();
            List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblBriefQuestionList = m2ostDbContext.Database.SqlQuery<tbl_brief_question>("select t1.*,t2.brief_category as brief_category_name,t3.brief_subcategory as brief_subcategory_name, t4.question_complexity_label as complexity_name from tbl_brief_question t1 inner join tbl_brief_category t2 on t1.id_brief_category = t2.id_brief_category inner join tbl_brief_subcategory t3 on t1.id_brief_sub_category = t3.id_brief_subcategory inner join tbl_brief_question_complexity t4 on t1.question_complexity = t4.question_complexity where t1.id_organization = {0} order by t1.brief_question", (object)int32).ToList<tbl_brief_question>();
            this.ViewData["qList"] = (object)tblBriefQuestionList;
            return (ActionResult)this.View();
        }

        public ActionResult DeleteQuestion(int id_qtn)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            try
            {
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("delete from tbl_brief_question where id_brief_question={0}", (object)id_qtn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("questionDashboard");
        }

        public ActionResult question(int flag = 0)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["complexity"] = (object)this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity where status='A' ").ToList<tbl_brief_question_complexity>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["subcategory"] = (object)list2;
            this.ViewData[nameof(flag)] = (object)flag;
            return (ActionResult)this.View();
        }

        public string randomCode()
        {
            int count = 8;
            Random random = new Random();
            return new string(Enumerable.Repeat<string>("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", count).Select<string, char>((Func<string, char>)(s => s[random.Next(s.Length)])).ToArray<char>());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult finishDratfBrief(string jdata, string rtcode)
        {
            JObject jobject = JObject.Parse(jdata);
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str1 = "BRF-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + this.randomCode();
            tbl_brief_master entity = new tbl_brief_master();
            entity.brief_title = jobject["brief-title"].ToString();
            entity.brief_code = str1;
            entity.brief_description = rtcode;
            entity.id_brief_category = new int?(Convert.ToInt32((object)jobject["select-category"]));
            entity.id_brief_sub_category = new int?(Convert.ToInt32((object)jobject["select-sub-category"]));
            DateTime now = DateTime.Now;
            DateTime datetime = new Utility().StringToDatetime(jobject["brief-expiry"].ToString());
            entity.scheduled_timestamp = new DateTime?(datetime);
            entity.override_dnd = new int?(Convert.ToInt32((object)jobject["overide-dnd"]));
            int int32_2 = Convert.ToInt32((object)jobject["question-option"]);
            entity.is_add_question = new int?(int32_2);
            int num1 = 0;
            if (int32_2 == 1)
                num1 = Convert.ToInt32((object)jobject["question-count"]);
            entity.question_count = new int?(num1);
            entity.id_organization = new int?(int32_1);
            int int32_3 = Convert.ToInt32((object)jobject["brief-distribution-category-option"]);
            entity.brief_type = new int?(int32_3);
            entity.status = "A";
            entity.updated_date_time = new DateTime?(DateTime.Now);
            this.db.tbl_brief_master.Add(entity);
            this.db.SaveChanges();
            if (entity.id_brief_master > 0)
            {
                if (int32_3 == 2)
                {
                    string str2 = jobject["brf-cat-val"].ToString().Trim(' ', ',');
                    char[] chArray = new char[1] { ',' };
                    foreach (string str3 in str2.Split(chArray))
                    {
                        this.db.tbl_brief_category_mapping.Add(new tbl_brief_category_mapping()
                        {
                            id_brief_category = new int?(Convert.ToInt32(str3)),
                            id_brief_master = new int?(entity.id_brief_master),
                            id_organization = new int?(int32_1),
                            status = "A",
                            updated_date_time = new DateTime?(DateTime.Now)
                        });
                        this.db.SaveChanges();
                    }
                }
                string str4 = jobject["brief-metadata"].ToString();
                char[] chArray1 = new char[1] { ',' };
                foreach (string str5 in str4.Split(chArray1))
                {
                    string str6 = str5.Trim();
                    this.db.tbl_brief_metadata.Add(new tbl_brief_metadata()
                    {
                        id_brief_master = new int?(entity.id_brief_master),
                        brief_metadata = str6,
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                List<int> intList = new List<int>();
                for (int index = 1; index <= 10; ++index)
                {
                    if (jobject["row-qtn-list-" + (object)index] != null)
                    {
                        int int32_4 = Convert.ToInt32((object)jobject["row-qtn-list-" + (object)index]);
                        intList.Add(int32_4);
                    }
                }
                foreach (int num2 in intList)
                {
                    this.db.tbl_brief_question_mapping.Add(new tbl_brief_question_mapping()
                    {
                        id_brief_master = new int?(entity.id_brief_master),
                        id_brief_question = new int?(num2),
                        question_link_type = new int?(1),
                        date_time_stamp = new DateTime?(DateTime.Now),
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(entity.id_brief_master),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(1),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Draft"
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult finishDratfBrief1()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            string str1 = "BRF-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + this.randomCode();
            tbl_brief_master entity1 = new tbl_brief_master();
            entity1.brief_title = this.Request.Form["brief-title"].ToString();
            entity1.brief_code = str1;
            entity1.brief_description = "";
            entity1.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
            entity1.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
            DateTime now1 = DateTime.Now;
            DateTime datetime = new Utility().StringToDatetime(this.Request.Form["brief-expiry"].ToString());
            entity1.scheduled_timestamp = new DateTime?(datetime);
            entity1.override_dnd = new int?(Convert.ToInt32(this.Request.Form["overide-dnd"]));
            int int32_3 = Convert.ToInt32(this.Request.Form["question-option"]);
            entity1.brief_attachment_flag = new int?(Convert.ToInt32(this.Request.Form["isEnquiry"].ToString()));
            entity1.is_add_question = new int?(0);
            int num1 = 0;
            entity1.is_add_question = new int?(int32_3);
            int? briefAttachmentFlag = entity1.brief_attachment_flag;
            int num2 = 0;
            if (briefAttachmentFlag.GetValueOrDefault() == num2 & briefAttachmentFlag.HasValue)
            {
                entity1.is_add_question = new int?(int32_3);
                if (int32_3 == 1)
                {
                    entity1.brief_attachment_flag = new int?(1);
                    num1 = Convert.ToInt32(this.Request.Form["question-count"]);
                }
                else
                    entity1.brief_attachment_flag = new int?(2);
            }
            else if (Convert.ToInt32(this.Request.Form["urlStus"].ToString()) == 0)
            {
                entity1.brief_attachment_flag = new int?(4);
                entity1.brief_attachement_url = this.Request.Form["Enquiry_url"].ToString();
            }
            else
            {
                entity1.brief_attachment_flag = new int?(3);
                entity1.brief_attachement_url = "";
            }
            entity1.question_count = new int?(num1);
            entity1.id_organization = new int?(int32_1);
            int int32_4 = Convert.ToInt32(this.Request.Form["brief-distribution-category-option"]);
            entity1.brief_type = new int?(int32_4);
            entity1.status = "A";
            entity1.updated_date_time = new DateTime?(DateTime.Now);
            this.db.tbl_brief_master.Add(entity1);
            this.db.SaveChanges();
            int idBriefMaster = entity1.id_brief_master;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_brief_master` SET `id_user` = {0},`brief_attachment_flag`={2},`brief_attachement_url`={3} WHERE `id_brief_master` ={1}", (object)int32_2, (object)idBriefMaster, (object)entity1.brief_attachment_flag, (object)entity1.brief_attachement_url);
                m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'C',{3})", (object)idBriefMaster, (object)int32_1, (object)int32_2, (object)DateTime.Now);
            }
            if (entity1.id_brief_master > 0)
            {
                DateTime now2 = DateTime.Now;
                string str2 = now2.ToString("yyyy");
                now2 = DateTime.Now;
                string str3 = now2.ToString("MM");
                string str4 = entity1.brief_code.Replace("-", "");
                tbl_brief_master_template entity2 = new tbl_brief_master_template();
                entity2.id_brief_master = new int?(entity1.id_brief_master);
                entity2.brief_code = entity1.brief_code;
                int int32_5 = Convert.ToInt32(this.Request.Form["template-card"].ToString());
                string str5 = "";
                if (int32_5 < 3)
                    str5 = "NA";
                if (int32_5 == 3)
                {
                    int int32_6 = Convert.ToInt32(this.Request.Form["t3-data-order"].ToString());
                    if (int32_6 == 1)
                        str5 = "LR";
                    if (int32_6 == 2)
                        str5 = "RL";
                }
                if (int32_5 == 4)
                {
                    int int32_7 = Convert.ToInt32(this.Request.Form["t4-data-order"].ToString());
                    if (int32_7 == 1)
                        str5 = "TB";
                    if (int32_7 == 2)
                        str5 = "BT";
                }
                entity2.resource_order = str5;
                entity2.brief_template = int32_5.ToString();
                entity2.status = "A";
                entity2.updated_date_time = new DateTime?(DateTime.Now);
                entity2.brief_destination = str4;
                this.db.tbl_brief_master_template.Add(entity2);
                this.db.SaveChanges();
                string str6 = "";
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/")))
                {
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/"));
                    str6 = "/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/";
                }
                if (int32_5 == 1)
                {
                    string HTMLCode = this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                    this.db.tbl_brief_master_body.Add(new tbl_brief_master_body()
                    {
                        id_brief_master = new int?(entity1.id_brief_master),
                        brief_code = entity1.brief_code,
                        brief_destination = str6,
                        brief_template = int32_5.ToString(),
                        media_type = new int?(1),
                        resouce_code = "NA",
                        resource_mime = "NA",
                        resource_number = "NA",
                        resource_type = new int?(1),
                        srno = new int?(1),
                        resouce_data = new HtmltoText().HTMLText(HTMLCode),
                        status = "A",
                        file_extension = "NA",
                        file_type = "NA",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                if (int32_5 == 2)
                {
                    tbl_brief_master_body entity3 = new tbl_brief_master_body();
                    entity3.id_brief_master = new int?(entity1.id_brief_master);
                    entity3.brief_code = entity1.brief_code;
                    entity3.brief_destination = str6;
                    entity3.brief_template = int32_5.ToString();
                    entity3.status = "A";
                    entity3.updated_date_time = new DateTime?(DateTime.Now);
                    if (Convert.ToInt32(this.Request.Form["t2-select-order"].ToString()) == 1)
                    {
                        string str7 = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                        entity3.media_type = new int?(1);
                        entity3.resouce_code = str7;
                        entity3.resource_mime = "NA";
                        entity3.resource_number = "NA";
                        entity3.resource_type = new int?(2);
                        entity3.srno = new int?(1);
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["t2-btn"].FileName);
                            string path2 = str7 + extension;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/"), path2);
                                file.SaveAs(filename);
                            }
                            entity3.resouce_data = path2;
                            entity3.file_extension = extension;
                            entity3.file_type = new BriefModel().getExtensionNumber(extension);
                            this.db.tbl_brief_master_body.Add(entity3);
                            this.db.SaveChanges();
                        }
                    }
                    else
                    {
                        entity3.media_type = new int?(2);
                        entity3.resouce_code = "NA";
                        entity3.resource_mime = "NA";
                        entity3.resource_number = "NA";
                        entity3.resource_type = new int?(2);
                        entity3.srno = new int?(1);
                        entity3.resouce_data = this.Request.Form["t2-btn-url"].ToString();
                        entity3.file_extension = "NA";
                        entity3.file_type = "NA";
                        this.db.tbl_brief_master_body.Add(entity3);
                        this.db.SaveChanges();
                    }
                }
                if (int32_5 == 3)
                {
                    this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                    tbl_brief_master_body entity4 = new tbl_brief_master_body();
                    entity4.id_brief_master = new int?(entity1.id_brief_master);
                    entity4.brief_code = entity1.brief_code;
                    entity4.brief_destination = str6;
                    entity4.brief_template = int32_5.ToString();
                    entity4.media_type = new int?(1);
                    entity4.resouce_code = "NA";
                    entity4.resource_mime = "NA";
                    entity4.resource_number = "NA";
                    entity4.resource_type = new int?(1);
                    entity4.srno = new int?(1);
                    entity4.resouce_data = this.Request.Form["brief-rt-data-t3"].ToString();
                    entity4.status = "A";
                    entity4.updated_date_time = new DateTime?(DateTime.Now);
                    entity4.file_extension = "NA";
                    entity4.file_type = "NA";
                    this.db.tbl_brief_master_body.Add(entity4);
                    this.db.SaveChanges();
                    tbl_brief_master_body entity5 = new tbl_brief_master_body();
                    entity5.id_brief_master = new int?(entity1.id_brief_master);
                    entity5.brief_code = entity1.brief_code;
                    entity5.brief_destination = str6;
                    entity5.brief_template = int32_5.ToString();
                    entity5.status = "A";
                    entity5.updated_date_time = new DateTime?(DateTime.Now);
                    if (Convert.ToInt32(this.Request.Form["t3-select-order"].ToString()) == 1)
                    {
                        string str8 = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                        entity5.media_type = new int?(1);
                        entity5.resouce_code = str8;
                        entity5.resource_mime = "NA";
                        entity5.resource_number = "NA";
                        entity5.resource_type = new int?(2);
                        entity5.srno = new int?(2);
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["t3-btn"];
                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["t3-btn"].FileName);
                            string path2 = str8 + extension;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/"), path2);
                                file.SaveAs(filename);
                            }
                            entity5.resouce_data = path2;
                            entity5.file_extension = extension;
                            entity5.file_type = new BriefModel().getExtensionNumber(extension);
                            this.db.tbl_brief_master_body.Add(entity5);
                            this.db.SaveChanges();
                        }
                    }
                    else
                    {
                        entity5.media_type = new int?(2);
                        entity5.resouce_code = "NA";
                        entity5.resource_mime = "NA";
                        entity5.resource_number = "NA";
                        entity5.resource_type = new int?(2);
                        entity5.srno = new int?(2);
                        entity5.resouce_data = this.Request.Form["t3-btn-url"].ToString();
                        entity4.file_extension = "NA";
                        entity4.file_type = "NA";
                        this.db.tbl_brief_master_body.Add(entity5);
                        this.db.SaveChanges();
                    }
                }
                if (int32_5 == 4)
                {
                    string HTMLCode = this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                    this.db.tbl_brief_master_body.Add(new tbl_brief_master_body()
                    {
                        id_brief_master = new int?(entity1.id_brief_master),
                        brief_code = entity1.brief_code,
                        brief_destination = str6,
                        brief_template = int32_5.ToString(),
                        media_type = new int?(1),
                        resouce_code = "NA",
                        resource_mime = "NA",
                        resource_number = "NA",
                        resource_type = new int?(1),
                        srno = new int?(1),
                        resouce_data = new HtmltoText().HTMLText(HTMLCode),
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now),
                        file_extension = "NA",
                        file_type = "NA"
                    });
                    this.db.SaveChanges();
                    tbl_brief_master_body entity6 = new tbl_brief_master_body();
                    entity6.id_brief_master = new int?(entity1.id_brief_master);
                    entity6.brief_code = entity1.brief_code;
                    entity6.brief_destination = str6;
                    entity6.brief_template = int32_5.ToString();
                    entity6.status = "A";
                    entity6.updated_date_time = new DateTime?(DateTime.Now);
                    if (Convert.ToInt32(this.Request.Form["t4-select-order"].ToString()) == 1)
                    {
                        string str9 = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                        entity6.media_type = new int?(1);
                        entity6.resouce_code = str9;
                        entity6.resource_mime = "NA";
                        entity6.resource_number = "NA";
                        entity6.resource_type = new int?(2);
                        entity6.srno = new int?(2);
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["t4-btn"];
                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["t4-btn"].FileName);
                            string path2 = str9 + extension;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/"), path2);
                                file.SaveAs(filename);
                            }
                            entity6.resouce_data = path2;
                            entity6.file_extension = extension;
                            entity6.file_type = new BriefModel().getExtensionNumber(extension);
                            this.db.tbl_brief_master_body.Add(entity6);
                            this.db.SaveChanges();
                        }
                    }
                    else
                    {
                        entity6.media_type = new int?(2);
                        entity6.resouce_code = "NA";
                        entity6.resource_mime = "NA";
                        entity6.resource_number = "NA";
                        entity6.resource_type = new int?(2);
                        entity6.srno = new int?(2);
                        entity6.resouce_data = this.Request.Form["t4-btn-url"].ToString();
                        entity6.file_extension = "NA";
                        entity6.file_type = "NA";
                        this.db.tbl_brief_master_body.Add(entity6);
                        this.db.SaveChanges();
                    }
                }
                if (int32_4 == 2)
                {
                    string str10 = this.Request.Form["brf-cat-val"].ToString().Trim(' ', ',');
                    char[] chArray = new char[1] { ',' };
                    foreach (string str11 in str10.Split(chArray))
                    {
                        this.db.tbl_brief_category_mapping.Add(new tbl_brief_category_mapping()
                        {
                            id_brief_category = new int?(Convert.ToInt32(str11)),
                            id_brief_master = new int?(entity1.id_brief_master),
                            id_organization = new int?(int32_1),
                            status = "A",
                            updated_date_time = new DateTime?(DateTime.Now)
                        });
                        this.db.SaveChanges();
                    }
                }
                string str12 = this.Request.Form["brief-metadata"].ToString();
                char[] chArray1 = new char[1] { ',' };
                foreach (string str13 in str12.Split(chArray1))
                {
                    string str14 = str13.Trim();
                    this.db.tbl_brief_metadata.Add(new tbl_brief_metadata()
                    {
                        id_brief_master = new int?(entity1.id_brief_master),
                        brief_metadata = str14,
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                List<int> intList = new List<int>();
                for (int index = 1; index <= 10; ++index)
                {
                    if (this.Request.Form["row-qtn-list-" + (object)index] != null)
                    {
                        int int32_8 = Convert.ToInt32(this.Request.Form["row-qtn-list-" + (object)index]);
                        intList.Add(int32_8);
                    }
                }
                foreach (int num3 in intList)
                {
                    this.db.tbl_brief_question_mapping.Add(new tbl_brief_question_mapping()
                    {
                        id_brief_master = new int?(entity1.id_brief_master),
                        id_brief_question = new int?(num3),
                        question_link_type = new int?(1),
                        date_time_stamp = new DateTime?(DateTime.Now),
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(entity1.id_brief_master),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(1),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Draft"
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult finishModifyBrief()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            int bid = Convert.ToInt32(this.Request.Form["id_brief_master"].ToString());
            tbl_brief_master briefmaster = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.id_brief_master == bid)).FirstOrDefault<tbl_brief_master>();
            int num1 = 0;
            if (briefmaster != null)
            {
                briefmaster.brief_title = this.Request.Form["brief-title"].ToString();
                briefmaster.brief_description = "";
                briefmaster.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                briefmaster.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                DateTime now = DateTime.Now;
                DateTime datetime = new Utility().StringToDatetime(this.Request.Form["brief-expiry"].ToString());
                briefmaster.scheduled_timestamp = new DateTime?(datetime);
                briefmaster.override_dnd = new int?(Convert.ToInt32(this.Request.Form["overide-dnd"]));
                int int32_3 = Convert.ToInt32(this.Request.Form["question-option"]);
                briefmaster.brief_attachment_flag = new int?(Convert.ToInt32(this.Request.Form["isEnquiry"].ToString()));
                briefmaster.is_add_question = new int?(0);
                int num2 = 0;
                int? briefAttachmentFlag = briefmaster.brief_attachment_flag;
                int num3 = 0;
                if (briefAttachmentFlag.GetValueOrDefault() == num3 & briefAttachmentFlag.HasValue)
                {
                    briefmaster.is_add_question = new int?(int32_3);
                    if (int32_3 == 1)
                    {
                        briefmaster.brief_attachment_flag = new int?(1);
                        num2 = Convert.ToInt32(this.Request.Form["question-count"]);
                    }
                    else
                        briefmaster.brief_attachment_flag = new int?(2);
                }
                else if (Convert.ToInt32(this.Request.Form["urlStus"].ToString()) == 0)
                {
                    briefmaster.brief_attachment_flag = new int?(4);
                    briefmaster.brief_attachement_url = this.Request.Form["Enquiry_url"].ToString();
                }
                else
                {
                    briefmaster.brief_attachment_flag = new int?(3);
                    briefmaster.brief_attachement_url = "";
                }
                briefmaster.question_count = new int?(num2);
                briefmaster.id_organization = new int?(int32_1);
                num1 = Convert.ToInt32(this.Request.Form["brief-distribution-category-option"]);
                briefmaster.brief_type = new int?(num1);
                briefmaster.status = "A";
                briefmaster.updated_date_time = new DateTime?(DateTime.Now);
                this.db.SaveChanges();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_brief_master` SET `id_user` = {0},`brief_attachment_flag`={2},`brief_attachement_url`={3} WHERE `id_brief_master` ={1}", (object)int32_2, (object)bid, (object)briefmaster.brief_attachment_flag, (object)briefmaster.brief_attachement_url);
                    m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'E',{3})", (object)bid, (object)int32_1, (object)int32_2, (object)DateTime.Now);
                }
                tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
                if (briefMasterTemplate != null)
                {
                    briefMasterTemplate.status = "A";
                    briefMasterTemplate.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                    int int32_4 = Convert.ToInt32(briefMasterTemplate.brief_template);
                    if (int32_4 == 1)
                    {
                        string HTMLCode = this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                        tbl_brief_master_body tblBriefMasterBody = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody.media_type = new int?(1);
                        tblBriefMasterBody.resouce_code = "NA";
                        tblBriefMasterBody.resource_mime = "NA";
                        tblBriefMasterBody.resource_number = "NA";
                        tblBriefMasterBody.resource_type = new int?(1);
                        tblBriefMasterBody.srno = new int?(1);
                        tblBriefMasterBody.resouce_data = new HtmltoText().HTMLText(HTMLCode);
                        tblBriefMasterBody.status = "A";
                        tblBriefMasterBody.file_extension = "NA";
                        tblBriefMasterBody.file_type = "NA";
                        tblBriefMasterBody.updated_date_time = new DateTime?(DateTime.Now);
                        this.db.SaveChanges();
                    }
                    if (int32_4 == 2)
                    {
                        tbl_brief_master_body tblBriefMasterBody = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody.status = "A";
                        tblBriefMasterBody.updated_date_time = new DateTime?(DateTime.Now);
                        if (Convert.ToInt32(this.Request.Form["t2-select-order"].ToString()) == 1)
                        {
                            string str = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                            tblBriefMasterBody.media_type = new int?(1);
                            tblBriefMasterBody.resouce_code = str;
                            tblBriefMasterBody.resource_mime = "NA";
                            tblBriefMasterBody.resource_number = "NA";
                            tblBriefMasterBody.resource_type = new int?(2);
                            tblBriefMasterBody.srno = new int?(1);
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file1 = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                                HttpPostedFile file2 = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                                if (file1 != null && file1.ContentLength > 0)
                                {
                                    string extension = Path.GetExtension(file2.FileName);
                                    string path2 = str + extension;
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~" + tblBriefMasterBody.brief_destination.Replace("\\", "/") ?? ""), path2);
                                    file1.SaveAs(filename);
                                    tblBriefMasterBody.resouce_data = path2;
                                    tblBriefMasterBody.file_extension = extension;
                                    tblBriefMasterBody.file_type = new BriefModel().getExtensionNumber(extension);
                                    this.db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            tblBriefMasterBody.media_type = new int?(2);
                            tblBriefMasterBody.resouce_code = "NA";
                            tblBriefMasterBody.resource_mime = "NA";
                            tblBriefMasterBody.resource_number = "NA";
                            tblBriefMasterBody.resource_type = new int?(2);
                            tblBriefMasterBody.srno = new int?(1);
                            tblBriefMasterBody.resouce_data = this.Request.Form["t2-btn-url"].ToString();
                            tblBriefMasterBody.file_extension = "NA";
                            tblBriefMasterBody.file_type = "NA";
                            this.db.SaveChanges();
                        }
                    }
                    if (int32_4 == 3)
                    {
                        this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                        tbl_brief_master_body tblBriefMasterBody1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.resource_type == (int?)1)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody1.media_type = new int?(1);
                        tblBriefMasterBody1.resouce_code = "NA";
                        tblBriefMasterBody1.resource_mime = "NA";
                        tblBriefMasterBody1.resource_number = "NA";
                        tblBriefMasterBody1.resource_type = new int?(1);
                        tblBriefMasterBody1.srno = new int?(1);
                        tblBriefMasterBody1.resouce_data = this.Request.Form["brief-rt-data-t3"].ToString();
                        tblBriefMasterBody1.status = "A";
                        tblBriefMasterBody1.updated_date_time = new DateTime?(DateTime.Now);
                        tblBriefMasterBody1.file_extension = "NA";
                        tblBriefMasterBody1.file_type = "NA";
                        this.db.SaveChanges();
                        tbl_brief_master_body tblBriefMasterBody2 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.resource_type == (int?)2)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody2.status = "A";
                        tblBriefMasterBody2.updated_date_time = new DateTime?(DateTime.Now);
                        if (Convert.ToInt32(this.Request.Form["t3-select-order"].ToString()) == 1)
                        {
                            string str1 = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                            tblBriefMasterBody2.media_type = new int?(1);
                            tblBriefMasterBody2.resouce_code = str1;
                            tblBriefMasterBody2.resource_mime = "NA";
                            tblBriefMasterBody2.resource_number = "NA";
                            tblBriefMasterBody2.resource_type = new int?(2);
                            tblBriefMasterBody2.srno = new int?(2);
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file3 = System.Web.HttpContext.Current.Request.Files["t3-btn"];
                                HttpPostedFile file4 = System.Web.HttpContext.Current.Request.Files["t3-btn"];
                                if (file3 != null && file3.ContentLength > 0)
                                {
                                    string extension = Path.GetExtension(file4.FileName);
                                    string path2 = str1 + extension;
                                    string str2 = tblBriefMasterBody2.brief_destination.Replace("\\", "/");
                                    string path = Path.Combine(this.HttpContext.Server.MapPath("~" + str2 ?? ""));
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~" + str2 ?? ""), path2);
                                    if (!Directory.Exists(path))
                                        Directory.CreateDirectory(path);
                                    file3.SaveAs(filename);
                                    tblBriefMasterBody2.resouce_data = path2;
                                    tblBriefMasterBody2.file_extension = extension;
                                    tblBriefMasterBody2.file_type = new BriefModel().getExtensionNumber(extension);
                                    this.db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            tblBriefMasterBody2.media_type = new int?(2);
                            tblBriefMasterBody2.resouce_code = "NA";
                            tblBriefMasterBody2.resource_mime = "NA";
                            tblBriefMasterBody2.resource_number = "NA";
                            tblBriefMasterBody2.resource_type = new int?(2);
                            tblBriefMasterBody2.srno = new int?(2);
                            tblBriefMasterBody2.resouce_data = this.Request.Form["t3-btn-url"].ToString();
                            tblBriefMasterBody2.file_extension = "NA";
                            tblBriefMasterBody2.file_type = "NA";
                            this.db.SaveChanges();
                        }
                    }
                    if (int32_4 == 4)
                    {
                        string HTMLCode = this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                        tbl_brief_master_body tblBriefMasterBody3 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.resource_type == (int?)1)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody3.media_type = new int?(1);
                        tblBriefMasterBody3.resouce_code = "NA";
                        tblBriefMasterBody3.resource_mime = "NA";
                        tblBriefMasterBody3.resource_number = "NA";
                        tblBriefMasterBody3.resource_type = new int?(1);
                        tblBriefMasterBody3.srno = new int?(1);
                        tblBriefMasterBody3.resouce_data = new HtmltoText().HTMLText(HTMLCode);
                        tblBriefMasterBody3.status = "A";
                        tblBriefMasterBody3.updated_date_time = new DateTime?(DateTime.Now);
                        tblBriefMasterBody3.file_extension = "NA";
                        tblBriefMasterBody3.file_type = "NA";
                        this.db.SaveChanges();
                        tbl_brief_master_body tblBriefMasterBody4 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.resource_type == (int?)2)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody4.status = "A";
                        tblBriefMasterBody4.updated_date_time = new DateTime?(DateTime.Now);
                        if (Convert.ToInt32(this.Request.Form["t4-select-order"].ToString()) == 1)
                        {
                            string str = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                            tblBriefMasterBody4.media_type = new int?(1);
                            tblBriefMasterBody4.resouce_code = str;
                            tblBriefMasterBody4.resource_mime = "NA";
                            tblBriefMasterBody4.resource_number = "NA";
                            tblBriefMasterBody4.resource_type = new int?(2);
                            tblBriefMasterBody4.srno = new int?(2);
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file5 = System.Web.HttpContext.Current.Request.Files["t4-btn"];
                                HttpPostedFile file6 = System.Web.HttpContext.Current.Request.Files["t4-btn"];
                                if (file5 != null && file5.ContentLength > 0)
                                {
                                    string extension = Path.GetExtension(file6.FileName);
                                    string path2 = str + extension;
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~" + tblBriefMasterBody4.brief_destination.Replace("\\", "/") ?? ""), path2);
                                    file5.SaveAs(filename);
                                    tblBriefMasterBody4.resouce_data = path2;
                                    tblBriefMasterBody4.file_extension = extension;
                                    tblBriefMasterBody4.file_type = new BriefModel().getExtensionNumber(extension);
                                    this.db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            tblBriefMasterBody4.media_type = new int?(2);
                            tblBriefMasterBody4.resouce_code = "NA";
                            tblBriefMasterBody4.resource_mime = "NA";
                            tblBriefMasterBody4.resource_number = "NA";
                            tblBriefMasterBody4.resource_type = new int?(2);
                            tblBriefMasterBody4.srno = new int?(2);
                            tblBriefMasterBody4.resouce_data = this.Request.Form["t4-btn-url"].ToString();
                            tblBriefMasterBody4.file_extension = "NA";
                            tblBriefMasterBody4.file_type = "NA";
                            this.db.SaveChanges();
                        }
                    }
                    if (int32_4 == 0)
                    {
                        tbl_brief_master_body tblBriefMasterBody = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.resource_type == (int?)2)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody.status = "A";
                        tblBriefMasterBody.updated_date_time = new DateTime?(DateTime.Now);
                        if (Convert.ToInt32(this.Request.Form["t2-select-order"].ToString()) == 2)
                        {
                            tblBriefMasterBody.media_type = new int?(2);
                            tblBriefMasterBody.resouce_code = "NA";
                            tblBriefMasterBody.resource_mime = "NA";
                            tblBriefMasterBody.resource_number = "NA";
                            tblBriefMasterBody.resource_type = new int?(2);
                            tblBriefMasterBody.srno = new int?(1);
                            tblBriefMasterBody.resouce_data = this.Request.Form["t2-btn-url"].ToString();
                            tblBriefMasterBody.file_extension = "NA";
                            tblBriefMasterBody.file_type = "NA";
                            this.db.SaveChanges();
                        }
                        else
                        {
                            string str = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                            tblBriefMasterBody.media_type = new int?(1);
                            tblBriefMasterBody.resouce_code = str;
                            tblBriefMasterBody.resource_mime = "NA";
                            tblBriefMasterBody.resource_number = "NA";
                            tblBriefMasterBody.resource_type = new int?(2);
                            tblBriefMasterBody.srno = new int?(1);
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file7 = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                                HttpPostedFile file8 = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                                if (file7 != null && file7.ContentLength > 0)
                                {
                                    string extension = Path.GetExtension(file8.FileName);
                                    string path2 = str + extension;
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~" + tblBriefMasterBody.brief_destination.Replace("\\", "/") ?? ""), path2);
                                    file7.SaveAs(filename);
                                    tblBriefMasterBody.resouce_data = path2;
                                    tblBriefMasterBody.file_extension = extension;
                                    tblBriefMasterBody.file_type = new BriefModel().getExtensionNumber(extension);
                                    this.db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            if (briefmaster.id_brief_master > 0)
            {
                DateTime now = DateTime.Now;
                now.ToString("yyyy");
                now = DateTime.Now;
                now.ToString("MM");
                briefmaster.brief_code.Replace("-", "");
                if (num1 == 2)
                {
                    DbSet<tbl_brief_category_mapping> briefCategoryMapping1 = this.db.tbl_brief_category_mapping;
                    Expression<Func<tbl_brief_category_mapping, bool>> predicate1 = (Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master);
                    foreach (tbl_brief_category_mapping briefCategoryMapping2 in briefCategoryMapping1.Where<tbl_brief_category_mapping>(predicate1).ToList<tbl_brief_category_mapping>())
                    {
                        briefCategoryMapping2.status = "T";
                        this.db.SaveChanges();
                    }
                    string str3 = this.Request.Form["brf-cat-val"].ToString().Trim(' ', ',');
                    char[] chArray = new char[1] { ',' };
                    foreach (string str4 in str3.Split(chArray))
                    {
                        string row = str4;
                        if (row.Count<char>() > 0)
                        {
                            tbl_brief_category_mapping briefCategoryMapping3 = this.db.tbl_brief_category_mapping.Where<tbl_brief_category_mapping>((Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.id_brief_category.ToString() == row)).FirstOrDefault<tbl_brief_category_mapping>();
                            if (briefCategoryMapping3 != null)
                            {
                                briefCategoryMapping3.status = "A";
                                this.db.SaveChanges();
                            }
                            else
                            {
                                this.db.tbl_brief_category_mapping.Add(new tbl_brief_category_mapping()
                                {
                                    id_brief_category = new int?(Convert.ToInt32(row)),
                                    id_brief_master = new int?(briefmaster.id_brief_master),
                                    id_organization = new int?(int32_1),
                                    status = "A",
                                    updated_date_time = new DateTime?(DateTime.Now)
                                });
                                this.db.SaveChanges();
                            }
                        }
                    }
                    DbSet<tbl_brief_category_mapping> briefCategoryMapping4 = this.db.tbl_brief_category_mapping;
                    Expression<Func<tbl_brief_category_mapping, bool>> predicate2 = (Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.status == "T");
                    foreach (tbl_brief_category_mapping entity in briefCategoryMapping4.Where<tbl_brief_category_mapping>(predicate2).ToList<tbl_brief_category_mapping>())
                    {
                        this.db.tbl_brief_category_mapping.Remove(entity);
                        this.db.SaveChanges();
                    }
                }
                DbSet<tbl_brief_metadata> tblBriefMetadata1 = this.db.tbl_brief_metadata;
                Expression<Func<tbl_brief_metadata, bool>> predicate3 = (Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master);
                foreach (tbl_brief_metadata tblBriefMetadata2 in tblBriefMetadata1.Where<tbl_brief_metadata>(predicate3).ToList<tbl_brief_metadata>())
                {
                    tblBriefMetadata2.status = "T";
                    this.db.SaveChanges();
                }
                string str5 = this.Request.Form["brief-metadata"].ToString();
                char[] chArray1 = new char[1] { ',' };
                foreach (string str6 in str5.Split(chArray1))
                {
                    string item = str6;
                    if (item.Count<char>() > 0)
                    {
                        tbl_brief_metadata tblBriefMetadata3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.brief_metadata.ToLower().Trim() == item.ToLower())).FirstOrDefault<tbl_brief_metadata>();
                        if (tblBriefMetadata3 != null)
                        {
                            tblBriefMetadata3.status = "A";
                            this.db.SaveChanges();
                        }
                        else
                        {
                            string str7 = item.Trim();
                            this.db.tbl_brief_metadata.Add(new tbl_brief_metadata()
                            {
                                id_brief_master = new int?(briefmaster.id_brief_master),
                                brief_metadata = str7,
                                status = "A",
                                updated_date_time = new DateTime?(DateTime.Now)
                            });
                            this.db.SaveChanges();
                        }
                    }
                }
                DbSet<tbl_brief_metadata> tblBriefMetadata4 = this.db.tbl_brief_metadata;
                Expression<Func<tbl_brief_metadata, bool>> predicate4 = (Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.status == "T");
                foreach (tbl_brief_metadata entity in tblBriefMetadata4.Where<tbl_brief_metadata>(predicate4).ToList<tbl_brief_metadata>())
                {
                    this.db.tbl_brief_metadata.Remove(entity);
                    this.db.SaveChanges();
                }
                string[] strArray = this.Request.Form["rem-brf-cat-val"].ToString().Split(',');
                List<int> intList = new List<int>();
                for (int index = 1; index <= 10; ++index)
                {
                    if (this.Request.Form["row-qtn-list-" + (object)index] != null)
                    {
                        int int32_5 = Convert.ToInt32(this.Request.Form["row-qtn-list-" + (object)index]);
                        intList.Add(int32_5);
                    }
                }
                foreach (int num4 in intList)
                {
                    int item = num4;
                    if (this.db.tbl_brief_question_mapping.Where<tbl_brief_question_mapping>((Expression<Func<tbl_brief_question_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.id_brief_question == (int?)item && t.status == "A")).FirstOrDefault<tbl_brief_question_mapping>() == null)
                    {
                        this.db.tbl_brief_question_mapping.Add(new tbl_brief_question_mapping()
                        {
                            id_brief_master = new int?(briefmaster.id_brief_master),
                            id_brief_question = new int?(item),
                            question_link_type = new int?(1),
                            date_time_stamp = new DateTime?(DateTime.Now),
                            status = "A",
                            updated_date_time = new DateTime?(DateTime.Now)
                        });
                        this.db.SaveChanges();
                    }
                }
                foreach (string str8 in strArray)
                {
                    string rmi = str8;
                    if (rmi.Count<char>() > 0)
                    {
                        tbl_brief_question_mapping briefQuestionMapping = this.db.tbl_brief_question_mapping.Where<tbl_brief_question_mapping>((Expression<Func<tbl_brief_question_mapping, bool>>)(t => t.id_brief_question.ToString() == rmi && t.id_brief_master == (int?)briefmaster.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_question_mapping>();
                        if (briefQuestionMapping != null)
                        {
                            briefQuestionMapping.status = "R";
                            briefQuestionMapping.updated_date_time = new DateTime?(DateTime.Now);
                            this.db.SaveChanges();
                        }
                    }
                }
            }
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        public ActionResult finishQuestionAction()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            try
            {
                switch (Convert.ToInt32(this.Request.Form["themeSele"].ToString()))
                {
                    case 1:
                        tbl_brief_question qtn1 = new tbl_brief_question();
                        qtn1.id_organization = new int?(int32_1);
                        qtn1.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                        qtn1.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                        qtn1.brief_question = this.Request.Form["question-title"].ToString();
                        qtn1.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                        qtn1.question_choice_type = new int?(0);
                        qtn1.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                        qtn1.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                        string dateString1 = this.Request.Form["question-expiry"].ToString();
                        DateTime now1 = DateTime.Now;
                        DateTime datetime1 = new Utility().StringToDatetime(dateString1);
                        qtn1.expiry_date = new DateTime?(datetime1);
                        qtn1.status = "A";
                        qtn1.updated_date_time = new DateTime?(DateTime.Now);
                        int num1 = new BriefLogic().InsertQuestion(qtn1);
                        if (num1 > 0)
                        {
                            int int32_2 = Convert.ToInt32(this.Request.Form["hid-row-count"].ToString());
                            string str1 = this.Request.Form["check-box-option"].ToString();
                            if (str1 != null)
                            {
                                string[] source = str1.Split(',');
                                int num2 = 0;
                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                    num2 = Convert.ToInt32(source[0]);
                                for (int index = 1; index <= int32_2; ++index)
                                    new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                    {
                                        id_organization = new int?(int32_1),
                                        id_brief_question = new int?(num1),
                                        brief_answer = this.Request.Form["brief-option-" + (object)index].ToString(),
                                        is_correct_answer = index != num2 ? new int?(0) : new int?(1),
                                        choice_type = new int?(0),
                                        status = "A",
                                        updated_date_time = new DateTime?(DateTime.Now)
                                    });
                            }
                            string str2 = this.Request.Form["question-metadata"].ToString();
                            char[] chArray = new char[1] { ',' };
                            foreach (string str3 in str2.Split(chArray))
                            {
                                string str4 = str3.Trim();
                                this.db.tbl_brief_question_metadata.Add(new tbl_brief_question_metadata()
                                {
                                    id_brief_question = new int?(num1),
                                    brief_question_metadata = str4,
                                    status = "A",
                                    updated_date_time = new DateTime?(DateTime.Now)
                                });
                                this.db.SaveChanges();
                            }
                            break;
                        }
                        break;
                    case 2:
                        int num3 = 0;
                        int num4 = 0;
                        switch (Convert.ToInt32(this.Request.Form["themtyp"].ToString()))
                        {
                            case 1:
                                tbl_brief_question qtn2 = new tbl_brief_question();
                                qtn2.id_organization = new int?(int32_1);
                                qtn2.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                                qtn2.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                                qtn2.brief_question = this.Request.Form["questionText"].ToString();
                                qtn2.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                                qtn2.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                                qtn2.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                                qtn2.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                                string dateString2 = this.Request.Form["question-expiry"].ToString();
                                DateTime now2 = DateTime.Now;
                                DateTime datetime2 = new Utility().StringToDatetime(dateString2);
                                qtn2.expiry_date = new DateTime?(datetime2);
                                qtn2.status = "A";
                                qtn2.updated_date_time = new DateTime?(DateTime.Now);
                                num3 = new BriefLogic().InsertQuestion(qtn2);
                                break;
                            case 2:
                                tbl_brief_question qtn3 = new tbl_brief_question();
                                qtn3.id_organization = new int?(int32_1);
                                qtn3.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                                qtn3.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                                qtn3.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                                qtn3.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                                qtn3.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                                qtn3.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                                string dateString3 = this.Request.Form["question-expiry"].ToString();
                                DateTime now3 = DateTime.Now;
                                DateTime datetime3 = new Utility().StringToDatetime(dateString3);
                                qtn3.expiry_date = new DateTime?(datetime3);
                                qtn3.status = "A";
                                qtn3.updated_date_time = new DateTime?(DateTime.Now);
                                num3 = new BriefLogic().InsertQuestionImage(qtn3);
                                tbl_brief_question tblBriefQuestion1 = new tbl_brief_question();
                                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                {
                                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                                    string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                                    string str = System.Web.HttpContext.Current.Request["id"];
                                    if (file != null && file.ContentLength > 0)
                                    {
                                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), num3.ToString() + "BriefQ" + extension);
                                        file.SaveAs(filename);
                                        tblBriefQuestion1.id_brief_question = num3;
                                        tblBriefQuestion1.question_image = num3.ToString() + "BriefQ" + extension;
                                        new BriefLogic().updateQuestion(tblBriefQuestion1);
                                        break;
                                    }
                                    break;
                                }
                                break;
                            case 3:
                                tbl_brief_question qtn4 = new tbl_brief_question();
                                qtn4.id_organization = new int?(int32_1);
                                qtn4.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                                qtn4.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                                qtn4.brief_question = this.Request.Form["questionText"].ToString();
                                qtn4.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                                qtn4.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                                qtn4.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                                qtn4.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                                string dateString4 = this.Request.Form["question-expiry"].ToString();
                                DateTime now4 = DateTime.Now;
                                DateTime datetime4 = new Utility().StringToDatetime(dateString4);
                                qtn4.expiry_date = new DateTime?(datetime4);
                                qtn4.status = "A";
                                qtn4.updated_date_time = new DateTime?(DateTime.Now);
                                num3 = new BriefLogic().InsertQuestion(qtn4);
                                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                {
                                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                                    string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                                    string str = System.Web.HttpContext.Current.Request["id"];
                                    if (file != null && file.ContentLength > 0)
                                    {
                                        tbl_brief_question tblBriefQuestion2 = new tbl_brief_question();
                                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), num3.ToString() + "BriefQ" + extension);
                                        file.SaveAs(filename);
                                        tblBriefQuestion2.id_brief_question = num3;
                                        tblBriefQuestion2.question_image = num3.ToString() + "BriefQ" + extension;
                                        new BriefLogic().updateQuestion(tblBriefQuestion2);
                                        break;
                                    }
                                    break;
                                }
                                break;
                        }
                        if (num3 > 0)
                        {
                            int int32_3 = Convert.ToInt32(this.Request.Form["imgChoiCount"].ToString());
                            string str5 = this.Request.Form["check-box-img-option"].ToString();
                            for (int index = 1; index <= int32_3; ++index)
                            {
                                int int32_4 = Convert.ToInt32(this.Request.Form["choiceType" + (object)index].ToString());
                                switch (int32_4)
                                {
                                    case 1:
                                        if (str5 != null)
                                        {
                                            string[] source = str5.Split(',');
                                            int num5 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num5 = Convert.ToInt32(source[0]);
                                            num4 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(num3),
                                                brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                choice_type = new int?(int32_4),
                                                is_correct_answer = index != num5 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                            break;
                                        }
                                        break;
                                    case 2:
                                        if (str5 != null)
                                        {
                                            string[] source = str5.Split(',');
                                            int num6 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num6 = Convert.ToInt32(source[0]);
                                            num4 = new BriefLogic().InsertimgChoiOpt(new tbl_brief_answer()
                                            {
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(num3),
                                                choice_type = new int?(int32_4),
                                                is_correct_answer = index != num6 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                        }
                                        tbl_brief_answer ans1 = new tbl_brief_answer();
                                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                        {
                                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                            string str6 = System.Web.HttpContext.Current.Request["id"];
                                            if (file != null && file.ContentLength > 0)
                                            {
                                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), num3.ToString() + "BriefChoice" + (object)index + extension);
                                                file.SaveAs(filename);
                                                ans1.id_brief_answer = num4;
                                                ans1.choice_image = num3.ToString() + "BriefChoice" + (object)index + extension;
                                                new BriefLogic().updateimgChoiOpt(ans1);
                                                break;
                                            }
                                            break;
                                        }
                                        break;
                                    case 3:
                                        if (str5 != null)
                                        {
                                            string[] source = str5.Split(',');
                                            int num7 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num7 = Convert.ToInt32(source[0]);
                                            num4 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(num3),
                                                brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                choice_type = new int?(int32_4),
                                                is_correct_answer = index != num7 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                        }
                                        tbl_brief_answer ans2 = new tbl_brief_answer();
                                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                        {
                                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                            string str7 = System.Web.HttpContext.Current.Request["id"];
                                            if (file != null && file.ContentLength > 0)
                                            {
                                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), num3.ToString() + "BriefChoice" + (object)index + extension);
                                                file.SaveAs(filename);
                                                ans2.id_brief_answer = num4;
                                                ans2.choice_image = num3.ToString() + "BriefChoice" + (object)index + extension;
                                                new BriefLogic().updateimgChoiOpt(ans2);
                                                break;
                                            }
                                            break;
                                        }
                                        break;
                                }
                            }
                        }
                        string str8 = this.Request.Form["question-metadata"].ToString();
                        char[] chArray1 = new char[1] { ',' };
                        foreach (string str9 in str8.Split(chArray1))
                        {
                            string str10 = str9.Trim();
                            this.db.tbl_brief_question_metadata.Add(new tbl_brief_question_metadata()
                            {
                                id_brief_question = new int?(num3),
                                brief_question_metadata = str10,
                                status = "A",
                                updated_date_time = new DateTime?(DateTime.Now)
                            });
                            this.db.SaveChanges();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("question", "Brief", (object)new
            {
                flag = 1
            });
        }

        public string getQuestionList(int sid, int sbid, string metadata)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string lower = metadata.Trim().ToLower();
            List<tbl_brief_question_complexity> list1 = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>)(t => t.status == "A")).ToList<tbl_brief_question_complexity>();
            string str1 = "";
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where id_organization =" + (object)int32 + " and id_brief_category=" + (object)sid + " and id_brief_sub_category=" + (object)sbid + " and id_brief_question in (select id_brief_question from tbl_brief_question_metadata where lower(brief_question_metadata) like '" + lower + "') order by brief_question").ToList<tbl_brief_question>();

            if (list2.Count<tbl_brief_question>() == 0)
            {
                ////list2 = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where id_organization =" + (object)int32 + " and (id_brief_category=" + (object)sid + " or id_brief_sub_category=" + (object)sbid + " or id_brief_question in (select id_brief_question from tbl_brief_question_metadata where lower(brief_question_metadata) like '" + lower + "')) order by brief_question").ToList<tbl_brief_question>();

                if (lower != "")
                {
                    list2 = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where (id_organization =" + (object)int32 + " and id_brief_category=" + (object)sid + " AND id_brief_sub_category=" + (object)sbid + " AND brief_question like '%" + lower + "%') order by brief_question").ToList<tbl_brief_question>();
                }
                else
                {
                    list2 = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where id_organization =" + (object)int32 + " and id_brief_category=" + (object)sid + " AND id_brief_sub_category=" + (object)sbid + " order by brief_question").ToList<tbl_brief_question>();
                }
            }

            if (list2.Count<tbl_brief_question>() != 0)
            {
                foreach (tbl_brief_question tblBriefQuestion in list2)
                {
                    tbl_brief_question item = tblBriefQuestion;
                    string str2 = "";
                    tbl_brief_question_complexity questionComplexity1 = list1.Where<tbl_brief_question_complexity>((Func<tbl_brief_question_complexity, bool>)(t =>
                   {
                       int? questionComplexity2 = t.question_complexity;
                       int? questionComplexity3 = item.question_complexity;
                       return questionComplexity2.GetValueOrDefault() == questionComplexity3.GetValueOrDefault() & questionComplexity2.HasValue == questionComplexity3.HasValue;
                   })).FirstOrDefault<tbl_brief_question_complexity>();
                    if (questionComplexity1 != null)
                        str2 = questionComplexity1.question_complexity_label;
                    tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == item.id_brief_category)).FirstOrDefault<tbl_brief_category>();
                    tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == item.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
                    double questionCounter = new BriefModel().getQuestionCounter("SELECT CASE WHEN SUM(b.is_correct_answer) THEN (SUM(b.is_correct_answer) / COUNT(*)) * 100 ELSE 0 END counter FROM tbl_brief_audit a, tbl_brief_answer b WHERE a.id_brief_question = " + (object)item.id_brief_question + " AND a.id_brief_answer = b.id_brief_answer");
                    str1 = str1 + "<tr id=\"row-qtn-brief-" + (object)item.id_brief_question + "\"><td id=\"row-td-" + (object)item.id_brief_question + "\">" + item.brief_question + " ( " + item.expiry_date.Value.ToShortDateString() + " ) </td>";
                    str1 = str1 + "<td>" + tblBriefCategory.brief_category + "</td>";
                    str1 = str1 + "<td>" + briefSubcategory.brief_subcategory + "</td>";
                    str1 = str1 + "<td>" + str2 + "</td>";
                    str1 = str1 + "<td>" + (object)questionCounter + "</td>";
                    str1 = str1 + "<td><a id=\"plus-qtn-brief-" + (object)item.id_brief_question + "\" href=\"javascript:void(0)\" onclick=\"addQuestionToBrief('" + (object)item.id_brief_question + "')\"><i class=\"glyphicon glyphicon-plus\"></i></a><i style=\"display:none;\" id=\"plus-ok-" + (object)item.id_brief_question + "\" class=\"glyphicon glyphicon-ok\"></i></td></tr>";
                }
            }
            return "<table id=\"brief-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>" + "<tr><th>Question/Expiry date</th>" + "<th>Category</th>" + "<th>Sub-Category</th>" + "<th>Complexity</th>" + "<th>Answered %</th>" + "<th></th></tr></thead><tbody>" + str1 + "</tbody></table>";
        }

        public string getEditQuestionList(int bid, int sid, int sbid, string metadata)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string lower = metadata.Trim().ToLower();
            List<tbl_brief_question_complexity> list1 = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>)(t => t.status == "A")).ToList<tbl_brief_question_complexity>();
            string str1 = "";
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where id_organization =" + (object)int32 + " and status='A' and id_brief_category=" + (object)sid + " and id_brief_sub_category=" + (object)sbid + " and id_brief_question in (select id_brief_question from tbl_brief_question_metadata where lower(brief_question_metadata) like '" + lower + "') and id_brief_question not in (SELECT id_brief_question FROM tbl_brief_question_mapping where id_brief_master=" + (object)bid + " and status='A') order by brief_question").ToList<tbl_brief_question>();
            if (list2.Count<tbl_brief_question>() == 0)
                list2 = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where id_organization =" + (object)int32 + " and status='A' and (id_brief_category=" + (object)sid + " or id_brief_sub_category=" + (object)sbid + " or id_brief_question in (select id_brief_question from tbl_brief_question_metadata where lower(brief_question_metadata) like '" + lower + "'))  and id_brief_question not in (SELECT id_brief_question FROM tbl_brief_question_mapping where id_brief_master=" + (object)bid + " and status='A') order by brief_question").ToList<tbl_brief_question>();
            if (list2.Count<tbl_brief_question>() != 0)
            {
                foreach (tbl_brief_question tblBriefQuestion in list2)
                {
                    tbl_brief_question item = tblBriefQuestion;
                    string str2 = "";
                    tbl_brief_question_complexity questionComplexity1 = list1.Where<tbl_brief_question_complexity>((Func<tbl_brief_question_complexity, bool>)(t =>
                   {
                       int? questionComplexity2 = t.question_complexity;
                       int? questionComplexity3 = item.question_complexity;
                       return questionComplexity2.GetValueOrDefault() == questionComplexity3.GetValueOrDefault() & questionComplexity2.HasValue == questionComplexity3.HasValue;
                   })).FirstOrDefault<tbl_brief_question_complexity>();
                    if (questionComplexity1 != null)
                        str2 = questionComplexity1.question_complexity_label;
                    tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == item.id_brief_category)).FirstOrDefault<tbl_brief_category>();
                    tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == item.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
                    double questionCounter = new BriefModel().getQuestionCounter("SELECT CASE WHEN SUM(b.is_correct_answer) THEN (SUM(b.is_correct_answer) / COUNT(*)) * 100 ELSE 0 END counter FROM tbl_brief_audit a, tbl_brief_answer b WHERE a.id_brief_question = " + (object)item.id_brief_question + " AND a.id_brief_answer = b.id_brief_answer");
                    str1 = str1 + "<tr id=\"row-qtn-brief-" + (object)item.id_brief_question + "\"><td id=\"row-td-" + (object)item.id_brief_question + "\">" + item.brief_question + " ( " + item.expiry_date.Value.ToShortDateString() + " ) </td>";
                    str1 = str1 + "<td>" + tblBriefCategory.brief_category + "</td>";
                    str1 = str1 + "<td>" + briefSubcategory.brief_subcategory + "</td>";
                    str1 = str1 + "<td>" + str2 + "</td>";
                    str1 = str1 + "<td>" + (object)questionCounter + "</td>";
                    str1 = str1 + "<td><a id=\"plus-qtn-brief-" + (object)item.id_brief_question + "\" href=\"javascript:void(0)\" onclick=\"addQuestionToBrief('" + (object)item.id_brief_question + "')\"><i class=\"glyphicon glyphicon-plus\"></i></a><i style=\"display:none;\" id=\"plus-ok-" + (object)item.id_brief_question + "\" class=\"glyphicon glyphicon-ok\"></i></td></tr>";
                }
            }
            return "<table id=\"brief-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>" + "<tr><th>Question/Expiry date</th>" + "<th>Category</th>" + "<th>Sub-Category</th>" + "<th>Complexity</th>" + "<th>Answered %</th>" + "<th></th></tr></thead><tbody>" + str1 + "</tbody></table>";
        }

        public ActionResult BriefDetail(string brf)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboard", "Brief");
            tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
            string str1 = brief.updated_date_time.Value.ToString("yyyy");
            string str2 = brief.updated_date_time.Value.ToString("MM");
            string str3 = brief.brief_code.Replace("-", "");
            if (briefMasterTemplate == null)
            {
                tbl_brief_master_template entity = new tbl_brief_master_template();
                entity.id_brief_master = new int?(brief.id_brief_master);
                entity.brief_code = brief.brief_code;
                string str4 = "NA";
                entity.resource_order = str4;
                entity.brief_template = "1";
                entity.status = "A";
                entity.updated_date_time = new DateTime?(DateTime.Now);
                entity.brief_destination = str3;
                this.db.tbl_brief_master_template.Add(entity);
                this.db.SaveChanges();
            }
            List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
            if (list1.Count<tbl_brief_master_body>() == 0)
            {
                string str5 = "";
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/")))
                {
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/"));
                    str5 = "/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/";
                }
                this.db.tbl_brief_master_body.Add(new tbl_brief_master_body()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    brief_code = brief.brief_code,
                    brief_destination = str5,
                    brief_template = "1",
                    media_type = new int?(1),
                    resouce_code = "NA",
                    resource_mime = "NA",
                    resource_number = "NA",
                    resource_type = new int?(1),
                    srno = new int?(1),
                    resouce_data = brief.brief_description,
                    status = "A",
                    file_extension = "NA",
                    file_type = "NA",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            if (list1.Count<tbl_brief_master_body>() > 0)
            {
                foreach (tbl_brief_master_body tblBriefMasterBody in list1)
                {
                    int? mediaType = tblBriefMasterBody.media_type;
                    int num = 2;
                    if (mediaType.GetValueOrDefault() == num & mediaType.HasValue)
                    {
                        string resouceData = tblBriefMasterBody.resouce_data;
                        string str6 = resouceData.Substring(32, resouceData.Length - 32);
                        tblBriefMasterBody.resouce_data = "https://www.youtube.com/embed/" + str6;
                    }
                }
            }
            brief.brief_description = WebUtility.HtmlDecode(brief.brief_description);
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + "  and status='A')  and status='A'").ToList<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            List<tbl_brief_metadata> list3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
            bool flag = false;
            if (tblBriefStatus != null)
            {
                int? statusCode = tblBriefStatus.status_code;
                int num = 1;
                if (statusCode.GetValueOrDefault() == num & statusCode.HasValue)
                    flag = true;
            }
            this.ViewData["briefmaster"] = (object)brief;
            this.ViewData["bTemplate"] = (object)briefMasterTemplate;
            this.ViewData["mBody"] = (object)list1;
            this.ViewData["questions"] = (object)list2;
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["metadata"] = (object)list3;
            this.ViewData["accessFlag"] = (object)flag;
            this.ViewData["qList"] = (object)list2;
            return (ActionResult)this.View();
        }

        public ActionResult cleanBrief(string brf)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboard", "Brief");
            tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
            List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
            brief.brief_description = WebUtility.HtmlDecode(brief.brief_description);
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + "  and status='A')  and status='A'").ToList<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            List<tbl_brief_metadata> list3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
            bool flag = false;
            if (tblBriefStatus != null)
            {
                int? statusCode = tblBriefStatus.status_code;
                int num = 1;
                if (statusCode.GetValueOrDefault() == num & statusCode.HasValue)
                    flag = true;
            }
            List<BriefResultSummery> briefResultSummery = new BriefModel().getBriefResultSummery("SELECT a.id_user,attempt_no,brief_result,c.FIRSTNAME prname,d.FIRSTNAME rmname,updated_date_time completedtime FROM tbl_brief_log a,tbl_user b,tbl_profile c,tbl_profile d where a.id_user=b.id_user and b.ID_USER=c.id_user and b.reporting_manager=d.ID_USER and a.id_brief_master=" + (object)brief.id_brief_master + " ");
            this.ViewData["unreadList"] = (object)new BriefModel().getBriefUnreadSummery("SELECT u.id_brief_user_assignment, b.id_user, c.FIRSTNAME prname, d.FIRSTNAME rmname, u.assignment_datetime assignedtime,m.brief_code FROM tbl_brief_master m, tbl_brief_user_assignment u, tbl_user a, tbl_user b, tbl_profile c, tbl_profile d WHERE m.id_brief_master = u.id_brief_master AND u.id_user = a.id_user AND a.id_user = b.id_user AND b.ID_USER = c.id_user AND b.reporting_manager = d.ID_USER AND m.id_brief_master = " + (object)brief.id_brief_master + " and m.id_brief_master not in (select id_brief_master from tbl_brief_log where id_brief_master=" + (object)brief.id_brief_master + ")");
            this.ViewData["summeryList"] = (object)briefResultSummery;
            this.ViewData["briefmaster"] = (object)brief;
            this.ViewData["bTemplate"] = (object)briefMasterTemplate;
            this.ViewData["mBody"] = (object)list1;
            this.ViewData["questions"] = (object)list2;
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["metadata"] = (object)list3;
            this.ViewData["accessFlag"] = (object)flag;
            return (ActionResult)this.View();
        }

        public ActionResult modifyBrief(string brf)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = new tbl_brief_master();
            int num1 = 0;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                num1 = m2ostDbContext.Database.SqlQuery<int>("select (case when episode_sequence is null then 0 else episode_sequence end) as episode from tbl_brief_master where brief_code={0}", (object)brf).FirstOrDefault<int>();
                brief = m2ostDbContext.Database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where status='A' and brief_code={0}", (object)brf).FirstOrDefault<tbl_brief_master>();
            }
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboard", "Brief");
            tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
            string str1 = brief.updated_date_time.Value.ToString("yyyy");
            string str2 = brief.updated_date_time.Value.ToString("MM");
            string str3 = brief.brief_code.Replace("-", "");
            if (briefMasterTemplate == null)
            {
                tbl_brief_master_template entity = new tbl_brief_master_template();
                entity.id_brief_master = new int?(brief.id_brief_master);
                entity.brief_code = brief.brief_code;
                string str4 = "NA";
                entity.resource_order = str4;
                entity.brief_template = "1";
                entity.status = "A";
                entity.updated_date_time = new DateTime?(DateTime.Now);
                entity.brief_destination = str3;
                this.db.tbl_brief_master_template.Add(entity);
                this.db.SaveChanges();
            }
            List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
            if (list1.Count<tbl_brief_master_body>() == 0)
            {
                string str5 = "";
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/")))
                {
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/"));
                    str5 = "/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/";
                }
                this.db.tbl_brief_master_body.Add(new tbl_brief_master_body()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    brief_code = brief.brief_code,
                    brief_destination = str5,
                    brief_template = "1",
                    media_type = new int?(1),
                    resouce_code = "NA",
                    resource_mime = "NA",
                    resource_number = "NA",
                    resource_type = new int?(1),
                    srno = new int?(1),
                    resouce_data = brief.brief_description,
                    status = "A",
                    file_extension = "NA",
                    file_type = "NA",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            brief.brief_description = WebUtility.HtmlDecode(brief.brief_description);
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + " and status='A') and status='A'").ToList<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            List<tbl_brief_metadata> list3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
            bool flag = false;
            if (tblBriefStatus != null)
            {
                int? statusCode = tblBriefStatus.status_code;
                int num2 = 1;
                if (statusCode.GetValueOrDefault() == num2 & statusCode.HasValue)
                    flag = true;
            }
            List<tbl_brief_category> list4 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list5 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["briefmaster"] = (object)brief;
            this.ViewData["bTemplate"] = (object)briefMasterTemplate;
            this.ViewData["mBody"] = (object)list1;
            this.ViewData["questions"] = (object)list2;
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["metadata"] = (object)list3;
            this.ViewData["accessFlag"] = (object)flag;
            this.ViewData["sequanceVal"] = (object)num1;
            this.ViewData["ListCategory"] = (object)list4;
            this.ViewData["ListSubcategory"] = (object)list5;
            return (ActionResult)this.View();
        }

        public ActionResult viewBrief()
        {
            this.ViewData["category"] = (object)this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            return (ActionResult)this.View();
        }

        public string getSubcategoryList(int cid)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_subcategory> list = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where id_brief_category = " + (object)cid + " and status='A'").ToList<tbl_brief_subcategory>();
            string subcategoryList = "";
            foreach (tbl_brief_subcategory briefSubcategory in list)
                subcategoryList = subcategoryList + "<option data-tokens=\"" + briefSubcategory.brief_subcategory + "\" value=\"" + (object)briefSubcategory.id_brief_subcategory + "\">" + briefSubcategory.brief_subcategory + "</option>";
            return subcategoryList;
        }

        public string getBriefViewData(int cid, int scid, string search)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            search = search.ToLower().Trim();
            List<briefView> briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)int32 + " and a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master" + " and a.id_brief_category= " + (object)cid + " and a.id_brief_sub_category=" + (object)scid + " and a.id_brief_master in (select id_brief_master from tbl_brief_metadata where lower(brief_metadata) like '" + search + "') limit 50 ");
            if (briefView1.Count<briefView>() == 0)
                briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status , d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE  a.id_organization=" + (object)int32 + " and a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master" + " and (a.id_brief_category= " + (object)cid + " or a.id_brief_sub_category=" + (object)scid + " or a.id_brief_master in (select id_brief_master from tbl_brief_metadata where lower(brief_metadata) like '" + search + "')) limit 50");
            if (briefView1.Count<briefView>() > 0)
            {
                foreach (briefView briefView2 in briefView1)
                {
                    str = str + "<tr><td>" + briefView2.brief_title + "</td>";
                    str = str + "<td>" + briefView2.brief_category + "/" + briefView2.brief_subcategory + "</td>";
                    str = str + "<td>" + briefView2.scheduled_timestamp.ToString("dd-MMM-yy HH:mm") + "</td>";
                    str = str + "<td>" + briefView2.brief_status + "</td>";
                    str += "<td>";
                    if (briefView2.status_code == 2)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefSummery('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    if (briefView2.status_code == 3)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefSummery('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    if (briefView2.status_code == 4)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefSummery('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    if (briefView2.status_code == 5)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefSummery('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    int statusCode = briefView2.status_code;
                    str += "</td>";
                    str += "</tr>";
                }
            }
            return "<table id=\"brief-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>" + "<tr><th>Question</th>" + "<th>Category / Sub-Category</th>" + "<th>Expiry Date</th>" + "<th>Status</th>" + "<th></th></tr></thead><tbody>" + str + "</tbody></table>";
        }

        public ActionResult approveBrief(string brf)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief != null)
            {
                List<tbl_brief_question> list1 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + " and status='A') and status='A'").ToList<tbl_brief_question>();
                tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
                tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
                List<tbl_brief_metadata> list2 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
                this.ViewData["briefmaster"] = (object)brief;
                this.ViewData["questions"] = (object)list1;
                this.ViewData["category"] = (object)tblBriefCategory;
                this.ViewData["subcategory"] = (object)briefSubcategory;
                this.ViewData["metadata"] = (object)list2;
            }
            return (ActionResult)this.View();
        }

        public ActionResult approveBriefAction(string brf)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief != null)
            {
                DbSet<tbl_brief_status> tblBriefStatus1 = this.db.tbl_brief_status;
                Expression<Func<tbl_brief_status, bool>> predicate = (Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_status tblBriefStatus2 in tblBriefStatus1.Where<tbl_brief_status>(predicate).ToList<tbl_brief_status>())
                {
                    if (tblBriefStatus2.status == "A")
                    {
                        tblBriefStatus2.status = "D";
                        tblBriefStatus2.updated_date_time = new DateTime?(DateTime.Now);
                        this.db.SaveChanges();
                    }
                }
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(2),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Approved"
                });
                this.db.SaveChanges();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'A',{3})", (object)brief.id_brief_master, (object)int32_1, (object)int32_2, (object)DateTime.Now);
            }
            return (ActionResult)this.RedirectToAction("BriefDetail", "Brief", (object)new
            {
                brf = brf
            });
        }

        public ActionResult cleanBriefApprove(string brf)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief != null)
            {
                DbSet<tbl_brief_status> tblBriefStatus1 = this.db.tbl_brief_status;
                Expression<Func<tbl_brief_status, bool>> predicate1 = (Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_status tblBriefStatus2 in tblBriefStatus1.Where<tbl_brief_status>(predicate1).ToList<tbl_brief_status>())
                {
                    if (tblBriefStatus2.status == "A")
                    {
                        tblBriefStatus2.status = "D";
                        tblBriefStatus2.updated_date_time = new DateTime?(DateTime.Now);
                        this.db.SaveChanges();
                    }
                }
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(7),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Deleted"
                });
                this.db.SaveChanges();
                tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
                List<tbl_brief_master_body> list = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
                if (briefMasterTemplate != null)
                {
                    briefMasterTemplate.status = "X";
                    briefMasterTemplate.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                foreach (tbl_brief_master_body tblBriefMasterBody in list)
                {
                    tblBriefMasterBody.status = "X";
                    tblBriefMasterBody.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                DbSet<tbl_brief_user_assignment> briefUserAssignment1 = this.db.tbl_brief_user_assignment;
                Expression<Func<tbl_brief_user_assignment, bool>> predicate2 = (Expression<Func<tbl_brief_user_assignment, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_user_assignment briefUserAssignment2 in briefUserAssignment1.Where<tbl_brief_user_assignment>(predicate2).ToList<tbl_brief_user_assignment>())
                {
                    briefUserAssignment2.status = "X";
                    briefUserAssignment2.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                DbSet<tbl_brief_category_mapping> briefCategoryMapping1 = this.db.tbl_brief_category_mapping;
                Expression<Func<tbl_brief_category_mapping, bool>> predicate3 = (Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_category_mapping briefCategoryMapping2 in briefCategoryMapping1.Where<tbl_brief_category_mapping>(predicate3).ToList<tbl_brief_category_mapping>())
                {
                    briefCategoryMapping2.status = "X";
                    briefCategoryMapping2.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                DbSet<tbl_brief_read_status> tblBriefReadStatus1 = this.db.tbl_brief_read_status;
                Expression<Func<tbl_brief_read_status, bool>> predicate4 = (Expression<Func<tbl_brief_read_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_read_status tblBriefReadStatus2 in tblBriefReadStatus1.Where<tbl_brief_read_status>(predicate4).ToList<tbl_brief_read_status>())
                {
                    tblBriefReadStatus2.status = "X";
                    tblBriefReadStatus2.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                brief.status = "X";
                brief.updated_date_time = new DateTime?(DateTime.Now);
                this.db.SaveChanges();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'X',{3})", (object)brief.id_brief_master, (object)int32_1, (object)int32_2, (object)DateTime.Now);
            }
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        public ActionResult userAssignment()
        {
            this.ViewData["category"] = (object)this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            return (ActionResult)this.View();
        }

        public string getBriefUserAssignmentData(int cid, int scid, string search)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "";
            search = search.ToLower().Trim();
            List<briefView> briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)int32 + " and  a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master" + " and a.id_brief_category= " + (object)cid + " and a.id_brief_sub_category=" + (object)scid + " and a.id_brief_master in (select id_brief_master from tbl_brief_metadata where lower(brief_metadata) like '" + search + "') ");
            if (briefView1.Count<briefView>() == 0)
            {
                ////briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status , d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)int32 + " and  a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master" + " and (a.id_brief_category= " + (object)cid + " or a.id_brief_sub_category=" + (object)scid + " or a.id_brief_master in (select id_brief_master from tbl_brief_metadata where lower(brief_metadata) like '" + search + "'))");
                if (search != "")
                {
                    briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status , d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)int32 + " and  a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND (a.id_brief_master = d.id_brief_master" + " AND a.brief_title like '%" + search + "%')");
                }
                else
                {
                    briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status , d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_organization=" + (object)int32 + " and  a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND (a.id_brief_master = d.id_brief_master" + " and (a.id_brief_category= " + (object)cid + " AND a.id_brief_sub_category=" + (object)scid + "))");
                }
            }

            if (briefView1.Count<briefView>() > 0)
            {
                foreach (briefView briefView2 in briefView1)
                {
                    str = str + "<tr><td>" + (object)briefView2.id_brief_master + "</td><td>" + briefView2.brief_title + "</td>";
                    str = str + "<td>" + briefView2.brief_category + "/" + briefView2.brief_subcategory + "</td>";
                    str = str + "<td>" + briefView2.scheduled_timestamp.ToString("dd-MMM-yy HH:mm") + "</td>";
                    str = str + "<td>" + briefView2.brief_status + "</td>";
                    str += "<td>";
                    if (briefView2.status_code == 2)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefBody('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    if (briefView2.status_code == 3)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefBody('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    if (briefView2.status_code == 4)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefBody('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    if (briefView2.status_code == 5)
                        str = str + "<a  class=\"table-anchor\" id=\"plus-qtn-brief-" + (object)briefView2.id_brief_master + "\" href=\"javascript:void(0)\" onclick=\"_setPartialBriefBody('" + briefView2.brief_code + "')\"><i class=\"glyphicon glyphicon glyphicon-info-sign\"></i></a>";
                    int statusCode = briefView2.status_code;
                    str += "</td>";
                    str += "</tr>";
                }
            }
            return "<table id=\"brief-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead>" + "<tr><th>ID</th><th>Brief</th>" + "<th>Category / Sub-Category</th>" + "<th>Expiry Date</th>" + "<th>Status</th>" + "<th></th></tr></thead><tbody>" + str + "</tbody></table>";
        }

        public ActionResult partialUserAssignment(string brf)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master tblBriefMaster = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (tblBriefMaster != null)
            {
                this.ViewData["RoleList"] = (object)this.db.tbl_csst_role.SqlQuery("select * from tbl_csst_role where ID_ORGANIZATION=" + (object)int32 + " and status='A'").ToList<tbl_csst_role>();
                this.ViewData["location"] = (object)new contentDashboardModel().getUserLocation(" select distinct case when a.CITY is null then UPPER(a.LOCATION) else UPPER(a.city) end as LOCATION " + " from tbl_profile a left join tbl_user b ON a.id_user = b.id_user where b.ID_ORGANIZATION=" + (object)int32 + "  and length(LOCATION)>3 order by LOCATION ");
                this.ViewData["bstatus"] = (object)true;
                this.ViewData["briefmaster"] = (object)tblBriefMaster;
            }
            else
                this.ViewData["bstatus"] = (object)false;
            return (ActionResult)this.View();
        }

        public ActionResult partialBriefSummery(string brf)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master tblBriefMaster = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (tblBriefMaster != null)
            {
                List<BriefResultSummery> briefResultSummery = new BriefModel().getBriefResultSummery("SELECT a.id_user,attempt_no,brief_result,c.FIRSTNAME prname,d.FIRSTNAME rmname,updated_date_time completedtime FROM tbl_brief_log a,tbl_user b,tbl_profile c,tbl_profile d where a.id_user=b.id_user and b.ID_USER=c.id_user and b.reporting_manager=d.ID_USER and a.id_brief_master=" + (object)tblBriefMaster.id_brief_master + " ");
                this.ViewData["unreadList"] = (object)new BriefModel().getBriefUnreadSummery("SELECT u.id_brief_user_assignment, b.id_user, c.FIRSTNAME prname, d.FIRSTNAME rmname, u.assignment_datetime assignedtime,m.brief_code FROM tbl_brief_master m, tbl_brief_user_assignment u, tbl_user a, tbl_user b, tbl_profile c, tbl_profile d WHERE m.id_brief_master = u.id_brief_master AND u.id_user = a.id_user AND a.id_user = b.id_user AND b.ID_USER = c.id_user AND b.reporting_manager = d.ID_USER AND m.id_brief_master = " + (object)tblBriefMaster.id_brief_master + " and m.id_brief_master not in (select id_brief_master from tbl_brief_log where id_brief_master=" + (object)tblBriefMaster.id_brief_master + ")");
                this.ViewData["summeryList"] = (object)briefResultSummery;
                this.ViewData["briefmaster"] = (object)tblBriefMaster;
            }
            return (ActionResult)this.View();
        }

        public string getRemoveAssignment(string brf, int ids)
        {
            string removeAssignment = "0";
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief != null)
            {
                tbl_brief_user_assignment uData = this.db.tbl_brief_user_assignment.Where<tbl_brief_user_assignment>((Expression<Func<tbl_brief_user_assignment, bool>>)(t => t.id_brief_user_assignment == ids)).FirstOrDefault<tbl_brief_user_assignment>();
                if (uData != null)
                {
                    if (this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>)(t => t.id_brief_master == brief.id_brief_master && uData.id_user == (int?)t.id_user)).FirstOrDefault<tbl_brief_log>() == null)
                    {
                        this.db.tbl_brief_user_assignment.Remove(uData);
                        this.db.SaveChanges();
                        removeAssignment = "1";
                    }
                }
            }
            return removeAssignment;
        }

        public string getAllUserList(int bid)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str1 = "SELECT a.ID_USER PRUSER,a.USERID PRUSERID,pp.FIRSTNAME PRNAME,a.user_function PRFUNCTION,pp.CITY PRCITY,pp.LOCATION PRLOCATION, b.ID_USER RMUSER,b.USERID RMUSERID,rp.FIRSTNAME RMNAME FROM tbl_user a, tbl_user b, tbl_profile pp, tbl_profile rp WHERE a.reporting_manager = b.ID_USER and pp.ID_USER=a.ID_USER and rp.ID_USER=b.ID_USER AND a.ID_ORGANIZATION = " + (object)int32 + " and a.status='A'  order by pp.FIRSTNAME ";
            List<BriefUser> briefUserList = new BriefModel().getBriefUserList(" SELECT a.ID_USER PRUSER, a.EMPLOYEEID PREMPLOYEEID ,a.USERID PRUSERID, pp.FIRSTNAME PRNAME, a.user_function PRFUNCTION, pp.CITY PRCITY, pp.LOCATION PRLOCATION, b.ID_USER RMUSER, b.USERID RMUSERID,  " + " rp.FIRSTNAME RMNAME, CASE WHEN (ua.id_brief_user_assignment) THEN DATE_FORMAT(ua.updated_date_time, '%d-%m-%y') ELSE '--' END DATETIMESTAMP, CASE WHEN (ua.id_brief_user_assignment) THEN ua.id_brief_user_assignment ELSE 0 END id_brief_user_assignment, case when(ua.id_brief_master=" + (object)bid + ") then ua.id_brief_master else 0 end id_brief_master   " + " FROM tbl_user a LEFT JOIN   tbl_brief_user_assignment ua ON a.id_user = ua.id_user  AND ua.id_brief_master = " + (object)bid + "  LEFT JOIN   tbl_user b ON b.ID_USER = a.reporting_manager, tbl_profile pp, tbl_profile rp  " + " WHERE a.reporting_manager = b.ID_USER   AND pp.ID_USER = a.ID_USER AND rp.ID_USER = b.ID_USER AND a.ID_ORGANIZATION = " + (object)int32 + " AND a.status = 'A'   ORDER BY pp.FIRSTNAME ");
            string str2 = "";
            foreach (BriefUser briefUser in briefUserList)
            {
                if (briefUser.id_brief_master == 0 || briefUser.id_brief_master == bid)
                {
                    str2 += "<tr>";
                    str2 = str2 + "<td>" + briefUser.PRNAME + " (" + briefUser.RMNAME + ")</td>";
                    str2 = str2 + "<td>" + briefUser.PRUSERID + "</td>";
                    str2 = str2 + "<td>" + briefUser.PREMPLOYEEID + "</td>";
                    str2 = str2 + "<td>" + briefUser.PRLOCATION + "</td>";
                    if (briefUser.id_brief_master > 0)
                        str2 = str2 + "<td><a class=\"btn btn-default btn-info \"> Sent on [" + briefUser.DATETIMESTAMP + "]</a></td>";
                    else
                        str2 = str2 + "<td><input style=\"margin-left:5%\" type=\"checkbox\" name=\"brf-user\" class=\"myCheckbox\"  value=\"" + (object)briefUser.PRUSER + "\"></td>";
                    str2 += "</tr>";
                }
            }
            string allUserList = "";
            if (briefUserList.Count<BriefUser>() != 0)
                allUserList = "<table id=\"report-user-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th width=\"20%\">User Name</th>" + " <th width=\"20%\">UserID</th>" + " <th width=\"20%\">Employee ID</th>" + " <th width=\"20%\">Location</th>" + "<th  width=\"20%\"><input type = \"checkbox\" id = \"checkAll\" onclick = \"check_all()\"> Select all &nbsp;&nbsp;</th>" + "</thead>" + "<tbody>" + str2 + "</tbody></table>";
            return allUserList;
        }

        public string getAllUserListByUname(int bid, string search)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            search = search.ToLower().Trim();
            List<BriefUser> briefUserList = new BriefModel().getBriefUserList("  SELECT a.ID_USER PRUSER, a.EMPLOYEEID PREMPLOYEEID ,a.USERID PRUSERID, pp.FIRSTNAME PRNAME, a.user_function PRFUNCTION, pp.CITY PRCITY, pp.LOCATION PRLOCATION, b.ID_USER RMUSER, b.USERID RMUSERID, " + "  rp.FIRSTNAME RMNAME,  CASE WHEN (ua.id_brief_user_assignment) THEN DATE_FORMAT(ua.updated_date_time, '%d-%m-%y') ELSE '--' END DATETIMESTAMP,  CASE WHEN (ua.id_brief_user_assignment) THEN ua.id_brief_user_assignment ELSE 0 END id_brief_user_assignment, CASE WHEN (ua.id_brief_master = " + (object)bid + ") THEN ua.id_brief_master ELSE 0 END id_brief_master  " + "  FROM tbl_user a LEFT JOIN   tbl_brief_user_assignment ua ON a.id_user = ua.id_user  AND ua.id_brief_master = " + (object)bid + "  LEFT JOIN   tbl_user b ON b.ID_USER = a.reporting_manager, tbl_profile pp, tbl_profile rp  " + "  WHERE LOWER(pp.FIRSTNAME) LIKE  '%" + search + "%'  AND a.reporting_manager = b.ID_USER AND pp.ID_USER = a.ID_USER AND rp.ID_USER = b.ID_USER AND a.ID_ORGANIZATION = " + (object)int32 + " AND a.status = 'A'   ORDER BY pp.FIRSTNAME ");
            string str = "";
            foreach (BriefUser briefUser in briefUserList)
            {
                if (briefUser.id_brief_master == 0 || briefUser.id_brief_master == bid)
                {
                    str += "<tr>";
                    str = str + "<td>" + briefUser.PRNAME + " (" + briefUser.RMNAME + ")</td>";
                    str = str + "<td>" + briefUser.PRUSERID + "</td>";
                    str = str + "<td>" + briefUser.PREMPLOYEEID + "</td>";
                    str = str + "<td>" + briefUser.PRLOCATION + "</td>";
                    if (briefUser.id_brief_master > 0)
                        str = str + "<td><a class=\"btn btn-default btn-info \"> Sent on [" + briefUser.DATETIMESTAMP + "]</a></td>";
                    else
                        str = str + "<td><input style=\"margin-left:5%\" type=\"checkbox\" name=\"brf-user\" class=\"myCheckbox\"  value=\"" + (object)briefUser.PRUSER + "\"></td>";
                    str += "</tr>";
                }
            }
            string allUserListByUname = "";
            if (briefUserList.Count<BriefUser>() != 0)
                allUserListByUname = "<table id=\"report-user-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th width=\"20%\">User Name</th>" + " <th width=\"20%\">UserID</th>" + " <th width=\"20%\">Employee ID</th>" + " <th width=\"20%\">Location</th>" + "<th  width=\"20%\"><input type = \"checkbox\" id = \"checkAll\" onclick = \"check_all()\"> Select all &nbsp;&nbsp;</th>" + "</thead>" + "<tbody>" + str + "</tbody></table>";
            return allUserListByUname;
        }

        public string getAllUserListByLocation(int bid, string location, string search)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            location = location.Trim().ToLower();
            string userListByLocation;
            if (!string.IsNullOrEmpty(location))
            {
                search = search.ToLower().Trim();
                string str1 = "";
                if (!string.IsNullOrEmpty(search))
                    str1 = " and lower(pp.FIRSTNAME) like '%" + search + "%' ";
                List<BriefUser> briefUserList = new BriefModel().getBriefUserList("  SELECT a.ID_USER PRUSER,a.EMPLOYEEID PREMPLOYEEID , a.USERID PRUSERID, pp.FIRSTNAME PRNAME, a.user_function PRFUNCTION, pp.CITY PRCITY, pp.LOCATION PRLOCATION, b.ID_USER RMUSER, b.USERID RMUSERID, " + "  rp.FIRSTNAME RMNAME,   CASE WHEN (ua.id_brief_user_assignment) THEN DATE_FORMAT(ua.updated_date_time, '%d-%m-%y') ELSE '--' END DATETIMESTAMP, CASE WHEN (ua.id_brief_user_assignment) THEN ua.id_brief_user_assignment ELSE 0 END id_brief_user_assignment, CASE WHEN (ua.id_brief_master = " + (object)bid + ") THEN ua.id_brief_master ELSE 0 END id_brief_master  " + "  FROM tbl_user a  LEFT JOIN   tbl_brief_user_assignment ua ON a.id_user = ua.id_user  AND ua.id_brief_master = " + (object)bid + "  LEFT JOIN   tbl_user b ON b.ID_USER = a.reporting_manager, tbl_profile pp, tbl_profile rp  " + "  WHERE a.reporting_manager = b.ID_USER  AND pp.ID_USER = a.ID_USER AND rp.ID_USER = b.ID_USER AND a.ID_ORGANIZATION = " + (object)int32 + " AND a.status = 'A'  AND lower(pp.LOCATION) like '%" + location + "%' " + str1 + "   order by pp.FIRSTNAME ");
                string str2 = "";
                foreach (BriefUser briefUser in briefUserList)
                {
                    if (briefUser.id_brief_master == 0 || briefUser.id_brief_master == bid)
                    {
                        str2 += "<tr>";
                        str2 = str2 + "<td>" + briefUser.PRNAME + " (" + briefUser.RMNAME + ")</td>";
                        str2 = str2 + "<td>" + briefUser.PRUSERID + "</td>";
                        str2 = str2 + "<td>" + briefUser.PREMPLOYEEID + "</td>";
                        str2 = str2 + "<td>" + briefUser.PRLOCATION + "</td>";
                        if (briefUser.id_brief_master > 0)
                            str2 = str2 + "<td><a class=\"btn btn-default btn-info \"> Sent on [" + briefUser.DATETIMESTAMP + "]</a></td>";
                        else
                            str2 = str2 + "<td><input style=\"margin-left:5%\" type=\"checkbox\" name=\"brf-user\" class=\"myCheckbox\"  value=\"" + (object)briefUser.PRUSER + "\"></td>";
                        str2 += "</tr>";
                    }
                }
                userListByLocation = briefUserList.Count<BriefUser>() != 0 ? "<table id=\"report-user-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th width=\"20%\">User Name</th>" + " <th width=\"20%\">UserID</th>" + " <th width=\"20%\">Employee ID</th>" + " <th width=\"20%\">Location</th>" + "<th  width=\"20%\"><input type = \"checkbox\" id = \"checkAll\" onclick = \"check_all()\"> Select all &nbsp;&nbsp;</th>" + "</thead>" + "<tbody>" + str2 + "</tbody></table>" : "<table id=\"report-user-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th>could not find any users for  " + location + "</th></tr>" + "</thead>" + "<tbody></tbody></table>";
            }
            else
                userListByLocation = "<table id=\"report-user-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th width=\"80%\">User Name</th>" + "<th  width=\"20%\"><input type = \"checkbox\" id = \"checkAll\" onclick = \"check_all()\"> Select all &nbsp;&nbsp;</th>" + "</thead>" + "<tbody></tbody></table>";
            return userListByLocation;
        }

        public string getAllUserListByRole(int bid, int id, string search)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string allUserListByRole = "";
            search = search.ToLower().Trim();
            string str1 = "";
            if (!string.IsNullOrEmpty(search))
                str1 = " and lower(pp.FIRSTNAME) like '%" + search + "%' ";
            List<BriefUser> briefUserList = new BriefModel().getBriefUserList("" + "  SELECT a.ID_USER PRUSER, a.EMPLOYEEID PREMPLOYEEID ,a.USERID PRUSERID, pp.FIRSTNAME PRNAME, a.user_function PRFUNCTION, pp.CITY PRCITY, pp.LOCATION PRLOCATION, b.ID_USER RMUSER, b.USERID RMUSERID, " + "  rp.FIRSTNAME RMNAME, CASE WHEN (ua.id_brief_user_assignment) THEN DATE_FORMAT(ua.updated_date_time, '%d-%m-%y') ELSE '--' END DATETIMESTAMP,   CASE WHEN (ua.id_brief_user_assignment) THEN ua.id_brief_user_assignment ELSE 0 END id_brief_user_assignment, CASE WHEN (ua.id_brief_master=" + (object)bid + ") THEN ua.id_brief_master ELSE 0 END id_brief_master  " + "  FROM tbl_user a  LEFT JOIN   tbl_brief_user_assignment ua ON a.id_user = ua.id_user  AND ua.id_brief_master = " + (object)bid + "  LEFT JOIN   tbl_user b ON b.ID_USER = a.reporting_manager, tbl_profile pp, tbl_profile rp  " + "  WHERE a.reporting_manager = b.ID_USER    AND pp.ID_USER = a.ID_USER AND rp.ID_USER = b.ID_USER AND a.ID_ORGANIZATION = " + (object)int32 + " AND a.status = 'A' and a.id_user in (select id_user from tbl_role_user_mapping  where id_csst_role=" + (object)id + ")" + str1 + "   ORDER BY pp.FIRSTNAME ");
            string str2 = "";
            foreach (BriefUser briefUser in briefUserList)
            {
                if (briefUser.id_brief_master == 0 || briefUser.id_brief_master == bid)
                {
                    str2 += "<tr>";
                    str2 = str2 + "<td>" + briefUser.PRNAME + " (" + briefUser.RMNAME + ")</td>";
                    str2 = str2 + "<td>" + briefUser.PRUSERID + "</td>";
                    str2 = str2 + "<td>" + briefUser.PREMPLOYEEID + "</td>";
                    str2 = str2 + "<td>" + briefUser.PRLOCATION + "</td>";
                    if (briefUser.id_brief_master > 0)
                        str2 = str2 + "<td><a class=\"btn btn-default btn-info \"> Sent on [" + briefUser.DATETIMESTAMP + "]</a></td>";
                    else
                        str2 = str2 + "<td><input style=\"margin-left:5%\" type=\"checkbox\" name=\"brf-user\" class=\"myCheckbox\"  value=\"" + (object)briefUser.PRUSER + "\"></td>";
                    str2 += "</tr>";
                }
            }
            if (briefUserList.Count<BriefUser>() != 0)
                allUserListByRole = "<table id=\"report-user-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th width=\"20%\">User Name</th>" + " <th width=\"20%\">UserID</th>" + " <th width=\"20%\">Employee ID</th>" + " <th width=\"20%\">Location</th>" + "<th  width=\"20%\"><input type = \"checkbox\" id = \"checkAll\" onclick = \"check_all()\"> Select all &nbsp;&nbsp;</th>" + "</thead>" + "<tbody>" + str2 + "</tbody></table>";
            return allUserListByRole;
        }

        public ActionResult finishUserBriefSchedulingAction(string udata, int ids, string sdt)
        {
            string[] array = ((IEnumerable<string>)udata.Split(',')).ToArray<string>();
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            DateTime now = DateTime.Now;
            DateTime datetime = new Utility().StringToDatetime(sdt);
            foreach (string str in array)
            {
                this.db.tbl_brief_user_assignment.Add(new tbl_brief_user_assignment()
                {
                    id_brief_master = new int?(ids),
                    scheduled_datetime = new DateTime?(datetime),
                    id_user = new int?(Convert.ToInt32(str)),
                    scheduled_status = "S",
                    published_datetime = new DateTime?(),
                    published_status = "NA",
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    assignment_datetime = new DateTime?(DateTime.Now),
                    assignment_status = "S"
                });
                this.db.SaveChanges();
                this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
                {
                    id_brief_master = new int?(ids),
                    id_organization = new int?(int32),
                    id_user = new int?(Convert.ToInt32(str)),
                    read_status = new int?(0),
                    action_status = new int?(0),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.status == "A" && t.id_brief_master == (int?)ids)).FirstOrDefault<tbl_brief_status>();
            if (tblBriefStatus != null)
            {
                tblBriefStatus.status = "D";
                tblBriefStatus.updated_date_time = new DateTime?(DateTime.Now);
                this.db.SaveChanges();
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(ids),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(4),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Scheduled"
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        public ActionResult finishUserBriefPublishAction(string udata, int ids)
        {
            string[] array = ((IEnumerable<string>)udata.Split(',')).ToArray<string>();
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            foreach (string str in array)
            {
                this.db.tbl_brief_user_assignment.Add(new tbl_brief_user_assignment()
                {
                    id_brief_master = new int?(ids),
                    scheduled_datetime = new DateTime?(),
                    id_user = new int?(Convert.ToInt32(str)),
                    scheduled_status = "NA",
                    published_datetime = new DateTime?(DateTime.Now),
                    published_status = "S",
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    assignment_datetime = new DateTime?(DateTime.Now),
                    assignment_status = "P"
                });
                this.db.SaveChanges();
                this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
                {
                    id_brief_master = new int?(ids),
                    id_user = new int?(Convert.ToInt32(str)),
                    read_status = new int?(0),
                    action_status = new int?(0),
                    id_organization = new int?(int32),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.status == "A" && t.id_brief_master == (int?)ids)).FirstOrDefault<tbl_brief_status>();
            if (tblBriefStatus != null)
            {
                tblBriefStatus.status = "D";
                tblBriefStatus.updated_date_time = new DateTime?(DateTime.Now);
                this.db.SaveChanges();
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(ids),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(5),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Published"
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        public ActionResult finishPublishtoAllUser(int ids)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            new BriefModel().getUserListById("SELECT a.id_user FROM tbl_user a WHERE a.id_user NOT IN (SELECT DISTINCT id_user FROM tbl_brief_user_assignment WHERE id_brief_master = " + (object)ids + ") AND a.ID_ORGANIZATION = " + (object)int32_1 + " AND a.status = 'A'");
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.status == "A" && t.id_brief_master == (int?)ids)).FirstOrDefault<tbl_brief_status>();
            if (tblBriefStatus != null)
            {
                tblBriefStatus.status = "D";
                tblBriefStatus.updated_date_time = new DateTime?(DateTime.Now);
                this.db.SaveChanges();
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(ids),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(5),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Published"
                });
                this.db.SaveChanges();
            }
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'P',{3})", (object)ids, (object)int32_1, (object)int32_2, (object)DateTime.Now);
            return (ActionResult)this.RedirectToAction("dashboard", "Brief");
        }

        public ActionResult briefTesting()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<TestBrief> testBriefUserList = new BriefModel().getTestBriefUserList("SELECT b.ID_USER, c.FIRSTNAME, d.id_brief_master, d.brief_code, d.brief_title FROM tbl_brief_user_assignment a, tbl_user b, tbl_profile c, tbl_brief_master d WHERE d.id_organization=" + (object)int32 + " and b.id_organization=" + (object)int32 + " and a.id_brief_master = d.id_brief_master AND a.id_user = b.ID_USER AND a.id_user = c.ID_USER and b.STATUS='A' and d.status='A'");
            List<BriefCollection> userTestResult = new BriefModel().getUserTestResult("SELECT b.brief_code,a.id_user, b.brief_title, CASE WHEN a.brief_result IS NULL THEN 0 ELSE a.brief_result END brief_result,a.attempt_no, c.FIRSTNAME FROM tbl_brief_log a, tbl_brief_master b, tbl_profile c WHERE a.id_brief_master = b.id_brief_master AND a.id_user = c.ID_USER AND a.id_organization=" + (object)int32 + " ");
            this.ViewData["testbrief"] = (object)testBriefUserList;
            this.ViewData["testResult"] = (object)userTestResult;
            return (ActionResult)this.View();
        }

        public ActionResult loadBrief(string brf, int ids)
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>)(t => t.ID_USER == ids)).FirstOrDefault<tbl_user>();
            if (brief != null && tblUser != null)
            {
                List<tbl_brief_question> list1 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + " and status='A') and status='A'").ToList<tbl_brief_question>();
                List<int> intList = new List<int>();
                List<QuestionList> questionListList = new List<QuestionList>();
                int int32_2 = Convert.ToInt32((object)brief.question_count);
                foreach (tbl_brief_question tblBriefQuestion in list1)
                {
                    QuestionList questionList = new QuestionList();
                    questionList.question = tblBriefQuestion;
                    List<tbl_brief_answer> list2 = this.db.tbl_brief_answer.SqlQuery("select * from tbl_brief_answer where id_organization=" + (object)int32_1 + " and id_brief_question=" + (object)tblBriefQuestion.id_brief_question + " and status='A'").ToList<tbl_brief_answer>();
                    questionList.answers = list2;
                    questionListList.Add(questionList);
                    intList.Add(tblBriefQuestion.id_brief_question);
                }
                int num1 = int32_2 - list1.Count<tbl_brief_question>();
                if (num1 > 0)
                {
                    int num2 = num1;
                    List<tbl_brief_category> list3 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + (object)int32_1 + " and id_brief_category not in (" + (object)brief.id_brief_category + ") and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + (object)int32_1 + ") limit " + (object)num1).ToList<tbl_brief_category>();
                    List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
                    int num3 = list3.Count<tbl_brief_category>();
                    int num4 = num3 * 5;
                    for (int index1 = 0; index1 < num4 && num2 != 0; ++index1)
                    {
                        int index2 = index1 % num3;
                        tbl_brief_category tblBriefCategory = list3[index2];
                        tbl_brief_question distributionQuestion = this.getProgressiveDistributionQuestion(ids, tblBriefCategory.id_brief_category);
                        if (distributionQuestion != null && !intList.Contains(distributionQuestion.id_brief_question))
                        {
                            tblBriefQuestionList.Add(distributionQuestion);
                            --num2;
                        }
                    }
                    foreach (tbl_brief_question tblBriefQuestion in tblBriefQuestionList)
                    {
                        QuestionList questionList = new QuestionList();
                        questionList.question = tblBriefQuestion;
                        List<tbl_brief_answer> list4 = this.db.tbl_brief_answer.SqlQuery("select * from tbl_brief_answer where id_organization=" + (object)int32_1 + " and id_brief_question=" + (object)tblBriefQuestion.id_brief_question + " and status='A'").ToList<tbl_brief_answer>();
                        questionList.answers = list4;
                        questionListList.Add(questionList);
                    }
                }
                tbl_brief_category tblBriefCategory1 = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
                tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
                tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
                bool flag = false;
                if (tblBriefStatus != null)
                {
                    int? statusCode = tblBriefStatus.status_code;
                    int num5 = 1;
                    if (statusCode.GetValueOrDefault() == num5 & statusCode.HasValue)
                        flag = true;
                }
                this.ViewData["questionlist"] = (object)questionListList;
                this.ViewData["briefmaster"] = (object)brief;
                this.ViewData["category"] = (object)tblBriefCategory1;
                this.ViewData["subcategory"] = (object)briefSubcategory;
                this.ViewData["accessFlag"] = (object)flag;
                this.ViewData["asuser"] = (object)tblUser;
            }
            return (ActionResult)this.View();
        }

        public ActionResult setBriefEvaluationAction()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(this.Request.Form["id_user"]);
            int idbriefmaster = Convert.ToInt32(this.Request.Form["id_brief_master"]);
            List<BriefDataCube> briefDataCubeList = new List<BriefDataCube>();
            List<BriefUserInput> briefUserInputList = new List<BriefUserInput>();
            BriefReturnResponse briefReturnResponse = new BriefReturnResponse();
            int num1 = 0;
            tbl_brief_master tblBriefMaster = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.id_brief_master == idbriefmaster)).FirstOrDefault<tbl_brief_master>();
            if (tblBriefMaster == null)
                return (ActionResult)this.RedirectToAction("dashboard", "Brief");
            int int32_3 = Convert.ToInt32(this.Request.Form["qtn_count"].ToString());
            if (int32_3 <= 0)
                return (ActionResult)this.RedirectToAction("loadBrief", "Brief", (object)new
                {
                    brf = tblBriefMaster.brief_code,
                    ids = int32_2
                });
            int num2 = new BriefModel().getAttamptNo("SELECT count(*) subcount FROM tbl_brief_index where id_user=" + (object)int32_2 + " and id_brief_master=" + (object)tblBriefMaster.id_brief_master + " ") + 1;
            this.db.tbl_brief_index.Add(new tbl_brief_index()
            {
                id_brief_master = tblBriefMaster.id_brief_master,
                id_user = int32_2,
                status = "A",
                updated_date_time = new DateTime?(DateTime.Now)
            });
            this.db.SaveChanges();
            for (int index = 1; index <= int32_3; ++index)
            {
                string str = this.Request.Form["brf-qtn-" + (object)index];
                if (str != null)
                {
                    BriefDataCube briefDataCube = new BriefDataCube();
                    briefDataCube.QID = Convert.ToInt32(str);
                    briefDataCube.AID = Convert.ToInt32(this.Request.Form["brf-ans-" + (object)index].ToString());
                    briefDataCube.VAL = "NA";
                    briefDataCubeList.Add(briefDataCube);
                    this.db.tbl_brief_audit.Add(new tbl_brief_audit()
                    {
                        attempt_no = new int?(num2),
                        id_brief_master = new int?(tblBriefMaster.id_brief_master),
                        id_brief_question = new int?(briefDataCube.QID),
                        id_brief_answer = new int?(briefDataCube.AID),
                        id_user = new int?(int32_2),
                        recorded_timestamp = new DateTime?(DateTime.Now),
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now),
                        value_sent = new int?(0)
                    });
                    this.db.SaveChanges();
                }
            }
            int num3 = 1;
            foreach (BriefDataCube briefDataCube in briefDataCubeList)
            {
                BriefDataCube item = briefDataCube;
                BriefUserInput briefUserInput = new BriefUserInput();
                tbl_brief_question tblBriefQuestion = this.db.tbl_brief_question.Where<tbl_brief_question>((Expression<Func<tbl_brief_question, bool>>)(t => t.id_brief_question == item.QID)).FirstOrDefault<tbl_brief_question>();
                if (tblBriefQuestion != null)
                {
                    tbl_brief_answer tblBriefAnswer1 = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>)(t => t.id_brief_answer == item.AID)).FirstOrDefault<tbl_brief_answer>();
                    tbl_brief_answer tblBriefAnswer2 = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>)(t => t.id_brief_question == (int?)item.QID && t.is_correct_answer == (int?)1)).FirstOrDefault<tbl_brief_answer>();
                    int? isCorrectAnswer = tblBriefAnswer1.is_correct_answer;
                    int num4 = 1;
                    if (isCorrectAnswer.GetValueOrDefault() == num4 & isCorrectAnswer.HasValue)
                        ++num1;
                    briefUserInput.Question = "Q. " + tblBriefQuestion.brief_question;
                    briefUserInput.Answer = "A. " + tblBriefAnswer1.brief_answer;
                    briefUserInput.WANS = tblBriefAnswer2.brief_answer;
                    briefUserInput.srno = num3++;
                    briefUserInputList.Add(briefUserInput);
                }
            }
            briefReturnResponse.briefReturn = briefUserInputList;
            briefReturnResponse.returnStat = "You have answered " + (object)num1 + " out of " + (object)int32_3 + " question right ";
            briefReturnResponse.rightCount = num1;
            briefReturnResponse.totalCount = int32_3;
            double num5 = 0.0;
            if (num1 > 0 && int32_3 > 0)
                num5 = Math.Round((double)(num1 * 100 / int32_3), 2);
            tbl_brief_log entity = new tbl_brief_log();
            entity.attempt_no = num2;
            entity.id_brief_master = tblBriefMaster.id_brief_master;
            entity.id_organization = new int?(int32_1);
            entity.id_user = int32_2;
            entity.brief_result = new double?(num5);
            entity.status = "A";
            entity.updated_date_time = new DateTime?(DateTime.Now);
            string str1 = JsonConvert.SerializeObject((object)briefReturnResponse);
            entity.json_response = str1;
            this.db.tbl_brief_log.Add(entity);
            this.db.SaveChanges();
            return (ActionResult)this.RedirectToAction("BriefEvaluation", "Brief", (object)new
            {
                brf = tblBriefMaster.brief_code,
                uid = int32_2,
                atm = num2
            });
        }

        public ActionResult BriefEvaluation(string brf, int uid, int atm)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower().Trim())).FirstOrDefault<tbl_brief_master>();
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboard", "Brief");
            tbl_brief_log tblBriefLog = this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>)(t => t.attempt_no == atm && t.id_brief_master == brief.id_brief_master && t.id_user == uid)).FirstOrDefault<tbl_brief_log>();
            if (tblBriefLog == null)
                return (ActionResult)this.RedirectToAction("loadBrief", "Brief", (object)new
                {
                    brf = brief.brief_code,
                    ids = uid
                });
            BriefReturnResponse briefReturnResponse = JsonConvert.DeserializeObject<BriefReturnResponse>(tblBriefLog.json_response);
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["response"] = (object)briefReturnResponse;
            this.ViewData["briefMaster"] = (object)brief;
            return (ActionResult)this.View();
        }

        public tbl_brief_question getProgressiveDistributionQuestion(int UID, int CID)
        {
            tbl_brief_audit lstQtn = new tbl_brief_audit();
            lstQtn = this.db.tbl_brief_audit.SqlQuery("SELECT * FROM tbl_brief_audit WHERE id_user = " + (object)UID + " AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE id_brief_category = " + (object)CID + ") ORDER BY id_brief_audit DESC LIMIT 1").FirstOrDefault<tbl_brief_audit>();
            bool status = false;
            if (lstQtn == null)
                return this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question WHERE id_brief_category =" + (object)CID + " AND status = 'A' AND expiry_date > NOW() ORDER BY question_complexity LIMIT 1").FirstOrDefault<tbl_brief_question>() ?? (tbl_brief_question)null;
            tbl_brief_question tblBriefQuestion = this.db.tbl_brief_question.Where<tbl_brief_question>((Expression<Func<tbl_brief_question, bool>>)(t => (int?)t.id_brief_question == lstQtn.id_brief_question)).FirstOrDefault<tbl_brief_question>();
            int? auditResult = lstQtn.audit_result;
            int num = 1;
            if (auditResult.GetValueOrDefault() == num & auditResult.HasValue)
                status = true;
            int complecityLevel = this.getComplecityLevel(CID, status, tblBriefQuestion.question_complexity);
            tbl_brief_question distributionQuestion = this.db.tbl_brief_question.SqlQuery("select * from tbl_breif_question where id_brief_question not in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + (object)UID + " ) and question_complexity=" + (object)complecityLevel + " and status='A' and expiry_date>now() ORDER BY  RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>();
            if (distributionQuestion != null)
                return distributionQuestion;
            return this.db.tbl_brief_question.SqlQuery("select * from tbl_breif_question where id_brief_question in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + (object)UID + " AND audit_result=0) and question_complexity=" + (object)complecityLevel + " and status='A' and expiry_date>now() ORDER BY RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>() ?? (tbl_brief_question)null;
        }

        public int getComplecityLevel(int CID, bool status, int? level)
        {
            string str = "";
            str = (!status ? string.Concat("  AND question_complexity < ", level, " order by question_complexity desc LIMIT 1 ") : string.Concat("  AND question_complexity > ", level, " order by question_complexity  LIMIT 1 "));
            string.Concat(new object[] { "SELECT * FROM tbl_brief_question_complexity WHERE question_complexity IN (SELECT DISTINCT question_complexity FROM tbl_brief_question WHERE id_brief_category = ", CID, ") ", str });
            tbl_brief_question_complexity tblBriefQuestionComplexity = this.db.tbl_brief_question_complexity.SqlQuery("", new object[0]).FirstOrDefault<tbl_brief_question_complexity>();
            if (tblBriefQuestionComplexity == null)
            {
                return Convert.ToInt32(level);
            }
            return Convert.ToInt32(tblBriefQuestionComplexity.question_complexity);
        }

        public string getCategoryDistributionList(int cid)
        {
            List<tbl_brief_category> list = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION='" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + "' AND status='A' and id_brief_category !='" + (object)cid + "' order by brief_category").ToList<tbl_brief_category>();
            string distributionList;
            if (list.Count > 0)
            {
                string str = "<div class=\"row\"><div class=\"col-md-12\">";
                foreach (tbl_brief_category tblBriefCategory in list)
                    str = str + "<div class=\"col-md-3\"><div class=\"checkbox\"> <input class=\"styled\" type=\"checkbox\" name=\"brf-category[]\"  value=\"" + (object)tblBriefCategory.id_brief_category + "\"><label for=\"check1\">" + tblBriefCategory.brief_category + "  </label></div></div>";
                distributionList = str + "</div></div>";
            }
            else
                distributionList = "<div class=\"row\"><div class=\"col-md-12\"><label class=\"control-label\">there are no categories found</label></div></div>";
            return distributionList;
        }

        public string getCategoryDistributionEditList(int cid, int bid)
        {
            List<tbl_brief_category> list = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION='" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + "' AND status='A' and id_brief_category !='" + (object)cid + "' order by brief_category").ToList<tbl_brief_category>();
            string distributionEditList;
            if (list.Count > 0)
            {
                string str1 = "<div class=\"row\"><div class=\"col-md-12\">";
                foreach (tbl_brief_category tblBriefCategory in list)
                {
                    tbl_brief_category item = tblBriefCategory;
                    tbl_brief_category_mapping briefCategoryMapping = this.db.tbl_brief_category_mapping.Where<tbl_brief_category_mapping>((Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_category == (int?)item.id_brief_category && t.id_brief_master == (int?)bid && t.status == "A")).FirstOrDefault<tbl_brief_category_mapping>();
                    string str2 = "";
                    if (briefCategoryMapping != null)
                        str2 = " checked ";
                    str1 = str1 + "<div class=\"col-md-3\"><div class=\"checkbox\"> <input " + str2 + " class=\"styled\" type=\"checkbox\" name=\"brf-category[]\"  value=\"" + (object)item.id_brief_category + "\"><label for=\"check1\">" + item.brief_category + "  </label></div></div>";
                }
                distributionEditList = str1 + "</div></div>";
            }
            else
                distributionEditList = "<div class=\"row\"><div class=\"col-md-12\"><label class=\"control-label\">there are no categories found</label></div></div>";
            return distributionEditList;
        }

        public ActionResult Tiles()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category_tile> briefCategoryTileList = new List<tbl_brief_category_tile>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                briefCategoryTileList = m2ostDbContext.Database.SqlQuery<tbl_brief_category_tile>("select * from tbl_brief_category_tile where status='A' and id_organization={0}", (object)int32).ToList<tbl_brief_category_tile>();
            this.ViewData["tiles"] = (object)briefCategoryTileList;
            return (ActionResult)this.View();
        }

        public ActionResult createTile() => (ActionResult)this.View();

        public ActionResult addBriefCategoryTile()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "BRCT" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
            tbl_brief_category_tile entity = new tbl_brief_category_tile();
            entity.id_organization = new int?(int32);
            entity.tile_code = str;
            entity.category_tile = this.Request.Form["category-tile"].ToString();
            entity.tile_description = this.Request.Form["tile-description"].ToString();
            entity.category_tile_type = new int?(Convert.ToInt32(this.Request.Form["select-tile-type"].ToString()));
            entity.tile_position = new int?(Convert.ToInt32(this.Request.Form["category-order"].ToString()));
            entity.attempt_limit = new int?(Convert.ToInt32(this.Request.Form["category-limit"].ToString()));
            entity.assessment_complation = new int?(Convert.ToInt32(this.Request.Form["select-tile-completion"].ToString()));
            entity.status = "A";
            entity.updated_date_time = new DateTime?(DateTime.Now);
            this.db.tbl_brief_category_tile.Add(entity);
            this.db.SaveChanges();
            entity.tile_type = new int?(Convert.ToInt32(this.Request.Form["non-learn-type"].ToString()));
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("update tbl_brief_category_tile set tile_type={0} where id_brief_category_tile={1}", (object)entity.tile_type, (object)entity.id_brief_category_tile);
            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["uploadBtn"];
                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["uploadBtn"].FileName);
                string path2 = str + extension;
                if (file != null && file.ContentLength > 0)
                {
                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)int32 + "/TILE/")))
                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)int32 + "/TILE/"));
                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)int32 + "/TILE/"), path2);
                    file.SaveAs(filename);
                    entity.tile_image = path2;
                    this.db.SaveChanges();
                }
            }
            return (ActionResult)this.RedirectToAction("Tiles", "Brief");
        }

        public ActionResult setCategoryTileMapping(int cit)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_category_tile briefCategoryTile = this.db.tbl_brief_category_tile.Where<tbl_brief_category_tile>((Expression<Func<tbl_brief_category_tile, bool>>)(t => t.id_brief_category_tile == cit && t.status == "A")).FirstOrDefault<tbl_brief_category_tile>();
            if (briefCategoryTile == null)
                return (ActionResult)this.RedirectToAction("Tiles", "Brief");
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND id_brief_category not in (SELECT id_brief_category FROM tbl_brief_tile_category_mapping where id_brief_category_tile=" + (object)briefCategoryTile.id_brief_category_tile + " and id_organization=" + (object)int32 + ") AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_category> list2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND id_brief_category in (SELECT id_brief_category FROM tbl_brief_tile_category_mapping where id_brief_category_tile=" + (object)briefCategoryTile.id_brief_category_tile + " and id_organization=" + (object)int32 + ") AND status='A' order by brief_category").ToList<tbl_brief_category>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["catList"] = (object)list2;
            this.ViewData["tile"] = (object)briefCategoryTile;
            return (ActionResult)this.View();
        }

        public string addCategoryToTile(int cid, int ids)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_category_tile briefCategoryTile = this.db.tbl_brief_category_tile.Where<tbl_brief_category_tile>((Expression<Func<tbl_brief_category_tile, bool>>)(t => t.id_brief_category_tile == cid && t.status == "A")).FirstOrDefault<tbl_brief_category_tile>();
            if (briefCategoryTile == null)
                return "0";
            tbl_brief_tile_category_mapping entity = new tbl_brief_tile_category_mapping();
            entity.id_brief_category = new int?(ids);
            entity.id_brief_category_tile = new int?(briefCategoryTile.id_brief_category_tile);
            entity.id_organization = briefCategoryTile.id_organization;
            entity.status = "A";
            entity.updated_date_time = new DateTime?(DateTime.Now);
            this.db.tbl_brief_tile_category_mapping.Add(entity);
            this.db.SaveChanges();
            return entity.id_brief_tile_category_mapping > 0 ? "1" : "0";
        }

        public JsonResult question_preview(int Qid)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_question> data = new List<tbl_brief_question>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                data = m2ostDbContext.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where id_brief_question={0}", (object)Qid).ToList<tbl_brief_question>();
            return this.Json((object)data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult briefAnsChoic(int Qid)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_answer> data = new List<tbl_brief_answer>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                data = m2ostDbContext.Database.SqlQuery<tbl_brief_answer>("SELECT * FROM tbl_brief_answer where id_brief_question={0}", (object)Qid).ToList<tbl_brief_answer>();
            return this.Json((object)data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult briefMetadaQues(int Qid)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_question_metadata> data = new List<tbl_brief_question_metadata>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                data = m2ostDbContext.Database.SqlQuery<tbl_brief_question_metadata>("select * from tbl_brief_question_metadata where id_brief_question={0}", (object)Qid).ToList<tbl_brief_question_metadata>();
            return this.Json((object)data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditQuestion(int idQue)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["complexity"] = (object)this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity where status='A' ").ToList<tbl_brief_question_complexity>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["subcategory"] = (object)list2;
            List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
            List<tbl_brief_answer> tblBriefAnswerList = new List<tbl_brief_answer>();
            List<tbl_brief_question_metadata> questionMetadataList = new List<tbl_brief_question_metadata>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                tblBriefQuestionList = m2ostDbContext.Database.SqlQuery<tbl_brief_question>("SELECT * FROM tbl_brief_question where id_brief_question={0}", (object)idQue).ToList<tbl_brief_question>();
                tblBriefAnswerList = m2ostDbContext.Database.SqlQuery<tbl_brief_answer>("SELECT * FROM tbl_brief_answer where id_brief_question={0}", (object)idQue).ToList<tbl_brief_answer>();
                questionMetadataList = m2ostDbContext.Database.SqlQuery<tbl_brief_question_metadata>("SELECT * FROM tbl_brief_question_metadata where id_brief_question={0}", (object)idQue).ToList<tbl_brief_question_metadata>();
            }
            this.ViewData["BriefQue"] = (object)tblBriefQuestionList;
            this.ViewData["BriefAns"] = (object)tblBriefAnswerList;
            this.ViewData["BriefMeta"] = (object)questionMetadataList;
            return (ActionResult)this.View();
        }

        public ActionResult updateQuestionAction()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            try
            {
                switch (Convert.ToInt32(this.Request.Form["themeSele"].ToString()))
                {
                    case 1:
                        tbl_brief_question qtn = new tbl_brief_question();
                        qtn.id_brief_question = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                        qtn.id_organization = new int?(int32_1);
                        qtn.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                        qtn.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                        qtn.brief_question = this.Request.Form["question-title"].ToString();
                        qtn.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                        qtn.question_choice_type = new int?(0);
                        qtn.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                        qtn.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                        string dateString1 = this.Request.Form["question-expiry"].ToString();
                        DateTime now1 = DateTime.Now;
                        DateTime datetime1 = new Utility().StringToDatetime(dateString1);
                        qtn.expiry_date = new DateTime?(datetime1);
                        qtn.status = "A";
                        qtn.updated_date_time = new DateTime?(DateTime.Now);
                        new BriefLogic().updateBriefQuestion(qtn);
                        int int32_2 = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                        if (int32_2 > 0)
                        {
                            int int32_3 = Convert.ToInt32(this.Request.Form["hid-row-count"].ToString());
                            string str1 = this.Request.Form["check-box-option"].ToString();
                            if (str1 != null)
                            {
                                string[] source = str1.Split(',');
                                int num = 0;
                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                    num = Convert.ToInt32(source[0]);
                                for (int index = 1; index <= int32_3; ++index)
                                {
                                    string str2 = this.Request.Form["idchoceques-" + (object)index];
                                    if (string.IsNullOrEmpty(str2))
                                        new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                        {
                                            id_organization = new int?(int32_1),
                                            id_brief_question = new int?(int32_2),
                                            brief_answer = this.Request.Form["brief-option-" + (object)index].ToString(),
                                            is_correct_answer = index != num ? new int?(0) : new int?(1),
                                            choice_type = new int?(0),
                                            status = "A",
                                            updated_date_time = new DateTime?(DateTime.Now)
                                        });
                                    else
                                        new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                        {
                                            id_brief_answer = Convert.ToInt32(str2),
                                            id_organization = new int?(int32_1),
                                            id_brief_question = new int?(int32_2),
                                            brief_answer = this.Request.Form["brief-option-" + (object)index].ToString(),
                                            is_correct_answer = index != num ? new int?(0) : new int?(1),
                                            choice_type = new int?(0),
                                            status = "A",
                                            updated_date_time = new DateTime?(DateTime.Now)
                                        });
                                }
                            }
                            DbSet<tbl_brief_question_metadata> questionMetadata1 = this.db.tbl_brief_question_metadata;
                            Expression<Func<tbl_brief_question_metadata, bool>> predicate1 = (Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)qtn.id_brief_question);
                            foreach (tbl_brief_question_metadata questionMetadata2 in questionMetadata1.Where<tbl_brief_question_metadata>(predicate1).ToList<tbl_brief_question_metadata>())
                            {
                                questionMetadata2.status = "T";
                                this.db.SaveChanges();
                            }
                            string str3 = this.Request.Form["question-metadata"].ToString();
                            char[] chArray = new char[1] { ',' };
                            foreach (string str4 in str3.Split(chArray))
                            {
                                string item = str4;
                                if (item.Count<char>() > 0)
                                {
                                    tbl_brief_question_metadata questionMetadata3 = this.db.tbl_brief_question_metadata.Where<tbl_brief_question_metadata>((Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)qtn.id_brief_question && t.brief_question_metadata.ToLower().Trim() == item.ToLower())).FirstOrDefault<tbl_brief_question_metadata>();
                                    if (questionMetadata3 != null)
                                    {
                                        questionMetadata3.status = "A";
                                        this.db.SaveChanges();
                                    }
                                    else
                                    {
                                        string str5 = item.Trim();
                                        this.db.tbl_brief_question_metadata.Add(new tbl_brief_question_metadata()
                                        {
                                            id_brief_question = new int?(int32_2),
                                            brief_question_metadata = str5,
                                            status = "A",
                                            updated_date_time = new DateTime?(DateTime.Now)
                                        });
                                        this.db.SaveChanges();
                                    }
                                }
                            }
                            DbSet<tbl_brief_question_metadata> questionMetadata4 = this.db.tbl_brief_question_metadata;
                            Expression<Func<tbl_brief_question_metadata, bool>> predicate2 = (Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)qtn.id_brief_question && t.status == "T");
                            using (List<tbl_brief_question_metadata>.Enumerator enumerator = questionMetadata4.Where<tbl_brief_question_metadata>(predicate2).ToList<tbl_brief_question_metadata>().GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    this.db.tbl_brief_question_metadata.Remove(enumerator.Current);
                                    this.db.SaveChanges();
                                }
                                break;
                            }
                        }
                        else
                            break;
                    case 2:
                        int insertedId = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                        int num1 = 0;
                        switch (Convert.ToInt32(this.Request.Form["themtyp"].ToString()))
                        {
                            case 1:
                                tbl_brief_question qtn1 = new tbl_brief_question();
                                qtn1.id_brief_question = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                                qtn1.id_organization = new int?(int32_1);
                                qtn1.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                                qtn1.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                                qtn1.brief_question = this.Request.Form["questionText"].ToString();
                                qtn1.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                                qtn1.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                                qtn1.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                                qtn1.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                                string dateString2 = this.Request.Form["question-expiry"].ToString();
                                DateTime now2 = DateTime.Now;
                                DateTime datetime2 = new Utility().StringToDatetime(dateString2);
                                qtn1.expiry_date = new DateTime?(datetime2);
                                qtn1.status = "A";
                                qtn1.updated_date_time = new DateTime?(DateTime.Now);
                                new BriefLogic().UpdateQuestionSec(qtn1);
                                break;
                            case 2:
                                tbl_brief_question qtn2 = new tbl_brief_question();
                                qtn2.id_brief_question = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                                qtn2.id_organization = new int?(int32_1);
                                qtn2.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                                qtn2.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                                qtn2.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                                qtn2.question_image = "";
                                qtn2.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                                qtn2.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                                qtn2.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                                string dateString3 = this.Request.Form["question-expiry"].ToString();
                                DateTime now3 = DateTime.Now;
                                DateTime datetime3 = new Utility().StringToDatetime(dateString3);
                                qtn2.expiry_date = new DateTime?(datetime3);
                                qtn2.status = "A";
                                qtn2.updated_date_time = new DateTime?(DateTime.Now);
                                new BriefLogic().UpdateQuestionImage(qtn2);
                                tbl_brief_question tblBriefQuestion1 = new tbl_brief_question();
                                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                {
                                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                                    string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                                    string str = System.Web.HttpContext.Current.Request["id"];
                                    if (file != null && file.ContentLength > 0)
                                    {
                                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), insertedId.ToString() + "BriefQ" + extension);
                                        file.SaveAs(filename);
                                        tblBriefQuestion1.id_brief_question = insertedId;
                                        tblBriefQuestion1.question_image = insertedId.ToString() + "BriefQ" + extension;
                                        new BriefLogic().updateQuestion(tblBriefQuestion1);
                                        break;
                                    }
                                    tblBriefQuestion1.id_brief_question = insertedId;
                                    tblBriefQuestion1.question_image = this.Request.Form["league-image"].ToString();
                                    new BriefLogic().updateQuestion(tblBriefQuestion1);
                                    break;
                                }
                                break;
                            case 3:
                                tbl_brief_question qtn3 = new tbl_brief_question();
                                qtn3.id_organization = new int?(int32_1);
                                qtn3.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                                qtn3.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                                qtn3.brief_question = this.Request.Form["questionText"].ToString();
                                qtn3.question_complexity = new int?(Convert.ToInt32(this.Request.Form["question-complexity"]));
                                qtn3.question_theme_type = new int?(Convert.ToInt32(this.Request.Form["themeSele"].ToString()));
                                qtn3.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                                qtn3.question_type = Convert.ToInt32(this.Request.Form["question-type"]);
                                string dateString4 = this.Request.Form["question-expiry"].ToString();
                                DateTime now4 = DateTime.Now;
                                DateTime datetime4 = new Utility().StringToDatetime(dateString4);
                                qtn3.expiry_date = new DateTime?(datetime4);
                                qtn3.status = "A";
                                qtn3.updated_date_time = new DateTime?(DateTime.Now);
                                new BriefLogic().UpdateQuestionSec(qtn3);
                                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                {
                                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                                    string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                                    string str = System.Web.HttpContext.Current.Request["id"];
                                    if (file != null && file.ContentLength > 0)
                                    {
                                        tbl_brief_question tblBriefQuestion2 = new tbl_brief_question();
                                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), insertedId.ToString() + "BriefQ" + extension);
                                        file.SaveAs(filename);
                                        tblBriefQuestion2.id_brief_question = insertedId;
                                        tblBriefQuestion2.question_image = insertedId.ToString() + "BriefQ" + extension;
                                        new BriefLogic().updateQuestion(tblBriefQuestion2);
                                        break;
                                    }
                                    new BriefLogic().updateQuestion(new tbl_brief_question()
                                    {
                                        id_brief_question = insertedId,
                                        question_image = this.Request.Form["league-image"].ToString()
                                    });
                                    break;
                                }
                                break;
                        }
                        if (insertedId > 0)
                        {
                            int int32_4 = Convert.ToInt32(this.Request.Form["imgChoiCount"].ToString());
                            string str6 = this.Request.Form["check-box-img-option"].ToString();
                            for (int index = 1; index <= int32_4; ++index)
                            {
                                int int32_5 = Convert.ToInt32(this.Request.Form["choiceType" + (object)index].ToString());
                                string str7 = this.Request.Form["secondchoceid-" + (object)index];
                                if (string.IsNullOrEmpty(str7))
                                {
                                    switch (int32_5)
                                    {
                                        case 1:
                                            if (str6 != null)
                                            {
                                                string[] source = str6.Split(',');
                                                int num2 = 0;
                                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                                    num2 = Convert.ToInt32(source[0]);
                                                num1 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                                {
                                                    id_organization = new int?(int32_1),
                                                    id_brief_question = new int?(insertedId),
                                                    brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                    choice_type = new int?(int32_5),
                                                    is_correct_answer = index != num2 ? new int?(0) : new int?(1),
                                                    status = "A",
                                                    updated_date_time = new DateTime?(DateTime.Now)
                                                });
                                                continue;
                                            }
                                            continue;
                                        case 2:
                                            if (str6 != null)
                                            {
                                                string[] source = str6.Split(',');
                                                int num3 = 0;
                                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                                    num3 = Convert.ToInt32(source[0]);
                                                num1 = new BriefLogic().InsertimgChoiOpt(new tbl_brief_answer()
                                                {
                                                    id_organization = new int?(int32_1),
                                                    id_brief_question = new int?(insertedId),
                                                    choice_type = new int?(int32_5),
                                                    is_correct_answer = index != num3 ? new int?(0) : new int?(1),
                                                    status = "A",
                                                    updated_date_time = new DateTime?(DateTime.Now)
                                                });
                                            }
                                            tbl_brief_answer ans1 = new tbl_brief_answer();
                                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                            {
                                                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                                string str8 = System.Web.HttpContext.Current.Request["id"];
                                                if (file != null && file.ContentLength > 0)
                                                {
                                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                    file.SaveAs(filename);
                                                    ans1.id_brief_answer = num1;
                                                    ans1.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                    new BriefLogic().updateimgChoiOpt(ans1);
                                                    continue;
                                                }
                                                continue;
                                            }
                                            continue;
                                        case 3:
                                            if (str6 != null)
                                            {
                                                string[] source = str6.Split(',');
                                                int num4 = 0;
                                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                                    num4 = Convert.ToInt32(source[0]);
                                                num1 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                                {
                                                    id_organization = new int?(int32_1),
                                                    id_brief_question = new int?(insertedId),
                                                    brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                    choice_type = new int?(int32_5),
                                                    is_correct_answer = index != num4 ? new int?(0) : new int?(1),
                                                    status = "A",
                                                    updated_date_time = new DateTime?(DateTime.Now)
                                                });
                                            }
                                            tbl_brief_answer ans2 = new tbl_brief_answer();
                                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                            {
                                                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                                string str9 = System.Web.HttpContext.Current.Request["id"];
                                                if (file != null && file.ContentLength > 0)
                                                {
                                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                    file.SaveAs(filename);
                                                    ans2.id_brief_answer = num1;
                                                    ans2.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                    new BriefLogic().updateimgChoiOpt(ans2);
                                                    continue;
                                                }
                                                continue;
                                            }
                                            continue;
                                        default:
                                            continue;
                                    }
                                }
                                else
                                {
                                    num1 = Convert.ToInt32(str7);
                                    switch (int32_5)
                                    {
                                        case 1:
                                            if (str6 != null)
                                            {
                                                string[] source = str6.Split(',');
                                                int num5 = 0;
                                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                                    num5 = Convert.ToInt32(source[0]);
                                                new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                                {
                                                    id_brief_answer = num1,
                                                    id_organization = new int?(int32_1),
                                                    id_brief_question = new int?(insertedId),
                                                    brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                    choice_type = new int?(int32_5),
                                                    is_correct_answer = index != num5 ? new int?(0) : new int?(1),
                                                    status = "A",
                                                    updated_date_time = new DateTime?(DateTime.Now)
                                                });
                                                continue;
                                            }
                                            continue;
                                        case 2:
                                            if (str6 != null)
                                            {
                                                string[] source = str6.Split(',');
                                                int num6 = 0;
                                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                                    num6 = Convert.ToInt32(source[0]);
                                                new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                                {
                                                    id_brief_answer = num1,
                                                    id_organization = new int?(int32_1),
                                                    id_brief_question = new int?(insertedId),
                                                    brief_answer = "",
                                                    choice_type = new int?(int32_5),
                                                    is_correct_answer = index != num6 ? new int?(0) : new int?(1),
                                                    status = "A",
                                                    updated_date_time = new DateTime?(DateTime.Now)
                                                });
                                            }
                                            tbl_brief_answer ans3 = new tbl_brief_answer();
                                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                            {
                                                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                                string str10 = System.Web.HttpContext.Current.Request["id"];
                                                if (file != null && file.ContentLength > 0)
                                                {
                                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                    file.SaveAs(filename);
                                                    ans3.id_brief_answer = num1;
                                                    ans3.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                    new BriefLogic().updateimgChoiOpt(ans3);
                                                    continue;
                                                }
                                                ans3.id_brief_answer = num1;
                                                ans3.choice_image = this.Request.Form["league-image" + (object)index].ToString();
                                                new BriefLogic().updateimgChoiOpt(ans3);
                                                continue;
                                            }
                                            continue;
                                        case 3:
                                            if (str6 != null)
                                            {
                                                string[] source = str6.Split(',');
                                                int num7 = 0;
                                                if (((IEnumerable<string>)source).Count<string>() > 0)
                                                    num7 = Convert.ToInt32(source[0]);
                                                new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                                {
                                                    id_brief_answer = num1,
                                                    id_organization = new int?(int32_1),
                                                    id_brief_question = new int?(insertedId),
                                                    brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                    choice_type = new int?(int32_5),
                                                    is_correct_answer = index != num7 ? new int?(0) : new int?(1),
                                                    status = "A",
                                                    updated_date_time = new DateTime?(DateTime.Now)
                                                });
                                            }
                                            tbl_brief_answer ans4 = new tbl_brief_answer();
                                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                            {
                                                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                                string str11 = System.Web.HttpContext.Current.Request["id"];
                                                if (file != null && file.ContentLength > 0)
                                                {
                                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                    file.SaveAs(filename);
                                                    ans4.id_brief_answer = num1;
                                                    ans4.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                    new BriefLogic().updateimgChoiOpt(ans4);
                                                    continue;
                                                }
                                                ans4.id_brief_answer = num1;
                                                ans4.choice_image = this.Request.Form["league-image" + (object)index].ToString();
                                                new BriefLogic().updateimgChoiOpt(ans4);
                                                continue;
                                            }
                                            continue;
                                        default:
                                            continue;
                                    }
                                }
                            }
                        }
                        DbSet<tbl_brief_question_metadata> questionMetadata5 = this.db.tbl_brief_question_metadata;
                        Expression<Func<tbl_brief_question_metadata, bool>> predicate3 = (Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)insertedId);
                        foreach (tbl_brief_question_metadata questionMetadata6 in questionMetadata5.Where<tbl_brief_question_metadata>(predicate3).ToList<tbl_brief_question_metadata>())
                        {
                            questionMetadata6.status = "T";
                            this.db.SaveChanges();
                        }
                        string str12 = this.Request.Form["question-metadata"].ToString();
                        char[] chArray1 = new char[1] { ',' };
                        foreach (string str13 in str12.Split(chArray1))
                        {
                            string item = str13;
                            if (item.Count<char>() > 0)
                            {
                                tbl_brief_question_metadata questionMetadata7 = this.db.tbl_brief_question_metadata.Where<tbl_brief_question_metadata>((Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)insertedId && t.brief_question_metadata.ToLower().Trim() == item.ToLower())).FirstOrDefault<tbl_brief_question_metadata>();
                                if (questionMetadata7 != null)
                                {
                                    questionMetadata7.status = "A";
                                    this.db.SaveChanges();
                                }
                                else
                                {
                                    string str14 = item.Trim();
                                    this.db.tbl_brief_question_metadata.Add(new tbl_brief_question_metadata()
                                    {
                                        id_brief_question = new int?(insertedId),
                                        brief_question_metadata = str14,
                                        status = "A",
                                        updated_date_time = new DateTime?(DateTime.Now)
                                    });
                                    this.db.SaveChanges();
                                }
                            }
                        }
                        DbSet<tbl_brief_question_metadata> questionMetadata8 = this.db.tbl_brief_question_metadata;
                        Expression<Func<tbl_brief_question_metadata, bool>> predicate4 = (Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)insertedId && t.status == "T");
                        using (List<tbl_brief_question_metadata>.Enumerator enumerator = questionMetadata8.Where<tbl_brief_question_metadata>(predicate4).ToList<tbl_brief_question_metadata>().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                this.db.tbl_brief_question_metadata.Remove(enumerator.Current);
                                this.db.SaveChanges();
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("questionDashboard", "Brief");
        }

        public ActionResult question_episode_mapping()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
            List<tbl_brief_category> tblBriefCategoryList = new List<tbl_brief_category>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                tblBriefMasterList = m2ostDbContext.Database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where id_organization={0} and status='A'", (object)int32).ToList<tbl_brief_master>();
                tblBriefCategoryList = m2ostDbContext.Database.SqlQuery<tbl_brief_category>("select * from tbl_brief_category where status='A' and id_organization={0}", (object)int32).ToList<tbl_brief_category>();
            }
            this.ViewData["briefMas"] = (object)tblBriefMasterList;
            this.ViewData["briefCat"] = (object)tblBriefCategoryList;
            return (ActionResult)this.View();
        }

        public string getSubcategory(int categ)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            List<tbl_brief_subcategory> briefSubcategoryList = new List<tbl_brief_subcategory>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                briefSubcategoryList = m2ostDbContext.Database.SqlQuery<tbl_brief_subcategory>("select * from tbl_brief_subcategory where status='A' and id_organization={0} and id_brief_category={1}", (object)int32, (object)categ).ToList<tbl_brief_subcategory>();
            string subcategory = "";
            if (briefSubcategoryList.Count > 0)
            {
                subcategory += "<option value=\"\">select</option>";
                foreach (tbl_brief_subcategory briefSubcategory in briefSubcategoryList)
                    subcategory = subcategory + "<option value=\"" + (object)briefSubcategory.id_brief_subcategory + "\">" + briefSubcategory.brief_subcategory + "</option>";
            }
            return subcategory;
        }

        public string getBriefQuestionList(int categ, int subCat, int briefId)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblBriefQuestionList = m2ostDbContext.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where status='A' and id_organization={0} and id_brief_category={1} and id_brief_sub_category={2}", (object)int32, (object)categ, (object)subCat).ToList<tbl_brief_question>();
            string briefQuestionList = "";
            if (tblBriefQuestionList.Count > 0)
            {
                foreach (tbl_brief_question tblBriefQuestion in tblBriefQuestionList)
                {
                    int num = 0;
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        num = m2ostDbContext.Database.SqlQuery<int>("select id_mapping from tbl_question_episode_mapping where id_brief={0} and id_question={1} and id_org={2}", (object)briefId, (object)tblBriefQuestion.id_brief_question, (object)int32).FirstOrDefault<int>();
                    if (num == 0)
                        briefQuestionList = briefQuestionList + "<tr><td>" + tblBriefQuestion.brief_question + "</td><td style=\"text-align:center;\" ><i class=\"fa fa-plus\" id=\"questionid" + (object)tblBriefQuestion.id_brief_question + "\" onclick=\"questionAdd(" + (object)tblBriefQuestion.id_brief_question + ")\" aria-hidden=\"true\" style=\"color:royalblue;font-size: 25px;\"></i></td></tr>";
                    else
                        briefQuestionList = briefQuestionList + "<tr><td>" + tblBriefQuestion.brief_question + "</td><td style=\"text-align:center;\"><i class=\"fa fa-check\" id=\"questionTick" + (object)tblBriefQuestion.id_brief_question + "\" aria-hidden=\"true\" style=\"color:green;font-size: 25px;\"></i></td></tr>";
                }
            }
            return briefQuestionList;
        }

        public bool postEpisodeMapping(int categ, int subCat, int briefId, int QueId)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            string str = "A";
            DateTime now = DateTime.Now;
            int num = 0;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                num = m2ostDbContext.Database.SqlQuery<int>("INSERT INTO `tbl_question_episode_mapping` (`id_brief`,`id_question`,`id_org`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4});select LAST_INSERT_ID();", (object)briefId, (object)QueId, (object)int32, (object)str, (object)now).FirstOrDefault<int>();
            bool flag = false;
            if (num > 0)
                flag = true;
            return flag;
        }

        public ActionResult editCategoryTile(int cit)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            tbl_brief_category_tile briefCategoryTile = new tbl_brief_category_tile();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                briefCategoryTile = m2ostDbContext.Database.SqlQuery<tbl_brief_category_tile>("select * from tbl_brief_category_tile where id_brief_category_tile ={0}", (object)cit).FirstOrDefault<tbl_brief_category_tile>();
            this.ViewData["tbct"] = (object)briefCategoryTile;
            return (ActionResult)this.View();
        }

        public ActionResult editBriefCategoryTile()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            string str = "BRCT" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
            tbl_brief_category_tile briefCategoryTile = new tbl_brief_category_tile();
            briefCategoryTile.id_brief_category_tile = Convert.ToInt32(this.Request.Form["id_brief_ctile"].ToString());
            briefCategoryTile.id_organization = new int?(int32);
            briefCategoryTile.category_tile = this.Request.Form["category-tile"].ToString();
            briefCategoryTile.tile_description = this.Request.Form["tile-description"].ToString();
            briefCategoryTile.category_tile_type = new int?(Convert.ToInt32(this.Request.Form["select-tile-type"].ToString()));
            briefCategoryTile.tile_position = new int?(Convert.ToInt32(this.Request.Form["category-order"].ToString()));
            briefCategoryTile.attempt_limit = new int?(Convert.ToInt32(this.Request.Form["category-limit"].ToString()));
            briefCategoryTile.assessment_complation = new int?(Convert.ToInt32(this.Request.Form["select-tile-completion"].ToString()));
            briefCategoryTile.status = "A";
            briefCategoryTile.updated_date_time = new DateTime?(DateTime.Now);
            briefCategoryTile.tile_type = new int?(Convert.ToInt32(this.Request.Form["non-learn-type"].ToString()));
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_brief_category_tile` SET `id_organization` = {0},`category_tile` = {1},`tile_description` = {2},`category_tile_type` = {3},`tile_position` = {4},`assessment_complation` = {5},`attempt_limit` = {6},`status` = {7},`updated_date_time` = {8},`tile_type`={10} WHERE `id_brief_category_tile` = {9}", (object)briefCategoryTile.id_organization, (object)briefCategoryTile.category_tile, (object)briefCategoryTile.tile_description, (object)briefCategoryTile.category_tile_type, (object)briefCategoryTile.tile_position, (object)briefCategoryTile.assessment_complation, (object)briefCategoryTile.attempt_limit, (object)briefCategoryTile.status, (object)briefCategoryTile.updated_date_time, (object)briefCategoryTile.id_brief_category_tile, (object)briefCategoryTile.tile_type);
            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["uploadBtn"];
                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["uploadBtn"].FileName);
                string path2 = str + extension;
                if (file != null && file.ContentLength > 0)
                {
                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)int32 + "/TILE/")))
                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)int32 + "/TILE/"));
                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)int32 + "/TILE/"), path2);
                    file.SaveAs(filename);
                    briefCategoryTile.tile_image = path2;
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_brief_category_tile` SET `tile_image` ={0}  WHERE `id_brief_category_tile` ={1}", (object)briefCategoryTile.tile_image, (object)briefCategoryTile.id_brief_category_tile);
                }
            }
            return (ActionResult)this.RedirectToAction("Tiles");
        }

        public ActionResult jobcategoryTile_dashboard()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_job_category> tblJobCategoryList = new List<tbl_job_category>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblJobCategoryList = m2ostDbContext.Database.SqlQuery<tbl_job_category>("select * from tbl_job_category where status='A'").ToList<tbl_job_category>();
            this.ViewData["tjc"] = (object)tblJobCategoryList;
            return (ActionResult)this.View();
        }

        public ActionResult create_job_category()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            return (ActionResult)this.View();
        }

        public ActionResult create_job_category_tile()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_job_category tblJobCategory = new tbl_job_category();
            tblJobCategory.job_category = this.Request.Form["job-Cat"].ToString();
            tblJobCategory.status = "A";
            tblJobCategory.updated_date_time = DateTime.Now;
            tblJobCategory.id_header = 1;
            int num = 0;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                num = m2ostDbContext.Database.SqlQuery<int>("INSERT INTO `tbl_job_category` (`job_category`,`status`,`updated_date_time`,`id_header`) VALUES ({0},{1},{2},{3});select last_insert_id();", (object)tblJobCategory.job_category, (object)tblJobCategory.status, (object)tblJobCategory.updated_date_time, (object)tblJobCategory.id_header).FirstOrDefault<int>();
            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                string str = System.Web.HttpContext.Current.Request["id"];
                if (file != null && file.ContentLength > 0)
                {
                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile/")))
                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile/"));
                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile"), num.ToString() + "jobCat" + extension);
                    file.SaveAs(filename);
                    tblJobCategory.tile_image = num.ToString() + "jobCat" + extension;
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_job_category` SET `tile_image` ={0} WHERE `id_job_category` ={1}", (object)tblJobCategory.tile_image, (object)num);
                }
            }
            return (ActionResult)this.RedirectToAction("jobcategoryTile_dashboard");
        }

        public ActionResult edit_job_category(int idJ)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_job_category tblJobCategory = new tbl_job_category();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                tblJobCategory = m2ostDbContext.Database.SqlQuery<tbl_job_category>("select * from tbl_job_category where status='A' and id_job_category={0}", (object)idJ).FirstOrDefault<tbl_job_category>();
            this.ViewData["tjc"] = (object)tblJobCategory;
            return (ActionResult)this.View();
        }

        public ActionResult edit_action_job_category()
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_job_category tblJobCategory = new tbl_job_category();
            tblJobCategory.id_job_category = Convert.ToInt32(this.Request.Form["idJC"].ToString());
            tblJobCategory.job_category = this.Request.Form["job-Cat"].ToString();
            tblJobCategory.updated_date_time = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_job_category` SET `job_category` ={0},`updated_date_time` ={1} WHERE `id_job_category` ={2}", (object)tblJobCategory.job_category, (object)tblJobCategory.updated_date_time, (object)tblJobCategory.id_job_category);
            int idJobCategory = tblJobCategory.id_job_category;
            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                string str = System.Web.HttpContext.Current.Request["id"];
                if (file != null && file.ContentLength > 0)
                {
                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile/")))
                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile/"));
                    System.IO.File.Delete(this.Request.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile/" + (object)idJobCategory + "jobCat" + extension));
                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/JobCategoryTile"), idJobCategory.ToString() + "jobCat" + extension);
                    file.SaveAs(filename);
                    tblJobCategory.tile_image = idJobCategory.ToString() + "jobCat" + extension;
                    using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_job_category` SET `tile_image` ={0} WHERE `id_job_category` ={1}", (object)tblJobCategory.tile_image, (object)tblJobCategory.id_job_category);
                }
            }
            return (ActionResult)this.RedirectToAction("jobcategoryTile_dashboard");
        }

        public ActionResult delete_job_category(int idJ)
        {
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                m2ostDbContext.Database.ExecuteSqlCommand("update tbl_job_category set status='X' where id_job_category={0}", (object)idJ);
            return (ActionResult)this.RedirectToAction("jobcategoryTile_dashboard");
        }

        public string randomCodeContent()
        {
            int count = 8;
            Random random = new Random();
            return new string(Enumerable.Repeat<string>("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", count).Select<string, char>((Func<string, char>)(s => s[random.Next(s.Length)])).ToArray<char>());
        }

        public ActionResult Content()
        {
            this.ViewData["category"] = (object)this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION) + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            return (ActionResult)this.View();
        }

        public ActionResult briefDraftContent()
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            List<tbl_brief_question_complexity> list3 = this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity where status='A' ").ToList<tbl_brief_question_complexity>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["subcategory"] = (object)list2;
            this.ViewData["complexity"] = (object)list3;
            return (ActionResult)this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult finishDraftBriefContent()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            string str1 = "BRF-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + this.randomCode();
            tbl_brief_master entity1 = new tbl_brief_master();
            entity1.brief_title = this.Request.Form["brief-title"].ToString();
            entity1.brief_code = str1;
            entity1.brief_description = "";
            entity1.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
            entity1.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
            DateTime now1 = DateTime.Now;
            entity1.override_dnd = new int?(0);
            entity1.is_add_question = new int?(1);
            entity1.brief_attachment_flag = new int?(1);
            DateTime datetime = new Utility().StringToDatetime("2030-02-21 11:23:00");
            entity1.scheduled_timestamp = new DateTime?(datetime);
            entity1.id_organization = new int?(int32_1);
            int num1 = 0;
            entity1.question_count = new int?(num1);
            int num2 = 0;
            entity1.brief_type = new int?(num2);
            entity1.status = "A";
            entity1.updated_date_time = new DateTime?(DateTime.Now);
            this.db.tbl_brief_master.Add(entity1);
            this.db.SaveChanges();
            int idBriefMaster = entity1.id_brief_master;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_brief_master` SET `id_user` = {0},`brief_attachment_flag`={2},`brief_attachement_url`={3} WHERE `id_brief_master` ={1}", (object)int32_2, (object)idBriefMaster, (object)entity1.brief_attachment_flag, (object)entity1.brief_attachement_url);
                m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'C',{3})", (object)idBriefMaster, (object)int32_1, (object)int32_2, (object)DateTime.Now);
            }
            if (entity1.id_brief_master > 0)
            {
                DateTime now2 = DateTime.Now;
                string str2 = now2.ToString("yyyy");
                now2 = DateTime.Now;
                string str3 = now2.ToString("MM");
                string str4 = entity1.brief_code.Replace("-", "");
                this.db.tbl_brief_master_template.Add(new tbl_brief_master_template()
                {
                    id_brief_master = new int?(entity1.id_brief_master),
                    brief_code = entity1.brief_code,
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    brief_destination = str4
                });
                this.db.SaveChanges();
                string str5 = "";
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/")))
                {
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/"));
                    str5 = "/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/";
                }
                this.Request.Unvalidated.Form["brf-rt-code"].ToString();
                tbl_brief_master_body entity2 = new tbl_brief_master_body();
                entity2.id_brief_master = new int?(entity1.id_brief_master);
                entity2.brief_code = entity1.brief_code;
                entity2.brief_destination = str5;
                entity2.media_type = new int?(1);
                entity2.resouce_code = "NA";
                entity2.resource_mime = "NA";
                entity2.resource_number = "NA";
                entity2.resource_type = new int?(1);
                entity2.srno = new int?(1);
                entity2.resouce_data = this.Request.Form["brief-rt-data-t3"].ToString();
                entity2.status = "A";
                entity2.updated_date_time = new DateTime?(DateTime.Now);
                entity2.file_extension = "NA";
                entity2.file_type = "NA";
                this.db.tbl_brief_master_body.Add(entity2);
                this.db.SaveChanges();
                tbl_brief_master_body entity3 = new tbl_brief_master_body();
                entity3.id_brief_master = new int?(entity1.id_brief_master);
                entity3.brief_code = entity1.brief_code;
                entity3.brief_destination = str5;
                entity3.status = "A";
                entity3.updated_date_time = new DateTime?(DateTime.Now);
                if (Convert.ToInt32(this.Request.Form["t2-select-order"].ToString()) == 2)
                {
                    entity3.media_type = new int?(2);
                    entity3.resouce_code = "NA";
                    entity3.resource_mime = "NA";
                    entity3.resource_number = "NA";
                    entity3.resource_type = new int?(2);
                    entity3.srno = new int?(2);
                    entity3.resouce_data = this.Request.Form["t2-btn-url"].ToString();
                    entity2.file_extension = "NA";
                    entity2.file_type = "NA";
                    this.db.tbl_brief_master_body.Add(entity3);
                    this.db.SaveChanges();
                }
                else
                {
                    string str6 = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                    entity3.media_type = new int?(1);
                    entity3.resouce_code = str6;
                    entity3.resource_mime = "NA";
                    entity3.resource_number = "NA";
                    entity3.resource_type = new int?(2);
                    entity3.srno = new int?(2);
                    if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                    {
                        HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                        string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["t2-btn"].FileName);
                        string path2 = str6 + extension;
                        if (file != null && file.ContentLength > 0)
                        {
                            string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)entity1.id_organization + "/" + str2 + "/" + str3 + "/" + str4 + "/"), path2);
                            file.SaveAs(filename);
                        }
                        entity3.resouce_data = path2;
                        entity3.file_extension = extension;
                        entity3.file_type = new BriefModel().getExtensionNumber(extension);
                        this.db.tbl_brief_master_body.Add(entity3);
                        this.db.SaveChanges();
                    }
                }
                if (num2 == 2)
                {
                    string str7 = this.Request.Form["brf-cat-val"].ToString().Trim(' ', ',');
                    char[] chArray = new char[1] { ',' };
                    foreach (string str8 in str7.Split(chArray))
                    {
                        this.db.tbl_brief_category_mapping.Add(new tbl_brief_category_mapping()
                        {
                            id_brief_category = new int?(Convert.ToInt32(str8)),
                            id_brief_master = new int?(entity1.id_brief_master),
                            id_organization = new int?(int32_1),
                            status = "A",
                            updated_date_time = new DateTime?(DateTime.Now)
                        });
                        this.db.SaveChanges();
                    }
                }
                string str9 = this.Request.Form["brief-metadata"].ToString();
                char[] chArray1 = new char[1] { ',' };
                foreach (string str10 in str9.Split(chArray1))
                {
                    string str11 = str10.Trim();
                    this.db.tbl_brief_metadata.Add(new tbl_brief_metadata()
                    {
                        id_brief_master = new int?(entity1.id_brief_master),
                        brief_metadata = str11,
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(entity1.id_brief_master),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(1),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Draft"
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("BriefContentDetail", "Brief", (object)new
            {
                brf = entity1.brief_code
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult finishModifyBriefCont()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            int bid = Convert.ToInt32(this.Request.Form["id_brief_master"].ToString());
            tbl_brief_master briefmaster = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.id_brief_master == bid)).FirstOrDefault<tbl_brief_master>();
            int num1 = 0;
            if (briefmaster != null)
            {
                briefmaster.brief_title = this.Request.Form["brief-title"].ToString();
                briefmaster.brief_description = "";
                briefmaster.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                briefmaster.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                DateTime now = DateTime.Now;
                briefmaster.override_dnd = new int?(0);
                briefmaster.is_add_question = new int?(1);
                briefmaster.brief_attachment_flag = new int?(1);
                DateTime datetime = new Utility().StringToDatetime("2030-02-21 11:23:00");
                briefmaster.scheduled_timestamp = new DateTime?(datetime);
                int num2 = 0;
                briefmaster.question_count = new int?(num2);
                briefmaster.id_organization = new int?(int32_1);
                num1 = 0;
                briefmaster.brief_type = new int?(num1);
                briefmaster.status = "A";
                briefmaster.updated_date_time = new DateTime?(DateTime.Now);
                this.db.SaveChanges();
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                {
                    m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_brief_master` SET `id_user` = {0},`brief_attachment_flag`={2},`brief_attachement_url`={3} WHERE `id_brief_master` ={1}", (object)int32_2, (object)bid, (object)briefmaster.brief_attachment_flag, (object)briefmaster.brief_attachement_url);
                    m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'E',{3})", (object)bid, (object)int32_1, (object)int32_2, (object)DateTime.Now);
                }
                tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
                if (briefMasterTemplate != null)
                {
                    briefMasterTemplate.status = "A";
                    briefMasterTemplate.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                    if (true)
                    {
                        tbl_brief_master_body tblBriefMasterBody = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.resource_type == (int?)2)).FirstOrDefault<tbl_brief_master_body>();
                        tblBriefMasterBody.status = "A";
                        tblBriefMasterBody.updated_date_time = new DateTime?(DateTime.Now);
                        if (Convert.ToInt32(this.Request.Form["t2-select-order"].ToString()) == 2)
                        {
                            tblBriefMasterBody.media_type = new int?(2);
                            tblBriefMasterBody.resouce_code = "NA";
                            tblBriefMasterBody.resource_mime = "NA";
                            tblBriefMasterBody.resource_number = "NA";
                            tblBriefMasterBody.resource_type = new int?(2);
                            tblBriefMasterBody.srno = new int?(1);
                            tblBriefMasterBody.resouce_data = this.Request.Form["t2-btn-url"].ToString();
                            tblBriefMasterBody.file_extension = "NA";
                            tblBriefMasterBody.file_type = "NA";
                            this.db.SaveChanges();
                        }
                        else
                        {
                            string str = "BRFR" + DateTime.Now.ToString("yyMMddHHmmss") + this.randomCode();
                            tblBriefMasterBody.media_type = new int?(1);
                            tblBriefMasterBody.resouce_code = str;
                            tblBriefMasterBody.resource_mime = "NA";
                            tblBriefMasterBody.resource_number = "NA";
                            tblBriefMasterBody.resource_type = new int?(2);
                            tblBriefMasterBody.srno = new int?(1);
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file1 = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                                HttpPostedFile file2 = System.Web.HttpContext.Current.Request.Files["t2-btn"];
                                if (file1 != null && file1.ContentLength > 0)
                                {
                                    string extension = Path.GetExtension(file2.FileName);
                                    string path2 = str + extension;
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~" + tblBriefMasterBody.brief_destination.Replace("\\", "/") ?? ""), path2);
                                    file1.SaveAs(filename);
                                    tblBriefMasterBody.resouce_data = path2;
                                    tblBriefMasterBody.file_extension = extension;
                                    tblBriefMasterBody.file_type = new BriefModel().getExtensionNumber(extension);
                                    this.db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            if (briefmaster.id_brief_master > 0)
            {
                DateTime now = DateTime.Now;
                now.ToString("yyyy");
                now = DateTime.Now;
                now.ToString("MM");
                briefmaster.brief_code.Replace("-", "");
                if (num1 == 2)
                {
                    DbSet<tbl_brief_category_mapping> briefCategoryMapping1 = this.db.tbl_brief_category_mapping;
                    Expression<Func<tbl_brief_category_mapping, bool>> predicate1 = (Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master);
                    foreach (tbl_brief_category_mapping briefCategoryMapping2 in briefCategoryMapping1.Where<tbl_brief_category_mapping>(predicate1).ToList<tbl_brief_category_mapping>())
                    {
                        briefCategoryMapping2.status = "T";
                        this.db.SaveChanges();
                    }
                    string str1 = this.Request.Form["brf-cat-val"].ToString().Trim(' ', ',');
                    char[] chArray = new char[1] { ',' };
                    foreach (string str2 in str1.Split(chArray))
                    {
                        string row = str2;
                        if (row.Count<char>() > 0)
                        {
                            tbl_brief_category_mapping briefCategoryMapping3 = this.db.tbl_brief_category_mapping.Where<tbl_brief_category_mapping>((Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.id_brief_category.ToString() == row)).FirstOrDefault<tbl_brief_category_mapping>();
                            if (briefCategoryMapping3 != null)
                            {
                                briefCategoryMapping3.status = "A";
                                this.db.SaveChanges();
                            }
                            else
                            {
                                this.db.tbl_brief_category_mapping.Add(new tbl_brief_category_mapping()
                                {
                                    id_brief_category = new int?(Convert.ToInt32(row)),
                                    id_brief_master = new int?(briefmaster.id_brief_master),
                                    id_organization = new int?(int32_1),
                                    status = "A",
                                    updated_date_time = new DateTime?(DateTime.Now)
                                });
                                this.db.SaveChanges();
                            }
                        }
                    }
                    DbSet<tbl_brief_category_mapping> briefCategoryMapping4 = this.db.tbl_brief_category_mapping;
                    Expression<Func<tbl_brief_category_mapping, bool>> predicate2 = (Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.status == "T");
                    foreach (tbl_brief_category_mapping entity in briefCategoryMapping4.Where<tbl_brief_category_mapping>(predicate2).ToList<tbl_brief_category_mapping>())
                    {
                        this.db.tbl_brief_category_mapping.Remove(entity);
                        this.db.SaveChanges();
                    }
                }
                DbSet<tbl_brief_metadata> tblBriefMetadata1 = this.db.tbl_brief_metadata;
                Expression<Func<tbl_brief_metadata, bool>> predicate3 = (Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master);
                foreach (tbl_brief_metadata tblBriefMetadata2 in tblBriefMetadata1.Where<tbl_brief_metadata>(predicate3).ToList<tbl_brief_metadata>())
                {
                    tblBriefMetadata2.status = "T";
                    this.db.SaveChanges();
                }
                string str3 = this.Request.Form["brief-metadata"].ToString();
                char[] chArray1 = new char[1] { ',' };
                foreach (string str4 in str3.Split(chArray1))
                {
                    string item = str4;
                    if (item.Count<char>() > 0)
                    {
                        tbl_brief_metadata tblBriefMetadata3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.brief_metadata.ToLower().Trim() == item.ToLower())).FirstOrDefault<tbl_brief_metadata>();
                        if (tblBriefMetadata3 != null)
                        {
                            tblBriefMetadata3.status = "A";
                            this.db.SaveChanges();
                        }
                        else
                        {
                            string str5 = item.Trim();
                            this.db.tbl_brief_metadata.Add(new tbl_brief_metadata()
                            {
                                id_brief_master = new int?(briefmaster.id_brief_master),
                                brief_metadata = str5,
                                status = "A",
                                updated_date_time = new DateTime?(DateTime.Now)
                            });
                            this.db.SaveChanges();
                        }
                    }
                }
                DbSet<tbl_brief_metadata> tblBriefMetadata4 = this.db.tbl_brief_metadata;
                Expression<Func<tbl_brief_metadata, bool>> predicate4 = (Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.status == "T");
                foreach (tbl_brief_metadata entity in tblBriefMetadata4.Where<tbl_brief_metadata>(predicate4).ToList<tbl_brief_metadata>())
                {
                    this.db.tbl_brief_metadata.Remove(entity);
                    this.db.SaveChanges();
                }
                string[] strArray = this.Request.Form["rem-brf-cat-val"].ToString().Split(',');
                List<int> intList = new List<int>();
                for (int index = 1; index <= 10; ++index)
                {
                    if (this.Request.Form["row-qtn-list-" + (object)index] != null)
                    {
                        int int32_3 = Convert.ToInt32(this.Request.Form["row-qtn-list-" + (object)index]);
                        intList.Add(int32_3);
                    }
                }
                foreach (int num3 in intList)
                {
                    int item = num3;
                    if (this.db.tbl_brief_question_mapping.Where<tbl_brief_question_mapping>((Expression<Func<tbl_brief_question_mapping, bool>>)(t => t.id_brief_master == (int?)briefmaster.id_brief_master && t.id_brief_question == (int?)item && t.status == "A")).FirstOrDefault<tbl_brief_question_mapping>() == null)
                    {
                        this.db.tbl_brief_question_mapping.Add(new tbl_brief_question_mapping()
                        {
                            id_brief_master = new int?(briefmaster.id_brief_master),
                            id_brief_question = new int?(item),
                            question_link_type = new int?(1),
                            date_time_stamp = new DateTime?(DateTime.Now),
                            status = "A",
                            updated_date_time = new DateTime?(DateTime.Now)
                        });
                        this.db.SaveChanges();
                    }
                }
                foreach (string str6 in strArray)
                {
                    string rmi = str6;
                    if (rmi.Count<char>() > 0)
                    {
                        tbl_brief_question_mapping briefQuestionMapping = this.db.tbl_brief_question_mapping.Where<tbl_brief_question_mapping>((Expression<Func<tbl_brief_question_mapping, bool>>)(t => t.id_brief_question.ToString() == rmi && t.id_brief_master == (int?)briefmaster.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_question_mapping>();
                        if (briefQuestionMapping != null)
                        {
                            briefQuestionMapping.status = "R";
                            briefQuestionMapping.updated_date_time = new DateTime?(DateTime.Now);
                            this.db.SaveChanges();
                        }
                    }
                }
            }
            return (ActionResult)this.RedirectToAction("dashboardContent", "Brief");
        }

        public ActionResult modifyBriefCont(string brf)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = new tbl_brief_master();
            int num1 = 0;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                num1 = m2ostDbContext.Database.SqlQuery<int>("select (case when episode_sequence is null then 0 else episode_sequence end) as episode from tbl_brief_master where brief_code={0}", (object)brf).FirstOrDefault<int>();
                brief = m2ostDbContext.Database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where status='A' and brief_code={0}", (object)brf).FirstOrDefault<tbl_brief_master>();
            }
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboardContent", "Brief");
            tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
            string str1 = brief.updated_date_time.Value.ToString("yyyy");
            string str2 = brief.updated_date_time.Value.ToString("MM");
            string str3 = brief.brief_code.Replace("-", "");
            if (briefMasterTemplate == null)
            {
                tbl_brief_master_template entity = new tbl_brief_master_template();
                entity.id_brief_master = new int?(brief.id_brief_master);
                entity.brief_code = brief.brief_code;
                string str4 = "NA";
                entity.resource_order = str4;
                entity.brief_template = "1";
                entity.status = "A";
                entity.updated_date_time = new DateTime?(DateTime.Now);
                entity.brief_destination = str3;
                this.db.tbl_brief_master_template.Add(entity);
                this.db.SaveChanges();
            }
            List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
            if (list1.Count<tbl_brief_master_body>() == 0)
            {
                string str5 = "";
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/")))
                {
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/"));
                    str5 = "/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/";
                }
                this.db.tbl_brief_master_body.Add(new tbl_brief_master_body()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    brief_code = brief.brief_code,
                    brief_destination = str5,
                    brief_template = "1",
                    media_type = new int?(1),
                    resouce_code = "NA",
                    resource_mime = "NA",
                    resource_number = "NA",
                    resource_type = new int?(1),
                    srno = new int?(1),
                    resouce_data = brief.brief_description,
                    status = "A",
                    file_extension = "NA",
                    file_type = "NA",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            brief.brief_description = WebUtility.HtmlDecode(brief.brief_description);
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + " and status='A') and status='A'").ToList<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            List<tbl_brief_metadata> list3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
            bool flag = false;
            if (tblBriefStatus != null)
            {
                int? statusCode = tblBriefStatus.status_code;
                int num2 = 1;
                if (statusCode.GetValueOrDefault() == num2 & statusCode.HasValue)
                    flag = true;
            }
            List<tbl_brief_category> list4 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list5 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["briefmaster"] = (object)brief;
            this.ViewData["bTemplate"] = (object)briefMasterTemplate;
            this.ViewData["mBody"] = (object)list1;
            this.ViewData["questions"] = (object)list2;
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["metadata"] = (object)list3;
            this.ViewData["accessFlag"] = (object)flag;
            this.ViewData["sequanceVal"] = (object)num1;
            this.ViewData["ListCategory"] = (object)list4;
            this.ViewData["ListSubcategory"] = (object)list5;
            return (ActionResult)this.View();
        }

        public ActionResult dashboardContent()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            List<briefView> briefView1 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE a.id_user=" + (object)int32_2 + " AND a.id_organization=" + (object)int32_1 + " and  a.status = 'A' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master and d.status_code < 7 order by a.id_brief_master desc");
            List<briefView> briefView2 = new BriefModel().getBriefView("SELECT a.id_brief_master, a.brief_title, a.brief_type, a.brief_code, a.question_count, a.updated_date_time scheduled_timestamp, b.brief_category, c.brief_subcategory, d.brief_status, d.status_code FROM tbl_brief_master a, tbl_brief_category b, tbl_brief_subcategory c, tbl_brief_status d  " + "  WHERE  a.id_user=" + (object)int32_2 + " AND a.id_organization=" + (object)int32_1 + " and  a.status = 'X' AND d.status = 'A' AND a.id_brief_category = b.id_brief_category AND a.id_brief_sub_category = c.id_brief_subcategory AND a.id_brief_master = d.id_brief_master and d.status_code=7 order by a.id_brief_master desc");
            this.ViewData["briefmaster"] = (object)briefView1;
            this.ViewData["deletedbriefs"] = (object)briefView2;
            return (ActionResult)this.View();
        }

        public ActionResult BriefContentDetail(string brf)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboardContent", "Brief");
            tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
            string str1 = brief.updated_date_time.Value.ToString("yyyy");
            string str2 = brief.updated_date_time.Value.ToString("MM");
            string str3 = brief.brief_code.Replace("-", "");
            if (briefMasterTemplate == null)
            {
                tbl_brief_master_template entity = new tbl_brief_master_template();
                entity.id_brief_master = new int?(brief.id_brief_master);
                entity.brief_code = brief.brief_code;
                string str4 = "NA";
                entity.resource_order = str4;
                entity.brief_template = "1";
                entity.status = "A";
                entity.updated_date_time = new DateTime?(DateTime.Now);
                entity.brief_destination = str3;
                this.db.tbl_brief_master_template.Add(entity);
                this.db.SaveChanges();
            }
            List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
            if (list1.Count<tbl_brief_master_body>() == 0)
            {
                string str5 = "";
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/")))
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/"));
                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/")))
                {
                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/"));
                    str5 = "/Content/SKILLMUNI_DATA/BRIEF/" + (object)brief.id_organization + "/" + str1 + "/" + str2 + "/" + str3 + "/";
                }
                this.db.tbl_brief_master_body.Add(new tbl_brief_master_body()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    brief_code = brief.brief_code,
                    brief_destination = str5,
                    brief_template = "1",
                    media_type = new int?(1),
                    resouce_code = "NA",
                    resource_mime = "NA",
                    resource_number = "NA",
                    resource_type = new int?(1),
                    srno = new int?(1),
                    resouce_data = brief.brief_description,
                    status = "A",
                    file_extension = "NA",
                    file_type = "NA",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            if (list1.Count<tbl_brief_master_body>() > 0)
            {
                foreach (tbl_brief_master_body tblBriefMasterBody in list1)
                {
                    int? mediaType = tblBriefMasterBody.media_type;
                    int num = 2;
                    if (mediaType.GetValueOrDefault() == num & mediaType.HasValue)
                    {
                        string resouceData = tblBriefMasterBody.resouce_data;
                        resouceData.Substring(32, resouceData.Length - 32);
                        tblBriefMasterBody.resouce_data = tblBriefMasterBody.resouce_data.Replace("watch?v=", "embed/");
                    }
                }
            }
            brief.brief_description = WebUtility.HtmlDecode(brief.brief_description);
            string sql = "SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + "  and status='A')  and status='A'";
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery(sql).ToList<tbl_brief_question>();
            tbl_brief_question tblBriefQuestion = this.db.tbl_brief_question.SqlQuery(sql).FirstOrDefault<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            List<tbl_brief_metadata> list3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
            bool flag = false;
            if (tblBriefStatus != null)
            {
                int? statusCode = tblBriefStatus.status_code;
                int num = 1;
                if (statusCode.GetValueOrDefault() == num & statusCode.HasValue)
                    flag = true;
            }
            this.ViewData["briefmaster"] = (object)brief;
            this.ViewData["bTemplate"] = (object)briefMasterTemplate;
            this.ViewData["mBody"] = (object)list1;
            this.ViewData["questions"] = (object)list2;
            this.ViewData["list"] = (object)tblBriefQuestion;
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["metadata"] = (object)list3;
            this.ViewData["accessFlag"] = (object)flag;
            return (ActionResult)this.View();
        }

        public ActionResult finishQuestionContentAction()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            Convert.ToInt32(content.ID_USER);
            int bid = Convert.ToInt32(this.Request.Form["id_brief_master"].ToString());
            tbl_brief_master tblBriefMaster = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.id_brief_master == bid)).FirstOrDefault<tbl_brief_master>();
            tbl_brief_question qtn = new tbl_brief_question();
            if (tblBriefMaster != null)
            {
                int num1 = 0;
                int int32_2 = Convert.ToInt32((object)tblBriefMaster.question_count);
                if (int32_2 == 0)
                    int32_2 = Convert.ToInt32(this.Request.Form["question-count"].ToString());
                int num2 = 0;
                switch (Convert.ToInt32(this.Request.Form["themtyp"].ToString()))
                {
                    case 1:
                        qtn.id_organization = new int?(int32_1);
                        qtn.id_brief_category = tblBriefMaster.id_brief_category;
                        qtn.id_brief_sub_category = tblBriefMaster.id_brief_sub_category;
                        qtn.brief_question = this.Request.Form["questionText"].ToString();
                        qtn.question_complexity = new int?(1);
                        qtn.question_theme_type = new int?(2);
                        qtn.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                        qtn.question_type = 1;
                        DateTime now1 = DateTime.Now;
                        qtn.expiry_date = new DateTime?(now1);
                        qtn.status = "A";
                        qtn.updated_date_time = new DateTime?(DateTime.Now);
                        num1 = new BriefLogic().InsertQuestion(qtn);
                        break;
                    case 2:
                        qtn.id_organization = new int?(int32_1);
                        qtn.id_brief_category = tblBriefMaster.id_brief_category;
                        qtn.id_brief_sub_category = tblBriefMaster.id_brief_sub_category;
                        qtn.question_complexity = new int?(1);
                        qtn.question_theme_type = new int?(2);
                        qtn.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                        qtn.question_type = 1;
                        DateTime now2 = DateTime.Now;
                        qtn.expiry_date = new DateTime?(now2);
                        qtn.status = "A";
                        qtn.updated_date_time = new DateTime?(DateTime.Now);
                        num1 = new BriefLogic().InsertQuestionImage(qtn);
                        tbl_brief_question tblBriefQuestion1 = new tbl_brief_question();
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                            string str = System.Web.HttpContext.Current.Request["id"];
                            if (file != null && file.ContentLength > 0)
                            {
                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), num1.ToString() + "BriefQ" + extension);
                                file.SaveAs(filename);
                                tblBriefQuestion1.id_brief_question = num1;
                                tblBriefQuestion1.question_image = num1.ToString() + "BriefQ" + extension;
                                new BriefLogic().updateQuestion(tblBriefQuestion1);
                                break;
                            }
                            break;
                        }
                        break;
                    case 3:
                        qtn.id_organization = new int?(int32_1);
                        qtn.id_brief_category = tblBriefMaster.id_brief_category;
                        qtn.id_brief_sub_category = tblBriefMaster.id_brief_sub_category;
                        qtn.brief_question = this.Request.Form["questionText"].ToString();
                        qtn.question_complexity = new int?(1);
                        qtn.question_theme_type = new int?(2);
                        qtn.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                        qtn.question_type = 1;
                        DateTime now3 = DateTime.Now;
                        qtn.expiry_date = new DateTime?(now3);
                        qtn.status = "A";
                        qtn.updated_date_time = new DateTime?(DateTime.Now);
                        num1 = new BriefLogic().InsertQuestion(qtn);
                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                        {
                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                            string str = System.Web.HttpContext.Current.Request["id"];
                            if (file != null && file.ContentLength > 0)
                            {
                                tbl_brief_question tblBriefQuestion2 = new tbl_brief_question();
                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), num1.ToString() + "BriefQ" + extension);
                                file.SaveAs(filename);
                                tblBriefQuestion2.id_brief_question = num1;
                                tblBriefQuestion2.question_image = num1.ToString() + "BriefQ" + extension;
                                new BriefLogic().updateQuestion(tblBriefQuestion2);
                                break;
                            }
                            break;
                        }
                        break;
                }
                if (num1 > 0)
                {
                    int int32_3 = Convert.ToInt32(this.Request.Form["imgChoiCount"].ToString());
                    string str1 = this.Request.Form["check-box-img-option"].ToString();
                    for (int index = 1; index <= int32_3; ++index)
                    {
                        int int32_4 = Convert.ToInt32(this.Request.Form["choiceType" + (object)index].ToString());
                        switch (int32_4)
                        {
                            case 1:
                                if (str1 != null)
                                {
                                    string[] source = str1.Split(',');
                                    int num3 = 0;
                                    if (((IEnumerable<string>)source).Count<string>() > 0)
                                        num3 = Convert.ToInt32(source[0]);
                                    num2 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                    {
                                        id_organization = new int?(int32_1),
                                        id_brief_question = new int?(num1),
                                        brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                        choice_type = new int?(int32_4),
                                        is_correct_answer = index != num3 ? new int?(0) : new int?(1),
                                        status = "A",
                                        updated_date_time = new DateTime?(DateTime.Now)
                                    });
                                    break;
                                }
                                break;
                            case 2:
                                if (str1 != null)
                                {
                                    string[] source = str1.Split(',');
                                    int num4 = 0;
                                    if (((IEnumerable<string>)source).Count<string>() > 0)
                                        num4 = Convert.ToInt32(source[0]);
                                    num2 = new BriefLogic().InsertimgChoiOpt(new tbl_brief_answer()
                                    {
                                        id_organization = new int?(int32_1),
                                        id_brief_question = new int?(num1),
                                        choice_type = new int?(int32_4),
                                        is_correct_answer = index != num4 ? new int?(0) : new int?(1),
                                        status = "A",
                                        updated_date_time = new DateTime?(DateTime.Now)
                                    });
                                }
                                tbl_brief_answer ans1 = new tbl_brief_answer();
                                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                {
                                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                    string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                    string str2 = System.Web.HttpContext.Current.Request["id"];
                                    if (file != null && file.ContentLength > 0)
                                    {
                                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), num1.ToString() + "BriefChoice" + (object)index + extension);
                                        file.SaveAs(filename);
                                        ans1.id_brief_answer = num2;
                                        ans1.choice_image = num1.ToString() + "BriefChoice" + (object)index + extension;
                                        new BriefLogic().updateimgChoiOpt(ans1);
                                        break;
                                    }
                                    break;
                                }
                                break;
                            case 3:
                                if (str1 != null)
                                {
                                    string[] source = str1.Split(',');
                                    int num5 = 0;
                                    if (((IEnumerable<string>)source).Count<string>() > 0)
                                        num5 = Convert.ToInt32(source[0]);
                                    num2 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                    {
                                        id_organization = new int?(int32_1),
                                        id_brief_question = new int?(num1),
                                        brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                        choice_type = new int?(int32_4),
                                        is_correct_answer = index != num5 ? new int?(0) : new int?(1),
                                        status = "A",
                                        updated_date_time = new DateTime?(DateTime.Now)
                                    });
                                }
                                tbl_brief_answer ans2 = new tbl_brief_answer();
                                if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                {
                                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                    string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                    string str3 = System.Web.HttpContext.Current.Request["id"];
                                    if (file != null && file.ContentLength > 0)
                                    {
                                        if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                        string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), num1.ToString() + "BriefChoice" + (object)index + extension);
                                        file.SaveAs(filename);
                                        ans2.id_brief_answer = num2;
                                        ans2.choice_image = num1.ToString() + "BriefChoice" + (object)index + extension;
                                        new BriefLogic().updateimgChoiOpt(ans2);
                                        break;
                                    }
                                    break;
                                }
                                break;
                        }
                    }
                }
                if (num1 > 0)
                {
                    tblBriefMaster.question_count = new int?(int32_2);
                    this.db.SaveChanges();
                }
                string str4 = this.Request.Form["question-metadata"].ToString();
                char[] chArray = new char[1] { ',' };
                foreach (string str5 in str4.Split(chArray))
                {
                    string str6 = str5.Trim();
                    this.db.tbl_brief_question_metadata.Add(new tbl_brief_question_metadata()
                    {
                        id_brief_question = new int?(num1),
                        brief_question_metadata = str6,
                        status = "A",
                        updated_date_time = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                }
                this.db.tbl_brief_question_mapping.Add(new tbl_brief_question_mapping()
                {
                    id_brief_master = new int?(tblBriefMaster.id_brief_master),
                    id_brief_question = new int?(num1),
                    question_link_type = new int?(1),
                    date_time_stamp = new DateTime?(DateTime.Now),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
            }
            return (ActionResult)this.RedirectToAction("BriefContentDetail", "Brief", (object)new
            {
                brf = tblBriefMaster.brief_code
            });
        }

        public ActionResult EditQuestionContent(int idQue)
        {
            int int32 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
            List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object)int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
            this.ViewData["complexity"] = (object)this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity where status='A' ").ToList<tbl_brief_question_complexity>();
            this.ViewData["category"] = (object)list1;
            this.ViewData["subcategory"] = (object)list2;
            List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
            List<tbl_brief_answer> tblBriefAnswerList = new List<tbl_brief_answer>();
            List<tbl_brief_question_metadata> questionMetadataList = new List<tbl_brief_question_metadata>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                tblBriefQuestionList = m2ostDbContext.Database.SqlQuery<tbl_brief_question>("SELECT * FROM tbl_brief_question where id_brief_question={0}", (object)idQue).ToList<tbl_brief_question>();
                tblBriefAnswerList = m2ostDbContext.Database.SqlQuery<tbl_brief_answer>("SELECT * FROM tbl_brief_answer where id_brief_question={0}", (object)idQue).ToList<tbl_brief_answer>();
                questionMetadataList = m2ostDbContext.Database.SqlQuery<tbl_brief_question_metadata>("SELECT * FROM tbl_brief_question_metadata where id_brief_question={0}", (object)idQue).ToList<tbl_brief_question_metadata>();
            }
            this.ViewData["BriefQue"] = (object)tblBriefQuestionList;
            this.ViewData["BriefAns"] = (object)tblBriefAnswerList;
            this.ViewData["BriefMeta"] = (object)questionMetadataList;
            return (ActionResult)this.View();
        }

        public ActionResult updateQuestionActionContent()
        {
            int int32_1 = Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            try
            {
                if (true)
                {
                    int insertedId = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                    int num1 = 0;
                    switch (Convert.ToInt32(this.Request.Form["themtyp"].ToString()))
                    {
                        case 1:
                            tbl_brief_question qtn1 = new tbl_brief_question();
                            qtn1.id_brief_question = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                            qtn1.id_organization = new int?(int32_1);
                            qtn1.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                            qtn1.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                            qtn1.brief_question = this.Request.Form["questionText"].ToString();
                            qtn1.question_complexity = new int?(1);
                            qtn1.question_theme_type = new int?(2);
                            qtn1.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                            qtn1.question_type = 1;
                            DateTime now1 = DateTime.Now;
                            DateTime datetime1 = new Utility().StringToDatetime("2030-02-21 11:23:00");
                            qtn1.expiry_date = new DateTime?(datetime1);
                            qtn1.status = "A";
                            qtn1.updated_date_time = new DateTime?(DateTime.Now);
                            new BriefLogic().UpdateQuestionSec(qtn1);
                            break;
                        case 2:
                            tbl_brief_question qtn2 = new tbl_brief_question();
                            qtn2.id_brief_question = Convert.ToInt32(this.Request.Form["idQues"].ToString());
                            qtn2.id_organization = new int?(int32_1);
                            qtn2.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                            qtn2.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                            qtn2.question_complexity = new int?(1);
                            qtn2.question_image = "";
                            qtn2.question_theme_type = new int?(2);
                            qtn2.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                            qtn2.question_type = 1;
                            DateTime now2 = DateTime.Now;
                            DateTime datetime2 = new Utility().StringToDatetime("2030-02-21 11:23:00");
                            qtn2.expiry_date = new DateTime?(datetime2);
                            qtn2.status = "A";
                            qtn2.updated_date_time = new DateTime?(DateTime.Now);
                            new BriefLogic().UpdateQuestionImage(qtn2);
                            tbl_brief_question tblBriefQuestion1 = new tbl_brief_question();
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                                string str = System.Web.HttpContext.Current.Request["id"];
                                if (file != null && file.ContentLength > 0)
                                {
                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), insertedId.ToString() + "BriefQ" + extension);
                                    file.SaveAs(filename);
                                    tblBriefQuestion1.id_brief_question = insertedId;
                                    tblBriefQuestion1.question_image = insertedId.ToString() + "BriefQ" + extension;
                                    new BriefLogic().updateQuestion(tblBriefQuestion1);
                                    break;
                                }
                                tblBriefQuestion1.id_brief_question = insertedId;
                                tblBriefQuestion1.question_image = this.Request.Form["league-image"].ToString();
                                new BriefLogic().updateQuestion(tblBriefQuestion1);
                                break;
                            }
                            break;
                        case 3:
                            tbl_brief_question qtn3 = new tbl_brief_question();
                            qtn3.id_organization = new int?(int32_1);
                            qtn3.id_brief_category = new int?(Convert.ToInt32(this.Request.Form["select-category"]));
                            qtn3.id_brief_sub_category = new int?(Convert.ToInt32(this.Request.Form["select-sub-category"]));
                            qtn3.brief_question = this.Request.Form["questionText"].ToString();
                            qtn3.question_complexity = new int?(1);
                            qtn3.question_theme_type = new int?(2);
                            qtn3.question_choice_type = new int?(Convert.ToInt32(this.Request.Form["themtyp"].ToString()));
                            qtn3.question_type = 1;
                            DateTime now3 = DateTime.Now;
                            DateTime datetime3 = new Utility().StringToDatetime("2030-02-21 11:23:00");
                            qtn3.expiry_date = new DateTime?(datetime3);
                            qtn3.status = "A";
                            qtn3.updated_date_time = new DateTime?(DateTime.Now);
                            new BriefLogic().UpdateQuestionSec(qtn3);
                            if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                            {
                                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["league-btn"];
                                string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["league-btn"].FileName);
                                string str = System.Web.HttpContext.Current.Request["id"];
                                if (file != null && file.ContentLength > 0)
                                {
                                    tbl_brief_question tblBriefQuestion2 = new tbl_brief_question();
                                    if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/")))
                                        Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"));
                                    string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/question/"), insertedId.ToString() + "BriefQ" + extension);
                                    file.SaveAs(filename);
                                    tblBriefQuestion2.id_brief_question = insertedId;
                                    tblBriefQuestion2.question_image = insertedId.ToString() + "BriefQ" + extension;
                                    new BriefLogic().updateQuestion(tblBriefQuestion2);
                                    break;
                                }
                                new BriefLogic().updateQuestion(new tbl_brief_question()
                                {
                                    id_brief_question = insertedId,
                                    question_image = this.Request.Form["league-image"].ToString()
                                });
                                break;
                            }
                            break;
                    }
                    if (insertedId > 0)
                    {
                        int int32_2 = Convert.ToInt32(this.Request.Form["imgChoiCount"].ToString());
                        string str1 = this.Request.Form["check-box-img-option"].ToString();
                        for (int index = 1; index <= int32_2; ++index)
                        {
                            int int32_3 = Convert.ToInt32(this.Request.Form["choiceType" + (object)index].ToString());
                            string str2 = this.Request.Form["secondchoceid-" + (object)index];
                            if (string.IsNullOrEmpty(str2))
                            {
                                switch (int32_3)
                                {
                                    case 1:
                                        if (str1 != null)
                                        {
                                            string[] source = str1.Split(',');
                                            int num2 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num2 = Convert.ToInt32(source[0]);
                                            num1 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(insertedId),
                                                brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                choice_type = new int?(int32_3),
                                                is_correct_answer = index != num2 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                            continue;
                                        }
                                        continue;
                                    case 2:
                                        if (str1 != null)
                                        {
                                            string[] source = str1.Split(',');
                                            int num3 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num3 = Convert.ToInt32(source[0]);
                                            num1 = new BriefLogic().InsertimgChoiOpt(new tbl_brief_answer()
                                            {
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(insertedId),
                                                choice_type = new int?(int32_3),
                                                is_correct_answer = index != num3 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                        }
                                        tbl_brief_answer ans1 = new tbl_brief_answer();
                                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                        {
                                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                            string str3 = System.Web.HttpContext.Current.Request["id"];
                                            if (file != null && file.ContentLength > 0)
                                            {
                                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                file.SaveAs(filename);
                                                ans1.id_brief_answer = num1;
                                                ans1.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                new BriefLogic().updateimgChoiOpt(ans1);
                                                continue;
                                            }
                                            continue;
                                        }
                                        continue;
                                    case 3:
                                        if (str1 != null)
                                        {
                                            string[] source = str1.Split(',');
                                            int num4 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num4 = Convert.ToInt32(source[0]);
                                            num1 = new BriefLogic().InsertChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(insertedId),
                                                brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                choice_type = new int?(int32_3),
                                                is_correct_answer = index != num4 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                        }
                                        tbl_brief_answer ans2 = new tbl_brief_answer();
                                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                        {
                                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                            string str4 = System.Web.HttpContext.Current.Request["id"];
                                            if (file != null && file.ContentLength > 0)
                                            {
                                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                file.SaveAs(filename);
                                                ans2.id_brief_answer = num1;
                                                ans2.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                new BriefLogic().updateimgChoiOpt(ans2);
                                                continue;
                                            }
                                            continue;
                                        }
                                        continue;
                                    default:
                                        continue;
                                }
                            }
                            else
                            {
                                num1 = Convert.ToInt32(str2);
                                switch (int32_3)
                                {
                                    case 1:
                                        if (str1 != null)
                                        {
                                            string[] source = str1.Split(',');
                                            int num5 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num5 = Convert.ToInt32(source[0]);
                                            new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_brief_answer = num1,
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(insertedId),
                                                brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                choice_type = new int?(int32_3),
                                                is_correct_answer = index != num5 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                            continue;
                                        }
                                        continue;
                                    case 2:
                                        if (str1 != null)
                                        {
                                            string[] source = str1.Split(',');
                                            int num6 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num6 = Convert.ToInt32(source[0]);
                                            new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_brief_answer = num1,
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(insertedId),
                                                brief_answer = "",
                                                choice_type = new int?(int32_3),
                                                is_correct_answer = index != num6 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                        }
                                        tbl_brief_answer ans3 = new tbl_brief_answer();
                                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                        {
                                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                            string str5 = System.Web.HttpContext.Current.Request["id"];
                                            if (file != null && file.ContentLength > 0)
                                            {
                                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                file.SaveAs(filename);
                                                ans3.id_brief_answer = num1;
                                                ans3.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                new BriefLogic().updateimgChoiOpt(ans3);
                                                continue;
                                            }
                                            ans3.id_brief_answer = num1;
                                            ans3.choice_image = this.Request.Form["league-image" + (object)index].ToString();
                                            new BriefLogic().updateimgChoiOpt(ans3);
                                            continue;
                                        }
                                        continue;
                                    case 3:
                                        if (str1 != null)
                                        {
                                            string[] source = str1.Split(',');
                                            int num7 = 0;
                                            if (((IEnumerable<string>)source).Count<string>() > 0)
                                                num7 = Convert.ToInt32(source[0]);
                                            new BriefLogic().UpdateChoiceOpt(new tbl_brief_answer()
                                            {
                                                id_brief_answer = num1,
                                                id_organization = new int?(int32_1),
                                                id_brief_question = new int?(insertedId),
                                                brief_answer = this.Request.Form["questionText" + (object)index].ToString(),
                                                choice_type = new int?(int32_3),
                                                is_correct_answer = index != num7 ? new int?(0) : new int?(1),
                                                status = "A",
                                                updated_date_time = new DateTime?(DateTime.Now)
                                            });
                                        }
                                        tbl_brief_answer ans4 = new tbl_brief_answer();
                                        if (((IEnumerable<string>)System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
                                        {
                                            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index];
                                            string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["imgBrief-btn" + (object)index].FileName);
                                            string str6 = System.Web.HttpContext.Current.Request["id"];
                                            if (file != null && file.ContentLength > 0)
                                            {
                                                if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/")))
                                                    Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"));
                                                string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/briefQuesion/" + (object)int32_1 + "/choice/"), insertedId.ToString() + "BriefChoice" + (object)index + extension);
                                                file.SaveAs(filename);
                                                ans4.id_brief_answer = num1;
                                                ans4.choice_image = insertedId.ToString() + "BriefChoice" + (object)index + extension;
                                                new BriefLogic().updateimgChoiOpt(ans4);
                                                continue;
                                            }
                                            ans4.id_brief_answer = num1;
                                            ans4.choice_image = this.Request.Form["league-image" + (object)index].ToString();
                                            new BriefLogic().updateimgChoiOpt(ans4);
                                            continue;
                                        }
                                        continue;
                                    default:
                                        continue;
                                }
                            }
                        }
                    }
                    DbSet<tbl_brief_question_metadata> questionMetadata1 = this.db.tbl_brief_question_metadata;
                    Expression<Func<tbl_brief_question_metadata, bool>> predicate1 = (Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)insertedId);
                    foreach (tbl_brief_question_metadata questionMetadata2 in questionMetadata1.Where<tbl_brief_question_metadata>(predicate1).ToList<tbl_brief_question_metadata>())
                    {
                        questionMetadata2.status = "T";
                        this.db.SaveChanges();
                    }
                    string str7 = this.Request.Form["question-metadata"].ToString();
                    char[] chArray = new char[1] { ',' };
                    foreach (string str8 in str7.Split(chArray))
                    {
                        string item = str8;
                        if (item.Count<char>() > 0)
                        {
                            tbl_brief_question_metadata questionMetadata3 = this.db.tbl_brief_question_metadata.Where<tbl_brief_question_metadata>((Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)insertedId && t.brief_question_metadata.ToLower().Trim() == item.ToLower())).FirstOrDefault<tbl_brief_question_metadata>();
                            if (questionMetadata3 != null)
                            {
                                questionMetadata3.status = "A";
                                this.db.SaveChanges();
                            }
                            else
                            {
                                string str9 = item.Trim();
                                this.db.tbl_brief_question_metadata.Add(new tbl_brief_question_metadata()
                                {
                                    id_brief_question = new int?(insertedId),
                                    brief_question_metadata = str9,
                                    status = "A",
                                    updated_date_time = new DateTime?(DateTime.Now)
                                });
                                this.db.SaveChanges();
                            }
                        }
                    }
                    DbSet<tbl_brief_question_metadata> questionMetadata4 = this.db.tbl_brief_question_metadata;
                    Expression<Func<tbl_brief_question_metadata, bool>> predicate2 = (Expression<Func<tbl_brief_question_metadata, bool>>)(t => t.id_brief_question == (int?)insertedId && t.status == "T");
                    foreach (tbl_brief_question_metadata entity in questionMetadata4.Where<tbl_brief_question_metadata>(predicate2).ToList<tbl_brief_question_metadata>())
                    {
                        this.db.tbl_brief_question_metadata.Remove(entity);
                        this.db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("dashboardContent", "Brief");
        }

        public ActionResult questionDashboardContent()
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>)(t => t.status == "A")).ToList<tbl_brief_question_complexity>();
            List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
                string sql = "SELECT t1.*, t2.brief_category AS brief_category_name, t3.brief_subcategory AS brief_subcategory_name, t4.question_complexity_label AS complexity_name FROM tbl_brief_question t1 INNER JOIN tbl_brief_category t2 ON t1.id_brief_category = t2.id_brief_category INNER JOIN tbl_brief_subcategory t3 ON t1.id_brief_sub_category = t3.id_brief_subcategory INNER JOIN tbl_brief_question_complexity t4 ON t1.question_complexity = t4.question_complexity, tbl_brief_question_mapping t5, tbl_brief_master t6 WHERE t1.id_brief_question = t5.id_brief_question AND t5.id_brief_master = t6.id_brief_master AND t6.id_user = " + (object)int32_2 + " AND t1.id_organization = " + (object)int32_1 + " AND t5.id_brief_master ORDER BY t1.brief_question";
                tblBriefQuestionList = m2ostDbContext.Database.SqlQuery<tbl_brief_question>(sql).ToList<tbl_brief_question>();
            }
            this.ViewData["qList"] = (object)tblBriefQuestionList;
            return (ActionResult)this.View();
        }

        public ActionResult cleanBriefContent(string brf)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief == null)
                return (ActionResult)this.RedirectToAction("dashboardContent", "Brief");
            tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
            List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
            brief.brief_description = WebUtility.HtmlDecode(brief.brief_description);
            List<tbl_brief_question> list2 = this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question where id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + (object)brief.id_brief_master + "  and status='A')  and status='A'").ToList<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.tbl_brief_category.Where<tbl_brief_category>((Expression<Func<tbl_brief_category, bool>>)(t => (int?)t.id_brief_category == brief.id_brief_category)).FirstOrDefault<tbl_brief_category>();
            tbl_brief_subcategory briefSubcategory = this.db.tbl_brief_subcategory.Where<tbl_brief_subcategory>((Expression<Func<tbl_brief_subcategory, bool>>)(t => (int?)t.id_brief_subcategory == brief.id_brief_sub_category)).FirstOrDefault<tbl_brief_subcategory>();
            List<tbl_brief_metadata> list3 = this.db.tbl_brief_metadata.Where<tbl_brief_metadata>((Expression<Func<tbl_brief_metadata, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).ToList<tbl_brief_metadata>();
            tbl_brief_status tblBriefStatus = this.db.tbl_brief_status.Where<tbl_brief_status>((Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master && t.status == "A")).FirstOrDefault<tbl_brief_status>();
            bool flag = false;
            if (tblBriefStatus != null)
            {
                int? statusCode = tblBriefStatus.status_code;
                int num = 1;
                if (statusCode.GetValueOrDefault() == num & statusCode.HasValue)
                    flag = true;
            }
            List<BriefResultSummery> briefResultSummery = new BriefModel().getBriefResultSummery("SELECT a.id_user,attempt_no,brief_result,c.FIRSTNAME prname,d.FIRSTNAME rmname,updated_date_time completedtime FROM tbl_brief_log a,tbl_user b,tbl_profile c,tbl_profile d where a.id_user=b.id_user and b.ID_USER=c.id_user and b.reporting_manager=d.ID_USER and a.id_brief_master=" + (object)brief.id_brief_master + " ");
            this.ViewData["unreadList"] = (object)new BriefModel().getBriefUnreadSummery("SELECT u.id_brief_user_assignment, b.id_user, c.FIRSTNAME prname, d.FIRSTNAME rmname, u.assignment_datetime assignedtime,m.brief_code FROM tbl_brief_master m, tbl_brief_user_assignment u, tbl_user a, tbl_user b, tbl_profile c, tbl_profile d WHERE m.id_brief_master = u.id_brief_master AND u.id_user = a.id_user AND a.id_user = b.id_user AND b.ID_USER = c.id_user AND b.reporting_manager = d.ID_USER AND m.id_brief_master = " + (object)brief.id_brief_master + " and m.id_brief_master not in (select id_brief_master from tbl_brief_log where id_brief_master=" + (object)brief.id_brief_master + ")");
            this.ViewData["summeryList"] = (object)briefResultSummery;
            this.ViewData["briefmaster"] = (object)brief;
            this.ViewData["bTemplate"] = (object)briefMasterTemplate;
            this.ViewData["mBody"] = (object)list1;
            this.ViewData["questions"] = (object)list2;
            this.ViewData["category"] = (object)tblBriefCategory;
            this.ViewData["subcategory"] = (object)briefSubcategory;
            this.ViewData["metadata"] = (object)list3;
            this.ViewData["accessFlag"] = (object)flag;
            return (ActionResult)this.View();
        }

        public ActionResult cleanBriefApproveContent(string brf)
        {
            UserSession content = (UserSession)this.HttpContext.Session.Contents["UserSession"];
            int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
            int int32_2 = Convert.ToInt32(content.ID_USER);
            tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>)(t => t.brief_code.ToLower() == brf.ToLower() && t.status == "A")).FirstOrDefault<tbl_brief_master>();
            if (brief != null)
            {
                DbSet<tbl_brief_status> tblBriefStatus1 = this.db.tbl_brief_status;
                Expression<Func<tbl_brief_status, bool>> predicate1 = (Expression<Func<tbl_brief_status, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_status tblBriefStatus2 in tblBriefStatus1.Where<tbl_brief_status>(predicate1).ToList<tbl_brief_status>())
                {
                    if (tblBriefStatus2.status == "A")
                    {
                        tblBriefStatus2.status = "D";
                        tblBriefStatus2.updated_date_time = new DateTime?(DateTime.Now);
                        this.db.SaveChanges();
                    }
                }
                this.db.tbl_brief_status.Add(new tbl_brief_status()
                {
                    id_brief_master = new int?(brief.id_brief_master),
                    status = "A",
                    updated_date_time = new DateTime?(DateTime.Now),
                    status_code = new int?(7),
                    status_time_stamp = new DateTime?(DateTime.Now),
                    brief_status = "Deleted"
                });
                this.db.SaveChanges();
                tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
                List<tbl_brief_master_body> list = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>)(t => t.srno)).ToList<tbl_brief_master_body>();
                if (briefMasterTemplate != null)
                {
                    briefMasterTemplate.status = "X";
                    briefMasterTemplate.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                foreach (tbl_brief_master_body tblBriefMasterBody in list)
                {
                    tblBriefMasterBody.status = "X";
                    tblBriefMasterBody.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                DbSet<tbl_brief_user_assignment> briefUserAssignment1 = this.db.tbl_brief_user_assignment;
                Expression<Func<tbl_brief_user_assignment, bool>> predicate2 = (Expression<Func<tbl_brief_user_assignment, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_user_assignment briefUserAssignment2 in briefUserAssignment1.Where<tbl_brief_user_assignment>(predicate2).ToList<tbl_brief_user_assignment>())
                {
                    briefUserAssignment2.status = "X";
                    briefUserAssignment2.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                DbSet<tbl_brief_category_mapping> briefCategoryMapping1 = this.db.tbl_brief_category_mapping;
                Expression<Func<tbl_brief_category_mapping, bool>> predicate3 = (Expression<Func<tbl_brief_category_mapping, bool>>)(t => t.id_brief_master == (int?)brief.id_brief_master);
                foreach (tbl_brief_category_mapping briefCategoryMapping2 in briefCategoryMapping1.Where<tbl_brief_category_mapping>(predicate3).ToList<tbl_brief_category_mapping>())
                {
                    briefCategoryMapping2.status = "X";
                    briefCategoryMapping2.updated_date_time = new DateTime?(DateTime.Now);
                    this.db.SaveChanges();
                }
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_brief_log_audit` (`id_brief`, `id_organization`,`cms_id_user`,`status`,`update_datetime`) VALUES({0},{1},{2},'X',{3})", (object)brief.id_brief_master, (object)int32_1, (object)int32_2, (object)DateTime.Now);
            }
            return (ActionResult)this.RedirectToAction("dashboardContent", "Brief");
        }

        public ActionResult DeleteQuestionContent(int id_qtn)
        {
            Convert.ToInt32(((UserSession)this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
            try
            {
                using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
                    m2ostDbContext.Database.ExecuteSqlCommand("delete from tbl_brief_question where id_brief_question={0}", (object)id_qtn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.RedirectToAction("questionDashboardContent", "Brief");
        }
    }
}
