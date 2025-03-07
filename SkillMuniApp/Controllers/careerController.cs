// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.careerController
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
  public class careerController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult career_evaluation()
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object) int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
      List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object) int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
      List<tbl_ce_evaluation_tile> ceEvaluationTileList = new List<tbl_ce_evaluation_tile>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        ceEvaluationTileList = m2ostDbContext.Database.SqlQuery<tbl_ce_evaluation_tile>("select * from tbl_ce_evaluation_tile where id_organization={0}", (object) int32).ToList<tbl_ce_evaluation_tile>();
      this.ViewData["category"] = (object) list1;
      this.ViewData["subcategory"] = (object) list2;
      this.ViewData["ce_eval_tile"] = (object) ceEvaluationTileList;
      return (ActionResult) this.View();
    }

    public ActionResult finishCareerEvaluation()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_ce_career_evaluation_master evaluationMaster = new tbl_ce_career_evaluation_master();
      evaluationMaster.id_organization = int32_1;
      evaluationMaster.career_evaluation_title = this.Request.Form["career-name"].ToString();
      string str1 = "CE" + new Utility().RandomString(8);
      evaluationMaster.career_evaluation_code = str1;
      evaluationMaster.id_ce_evaluation_tile = Convert.ToInt32(this.Request.Form["career-evaluation-type"].ToString());
      evaluationMaster.ce_description = this.Request.Form["career-Description"].ToString();
      evaluationMaster.validation_period = 0;
      evaluationMaster.is_time_enforced = Convert.ToInt32(this.Request.Form["career-timeCondition"].ToString());
      evaluationMaster.time_enforced = 0;
      if (evaluationMaster.is_time_enforced == 1)
        evaluationMaster.time_enforced = Convert.ToInt32(this.Request.Form["career-time-enforced"].ToString());
      evaluationMaster.ce_assessment_type = Convert.ToInt32(this.Request.Form["career-assessment"].ToString());
      evaluationMaster.ordering_sequence_number = Convert.ToInt32(this.Request.Form["career-order-sequence"].ToString());
      evaluationMaster.no_of_question = 0;
      evaluationMaster.job_points_for_ra = 0;
      if (evaluationMaster.ce_assessment_type == 1)
      {
        evaluationMaster.no_of_question = Convert.ToInt32(this.Request.Form["career-no-of-question"].ToString());
        evaluationMaster.job_points_for_ra = Convert.ToInt32(this.Request.Form["career-right-answer"].ToString());
      }
      else if (evaluationMaster.ce_assessment_type == 2)
        evaluationMaster.validation_period = Convert.ToInt32(this.Request.Form["career-psycho-Re-Assessment"].ToString());
      evaluationMaster.created_date = DateTime.Now;
      evaluationMaster.status = "D";
      evaluationMaster.updated_date_time = DateTime.Now;
      int num = 0;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        num = m2ostDbContext.Database.SqlQuery<int>("INSERT INTO `tbl_ce_career_evaluation_master`(`id_organization`,`career_evaluation_title`,`career_evaluation_code`,`id_ce_evaluation_tile`,`ce_description`,`validation_period`,`ordering_sequence_number`,`no_of_question`,`is_time_enforced`,`time_enforced`,`ce_assessment_type`,`job_points_for_ra`,`created_date`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14});SELECT LAST_INSERT_ID();", (object) evaluationMaster.id_organization, (object) evaluationMaster.career_evaluation_title, (object) evaluationMaster.career_evaluation_code, (object) evaluationMaster.id_ce_evaluation_tile, (object) evaluationMaster.ce_description, (object) evaluationMaster.validation_period, (object) evaluationMaster.ordering_sequence_number, (object) evaluationMaster.no_of_question, (object) evaluationMaster.is_time_enforced, (object) evaluationMaster.time_enforced, (object) evaluationMaster.ce_assessment_type, (object) evaluationMaster.job_points_for_ra, (object) evaluationMaster.created_date, (object) evaluationMaster.status, (object) evaluationMaster.updated_date_time).FirstOrDefault<int>();
      if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
      {
        HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["career-logo"];
        string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["career-logo"].FileName);
        string str2 = System.Web.HttpContext.Current.Request["id"];
        if (file != null && file.ContentLength > 0)
        {
          if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/career/" + (object) int32_1 + "/")))
            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/career/" + (object) int32_1 + "/"));
          string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/career/" + (object) int32_1 + "/"), num.ToString() + "career" + (object) int32_1 + extension);
          file.SaveAs(filename);
          evaluationMaster.ce_image = num.ToString() + "career" + (object) int32_1 + extension;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_ce_career_evaluation_master` SET `ce_image` = {0} WHERE `id_ce_career_evaluation_master` = {1}", (object) evaluationMaster.ce_image, (object) num);
        }
      }
      if (evaluationMaster.ce_assessment_type == 1)
      {
        int int32_2 = Convert.ToInt32(this.Request.Form["catChkcCount"].ToString());
        for (int index = 1; index <= int32_2; ++index)
        {
          string str3 = this.Request.Form["ce-check-" + (object) index].ToString();
          if (str3 != null && str3 != "")
          {
            int int32_3 = Convert.ToInt32(str3);
            tbl_ce_category_mapping ceCategoryMapping = new tbl_ce_category_mapping();
            int int32_4 = Convert.ToInt32(int32_3);
            ceCategoryMapping.id_organization = int32_1;
            ceCategoryMapping.id_ce_career_evaluation_master = num;
            ceCategoryMapping.id_brief_category = int32_4;
            ceCategoryMapping.status = "A";
            ceCategoryMapping.updated_date_time = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
              m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_category_mapping`(`id_organization`,`id_ce_career_evaluation_master`,`id_brief_category`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4});", (object) ceCategoryMapping.id_organization, (object) ceCategoryMapping.id_ce_career_evaluation_master, (object) ceCategoryMapping.id_brief_category, (object) ceCategoryMapping.status, (object) ceCategoryMapping.updated_date_time);
          }
        }
      }
      else if (evaluationMaster.ce_assessment_type == 2)
      {
        tbl_ce_category_mapping ceCategoryMapping = new tbl_ce_category_mapping();
        int int32_5 = Convert.ToInt32(this.Request.Form["career-psycho-category"].ToString());
        ceCategoryMapping.id_organization = int32_1;
        ceCategoryMapping.id_ce_career_evaluation_master = num;
        ceCategoryMapping.id_brief_category = int32_5;
        ceCategoryMapping.status = "A";
        ceCategoryMapping.updated_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_category_mapping`(`id_organization`,`id_ce_career_evaluation_master`,`id_brief_category`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4});", (object) ceCategoryMapping.id_organization, (object) ceCategoryMapping.id_ce_career_evaluation_master, (object) ceCategoryMapping.id_brief_category, (object) ceCategoryMapping.status, (object) ceCategoryMapping.updated_date_time);
        int int32_6 = Convert.ToInt32(this.Request.Form["career-psycho-keys"].ToString());
        for (int index = 1; index <= int32_6; ++index)
        {
          tbl_ce_evalution_answer_key evalutionAnswerKey = new tbl_ce_evalution_answer_key();
          evalutionAnswerKey.id_organization = int32_1;
          Random random = new Random();
          evalutionAnswerKey.akcode = "AK" + new Utility().RandomString(6) + (object) index + DateTime.Now.ToString("ssfff");
          evalutionAnswerKey.id_ce_career_evaluation_master = num;
          evalutionAnswerKey.key_code = this.Request.Form["psycho-code-" + (object) index].ToString();
          evalutionAnswerKey.answer_key = this.Request.Form["psycho-key-" + (object) index].ToString();
          evalutionAnswerKey.description = this.Request.Form["psycho-descryp-" + (object) index].ToString();
          evalutionAnswerKey.status = "A";
          evalutionAnswerKey.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_evalution_answer_key` (`id_organization`,`akcode`,`id_ce_career_evaluation_master`,`key_code`,`answer_key`,`description`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})", (object) evalutionAnswerKey.id_organization, (object) evalutionAnswerKey.akcode, (object) evalutionAnswerKey.id_ce_career_evaluation_master, (object) evalutionAnswerKey.key_code, (object) evalutionAnswerKey.answer_key, (object) evalutionAnswerKey.description, (object) evalutionAnswerKey.status, (object) evalutionAnswerKey.updated_date_time);
        }
      }
      return evaluationMaster.ce_assessment_type == 2 ? (ActionResult) this.RedirectToAction("PsychoMetricQuestion", (object) new
      {
        v = evaluationMaster.career_evaluation_code
      }) : (ActionResult) this.RedirectToAction("career_evaluation_preview", (object) new
      {
        v = evaluationMaster.career_evaluation_code
      });
    }

    public ActionResult PsychoMetricQuestion(string v)
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int num1 = 0;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        num1 = m2ostDbContext.Database.SqlQuery<int>("select id_ce_career_evaluation_master from tbl_ce_career_evaluation_master where career_evaluation_code = {0}", (object) v).FirstOrDefault<int>();
      List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
      List<tbl_ce_evalution_answer_key> evalutionAnswerKeyList = new List<tbl_ce_evalution_answer_key>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        int num2 = m2ostDbContext.Database.SqlQuery<int>("select id_brief_category from tbl_ce_category_mapping where id_ce_career_evaluation_master={0}", (object) num1).FirstOrDefault<int>();
        tblBriefQuestionList = m2ostDbContext.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where status='A' and id_brief_category={0} and id_organization={1}", (object) num2, (object) int32).ToList<tbl_brief_question>();
        foreach (tbl_brief_question tblBriefQuestion in tblBriefQuestionList)
          tblBriefQuestion.answer = m2ostDbContext.Database.SqlQuery<tbl_brief_answer>("select * from tbl_brief_answer where id_brief_question={0}", (object) tblBriefQuestion.id_brief_question).ToList<tbl_brief_answer>();
        evalutionAnswerKeyList = m2ostDbContext.Database.SqlQuery<tbl_ce_evalution_answer_key>("select * from tbl_ce_evalution_answer_key where id_ce_career_evaluation_master={0}", (object) num1).ToList<tbl_ce_evalution_answer_key>();
      }
      this.ViewData["tbl_Brief_que"] = (object) tblBriefQuestionList;
      this.ViewData["tbl_ceak"] = (object) evalutionAnswerKeyList;
      return (ActionResult) this.View();
    }

    public ActionResult finishPsychometric()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_ce_evalution_psychometric_setup psychometricSetup = new tbl_ce_evalution_psychometric_setup();
      psychometricSetup.id_organization = int32_1;
      psychometricSetup.id_ce_career_evaluation_master = Convert.ToInt32(this.Request.Form["career-evaluation-id"].ToString());
      int int32_2 = Convert.ToInt32(this.Request.Form["questionCount"].ToString());
      string str = "";
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        str = m2ostDbContext.Database.SqlQuery<string>("select career_evaluation_code from tbl_ce_career_evaluation_master where id_ce_career_evaluation_master={0}", (object) psychometricSetup.id_ce_career_evaluation_master).FirstOrDefault<string>();
      for (int index1 = 1; index1 <= int32_2; ++index1)
      {
        psychometricSetup.id_brief_question = Convert.ToInt32(this.Request.Form["questionId-" + (object) index1].ToString());
        psychometricSetup.ordering_sequence = Convert.ToInt32(this.Request.Form["question-txt-" + (object) index1].ToString());
        int int32_3 = Convert.ToInt32(this.Request.Form["anscount-" + (object) index1].ToString());
        for (int index2 = 1; index2 <= int32_3; ++index2)
        {
          psychometricSetup.id_brief_answer = Convert.ToInt32(this.Request.Form["id-answer-" + (object) index1 + "-" + (object) index2].ToString());
          psychometricSetup.id_ce_evalution_answer_key = Convert.ToInt32(this.Request.Form["question-choice-" + (object) index1 + "-" + (object) index2].ToString());
          tbl_ce_evalution_answer_key evalutionAnswerKey = new tbl_ce_evalution_answer_key();
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            evalutionAnswerKey = m2ostDbContext.Database.SqlQuery<tbl_ce_evalution_answer_key>("select * from tbl_ce_evalution_answer_key where id_ce_evalution_answer_key={0}", (object) psychometricSetup.id_ce_evalution_answer_key).FirstOrDefault<tbl_ce_evalution_answer_key>();
          psychometricSetup.status = "A";
          psychometricSetup.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_evalution_psychometric_setup` (`id_organization`,`id_ce_career_evaluation_master`,`id_brief_question`,`ordering_sequence`,`id_brief_answer`,`id_ce_evalution_answer_key`,`key_code`,`answer_key`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", (object) psychometricSetup.id_organization, (object) psychometricSetup.id_ce_career_evaluation_master, (object) psychometricSetup.id_brief_question, (object) psychometricSetup.ordering_sequence, (object) psychometricSetup.id_brief_answer, (object) psychometricSetup.id_ce_evalution_answer_key, (object) evalutionAnswerKey.key_code, (object) evalutionAnswerKey.answer_key, (object) psychometricSetup.status, (object) psychometricSetup.updated_date_time);
        }
      }
      return (ActionResult) this.RedirectToAction("career_evaluation_preview", (object) new
      {
        v = str
      });
    }

    public ActionResult career_evaluation_preview(string v)
    {
      Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int num = 0;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        num = m2ostDbContext.Database.SqlQuery<int>("select id_ce_career_evaluation_master from tbl_ce_career_evaluation_master where career_evaluation_code = {0}", (object) v).FirstOrDefault<int>();
      tbl_ce_career_evaluation_master evaluationMaster = new tbl_ce_career_evaluation_master();
      List<tbl_ce_category_mapping> ceCategoryMappingList = new List<tbl_ce_category_mapping>();
      List<tbl_brief_category> tblBriefCategoryList = new List<tbl_brief_category>();
      List<tbl_brief_question_ce> tblBriefQuestionCeList = new List<tbl_brief_question_ce>();
      string str = "";
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        evaluationMaster = m2ostDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_ce_career_evaluation_master = {0}", (object) num).FirstOrDefault<tbl_ce_career_evaluation_master>();
        str = m2ostDbContext.Database.SqlQuery<string>("select ce_evaluation_tile from tbl_ce_evaluation_tile where status='A' and id_ce_evaluation_tile ={0}", (object) evaluationMaster.id_ce_evaluation_tile).FirstOrDefault<string>();
        m2ostDbContext.Database.SqlQuery<tbl_ce_category_mapping>("select * from tbl_ce_category_mapping where id_ce_career_evaluation_master={0}", (object) num).ToList<tbl_ce_category_mapping>();
        tblBriefCategoryList = m2ostDbContext.Database.SqlQuery<tbl_brief_category>("SELECT t1.* FROM tbl_brief_category t1 INNER JOIN tbl_ce_category_mapping t2 on t1.id_brief_category = t2.id_brief_category where t2.id_ce_career_evaluation_master = {0} and t1.status='A' and t2.status='A'", (object) num).ToList<tbl_brief_category>();
        if (evaluationMaster.ce_assessment_type == 2)
        {
          tblBriefQuestionCeList = m2ostDbContext.Database.SqlQuery<tbl_brief_question_ce>("select t2.*,t1.ordering_sequence from tbl_ce_evalution_psychometric_setup t1 inner join tbl_brief_question t2 on t1.id_brief_question = t2.id_brief_question where t1.id_ce_career_evaluation_master = {0} group by t1.id_brief_question", (object) num).ToList<tbl_brief_question_ce>();
          foreach (tbl_brief_question_ce tblBriefQuestionCe in tblBriefQuestionCeList)
            tblBriefQuestionCe.answer = m2ostDbContext.Database.SqlQuery<tbl_brief_answer_ce>("SELECT t2.*, t1.answer_key FROM tbl_ce_evalution_psychometric_setup t1 INNER JOIN tbl_brief_answer t2 ON t1.id_brief_answer = t2.id_brief_answer WHERE t1.id_brief_question = {0}", (object) tblBriefQuestionCe.id_brief_question).ToList<tbl_brief_answer_ce>();
        }
      }
      this.ViewData["tcem"] = (object) evaluationMaster;
      this.ViewData["evaluationTy"] = (object) str;
      this.ViewData["tbc"] = (object) tblBriefCategoryList;
      this.ViewData["tblBQ"] = (object) tblBriefQuestionCeList;
      return (ActionResult) this.View();
    }

    public ActionResult publish_career_evaluation(int id_cem)
    {
      Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_ce_career_evaluation_master` SET `status` = 'A' WHERE `id_ce_career_evaluation_master` ={0}", (object) id_cem);
      return (ActionResult) this.Redirect("career_evaluation");
    }

    public ActionResult career_evaluation_dashboard()
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_ce_career_evaluation_master> evaluationMasterList = new List<tbl_ce_career_evaluation_master>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        evaluationMasterList = m2ostDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_organization={0}", (object) int32).ToList<tbl_ce_career_evaluation_master>();
      this.ViewData["tcem"] = (object) evaluationMasterList;
      return (ActionResult) this.View();
    }

    public ActionResult edit_career_evaluation(string v)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32 = Convert.ToInt32(content.id_ORGANIZATION);
      Convert.ToInt32(content.ID_USER);
      List<tbl_brief_category> list1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where ID_ORGANIZATION=" + (object) int32 + " AND status='A' order by brief_category").ToList<tbl_brief_category>();
      List<tbl_brief_subcategory> list2 = this.db.tbl_brief_subcategory.SqlQuery("select * from tbl_brief_subcategory where ID_ORGANIZATION=" + (object) int32 + " AND status='A' order by brief_subcategory").ToList<tbl_brief_subcategory>();
      List<tbl_ce_evaluation_tile> ceEvaluationTileList = new List<tbl_ce_evaluation_tile>();
      tbl_ce_career_evaluation_master evaluationMaster = new tbl_ce_career_evaluation_master();
      List<tbl_ce_category_mapping> ceCategoryMappingList = new List<tbl_ce_category_mapping>();
      List<tbl_ce_evalution_answer_key> evalutionAnswerKeyList = new List<tbl_ce_evalution_answer_key>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        ceEvaluationTileList = m2ostDbContext.Database.SqlQuery<tbl_ce_evaluation_tile>("select * from tbl_ce_evaluation_tile where id_organization={0}", (object) int32).ToList<tbl_ce_evaluation_tile>();
        ceCategoryMappingList = m2ostDbContext.Database.SqlQuery<tbl_ce_category_mapping>("select t2.* from tbl_ce_career_evaluation_master t1 inner join tbl_ce_category_mapping t2 on t1.id_ce_career_evaluation_master = t2.id_ce_career_evaluation_master where t1.career_evaluation_code = {0} and t2.status='A'", (object) v).ToList<tbl_ce_category_mapping>();
        evalutionAnswerKeyList = m2ostDbContext.Database.SqlQuery<tbl_ce_evalution_answer_key>("select t1.* from tbl_ce_evalution_answer_key t1 inner join tbl_ce_career_evaluation_master t2 on t2.id_ce_career_evaluation_master = t1.id_ce_career_evaluation_master where t2.career_evaluation_code = {0}", (object) v).ToList<tbl_ce_evalution_answer_key>();
        evaluationMaster = m2ostDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where career_evaluation_code={0}", (object) v).FirstOrDefault<tbl_ce_career_evaluation_master>();
      }
      this.ViewData["category"] = (object) list1;
      this.ViewData["subcategory"] = (object) list2;
      this.ViewData["ce_eval_tile"] = (object) ceEvaluationTileList;
      this.ViewData["tcem"] = (object) evaluationMaster;
      this.ViewData["tcm"] = (object) ceCategoryMappingList;
      this.ViewData["teak"] = (object) evalutionAnswerKeyList;
      return (ActionResult) this.View();
    }

    public ActionResult update_career_evaluation()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_ce_career_evaluation_master evaluationMaster = new tbl_ce_career_evaluation_master();
      evaluationMaster.id_organization = int32_1;
      evaluationMaster.career_evaluation_title = this.Request.Form["career-name"].ToString();
      evaluationMaster.career_evaluation_code = this.Request.Form["ce_code"].ToString();
      evaluationMaster.id_ce_evaluation_tile = Convert.ToInt32(this.Request.Form["career-evaluation-type"].ToString());
      evaluationMaster.ce_description = this.Request.Form["career-Description"].ToString();
      evaluationMaster.validation_period = 0;
      evaluationMaster.is_time_enforced = Convert.ToInt32(this.Request.Form["career-timeCondition"].ToString());
      evaluationMaster.time_enforced = 0;
      if (evaluationMaster.is_time_enforced == 1)
        evaluationMaster.time_enforced = Convert.ToInt32(this.Request.Form["career-time-enforced"].ToString());
      evaluationMaster.ce_assessment_type = Convert.ToInt32(this.Request.Form["career-assessment"].ToString());
      evaluationMaster.ordering_sequence_number = Convert.ToInt32(this.Request.Form["career-order-sequence"].ToString());
      evaluationMaster.no_of_question = 0;
      evaluationMaster.job_points_for_ra = 0;
      if (evaluationMaster.ce_assessment_type == 1)
      {
        evaluationMaster.no_of_question = Convert.ToInt32(this.Request.Form["career-no-of-question"].ToString());
        evaluationMaster.job_points_for_ra = Convert.ToInt32(this.Request.Form["career-right-answer"].ToString());
      }
      evaluationMaster.status = "D";
      evaluationMaster.updated_date_time = DateTime.Now;
      int int32_2 = Convert.ToInt32(this.Request.Form["id_ce_mas"].ToString());
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_ce_career_evaluation_master` SET `id_organization` = {0},`career_evaluation_title` = {1},`id_ce_evaluation_tile` = {2},`ce_description` = {3},`ordering_sequence_number` = {4},`no_of_question` = {5},`is_time_enforced` = {6},`time_enforced` = {7},`ce_assessment_type` = {8},`job_points_for_ra` = {9},`updated_date_time` = {10} WHERE `career_evaluation_code` = {11}", (object) evaluationMaster.id_organization, (object) evaluationMaster.career_evaluation_title, (object) evaluationMaster.id_ce_evaluation_tile, (object) evaluationMaster.ce_description, (object) evaluationMaster.ordering_sequence_number, (object) evaluationMaster.no_of_question, (object) evaluationMaster.is_time_enforced, (object) evaluationMaster.time_enforced, (object) evaluationMaster.ce_assessment_type, (object) evaluationMaster.job_points_for_ra, (object) evaluationMaster.updated_date_time, (object) evaluationMaster.career_evaluation_code);
      if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
      {
        HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["career-logo"];
        string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["career-logo"].FileName);
        string str = System.Web.HttpContext.Current.Request["id"];
        if (file != null && file.ContentLength > 0)
        {
          if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/career/" + (object) int32_1 + "/")))
            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/career/" + (object) int32_1 + "/"));
          string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/career/" + (object) int32_1 + "/"), int32_2.ToString() + "career" + (object) int32_1 + extension);
          file.SaveAs(filename);
          evaluationMaster.ce_image = int32_2.ToString() + "career" + (object) int32_1 + extension;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_ce_career_evaluation_master` SET `ce_image` = {0} WHERE `id_ce_career_evaluation_master` = {1}", (object) evaluationMaster.ce_image, (object) int32_2);
        }
      }
      if (evaluationMaster.ce_assessment_type == 1)
      {
        int int32_3 = Convert.ToInt32(this.Request.Form["catChkcCount"].ToString());
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_ce_category_mapping` SET `status` =  'D' WHERE `id_ce_career_evaluation_master` = {0}", (object) int32_2);
        for (int index = 1; index <= int32_3; ++index)
        {
          string str = this.Request.Form["ce-check-" + (object) index].ToString();
          if (str != null && str != "")
          {
            int int32_4 = Convert.ToInt32(str);
            tbl_ce_category_mapping ceCategoryMapping = new tbl_ce_category_mapping();
            int int32_5 = Convert.ToInt32(int32_4);
            ceCategoryMapping.id_organization = int32_1;
            ceCategoryMapping.id_ce_career_evaluation_master = int32_2;
            ceCategoryMapping.id_brief_category = int32_5;
            ceCategoryMapping.status = "A";
            ceCategoryMapping.updated_date_time = DateTime.Now;
            int num = 0;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
              List<tbl_ce_category_mapping> ceCategoryMappingList = new List<tbl_ce_category_mapping>();
              if (m2ostDbContext.Database.SqlQuery<tbl_ce_category_mapping>("select * from tbl_ce_category_mapping where id_ce_career_evaluation_master = {0} and id_brief_category = {1}", (object) int32_2, (object) int32_4).ToList<tbl_ce_category_mapping>().Count > 0)
                num = 1;
              if (num == 1)
                m2ostDbContext.Database.ExecuteSqlCommand("UPDATE `tbl_ce_category_mapping` SET `status` =  'A' WHERE `id_ce_career_evaluation_master` = {0} and `id_brief_category` = {1}", (object) int32_2, (object) int32_4);
              else
                m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_category_mapping`(`id_organization`,`id_ce_career_evaluation_master`,`id_brief_category`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4});", (object) ceCategoryMapping.id_organization, (object) ceCategoryMapping.id_ce_career_evaluation_master, (object) ceCategoryMapping.id_brief_category, (object) ceCategoryMapping.status, (object) ceCategoryMapping.updated_date_time);
            }
          }
        }
      }
      else if (evaluationMaster.ce_assessment_type == 2)
      {
        tbl_ce_category_mapping ceCategoryMapping = new tbl_ce_category_mapping();
        int int32_6 = Convert.ToInt32(this.Request.Form["career-psycho-category"].ToString());
        ceCategoryMapping.id_organization = int32_1;
        ceCategoryMapping.id_ce_career_evaluation_master = int32_2;
        ceCategoryMapping.id_brief_category = int32_6;
        ceCategoryMapping.status = "A";
        ceCategoryMapping.updated_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_category_mapping`(`id_organization`,`id_ce_career_evaluation_master`,`id_brief_category`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4});", (object) ceCategoryMapping.id_organization, (object) ceCategoryMapping.id_ce_career_evaluation_master, (object) ceCategoryMapping.id_brief_category, (object) ceCategoryMapping.status, (object) ceCategoryMapping.updated_date_time);
        int int32_7 = Convert.ToInt32(this.Request.Form["career-psycho-keys"].ToString());
        for (int index = 1; index <= int32_7; ++index)
        {
          tbl_ce_evalution_answer_key evalutionAnswerKey = new tbl_ce_evalution_answer_key();
          evalutionAnswerKey.id_organization = int32_1;
          Random random = new Random();
          evalutionAnswerKey.akcode = "AK" + new Utility().RandomString(6) + DateTime.Now.ToString("ssfff");
          evalutionAnswerKey.id_ce_career_evaluation_master = int32_2;
          evalutionAnswerKey.key_code = this.Request.Form["psycho-code-" + (object) index].ToString();
          evalutionAnswerKey.answer_key = this.Request.Form["psycho-key-" + (object) index].ToString();
          evalutionAnswerKey.description = this.Request.Form["psycho-descryp-" + (object) index].ToString();
          evalutionAnswerKey.status = "A";
          evalutionAnswerKey.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO `tbl_ce_evalution_answer_key` (`id_organization`,`akcode`,`id_ce_career_evaluation_master`,`key_code`,`answer_key`,`description`,`status`,`updated_date_time`) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})", (object) evalutionAnswerKey.id_organization, (object) evalutionAnswerKey.akcode, (object) evalutionAnswerKey.id_ce_career_evaluation_master, (object) evalutionAnswerKey.key_code, (object) evalutionAnswerKey.answer_key, (object) evalutionAnswerKey.description, (object) evalutionAnswerKey.status, (object) evalutionAnswerKey.updated_date_time);
        }
      }
      return evaluationMaster.ce_assessment_type == 2 ? (ActionResult) this.RedirectToAction("PsychoMetricQuestion", (object) new
      {
        v = evaluationMaster.career_evaluation_code
      }) : (ActionResult) this.RedirectToAction("career_evaluation_dashboard");
    }
  }
}
