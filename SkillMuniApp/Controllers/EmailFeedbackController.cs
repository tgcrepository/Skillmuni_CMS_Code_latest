// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.EmailFeedbackController
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
  public class EmailFeedbackController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private int mail_template_id_user = 1;
    private int mail_template_id_incharge = 2;

    public ActionResult Index()
    {
      this.ViewData["feedback"] = (object) this.db.Database.SqlQuery<Email_feedback_cms>("SELECT GROUP_CONCAT(b.role_name) as data, a.emp_id,a.emp_name,a.emp_emailID FROM tbl_email_feedback_user a, tbl_feedback_role b WHERE a.id_role = b.id_role GROUP BY  a.emp_id").ToList<Email_feedback_cms>();
      return (ActionResult) this.View();
    }

    public ActionResult add_email_feedback()
    {
      List<tbl_feedback_role> tblFeedbackRoleList = new List<tbl_feedback_role>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql = "SELECT * FROM tbl_feedback_role WHERE status='A'";
        tblFeedbackRoleList = m2ostDbContext.Database.SqlQuery<tbl_feedback_role>(sql).ToList<tbl_feedback_role>();
      }
      this.ViewData["role"] = (object) tblFeedbackRoleList;
      return (ActionResult) this.View();
    }

    public ActionResult add_email_feedback_action()
    {
      int int32_1 = Convert.ToInt32(this.Request.Form["hid-category"].ToString());
      Email_feedback_user emailFeedbackUser = new Email_feedback_user();
      for (int index = 1; index <= int32_1; ++index)
      {
        if (this.Request.Form["add-con-category-" + (object) index] != null)
        {
          int int32_2 = Convert.ToInt32(this.Request.Form["add-con-category-" + (object) index]);
          emailFeedbackUser.id_role = int32_2;
          emailFeedbackUser.emp_emailID = this.Request.Form["emailid"];
          emailFeedbackUser.emp_name = this.Request.Form["employeename"];
          emailFeedbackUser.emp_id = Convert.ToInt32(this.Request.Form["employeeid"]);
          emailFeedbackUser.status = "A";
          emailFeedbackUser.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_email_feedback_user(id_role,emp_emailID,emp_name,emp_id,status,updated_date_time) VALUES ({0},{1},{2},{3},{4},{5});", (object) emailFeedbackUser.id_role, (object) emailFeedbackUser.emp_emailID, (object) emailFeedbackUser.emp_name, (object) emailFeedbackUser.emp_id, (object) emailFeedbackUser.status, (object) emailFeedbackUser.updated_date_time);
        }
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult editEmailFeedback(int emp_id)
    {
      List<tbl_feedback_role> tblFeedbackRoleList1 = new List<tbl_feedback_role>();
      Email_feedback_user model = new Email_feedback_user();
      List<tbl_feedback_role> tblFeedbackRoleList2 = new List<tbl_feedback_role>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql1 = "SELECT * FROM tbl_feedback_role WHERE status='A'";
        tblFeedbackRoleList1 = m2ostDbContext.Database.SqlQuery<tbl_feedback_role>(sql1).ToList<tbl_feedback_role>();
        string sql2 = "SELECT * FROM tbl_email_feedback_user WHERE emp_id=" + (object) emp_id + " AND status='A'";
        model = m2ostDbContext.Database.SqlQuery<Email_feedback_user>(sql2).FirstOrDefault<Email_feedback_user>();
        string sql3 = "SELECT * FROM tbl_feedback_role WHERE status='A' and id_role in (SELECT id_role FROM tbl_email_feedback_user WHERE status='A' and emp_id='" + (object) emp_id + "')";
        tblFeedbackRoleList2 = m2ostDbContext.Database.SqlQuery<tbl_feedback_role>(sql3).ToList<tbl_feedback_role>();
      }
      this.ViewData["FeedbackRole"] = (object) tblFeedbackRoleList1;
      this.ViewData["map"] = (object) model;
      this.ViewData["employeeRoleList"] = (object) tblFeedbackRoleList2;
      return (ActionResult) this.View((object) model);
    }

    public ActionResult editEmailFeedbackAction(int emp_id)
    {
      int int32_1 = Convert.ToInt32(this.Request.Form["hid-category"].ToString());
      Email_feedback_user emailFeedbackUser = new Email_feedback_user();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_email_feedback_user SET status = 'D' WHERE emp_id =" + (object) emp_id ?? "");
      for (int index = 1; index <= int32_1; ++index)
      {
        if (this.Request.Form["add-con-category-" + (object) index] != null)
        {
          int int32_2 = Convert.ToInt32(this.Request.Form["add-con-category-" + (object) index]);
          emailFeedbackUser.id_role = int32_2;
          emailFeedbackUser.emp_emailID = this.Request.Form["emailid"];
          emailFeedbackUser.emp_name = this.Request.Form["employeename"];
          emailFeedbackUser.emp_id = Convert.ToInt32(this.Request.Form["employeeid"]);
          emailFeedbackUser.status = "A";
          emailFeedbackUser.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_email_feedback_user(id_role,emp_emailID,emp_name,emp_id,status,updated_date_time) VALUES ({0},{1},{2},{3},{4},{5});", (object) emailFeedbackUser.id_role, (object) emailFeedbackUser.emp_emailID, (object) emailFeedbackUser.emp_name, (object) emailFeedbackUser.emp_id, (object) emailFeedbackUser.status, (object) emailFeedbackUser.updated_date_time);
        }
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM tbl_email_feedback_user WHERE status='D'");
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult add_email_template()
    {
      List<tbl_feedback_role> tblFeedbackRoleList = new List<tbl_feedback_role>();
      tbl_email_template tblEmailTemplate = new tbl_email_template();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql1 = "SELECT * FROM tbl_feedback_role WHERE status='A'";
        tblFeedbackRoleList = m2ostDbContext.Database.SqlQuery<tbl_feedback_role>(sql1).ToList<tbl_feedback_role>();
        string sql2 = "SELECT * FROM tbl_email_template WHERE status='A'";
        tblEmailTemplate = m2ostDbContext.Database.SqlQuery<tbl_email_template>(sql2).FirstOrDefault<tbl_email_template>();
      }
      this.ViewData["role"] = (object) tblFeedbackRoleList;
      this.ViewData["temp"] = (object) tblEmailTemplate;
      return (ActionResult) this.View();
    }

    public ActionResult add_email_template_action()
    {
      int int32_1 = Convert.ToInt32(this.Request.Form["countBanr"].ToString());
      int int32_2 = Convert.ToInt32(this.Request.Form["countBanr1"].ToString());
      for (int i = 1; i <= int32_1; ++i)
        this.save_email_template(this.mail_template_id_user, i);
      for (int i = 1; i <= int32_2; ++i)
        this.save_email_template(this.mail_template_id_incharge, i);
      return (ActionResult) this.RedirectToAction("email_template");
    }

    public void save_email_template(int type, int i)
    {
      tbl_email_template_body emailTemplateBody = new tbl_email_template_body();
      emailTemplateBody.body_content = type != this.mail_template_id_incharge ? this.Request.Form["banner-url-" + (object) i].ToString() : this.Request.Form["banner-urlR-" + (object) i].ToString();
      emailTemplateBody.status = "A";
      emailTemplateBody.updated_date_time = DateTime.Now;
      emailTemplateBody.id_email_template = Convert.ToInt32(this.Request.Form["rolename"].ToString());
      emailTemplateBody.mail_template = type;
      emailTemplateBody.sequence = i;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string str = m2ostDbContext.Database.SqlQuery<string>("select id_email_template from tbl_email_template where id_role ={0}", (object) emailTemplateBody.id_email_template).FirstOrDefault<string>();
        m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_email_template_body(id_email_template,body_content,status,updated_date_time,mail_template,sequence) VALUES ({0},{1},{2},{3},{4},{5});", (object) str, (object) emailTemplateBody.body_content, (object) emailTemplateBody.status, (object) emailTemplateBody.updated_date_time, (object) emailTemplateBody.mail_template, (object) emailTemplateBody.sequence);
      }
    }

    public ActionResult email_template()
    {
      this.ViewData["temp"] = (object) this.db.Database.SqlQuery<Email_template_cms>("SELECT GROUP_CONCAT(b.body_content) AS data, a.id_email_template,c.role_name FROM tbl_email_template_body b, tbl_email_template a, tbl_feedback_role c WHERE a.id_email_template = b.id_email_template and a.id_role = c.id_role GROUP BY a.id_role").ToList<Email_template_cms>();
      return (ActionResult) this.View();
    }

    public ActionResult edit_Email_Feedback_Template(int id_email_template)
    {
      List<tbl_feedback_role> tblFeedbackRoleList = new List<tbl_feedback_role>();
      tbl_email_template model = new tbl_email_template();
      List<tbl_email_template_body> emailTemplateBodyList1 = new List<tbl_email_template_body>();
      List<tbl_email_template_body> emailTemplateBodyList2 = new List<tbl_email_template_body>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string sql1 = "SELECT * FROM tbl_email_template_body WHERE id_email_template = " + (object) id_email_template + " AND mail_template = 2 AND status='A' ORDER BY sequence";
        emailTemplateBodyList1 = m2ostDbContext.Database.SqlQuery<tbl_email_template_body>(sql1).ToList<tbl_email_template_body>();
        string sql2 = "SELECT * FROM tbl_email_template_body WHERE id_email_template = " + (object) id_email_template + " AND mail_template = 1 AND status='A' ORDER BY sequence";
        emailTemplateBodyList2 = m2ostDbContext.Database.SqlQuery<tbl_email_template_body>(sql2).ToList<tbl_email_template_body>();
        string sql3 = "SELECT * FROM tbl_email_template WHERE id_email_template = " + (object) id_email_template + " AND status='A'";
        model = m2ostDbContext.Database.SqlQuery<tbl_email_template>(sql3).FirstOrDefault<tbl_email_template>();
        string sql4 = "SELECT * FROM tbl_feedback_role WHERE status='A'";
        tblFeedbackRoleList = m2ostDbContext.Database.SqlQuery<tbl_feedback_role>(sql4).ToList<tbl_feedback_role>();
      }
      this.ViewData["role"] = (object) tblFeedbackRoleList;
      this.ViewData["templateincharge"] = (object) emailTemplateBodyList1;
      this.ViewData["templateuser"] = (object) emailTemplateBodyList2;
      this.ViewData["template"] = (object) model;
      return (ActionResult) this.View((object) model);
    }

    public ActionResult edit_Email_Feedback_Template_Action()
    {
      tbl_email_template_body emailTemplateBody = new tbl_email_template_body();
      int int32_1 = Convert.ToInt32(this.Request.Form["countBanr"].ToString());
      int int32_2 = Convert.ToInt32(this.Request.Form["countBanr1"].ToString());
      emailTemplateBody.id_email_template = Convert.ToInt32(this.Request.Form["rolename"].ToString());
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string str = m2ostDbContext.Database.SqlQuery<string>("select id_email_template from tbl_email_template where id_role ={0}", (object) emailTemplateBody.id_email_template).FirstOrDefault<string>();
        m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_email_template_body SET status ='D' WHERE id_email_template=" + str + " ");
      }
      for (int i = 1; i <= int32_1; ++i)
        this.update_email_template(this.mail_template_id_user, i);
      for (int i = 1; i <= int32_2; ++i)
        this.update_email_template(this.mail_template_id_incharge, i);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("DELETE FROM tbl_email_template_body WHERE status='D'");
      return (ActionResult) this.RedirectToAction("email_template");
    }

    public void update_email_template(int type, int i)
    {
      tbl_email_template_body emailTemplateBody = new tbl_email_template_body();
      emailTemplateBody.body_content = type != this.mail_template_id_incharge ? this.Request.Form["banner-url-" + (object) i].ToString() : this.Request.Form["banner-urlR-" + (object) i].ToString();
      emailTemplateBody.status = "A";
      emailTemplateBody.updated_date_time = DateTime.Now;
      emailTemplateBody.id_email_template = Convert.ToInt32(this.Request.Form["rolename"].ToString());
      emailTemplateBody.mail_template = type;
      emailTemplateBody.sequence = i;
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        string str = m2ostDbContext.Database.SqlQuery<string>("select id_email_template from tbl_email_template where id_role ={0}", (object) emailTemplateBody.id_email_template).FirstOrDefault<string>();
        m2ostDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_email_template_body(id_email_template,body_content,status,updated_date_time,mail_template,sequence) VALUES ({0},{1},{2},{3},{4},{5});", (object) str, (object) emailTemplateBody.body_content, (object) emailTemplateBody.status, (object) emailTemplateBody.updated_date_time, (object) emailTemplateBody.mail_template, (object) emailTemplateBody.sequence);
      }
    }
  }
}
