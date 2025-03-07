﻿// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.AssessmentController
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
  [UserFilter]
  public class AssessmentController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    [RoleAccessController(KEY = 9)]
    public ActionResult Index(int flag = 0)
    {
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      this.ViewData["assessment"] = (object) this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_organization == (int?) oid && t.status != "D")).ToList<tbl_assessment>();
      this.ViewData[nameof (flag)] = (object) flag;
      return (ActionResult) this.View();
    }

    public ActionResult AssessmentDetails(string id)
    {
      int ids = Convert.ToInt32(id);
      Assessment assessment = new Assessment();
      assessment.tbl_assessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == ids && t.status == "A")).FirstOrDefault<tbl_assessment>();
      if (assessment.tbl_assessment == null)
        return (ActionResult) this.RedirectToAction("Index");
      List<AssessmentQuestion> assessmentQuestionList = new List<AssessmentQuestion>();
      DbSet<tbl_assessment_question> assessmentQuestion1 = this.db.tbl_assessment_question;
      Expression<Func<tbl_assessment_question, bool>> predicate = (Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment == (int?) ids);
      foreach (tbl_assessment_question assessmentQuestion2 in assessmentQuestion1.Where<tbl_assessment_question>(predicate).ToList<tbl_assessment_question>())
      {
        tbl_assessment_question obtbl_assessment_question = assessmentQuestion2;
        AssessmentQuestion assessmentQuestion3 = new AssessmentQuestion();
        assessmentQuestion3.tbl_assessment_question = obtbl_assessment_question;
        assessmentQuestion3.tbl_assessment_answer = this.db.tbl_assessment_answer.Where<tbl_assessment_answer>((Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_question == (int?) obtbl_assessment_question.id_assessment_question)).ToList<tbl_assessment_answer>();
        assessmentQuestionList.Add(assessmentQuestion3);
      }
      assessment.assessment_question = assessmentQuestionList;
      this.ViewData["Assessment"] = (object) assessment;
      return (ActionResult) this.PartialView(nameof (AssessmentDetails), (object) id);
    }

    [RoleAccessController(KEY = 8)]
    public ActionResult createAssessment() => (ActionResult) this.View();

    public ActionResult editAssessment(string id)
    {
      int ids = Convert.ToInt32(id);
      tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == ids)).FirstOrDefault<tbl_assessment>();
      if (tblAssessment == null)
        return (ActionResult) this.RedirectToAction("Index");
      List<tbl_assessment_scoring_key> list = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == (int?) ids)).OrderBy<tbl_assessment_scoring_key, int?>((Expression<Func<tbl_assessment_scoring_key, int?>>) (t => t.position)).ToList<tbl_assessment_scoring_key>();
      this.ViewData["assessment"] = (object) tblAssessment;
      this.ViewData["scoringkey"] = (object) list;
      return (ActionResult) this.View();
    }

    public ActionResult edit_assessment_action()
    {
      Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      this.Request.Form["btn_submit"].ToString();
      int assid = Convert.ToInt32(this.Request.Form["id_assessment"].ToString());
      tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == assid)).FirstOrDefault<tbl_assessment>();
      if (tblAssessment == null)
        return (ActionResult) this.RedirectToAction("Index");
      tblAssessment.assessment_title = this.Request.Form["assessment-name"].ToString();
      tblAssessment.assesment_description = this.Request.Form["assessment-desc"].ToString();
      tblAssessment.answer_description = this.Request.Form["answer-description"].ToString();
      tblAssessment.assess_created = new DateTime?(new Utility().StringToDatetime(this.Request.Form["assessment-started"].ToString()));
      tblAssessment.assess_ended = new DateTime?(new Utility().StringToDatetime(this.Request.Form["assessment-ended"].ToString()));
      tblAssessment.total_attempt = new int?(Convert.ToInt32(this.Request.Form["max-attempt"].ToString()));
      tblAssessment.ans_requiered = new int?(0);
      int? assessGroup = tblAssessment.assess_group;
      int num1 = 1;
      if (assessGroup.GetValueOrDefault() == num1 & assessGroup.HasValue)
        tblAssessment.ans_requiered = new int?(Convert.ToInt32(this.Request.Form["answer-display"].ToString()));
      assessGroup = tblAssessment.assess_group;
      int num2 = 3;
      if (assessGroup.GetValueOrDefault() == num2 & assessGroup.HasValue)
      {
        tblAssessment.lower_title = this.Request.Form["low-value-title"].ToString();
        tblAssessment.lower_value = this.Request.Form["low-value"].ToString();
        tblAssessment.high_title = this.Request.Form["high-value-title"].ToString();
        tblAssessment.high_value = this.Request.Form["high-value"].ToString();
      }
      tblAssessment.updated_date_time = new DateTime?(DateTime.Now);
      this.db.SaveChanges();
      return (ActionResult) this.RedirectToAction("LoadAssessment", "Assessment", (object) new
      {
        id = tblAssessment.id_assessment
      });
    }

    public ActionResult viewAssessment(string id)
    {
      int ids = Convert.ToInt32(id);
      tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == ids)).FirstOrDefault<tbl_assessment>();
      this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment == (int?) ids)).ToList<tbl_assessment_question>();
      this.ViewData["assessment"] = (object) tblAssessment;
      return (ActionResult) this.View();
    }

    public ActionResult add_assessment()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      string str = this.Request.Form["btn_submit"].ToString();
      tbl_assessment entity1 = new tbl_assessment();
      entity1.id_organization = new int?(int32_1);
      entity1.assessment_title = this.Request.Form["assessment-name"].ToString();
      entity1.assesment_description = this.Request.Form["assessment-desc"].ToString();
      entity1.answer_description = this.Request.Form["answer-description"].ToString();
      entity1.assess_start = new DateTime?(new Utility().StringToDatetime(this.Request.Form["assessment-created"].ToString()));
      entity1.assess_created = new DateTime?(new Utility().StringToDatetime(this.Request.Form["assessment-started"].ToString()));
      entity1.assess_ended = new DateTime?(new Utility().StringToDatetime(this.Request.Form["assessment-ended"].ToString()));
      entity1.assess_type = this.Request.Form["assessment-type"].ToString();
      entity1.assessment_type = new int?(Convert.ToInt32(this.Request.Form["assessment-div"].ToString()));
      entity1.assess_group = new int?(Convert.ToInt32(this.Request.Form["assessment-group"].ToString()));
      entity1.total_attempt = new int?(Convert.ToInt32(this.Request.Form["max-attempt"].ToString()));
      entity1.ans_requiered = new int?(0);
      int? assessGroup1 = entity1.assess_group;
      int num1 = 1;
      if (assessGroup1.GetValueOrDefault() == num1 & assessGroup1.HasValue)
        entity1.ans_requiered = new int?(Convert.ToInt32(this.Request.Form["answer-display"].ToString()));
      int? assessGroup2 = entity1.assess_group;
      int num2 = 3;
      if (assessGroup2.GetValueOrDefault() == num2 & assessGroup2.HasValue)
      {
        entity1.lower_title = this.Request.Form["low-value-title"].ToString();
        entity1.lower_value = this.Request.Form["low-value"].ToString();
        entity1.high_title = this.Request.Form["high-value-title"].ToString();
        entity1.high_value = this.Request.Form["high-value"].ToString();
      }
      entity1.status = "T";
      entity1.updated_date_time = new DateTime?(DateTime.Now);
      this.db.tbl_assessment.Add(entity1);
      this.db.SaveChanges();
      assessGroup2 = entity1.assess_group;
      int num3 = 2;
      if (assessGroup2.GetValueOrDefault() == num3 & assessGroup2.HasValue)
      {
        int int32_2 = Convert.ToInt32(this.Request.Form["no-of-key-vak"].ToString());
        for (int index = 1; index <= int32_2; ++index)
        {
          tbl_assessment_scoring_key entity2 = new tbl_assessment_scoring_key()
          {
            header_name = this.Request.Form["t-scoring-key-" + (object) index].ToString()
          };
          entity2.header_name = entity2.header_name.Trim(' ', ',');
          entity2.id_assessment = new int?(entity1.id_assessment);
          entity2.position = new int?(index);
          entity2.id_assessment_theme = entity1.assess_group;
          entity2.status = "A";
          entity2.updated_date_time = new DateTime?(DateTime.Now);
          this.db.tbl_assessment_scoring_key.Add(entity2);
          this.db.SaveChanges();
        }
      }
      assessGroup2 = entity1.assess_group;
      int num4 = 4;
      if (assessGroup2.GetValueOrDefault() == num4 & assessGroup2.HasValue)
      {
        int int32_3 = Convert.ToInt32(this.Request.Form["no-of-key-rank"].ToString());
        for (int index = 1; index <= int32_3; ++index)
        {
          tbl_assessment_scoring_key entity3 = new tbl_assessment_scoring_key()
          {
            header_name = this.Request.Form["t-scoring-key-" + (object) index].ToString()
          };
          entity3.header_name = entity3.header_name.Trim(' ', ',');
          entity3.id_assessment = new int?(entity1.id_assessment);
          entity3.position = new int?(index);
          entity3.id_assessment_theme = entity1.assess_group;
          entity3.status = "A";
          entity3.updated_date_time = new DateTime?(DateTime.Now);
          this.db.tbl_assessment_scoring_key.Add(entity3);
          this.db.SaveChanges();
        }
      }
      assessGroup2 = entity1.assess_group;
      int num5 = 3;
      if (assessGroup2.GetValueOrDefault() == num5 & assessGroup2.HasValue)
      {
        int int32_4 = Convert.ToInt32(this.Request.Form["no-of-key-range"].ToString());
        for (int index = 1; index <= int32_4; ++index)
        {
          tbl_assessment_scoring_key entity4 = new tbl_assessment_scoring_key()
          {
            header_name = this.Request.Form["t-scoring-key-" + (object) index].ToString()
          };
          entity4.header_name = entity4.header_name.Trim(' ', ',');
          entity4.id_assessment = new int?(entity1.id_assessment);
          entity4.position = new int?(index);
          entity4.id_assessment_theme = entity1.assess_group;
          entity4.status = "A";
          entity4.updated_date_time = new DateTime?(DateTime.Now);
          this.db.tbl_assessment_scoring_key.Add(entity4);
          this.db.SaveChanges();
        }
      }
      this.db.tbl_assessment_sheet.Add(new tbl_assessment_sheet()
      {
        id_organization = new int?(int32_1),
        id_assesment = entity1.id_assessment,
        id_assessment_theme = entity1.assess_group,
        status = "A",
        updated_date_time = new DateTime?(DateTime.Now)
      });
      this.db.SaveChanges();
      return str.Equals("Save") ? (ActionResult) this.RedirectToAction("assessmentQuestions", "Assessment", (object) new
      {
        id = entity1.id_assessment,
        flag = 1
      }) : (ActionResult) this.RedirectToAction("Index", (object) new
      {
        flag = 1
      });
    }

    public ActionResult assessmentQuestions(string id, int flag = 0)
    {
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int ids = Convert.ToInt32(id);
      tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == ids && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment>();
      if (tblAssessment == null)
        return (ActionResult) this.RedirectToAction("Index");
      List<tbl_assessment_question> list = this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment == (int?) ids)).ToList<tbl_assessment_question>();
      this.ViewData["scoring_key"] = (object) this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == (int?) ids)).ToList<tbl_assessment_scoring_key>();
      this.ViewData["assessment_question"] = (object) list;
      this.ViewData[nameof (flag)] = (object) flag;
      this.ViewData["assessment"] = (object) tblAssessment;
      return (ActionResult) this.View();
    }

    public ActionResult add_assessment_question()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int assint = Convert.ToInt32(this.Request.Form["id-assessment"].ToString());
      tbl_assessment tblAssessment1 = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == assint)).FirstOrDefault<tbl_assessment>();
      tbl_assessment_question question = new tbl_assessment_question();
      question.assessment_question = this.Request.Form["q-title"].ToString();
      question.id_organization = new int?(int32_1);
      question.id_assessment = new int?(assint);
      int? assessGroup1 = tblAssessment1.assess_group;
      int num1 = 3;
      if (assessGroup1.GetValueOrDefault() == num1 & assessGroup1.HasValue)
      {
        int int32_2 = Convert.ToInt32(this.Request.Form["select-scoring-key"].ToString());
        question.id_assessment_scoring_key = new int?(int32_2);
      }
      else
        question.id_assessment_scoring_key = new int?(0);
      question.status = "A";
      question.updated_date_time = new DateTime?(DateTime.Now);
      this.db.tbl_assessment_question.Add(question);
      this.db.SaveChanges();
      if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
      {
        HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["question-image"];
        string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["question-image"].FileName);
        if (file.ContentLength > 0)
        {
          if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Assessment/")))
            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Assessment/"));
          string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Assessment/"), "aq_" + (object) question.id_assessment_question + extension);
          file.SaveAs(filename);
          question.question_image = "aq_" + (object) question.id_assessment_question + extension;
          this.db.SaveChanges();
        }
      }
      tbl_assessment tblAssessment2 = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == question.id_assessment)).FirstOrDefault<tbl_assessment>();
      int? assessGroup2 = tblAssessment2.assess_group;
      int num2 = 3;
      if (!(assessGroup2.GetValueOrDefault() == num2 & assessGroup2.HasValue))
      {
        int num3 = 0;
        int num4 = 0;
        int? assessGroup3 = tblAssessment2.assess_group;
        int num5 = 1;
        if (assessGroup3.GetValueOrDefault() == num5 & assessGroup3.HasValue)
        {
          num4 = Convert.ToInt32(this.Request.Form["select-answer"].ToString());
          num3 = Convert.ToInt32(this.Request.Form["select-options-type"].ToString());
        }
        else
        {
          assessGroup3 = tblAssessment2.assess_group;
          int num6 = 2;
          if (assessGroup3.GetValueOrDefault() == num6 & assessGroup3.HasValue)
          {
            num3 = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == question.id_assessment)).ToList<tbl_assessment_scoring_key>().Count;
          }
          else
          {
            assessGroup3 = tblAssessment2.assess_group;
            int num7 = 4;
            if (assessGroup3.GetValueOrDefault() == num7 & assessGroup3.HasValue)
              num3 = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == question.id_assessment)).ToList<tbl_assessment_scoring_key>().Count;
          }
        }
        for (int index = 1; index <= num3; ++index)
        {
          tbl_assessment_answer entity = new tbl_assessment_answer();
          entity.answer_description = this.Request.Form["t-content-" + (object) index].ToString();
          entity.id_assessment_question = new int?(question.id_assessment_question);
          entity.id_assessment = question.id_assessment;
          entity.position = new int?(index);
          assessGroup3 = tblAssessment2.assess_group;
          int num8 = 4;
          entity.id_assessment_scoring_key = !(assessGroup3.GetValueOrDefault() == num8 & assessGroup3.HasValue) ? new int?(0) : new int?(Convert.ToInt32(this.Request.Form["t-scoring-key-" + (object) index].ToString()));
          entity.status = "A";
          entity.updated_date_time = new DateTime?(DateTime.Now);
          this.db.tbl_assessment_answer.Add(entity);
          this.db.SaveChanges();
          if (num4 == index)
          {
            question.aq_answer = Convert.ToString(entity.id_assessment_answer);
            this.db.SaveChanges();
          }
        }
      }
      return (ActionResult) this.RedirectToAction("assessmentQuestions", "Assessment", (object) new
      {
        id = question.id_assessment
      });
    }

    public string edit_assessment_question_action()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int assint = Convert.ToInt32(this.Request.Form["id-assessment"].ToString());
      tbl_assessment tblAssessment1 = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == assint)).FirstOrDefault<tbl_assessment>();
      int qtnId = Convert.ToInt32(this.Request.Form["id_hid_question"].ToString());
      tbl_assessment_question question = this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment_question == qtnId && t.id_assessment == (int?) assint)).FirstOrDefault<tbl_assessment_question>();
      if (question != null)
      {
        question.assessment_question = this.Request.Form["q-title"].ToString();
        question.id_organization = new int?(int32_1);
        question.id_assessment = new int?(assint);
        int? assessGroup = tblAssessment1.assess_group;
        int num1 = 3;
        if (assessGroup.GetValueOrDefault() == num1 & assessGroup.HasValue)
        {
          int int32_2 = Convert.ToInt32(this.Request.Form["select-scoring-key"].ToString());
          question.id_assessment_scoring_key = new int?(int32_2);
        }
        else
          question.id_assessment_scoring_key = new int?(0);
        question.status = "A";
        question.updated_date_time = new DateTime?(DateTime.Now);
        if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
        {
          HttpPostedFile file1 = System.Web.HttpContext.Current.Request.Files["question-image"];
          HttpPostedFile file2 = System.Web.HttpContext.Current.Request.Files["question-image"];
          if (file1.ContentLength > 0)
          {
            string extension = Path.GetExtension(file2.FileName);
            if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Assessment/")))
              Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Assessment/"));
            string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/SKILLMUNI_DATA/Assessment/"), "aq_" + (object) question.id_assessment_question + extension);
            file1.SaveAs(filename);
            question.question_image = "aq_" + (object) question.id_assessment_question + extension;
            this.db.SaveChanges();
          }
        }
        this.db.SaveChanges();
        List<tbl_assessment_answer> list = this.db.tbl_assessment_answer.Where<tbl_assessment_answer>((Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_question == (int?) question.id_assessment_question)).ToList<tbl_assessment_answer>();
        tbl_assessment tblAssessment2 = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == question.id_assessment)).FirstOrDefault<tbl_assessment>();
        assessGroup = tblAssessment2.assess_group;
        int num2 = 3;
        if (!(assessGroup.GetValueOrDefault() == num2 & assessGroup.HasValue))
        {
          int num3 = 0;
          int num4 = 0;
          assessGroup = tblAssessment2.assess_group;
          int num5 = 1;
          if (assessGroup.GetValueOrDefault() == num5 & assessGroup.HasValue)
          {
            num4 = Convert.ToInt32(this.Request.Form["select-answer"].ToString());
            num3 = list.Count<tbl_assessment_answer>();
          }
          else
          {
            assessGroup = tblAssessment2.assess_group;
            int num6 = 2;
            if (assessGroup.GetValueOrDefault() == num6 & assessGroup.HasValue)
            {
              num3 = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == question.id_assessment)).ToList<tbl_assessment_scoring_key>().Count;
            }
            else
            {
              assessGroup = tblAssessment2.assess_group;
              int num7 = 4;
              if (assessGroup.GetValueOrDefault() == num7 & assessGroup.HasValue)
                num3 = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == question.id_assessment)).ToList<tbl_assessment_scoring_key>().Count;
            }
          }
          for (int index = 1; index <= num3; ++index)
          {
            int sAns = Convert.ToInt32(this.Request.Form["hid_answer_id_" + (object) index].ToString());
            tbl_assessment_answer assessmentAnswer = this.db.tbl_assessment_answer.Where<tbl_assessment_answer>((Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_answer == sAns)).FirstOrDefault<tbl_assessment_answer>();
            if (assessmentAnswer != null)
            {
              assessmentAnswer.answer_description = this.Request.Form["t-content-" + (object) index].ToString();
              assessmentAnswer.id_assessment_question = new int?(question.id_assessment_question);
              assessmentAnswer.id_assessment = question.id_assessment;
              assessmentAnswer.position = new int?(index);
              assessGroup = tblAssessment2.assess_group;
              int num8 = 4;
              assessmentAnswer.id_assessment_scoring_key = !(assessGroup.GetValueOrDefault() == num8 & assessGroup.HasValue) ? new int?(0) : new int?(Convert.ToInt32(this.Request.Form["t-scoring-key-" + (object) index].ToString()));
              assessmentAnswer.status = "A";
              assessmentAnswer.updated_date_time = new DateTime?(DateTime.Now);
              this.db.SaveChanges();
            }
            if (num4 == index)
            {
              question.aq_answer = Convert.ToString(assessmentAnswer.id_assessment_answer);
              this.db.SaveChanges();
            }
          }
        }
      }
      return "1";
    }

    public ActionResult edit_assessment_question(string id)
    {
      int ids = Convert.ToInt32(id);
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_assessment_question assessment = this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment_question == ids && t.id_organization == (int?) oid)).FirstOrDefault<tbl_assessment_question>();
      if (assessment == null)
        return (ActionResult) this.RedirectToAction("Index");
      tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == assessment.id_assessment)).FirstOrDefault<tbl_assessment>();
      List<tbl_assessment_answer> list = this.db.tbl_assessment_answer.Where<tbl_assessment_answer>((Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_question == (int?) assessment.id_assessment_question)).ToList<tbl_assessment_answer>();
      this.ViewData["assessment"] = (object) tblAssessment;
      this.ViewData["question"] = (object) assessment;
      this.ViewData["answers"] = (object) list;
      this.ViewData["scoringkey"] = (object) this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == assessment.id_assessment)).ToList<tbl_assessment_scoring_key>();
      return (ActionResult) this.View();
    }

    public int DeleteQuestion(int id)
    {
      tbl_assessment_question entity = this.db.tbl_assessment_question.Find(new object[1]
      {
        (object) id
      });
      if (entity == null)
        return 0;
      this.db.tbl_assessment_question.Remove(entity);
      this.db.SaveChanges();
      return 1;
    }

    public ActionResult publish_assessment()
    {
      tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
      {
        (object) Convert.ToInt32(this.Request.Form["asses-id"].ToString())
      });
      if (tblAssessment != null)
      {
        tblAssessment.status = "A";
        this.db.SaveChanges();
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult submit_assesment()
    {
      tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
      {
        (object) Convert.ToInt32(this.Request.Form["asses-id"].ToString())
      });
      if (tblAssessment != null && tblAssessment.status != "A")
      {
        if (this.Request.Form["btn_submit"].ToString().Equals("edit"))
          return (ActionResult) this.RedirectToAction("assessmentQuestions", "Assessment", (object) new
          {
            id = tblAssessment.id_assessment
          });
        this.publish_assessment();
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    [RoleAccessController(KEY = 8)]
    public ActionResult AssessmentSheets()
    {
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      this.ViewData["assessment"] = (object) this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_organization == (int?) oid)).ToList<tbl_assessment>();
      return (ActionResult) this.View();
    }

    public ActionResult createAssessmentSheets(string id)
    {
      int int32 = Convert.ToInt32(id);
      if (int32 > 0)
      {
        tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
        {
          (object) int32
        });
        int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
        this.ViewData["questionList"] = (object) this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.id_organization == (int?) orgid)).ToList<tbl_assessment_question>();
        this.ViewData["assessment"] = (object) tblAssessment;
      }
      return (ActionResult) this.View();
    }

    public string getAssessmentList()
    {
      string str1 = "";
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_assessment> list = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_organization == (int?) oid)).ToList<tbl_assessment>();
      string str2 = "";
      string assessmentList;
      if (list.Count > 0)
      {
        foreach (tbl_assessment tblAssessment in list)
        {
          str1 += "<tr>";
          str1 = str1 + "<td>" + tblAssessment.assessment_title + "</td>";
          str1 = str1 + "<td>" + tblAssessment.assesment_description + "</td>";
          string str3 = str1;
          DateTime dateTime = tblAssessment.assess_start.Value;
          string str4 = dateTime.ToString("dd-mm-yyyy");
          str1 = str3 + "<td>" + str4 + "</td>";
          string str5 = str1;
          dateTime = tblAssessment.assess_ended.Value;
          string str6 = dateTime.ToString("dd-mm-yyyy");
          str1 = str5 + "<td>" + str6 + "</td>";
          str1 += "</tr>";
        }
        assessmentList = "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>Assessment Title</th><th>Description</th><th>Start Date</th><th>End Date</th></tr></thead>" + "<tbody>" + str1 + "</tbody></table>";
      }
      else
        assessmentList = (str2 = "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>There are no Assessment in the Sheet</th></tr></thead>") + "</table>";
      return assessmentList;
    }

    public string getAssessmentQuestionList()
    {
      string str1 = "";
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_assessment_question> list = this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.id_organization == (int?) oid)).ToList<tbl_assessment_question>();
      string str2 = "";
      string assessmentQuestionList;
      if (list.Count > 0)
      {
        foreach (tbl_assessment_question assessmentQuestion in list)
        {
          str1 += "<tr>";
          str1 = str1 + "<td>" + assessmentQuestion.assessment_question + "</td>";
          str1 = str1 + "<td>" + assessmentQuestion.aq_answer + "</td>";
          str1 += "</tr>";
        }
        assessmentQuestionList = "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>Question</th><th>Answer Option</th> </tr></thead>" + "<tbody>" + str1 + "</tbody></table>";
      }
      else
        assessmentQuestionList = (str2 = "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>There are no Assessment Question in the Sheet</th></tr></thead>") + "</table>";
      return assessmentQuestionList;
    }

    public ActionResult LoadAssessment(string id)
    {
      int ids = Convert.ToInt32(id);
      Assessment assessment = new Assessment();
      tbl_assessment sheet = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == ids)).FirstOrDefault<tbl_assessment>();
      if (sheet == null)
        return (ActionResult) this.RedirectToAction("Index");
      assessment.tbl_assessment = sheet;
      List<AssessmentQuestion> assessmentQuestionList = new List<AssessmentQuestion>();
      DbSet<tbl_assessment_question> assessmentQuestion1 = this.db.tbl_assessment_question;
      Expression<Func<tbl_assessment_question, bool>> predicate = (Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment == (int?) sheet.id_assessment);
      foreach (tbl_assessment_question assessmentQuestion2 in assessmentQuestion1.Where<tbl_assessment_question>(predicate).ToList<tbl_assessment_question>())
      {
        tbl_assessment_question item = assessmentQuestion2;
        AssessmentQuestion assessmentQuestion3 = new AssessmentQuestion();
        List<tbl_assessment_answer> list = this.db.tbl_assessment_answer.Where<tbl_assessment_answer>((Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_question == (int?) item.id_assessment_question)).ToList<tbl_assessment_answer>();
        assessmentQuestion3.tbl_assessment_question = item;
        assessmentQuestion3.tbl_assessment_answer = list;
        assessmentQuestionList.Add(assessmentQuestion3);
      }
      assessment.assessment_question = assessmentQuestionList;
      this.ViewData["assessment"] = (object) assessment;
      return (ActionResult) this.View();
    }

    public ActionResult AssessmentToContent(string id)
    {
      int ids = Convert.ToInt32(id);
      tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == ids)).FirstOrDefault<tbl_assessment>();
      if (tblAssessment == null)
        return (ActionResult) this.RedirectToAction("Index");
      tbl_assessment_sheet AssSheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == ids)).FirstOrDefault<tbl_assessment_sheet>();
      this.ViewData["assessment"] = (object) tblAssessment;
      this.ViewData["assessment_sheet"] = (object) AssSheet;
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_assessment_mapping> list = this.db.tbl_assessment_mapping.Where<tbl_assessment_mapping>((Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_assessment_sheet == (int?) AssSheet.id_assessment_sheet && t.id_organization == (int?) orgid)).ToList<tbl_assessment_mapping>();
      List<tbl_content> tblContentList = new List<tbl_content>();
      foreach (tbl_assessment_mapping assessmentMapping in list)
      {
        tbl_assessment_mapping item = assessmentMapping;
        tbl_content tblContent = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => (int?) t.ID_CONTENT == item.id_content)).FirstOrDefault<tbl_content>();
        if (tblContent != null)
          tblContentList.Add(tblContent);
      }
      this.ViewData["CategoryList"] = (object) this.db.tbl_category.SqlQuery("select * from tbl_category where ID_ORGANIZATION=" + (object) orgid + " AND CATEGORY_TYPE in (0,1,2) AND status='A' order by CATEGORYNAME").ToList<tbl_category>();
      this.ViewData["contentlist"] = (object) tblContentList;
      return (ActionResult) this.View();
    }

    public string getAssessmentContent(string id, string pattern, string aid)
    {
      if (id == "")
        id = "0";
      int int32 = Convert.ToInt32(id);
      int aids = Convert.ToInt32(aid);
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      pattern = pattern == null ? "" : pattern.Replace("'", "''");
      string sql = "select * from tbl_content where true  ";
      if (int32 > 0)
        sql = sql + " and id_content in (select id_content from tbl_content_organization_mapping where id_category=" + (object) int32 + ")";
      if (!string.IsNullOrEmpty(pattern))
        sql = sql + "and upper(CONTENT_QUESTION) like '%" + pattern + "%'";
      List<tbl_content> list1 = this.db.tbl_content.SqlQuery(sql).Take<tbl_content>(100).ToList<tbl_content>();
      string str = "";
      foreach (tbl_content tblContent in list1)
      {
        tbl_content item = tblContent;
        tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assessment_sheet == aids)).FirstOrDefault<tbl_assessment_sheet>();
        this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == sheet.id_assesment)).FirstOrDefault<tbl_assessment>();
        List<tbl_assessment_mapping> list2 = this.db.tbl_assessment_mapping.Where<tbl_assessment_mapping>((Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_content == (int?) item.ID_CONTENT && t.id_organization == (int?) orgid)).ToList<tbl_assessment_mapping>();
        tbl_assessment_mapping assessmentMapping = this.db.tbl_assessment_mapping.Where<tbl_assessment_mapping>((Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_content == (int?) item.ID_CONTENT && t.id_assessment_sheet == (int?) sheet.id_assessment_sheet && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_mapping>();
        str = str + "<tr><td><a target=\"_blank\" href=" + this.Url.Action("LoadContent", "contentDashboard", (object) new
        {
          id = item.ID_CONTENT
        }) + ">" + item.CONTENT_QUESTION + "(" + (object) item.ID_CONTENT + ")</a></td>";
        str += "<td> ";
        str = assessmentMapping != null ? str + "Assessment Already attached" : (list2.Count != 0 ? str + "No. of Assessment attached : " + (object) list2.Count : str + "No Assessment attached");
        str += "</td><td> ";
        if (assessmentMapping == null)
          str = str + "  <a href=\"#\" onclick=\"addAssessmentToContent(" + (object) item.ID_CONTENT + ")\"><i class=\"glyphicon glyphicon-plus\"></i></a>";
        else
          str = str + "  <a href=\"#\" onclick=\"removeContenFromAssessment(" + (object) item.ID_CONTENT + ")\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
        str += "</td></tr>";
      }
      return "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr><th width=\"45%\">Content</th><th width=\"45%\"></th><th width=\"8%\"></th></tr></thead>" + "<tbody>" + str + "</tbody></table>";
    }

    public string addAssessmentToContent(string id, string cid)
    {
      int ids = Convert.ToInt32(id);
      int cids = Convert.ToInt32(cid);
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_content content = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => t.ID_CONTENT == cids)).FirstOrDefault<tbl_content>();
      if (content != null)
      {
        if (this.db.tbl_assessment_mapping.Where<tbl_assessment_mapping>((Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_content == (int?) content.ID_CONTENT && t.id_assessment_sheet == (int?) ids && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_mapping>() == null)
        {
          this.db.tbl_assessment_mapping.Add(new tbl_assessment_mapping()
          {
            id_assessment_sheet = new int?(ids),
            id_content = new int?(content.ID_CONTENT),
            id_organization = new int?(orgid),
            status = "A",
            updated_date_time = new DateTime?(DateTime.Now)
          });
          this.db.SaveChanges();
        }
      }
      return "";
    }

    public string removeContenFromAssessment(string id, string cid)
    {
      int ids = Convert.ToInt32(id);
      int cids = Convert.ToInt32(cid);
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_content content = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => t.ID_CONTENT == cids)).FirstOrDefault<tbl_content>();
      if (content != null)
      {
        tbl_assessment_mapping entity = this.db.tbl_assessment_mapping.Where<tbl_assessment_mapping>((Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_content == (int?) content.ID_CONTENT && t.id_assessment_sheet == (int?) ids && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_mapping>();
        if (entity != null)
        {
          this.db.tbl_assessment_mapping.Remove(entity);
          this.db.SaveChanges();
        }
      }
      return "";
    }

    public string deleteAssessment(string id)
    {
      int aids = Convert.ToInt32(id);
      if (aids > 0)
      {
        int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
        tbl_assessment assess = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == aids)).FirstOrDefault<tbl_assessment>();
        if (assess != null)
        {
          tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == assess.id_assessment)).FirstOrDefault<tbl_assessment_sheet>();
          DbSet<tbl_assessment_scoring_key> assessmentScoringKey1 = this.db.tbl_assessment_scoring_key;
          Expression<Func<tbl_assessment_scoring_key, bool>> predicate1 = (Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == (int?) assess.id_assessment);
          foreach (tbl_assessment_scoring_key assessmentScoringKey2 in assessmentScoringKey1.Where<tbl_assessment_scoring_key>(predicate1).ToList<tbl_assessment_scoring_key>())
          {
            assessmentScoringKey2.status = "D";
            this.db.SaveChanges();
          }
          DbSet<tbl_assessment_question> assessmentQuestion1 = this.db.tbl_assessment_question;
          Expression<Func<tbl_assessment_question, bool>> predicate2 = (Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment == (int?) assess.id_assessment && t.id_organization == (int?) orgid);
          foreach (tbl_assessment_question assessmentQuestion2 in assessmentQuestion1.Where<tbl_assessment_question>(predicate2).ToList<tbl_assessment_question>())
          {
            tbl_assessment_question item = assessmentQuestion2;
            item.status = "D";
            this.db.SaveChanges();
            DbSet<tbl_assessment_answer> assessmentAnswer1 = this.db.tbl_assessment_answer;
            Expression<Func<tbl_assessment_answer, bool>> predicate3 = (Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_question == (int?) item.id_assessment_question);
            foreach (tbl_assessment_answer assessmentAnswer2 in assessmentAnswer1.Where<tbl_assessment_answer>(predicate3).ToList<tbl_assessment_answer>())
            {
              assessmentAnswer2.status = "D";
              this.db.SaveChanges();
            }
            if (sheet != null)
            {
              DbSet<tbl_assessment_categoty_mapping> assessmentCategotyMapping1 = this.db.tbl_assessment_categoty_mapping;
              Expression<Func<tbl_assessment_categoty_mapping, bool>> predicate4 = (Expression<Func<tbl_assessment_categoty_mapping, bool>>) (t => t.id_assessment_sheet == sheet.id_assessment_sheet);
              foreach (tbl_assessment_categoty_mapping assessmentCategotyMapping2 in assessmentCategotyMapping1.Where<tbl_assessment_categoty_mapping>(predicate4).ToList<tbl_assessment_categoty_mapping>())
              {
                assessmentCategotyMapping2.status = "D";
                this.db.SaveChanges();
              }
              DbSet<tbl_assessment_mapping> assessmentMapping1 = this.db.tbl_assessment_mapping;
              Expression<Func<tbl_assessment_mapping, bool>> predicate5 = (Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_assessment_sheet == (int?) sheet.id_assessment_sheet && t.id_organization == (int?) orgid);
              foreach (tbl_assessment_mapping assessmentMapping2 in assessmentMapping1.Where<tbl_assessment_mapping>(predicate5).ToList<tbl_assessment_mapping>())
              {
                assessmentMapping2.status = "D";
                this.db.SaveChanges();
              }
              DbSet<tbl_assessment_push> tblAssessmentPush1 = this.db.tbl_assessment_push;
              Expression<Func<tbl_assessment_push, bool>> predicate6 = (Expression<Func<tbl_assessment_push, bool>>) (t => t.id_assesment_sheet == sheet.id_assessment_sheet);
              foreach (tbl_assessment_push tblAssessmentPush2 in tblAssessmentPush1.Where<tbl_assessment_push>(predicate6).ToList<tbl_assessment_push>())
              {
                tblAssessmentPush2.status = "D";
                this.db.SaveChanges();
              }
              DbSet<tbl_assessment_general> assessmentGeneral1 = this.db.tbl_assessment_general;
              Expression<Func<tbl_assessment_general, bool>> predicate7 = (Expression<Func<tbl_assessment_general, bool>>) (t => t.id_assesment_sheet == sheet.id_assessment_sheet && t.id_organization == (int?) orgid);
              foreach (tbl_assessment_general assessmentGeneral2 in assessmentGeneral1.Where<tbl_assessment_general>(predicate7).ToList<tbl_assessment_general>())
              {
                assessmentGeneral2.status = "D";
                this.db.SaveChanges();
              }
            }
          }
          if (sheet != null)
          {
            sheet.status = "D";
            this.db.SaveChanges();
          }
          assess.status = "D";
          this.db.SaveChanges();
        }
      }
      return "";
    }

    public ActionResult ContentToAssessment(string id)
    {
      int ids = Convert.ToInt32(id);
      List<tbl_assessment> tblAssessmentList1 = new List<tbl_assessment>();
      tbl_content content = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => t.ID_CONTENT == ids)).FirstOrDefault<tbl_content>();
      if (content == null)
        return (ActionResult) this.RedirectToAction("Index", "dashboard");
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_assessment> list1 = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_organization == (int?) orgid && t.status == "A")).ToList<tbl_assessment>();
      List<tbl_assessment_mapping> list2 = this.db.tbl_assessment_mapping.Where<tbl_assessment_mapping>((Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_content == (int?) content.ID_CONTENT && t.id_organization == (int?) orgid)).ToList<tbl_assessment_mapping>();
      List<tbl_assessment> tblAssessmentList2 = new List<tbl_assessment>();
      foreach (tbl_assessment_mapping assessmentMapping in list2)
      {
        tbl_assessment_mapping item = assessmentMapping;
        tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => (int?) t.id_assessment_sheet == item.id_assessment_sheet)).FirstOrDefault<tbl_assessment_sheet>();
        if (sheet != null)
        {
          tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == sheet.id_assesment)).FirstOrDefault<tbl_assessment>();
          if (tblAssessment != null)
            tblAssessmentList2.Add(tblAssessment);
        }
      }
      this.ViewData["assessData"] = (object) tblAssessmentList2;
      this.ViewData["assessment"] = (object) list1;
      this.ViewData["content"] = (object) content;
      return (ActionResult) this.View();
    }

    public ActionResult CategoryToAssessment()
    {
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_category> list1 = this.db.tbl_category.SqlQuery("select * from tbl_category where ID_ORGANIZATION=" + (object) orgid + " AND CATEGORY_TYPE in (0,1,2) AND status='A' order by CATEGORYNAME").ToList<tbl_category>();
      List<tbl_assessment_sheet> list2 = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_organization == (int?) orgid && t.status == "A")).ToList<tbl_assessment_sheet>();
      List<AssessmentSheet> assessmentSheetList = new List<AssessmentSheet>();
      foreach (tbl_assessment_sheet tblAssessmentSheet in list2)
      {
        tbl_assessment_sheet item = tblAssessmentSheet;
        tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == item.id_assesment && t.status == "A")).FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
          assessmentSheetList.Add(new AssessmentSheet()
          {
            Sheet = item,
            Assessment = tblAssessment
          });
      }
      this.ViewData["assessmentList"] = (object) assessmentSheetList;
      this.ViewData["category"] = (object) list1;
      return (ActionResult) this.View();
    }

    public string addAssessmentToCategory(string cid, string aid)
    {
      int cids = Convert.ToInt32(cid);
      int aids = Convert.ToInt32(aid);
      string category = "0";
      tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == aids && t.status == "A")).FirstOrDefault<tbl_assessment_sheet>();
      if (sheet != null)
      {
        if (this.db.tbl_assessment_categoty_mapping.Where<tbl_assessment_categoty_mapping>((Expression<Func<tbl_assessment_categoty_mapping, bool>>) (t => t.id_assessment_sheet == sheet.id_assessment_sheet && t.id_category == cids)).FirstOrDefault<tbl_assessment_categoty_mapping>() == null)
        {
          this.db.tbl_assessment_categoty_mapping.Add(new tbl_assessment_categoty_mapping()
          {
            id_category = cids,
            id_assessment_sheet = sheet.id_assessment_sheet,
            id_assessment = new int?(aids),
            status = "A",
            updated_date_time = new DateTime?(DateTime.Now)
          });
          this.db.SaveChanges();
          category = "1";
        }
        else
          category = "2";
      }
      return category;
    }

    public string removeAssessmentToCategory(string cid, string aid)
    {
      int cids = Convert.ToInt32(cid);
      int aids = Convert.ToInt32(aid);
      string category = "0";
      tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assessment_sheet == aids && t.status == "A")).FirstOrDefault<tbl_assessment_sheet>();
      if (sheet != null)
      {
        tbl_assessment_categoty_mapping entity = this.db.tbl_assessment_categoty_mapping.Where<tbl_assessment_categoty_mapping>((Expression<Func<tbl_assessment_categoty_mapping, bool>>) (t => t.id_assessment == (int?) sheet.id_assesment && t.id_category == cids)).FirstOrDefault<tbl_assessment_categoty_mapping>();
        if (entity != null)
        {
          this.db.tbl_assessment_categoty_mapping.Remove(entity);
          this.db.SaveChanges();
          category = "1";
        }
      }
      return category;
    }

    public string getAssessmentForCategoryList(string id)
    {
      int cids = Convert.ToInt32(id);
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<tbl_assessment_sheet> list = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_organization == (int?) orgid && t.status == "A")).ToList<tbl_assessment_sheet>();
      List<AssessmentSheet> assessmentSheetList = new List<AssessmentSheet>();
      foreach (tbl_assessment_sheet tblAssessmentSheet in list)
      {
        tbl_assessment_sheet item = tblAssessmentSheet;
        tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == item.id_assesment && t.status == "A")).FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
        {
          tbl_assessment_categoty_mapping assessmentCategotyMapping = this.db.tbl_assessment_categoty_mapping.Where<tbl_assessment_categoty_mapping>((Expression<Func<tbl_assessment_categoty_mapping, bool>>) (t => t.id_assessment == (int?) item.id_assesment && t.id_category == cids)).FirstOrDefault<tbl_assessment_categoty_mapping>();
          assessmentSheetList.Add(new AssessmentSheet()
          {
            Sheet = item,
            Assessment = tblAssessment,
            FLAG = assessmentCategotyMapping != null
          });
        }
      }
      string str = "";
      foreach (AssessmentSheet assessmentSheet in assessmentSheetList)
      {
        str = str + " <tr><td>" + assessmentSheet.Assessment.assessment_title + "</td>";
        str += " <td>";
        if (assessmentSheet.FLAG)
        {
          str = str + " <a  style=\"display:none;\" id=\"add_" + (object) assessmentSheet.Assessment.id_assessment + "\" href=\"#\" onclick=\"addAssessmentCategory('" + (object) assessmentSheet.Assessment.id_assessment + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
          str = str + " <i id=\"done_" + (object) assessmentSheet.Assessment.id_assessment + "\" class=\"glyphicon glyphicon-ok\"></i>";
          str = str + " <a id=\"link_" + (object) assessmentSheet.Assessment.id_assessment + "\" href=\"#\" onclick=\"removeAssessmentCategory('" + (object) assessmentSheet.Assessment.id_assessment + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
        }
        else
        {
          str = str + " <a id=\"add_" + (object) assessmentSheet.Assessment.id_assessment + "\" href=\"#\" onclick=\"addAssessmentCategory('" + (object) assessmentSheet.Assessment.id_assessment + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
          str = str + " <i style=\"display:none;\"  id=\"done_" + (object) assessmentSheet.Assessment.id_assessment + "\" class=\"glyphicon glyphicon-ok\"></i>";
          str = str + " <a style=\"display:none;\"  id=\"link_" + (object) assessmentSheet.Assessment.id_assessment + "\" href=\"#\" onclick=\"removeAssessmentCategory('" + (object) assessmentSheet.Assessment.id_assessment + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
        }
        str += " </td></tr>";
      }
      return "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"> <thead>  <tr><th width=\"95%\">Assesment</th><th width=\"5%\">Action</th> </tr></thead> <tbody>" + str + " </tbody></table>";
    }
  }
}
