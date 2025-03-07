// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.NotificationController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class NotificationController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index(int flag = 0)
    {
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      this.ViewData["notification"] = (object) this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_organization == oid)).ToList<tbl_notification>();
      this.ViewData[nameof (flag)] = (object) flag;
      return (ActionResult) this.View();
    }

    public ActionResult createNotification()
    {
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      DateTime? sysDate = new DateTime?(DateTime.Now);
      this.ViewData["notification"] = (object) this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_organization == oid && t.notification_type == 4 && t.status == "A" && t.end_date >= sysDate)).ToList<tbl_notification>();
      return (ActionResult) this.View();
    }

    public ActionResult add_notification()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(this.Request.Form["notification-type"]);
      int int32_3 = Convert.ToInt32(this.Request.Form["availablity-div"]);
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (int32_2 == 4)
      {
        num2 = Convert.ToInt32(this.Request.Form["reminder-timegap"]);
        num3 = Convert.ToInt32(this.Request.Form["reminder-frequency"]);
      }
      tbl_notification entity1 = new tbl_notification();
      entity1.notification_name = this.Request.Form["notification-title"];
      entity1.notification_description = this.Request.Form["notification-desc"];
      entity1.notification_message = this.Request.Form["notification-message"];
      entity1.notification_type = int32_2;
      entity1.notification_action_type = "NA";
      entity1.created_date = new DateTime?(new Utility().StringToDatetime(this.Request.Form["notification-created"].ToString()));
      entity1.start_date = new DateTime?(new Utility().StringToDatetime(this.Request.Form["notification-started"].ToString()));
      entity1.end_date = new DateTime?(new Utility().StringToDatetime(this.Request.Form["notification-ended"].ToString()));
      entity1.reminder_flag = num1;
      entity1.reminder_frequency = num3;
      entity1.reminder_time = num2;
      entity1.id_organization = int32_1;
      entity1.notification_key = this.GenerateTransaction();
      entity1.status = int32_3 != 1 ? "N" : "A";
      entity1.updated_date_time = new DateTime?(DateTime.Now);
      this.db.tbl_notification.Add(entity1);
      this.db.SaveChanges();
      if (int32_2 == 4)
      {
        int int32_4 = Convert.ToInt32(this.Request.Form["setup-type"]);
        int num4;
        int num5;
        int num6;
        int num7;
        if (int32_4 == 1)
        {
          num4 = 0;
          num5 = 0;
          num6 = 0;
          num7 = 0;
        }
        else
        {
          num4 = Convert.ToInt32(this.Request.Form["select-DH"]);
          num5 = Convert.ToInt32(this.Request.Form["select-YH"]);
          num6 = Convert.ToInt32(this.Request.Form["select-YM"]);
          num7 = Convert.ToInt32(this.Request.Form["select-TM"]);
        }
        this.db.tbl_reminder_notification_config.Add(new tbl_reminder_notification_config()
        {
          id_notification = new int?(entity1.id_notification),
          reminder_type = new int?(int32_4),
          DH = new int?(num4),
          YH = new int?(num5),
          YM = new int?(num6),
          TM = new int?(num7),
          status = "A",
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
      }
      else if (Convert.ToInt32(this.Request.Form["reminder-flag"]) == 1)
      {
        tbl_notification_reminder entity2 = new tbl_notification_reminder();
        entity2.id_notification = new int?(entity1.id_notification);
        int int32_5 = Convert.ToInt32(this.Request.Form["reminder-notification"]);
        entity2.id_reminder_notification = new int?(int32_5);
        entity2.reminder_frequency = new int?(entity1.reminder_frequency);
        entity2.reminder_timeout = new int?(entity1.reminder_time);
        entity2.status = "A";
        entity2.updated_date_time = new DateTime?(DateTime.Now);
        this.db.tbl_notification_reminder.Add(entity2);
        this.db.SaveChanges();
      }
      return (ActionResult) this.RedirectToAction("Index", (object) new
      {
        flag = 1
      });
    }

    public string GenerateTransaction()
    {
      int num1 = 999999;
      int num2 = 100000;
      int num3 = new Random().Next(1, 13);
      Math.Round((double) (num3 * (num1 - num2 + 1) + num2));
      string str = DateTime.Now.ToString("yyyyMMddHHmmss");
      return "NTF" + (object) num3 + str;
    }

    public ActionResult editNotification(string id)
    {
      int ids = Convert.ToInt32(id);
      tbl_notification current = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == ids)).FirstOrDefault<tbl_notification>();
      this.ViewData["notification"] = (object) current;
      int oid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      DateTime? sysDate = new DateTime?(DateTime.Now);
      this.ViewData["notificationList"] = (object) this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_organization == oid && t.notification_type == 4 && t.status == "A" && t.end_date > sysDate)).ToList<tbl_notification>();
      int num = 0;
      tbl_reminder_notification_config notificationConfig = new tbl_reminder_notification_config();
      if (current.notification_type == 4)
      {
        tbl_notification_reminder notificationReminder = this.db.tbl_notification_reminder.Where<tbl_notification_reminder>((Expression<Func<tbl_notification_reminder, bool>>) (t => t.id_notification == (int?) current.id_notification)).FirstOrDefault<tbl_notification_reminder>();
        if (notificationReminder != null)
          num = Convert.ToInt32((object) notificationReminder.id_notification);
        notificationConfig = this.db.tbl_reminder_notification_config.Where<tbl_reminder_notification_config>((Expression<Func<tbl_reminder_notification_config, bool>>) (t => t.id_notification == (int?) current.id_notification)).FirstOrDefault<tbl_reminder_notification_config>();
      }
      this.ViewData["reminder"] = (object) num;
      this.ViewData["reminderData"] = (object) notificationConfig;
      return (ActionResult) this.View();
    }

    public ActionResult edit_notification()
    {
      int int32_1 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      int id_notification = Convert.ToInt32(this.Request.Form["id_notification"]);
      int int32_2 = Convert.ToInt32(this.Request.Form["notification-type"]);
      int int32_3 = Convert.ToInt32(this.Request.Form["availablity-div"]);
      int int32_4 = Convert.ToInt32(this.Request.Form["reminder-flag"]);
      tbl_notification notify = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == id_notification)).FirstOrDefault<tbl_notification>();
      if (notify != null)
      {
        notify.notification_name = this.Request.Form["notification-title"];
        notify.notification_description = this.Request.Form["notification-desc"];
        notify.notification_message = this.Request.Form["notification-message"];
        notify.notification_type = int32_2;
        notify.created_date = new DateTime?(new Utility().StringToDatetime(this.Request.Form["notification-created"].ToString()));
        notify.start_date = new DateTime?(new Utility().StringToDatetime(this.Request.Form["notification-started"].ToString()));
        notify.end_date = new DateTime?(new Utility().StringToDatetime(this.Request.Form["notification-ended"].ToString()));
        notify.reminder_flag = int32_4;
        int num1 = 0;
        int num2 = 0;
        notify.reminder_frequency = num2;
        notify.reminder_time = num1;
        notify.id_organization = int32_1;
        notify.status = int32_3 != 1 ? "N" : "A";
        notify.updated_date_time = new DateTime?(DateTime.Now);
        this.db.SaveChanges();
        if (int32_2 == 4)
        {
          int int32_5 = Convert.ToInt32(this.Request.Form["setup-type"]);
          int num3;
          int num4;
          int num5;
          int num6;
          if (int32_5 == 1)
          {
            num3 = 0;
            num4 = 0;
            num5 = 0;
            num6 = 0;
          }
          else
          {
            num3 = Convert.ToInt32(this.Request.Form["select-DH"]);
            num4 = Convert.ToInt32(this.Request.Form["select-YH"]);
            num5 = Convert.ToInt32(this.Request.Form["select-YM"]);
            num6 = Convert.ToInt32(this.Request.Form["select-TM"]);
          }
          tbl_reminder_notification_config notificationConfig = this.db.tbl_reminder_notification_config.Where<tbl_reminder_notification_config>((Expression<Func<tbl_reminder_notification_config, bool>>) (t => t.id_notification == (int?) notify.id_notification)).FirstOrDefault<tbl_reminder_notification_config>();
          if (notificationConfig == null)
          {
            this.db.tbl_reminder_notification_config.Add(new tbl_reminder_notification_config()
            {
              id_notification = new int?(notify.id_notification),
              reminder_type = new int?(int32_5),
              DH = new int?(num3),
              YH = new int?(num4),
              YM = new int?(num5),
              TM = new int?(num6),
              status = "A",
              updated_date_time = new DateTime?(DateTime.Now)
            });
            this.db.SaveChanges();
          }
          else
          {
            notificationConfig.id_notification = new int?(notify.id_notification);
            notificationConfig.reminder_type = new int?(int32_5);
            notificationConfig.DH = new int?(num3);
            notificationConfig.YH = new int?(num4);
            notificationConfig.YM = new int?(num5);
            notificationConfig.TM = new int?(num6);
            notificationConfig.status = "A";
            notificationConfig.updated_date_time = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
        }
        else if (Convert.ToInt32(this.Request.Form["reminder-flag"]) == 1)
        {
          tbl_notification_reminder entity = this.db.tbl_notification_reminder.Where<tbl_notification_reminder>((Expression<Func<tbl_notification_reminder, bool>>) (t => t.id_notification == (int?) notify.id_notification)).FirstOrDefault<tbl_notification_reminder>();
          entity.id_notification = new int?(notify.id_notification);
          int int32_6 = Convert.ToInt32(this.Request.Form["reminder-notification"]);
          entity.id_reminder_notification = new int?(int32_6);
          entity.reminder_frequency = new int?(notify.reminder_frequency);
          entity.reminder_timeout = new int?(notify.reminder_time);
          entity.status = "A";
          entity.updated_date_time = new DateTime?(DateTime.Now);
          this.db.tbl_notification_reminder.Add(entity);
          this.db.SaveChanges();
        }
      }
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult LoadNotification(string id)
    {
      int ids = Convert.ToInt32(id);
      tbl_notification notification = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == ids)).FirstOrDefault<tbl_notification>();
      this.ViewData["notification"] = (object) notification;
      List<NotificationUserList> notificationUserListList = new List<NotificationUserList>();
      List<tbl_notification_config> list = this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_notification == (int?) notification.id_notification)).ToList<tbl_notification_config>();
      if (list != null)
      {
        foreach (tbl_notification_config notificationConfig in list)
        {
          tbl_notification_config item = notificationConfig;
          NotificationUserList notificationUserList = new NotificationUserList();
          this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => (int?) t.ID_USER == item.id_user)).FirstOrDefault<tbl_user>();
          tbl_profile tblProfile = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => (int?) t.ID_USER == item.id_user)).FirstOrDefault<tbl_profile>();
          notificationUserList.USER = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
          notificationUserList.STATUS = !(item.status == "R") ? "Unread" : "Read";
          notificationUserListList.Add(notificationUserList);
        }
      }
      this.ViewData["noteList"] = (object) notificationUserListList;
      return (ActionResult) this.View();
    }

    public ActionResult NotificationAttachment(string id)
    {
      int ids = Convert.ToInt32(id);
      tbl_notification current = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == ids)).FirstOrDefault<tbl_notification>();
      this.ViewData["notification"] = (object) current;
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_notification tblNotification = (tbl_notification) null;
      if (current.notification_type == 4)
      {
        tbl_notification_reminder remData = this.db.tbl_notification_reminder.Where<tbl_notification_reminder>((Expression<Func<tbl_notification_reminder, bool>>) (t => t.id_notification == (int?) current.id_notification)).FirstOrDefault<tbl_notification_reminder>();
        if (remData != null)
          tblNotification = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => (int?) t.id_notification == remData.id_reminder_notification)).FirstOrDefault<tbl_notification>();
      }
      this.ViewData["reminder"] = (object) tblNotification;
      this.ViewData["RoleList"] = (object) this.db.tbl_csst_role.SqlQuery("select * from tbl_csst_role where ID_ORGANIZATION=" + (object) int32 + " and status='A'").ToList<tbl_csst_role>();
      return (ActionResult) this.View();
    }

    public string getUserListForNotification(string id, string pattern, string cid, string type)
    {
      int ids = Convert.ToInt32(id);
      int int32_1 = Convert.ToInt32(cid);
      int int32_2 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      pattern = pattern.Replace("'", "''");
      string str1 = "";
      if (type == "1")
        str1 = " and id_user in (select id_user from tbl_role_user_mapping  where id_csst_role=" + (object) int32_1 + ") ";
      string sql = "select * from tbl_user where id_organization=" + (object) int32_2 + " and  id_user in (select id_user from tbl_profile where upper(FIRSTNAME) like '%" + pattern + "%' or upper(LASTNAME) like '%" + pattern + "%')  and upper(USERID) like '%" + pattern + "%' " + str1;
      List<int> intList = new List<int>();
      List<tbl_user> list = this.db.tbl_user.SqlQuery(sql).ToList<tbl_user>();
      bool flag = false;
      string str2 = "";
      string str3 = "";
      string str4 = "";
      foreach (tbl_user tblUser in list)
      {
        tbl_user item = tblUser;
        tbl_profile tblProfile = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == item.ID_USER)).FirstOrDefault<tbl_profile>();
        if (tblProfile != null)
        {
          str4 = str4 + "<tr><td>" + tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME + " (" + item.USERID + ") </td>";
          str4 += "<td>";
          if (this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_notification == (int?) ids & t.id_user == (int?) item.ID_USER)).FirstOrDefault<tbl_notification_config>() == null)
          {
            str4 = str4 + " <a id=\"link_" + (object) item.ID_USER + "\" href=\"javascript:void(0)\" onclick=\"sensNotificationToUser('" + (object) item.ID_USER + "')\"><i class=\"glyphicon glyphicon-send\"></i></a>";
            str4 = str4 + "<i style=\"display:none;\" id=\"done_" + (object) item.ID_USER + "\" class=\"glyphicon glyphicon-ok\"></i>";
          }
          else
            str4 += "<h5>Notification Sent</h5>";
          str4 += "</td>";
          str4 += "</tr>";
        }
      }
      string str5 = "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr>" + " <th width=\"80%\">User Info</th>" + "<th  width=\"20%\">Send Notification</th>" + "</thead>" + "<tbody>" + str4 + "</tbody></table>";
      if (flag)
      {
          str5 =
              " <div class=\"row\" id=\"div-remove\" >   <div class=\"col-md-12\">   <div class=\"alert alert-info alert-dismissable\">   <input id=\"program-assignment\" type=\"button\" class=\"btn btn-primary btn-sm\" value=\"Remove Program From Role\" onclick=\"removeProgramToRole(0)\" /><strong>&nbsp;&nbsp; Click to Remove Role from  Program  </strong>   </div>   </div>   </div><hr/>" +
              str5;
          str5 +=
              "<div class='row'>  <div class='form-group'> <div class='col-md-2'><label class='control-label'> Start Date</label></div> <div class='col-md-4'>  <div class='input-group date'> <input type='text' class='form-control validate[required]' id='datetimepicker1' name='start-date' value='" +
              str2 +
              "' /> <span class='input-group-addon'><span class='glyphicon glyphicon-calendar'></span> </span>  </div> </div> <div class='col-md-2'><label class='control-label'>Expiry Date</label></div> <div class='col-md-4'>  <div class='input-group date'> <input type='text' class='form-control validate[required]' id='datetimepicker2' name='expiry-date'  value='" +
              str3 +
              "' /> <span class='input-group-addon'><span class='glyphicon glyphicon-calendar'></span> </span>  </div> </div>  </div>   </div>";
      }

      return "<hr/>" + str5;
    }

    public string sendNotificationToUser(string nid, string uid)
    {
      int id_config = 0;
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      Convert.ToInt32(content.id_ORGANIZATION);
      int int32 = Convert.ToInt32(content.ID_USER);
      int suid = Convert.ToInt32(uid);
      int nids = Convert.ToInt32(nid);
      tbl_notification note = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == nids && t.status == "A")).FirstOrDefault<tbl_notification>();
      tbl_profile mailId = new addCMS_CategoryModel().get_mail_id(suid);
      if (note != null)
      {
        if (this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_notification == (int?) note.id_notification && t.id_user == (int?) suid && t.id_category == (int?) 0 && t.id_content == (int?) 0 && t.id_assessment == (int?) 0)).FirstOrDefault<tbl_notification_config>() == null)
        {
          tbl_notification_config entity = new tbl_notification_config();
          entity.id_assessment = new int?(0);
          entity.id_category = new int?(0);
          entity.id_content = new int?(0);
          entity.id_notification = new int?(nids);
          entity.id_user = new int?(suid);
          entity.id_creater = new int?(int32);
          entity.notification_key = note.notification_key;
          entity.notification_action_type = "GEN";
          entity.read_timestamp = new DateTime?(DateTime.Now);
          entity.updated_date_time = new DateTime?(DateTime.Now);
          entity.user_type = "1";
          entity.status = "A";
          this.db.tbl_notification_config.Add(entity);
          this.db.SaveChanges();
          id_config = entity.id_notification_config;
          string msg = "";
          msg = note.notification_message;
          DbSet<tbl_user_gcm_log> tblUserGcmLog1 = this.db.tbl_user_gcm_log;
          Expression<Func<tbl_user_gcm_log, bool>> predicate = (Expression<Func<tbl_user_gcm_log, bool>>) (t => t.id_user == (int?) suid);
          foreach (tbl_user_gcm_log tblUserGcmLog2 in tblUserGcmLog1.Where<tbl_user_gcm_log>(predicate).ToList<tbl_user_gcm_log>())
          {
            tbl_user_gcm_log gcm = tblUserGcmLog2;
            if (!string.IsNullOrEmpty(gcm.GCMID))
              Task.Run<Notification>((Func<Notification>) (() => new Notification(gcm.GCMID, msg)));
          }
        }
      }
      if (mailId.EMAIL != "" && note != null)
        new addCMS_CategoryModel().sendmail(mailId, note, id_config);
      return "1";
    }

    public string sendNotificationToRole(string nid, string uid)
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(content.ID_USER);
      int nids = Convert.ToInt32(nid);
      tbl_notification note = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == nids && t.status == "A")).FirstOrDefault<tbl_notification>();
      if (note != null)
      {
        int id_config = 0;
        object[] objArray = new object[5]
        {
          (object) "select * from tbl_user where id_organization=",
          (object) int32_1,
          (object) " and  id_user in (select id_user from tbl_role_user_mapping  where id_csst_role=",
          (object) uid,
          (object) ") "
        };
        foreach (tbl_user tblUser in this.db.tbl_user.SqlQuery(string.Concat(objArray)).ToList<tbl_user>())
        {
          tbl_user item = tblUser;
          tbl_profile mailId = new addCMS_CategoryModel().get_mail_id(item.ID_USER);
          if (this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_notification == (int?) note.id_notification && t.id_user == (int?) item.ID_USER && t.id_category == (int?) 0 && t.id_content == (int?) 0 && t.id_assessment == (int?) 0)).FirstOrDefault<tbl_notification_config>() == null)
          {
            tbl_notification_config entity = new tbl_notification_config();
            entity.id_assessment = new int?(0);
            entity.id_category = new int?(0);
            entity.id_content = new int?(0);
            entity.id_notification = new int?(nids);
            entity.id_user = new int?(item.ID_USER);
            entity.id_creater = new int?(int32_2);
            entity.notification_key = note.notification_key;
            entity.notification_action_type = "GEN";
            entity.read_timestamp = new DateTime?(DateTime.Now);
            entity.updated_date_time = new DateTime?(DateTime.Now);
            entity.user_type = "1";
            entity.status = "A";
            this.db.tbl_notification_config.Add(entity);
            this.db.SaveChanges();
            id_config = entity.id_notification_config;
            string msg = "";
            msg = note.notification_message;
            DbSet<tbl_user_gcm_log> tblUserGcmLog1 = this.db.tbl_user_gcm_log;
            Expression<Func<tbl_user_gcm_log, bool>> predicate = (Expression<Func<tbl_user_gcm_log, bool>>) (t => t.id_user == (int?) item.ID_USER);
            foreach (tbl_user_gcm_log tblUserGcmLog2 in tblUserGcmLog1.Where<tbl_user_gcm_log>(predicate).ToList<tbl_user_gcm_log>())
            {
              tbl_user_gcm_log gcm = tblUserGcmLog2;
              if (!string.IsNullOrEmpty(gcm.GCMID))
                Task.Run<Notification>((Func<Notification>) (() => new Notification(gcm.GCMID, msg)));
            }
          }
          if (mailId.EMAIL != "" && note != null)
            new addCMS_CategoryModel().sendmail(mailId, note, id_config);
        }
      }
      return "1";
    }

    public string sensNotificationToUserString(string nid, string value, string type)
    {
      int id_config = 0;
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(content.ID_USER);
      int nids = Convert.ToInt32(nid);
      tbl_notification note = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == nids && t.status == "A")).FirstOrDefault<tbl_notification>();
      if (note != null)
      {
        object[] objArray = new object[9]
        {
          (object) "select * from tbl_user where id_organization= ",
          (object) int32_1,
          (object) " and id_user in (select id_user from tbl_profile where upper(FIRSTNAME) like '%",
          (object) value,
          (object) "%' or upper(LASTNAME) like '%",
          (object) value,
          (object) "%')  and upper(USERID) like '%",
          (object) value,
          (object) "%' "
        };
        foreach (tbl_user tblUser in this.db.tbl_user.SqlQuery(string.Concat(objArray)).ToList<tbl_user>())
        {
          tbl_user item = tblUser;
          tbl_profile mailId = new addCMS_CategoryModel().get_mail_id(item.ID_USER);
          if (this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_notification == (int?) note.id_notification && t.id_user == (int?) item.ID_USER && t.id_category == (int?) 0 && t.id_content == (int?) 0 && t.id_assessment == (int?) 0)).FirstOrDefault<tbl_notification_config>() == null)
          {
            tbl_notification_config entity = new tbl_notification_config();
            entity.id_assessment = new int?(0);
            entity.id_category = new int?(0);
            entity.id_content = new int?(0);
            entity.id_notification = new int?(nids);
            entity.id_user = new int?(item.ID_USER);
            entity.id_creater = new int?(int32_2);
            entity.notification_key = note.notification_key;
            entity.notification_action_type = "GEN";
            entity.read_timestamp = new DateTime?(DateTime.Now);
            entity.updated_date_time = new DateTime?(DateTime.Now);
            entity.user_type = "1";
            entity.status = "A";
            this.db.tbl_notification_config.Add(entity);
            this.db.SaveChanges();
            id_config = entity.id_notification_config;
            string msg = "";
            msg = note.notification_message;
            DbSet<tbl_user_gcm_log> tblUserGcmLog1 = this.db.tbl_user_gcm_log;
            Expression<Func<tbl_user_gcm_log, bool>> predicate = (Expression<Func<tbl_user_gcm_log, bool>>) (t => t.id_user == (int?) item.ID_USER);
            foreach (tbl_user_gcm_log tblUserGcmLog2 in tblUserGcmLog1.Where<tbl_user_gcm_log>(predicate).ToList<tbl_user_gcm_log>())
            {
              tbl_user_gcm_log gcm = tblUserGcmLog2;
              if (!string.IsNullOrEmpty(gcm.GCMID))
                Task.Run<Notification>((Func<Notification>) (() => new Notification(gcm.GCMID, msg)));
            }
          }
          if (mailId.EMAIL != "" && note != null)
            new addCMS_CategoryModel().sendmail(mailId, note, id_config);
        }
      }
      return "1";
    }

    public string sendNotificationToAllUser(string nid, string value, string type)
    {
      int id_config = 0;
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int int32_1 = Convert.ToInt32(content.id_ORGANIZATION);
      int int32_2 = Convert.ToInt32(content.ID_USER);
      int nids = Convert.ToInt32(nid);
      tbl_notification note = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => t.id_notification == nids && t.status == "A")).FirstOrDefault<tbl_notification>();
      if (note != null)
      {
        object[] objArray = new object[5]
        {
          (object) "select * from tbl_user where  id_organization= ",
          (object) int32_1,
          (object) " and id_user in (select distinct id_user from tbl_role_user_mapping where id_csst_role in (select id_csst_role from tbl_csst_role where id_organization=",
          (object) int32_1,
          (object) "))"
        };
        foreach (tbl_user tblUser in this.db.tbl_user.SqlQuery(string.Concat(objArray)).ToList<tbl_user>())
        {
          tbl_user item = tblUser;
          tbl_profile mailId = new addCMS_CategoryModel().get_mail_id(item.ID_USER);
          if (this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_notification == (int?) note.id_notification && t.id_user == (int?) item.ID_USER && t.id_category == (int?) 0 && t.id_content == (int?) 0 && t.id_assessment == (int?) 0)).FirstOrDefault<tbl_notification_config>() == null)
          {
            tbl_notification_config entity = new tbl_notification_config();
            entity.id_assessment = new int?(0);
            entity.id_category = new int?(0);
            entity.id_content = new int?(0);
            entity.id_notification = new int?(nids);
            entity.id_user = new int?(item.ID_USER);
            entity.id_creater = new int?(int32_2);
            entity.notification_key = note.notification_key;
            entity.notification_action_type = "GEN";
            entity.read_timestamp = new DateTime?(DateTime.Now);
            entity.updated_date_time = new DateTime?(DateTime.Now);
            entity.user_type = "1";
            entity.status = "A";
            this.db.tbl_notification_config.Add(entity);
            this.db.SaveChanges();
            id_config = entity.id_notification_config;
            string msg = "";
            msg = note.notification_message;
            DbSet<tbl_user_gcm_log> tblUserGcmLog1 = this.db.tbl_user_gcm_log;
            Expression<Func<tbl_user_gcm_log, bool>> predicate = (Expression<Func<tbl_user_gcm_log, bool>>) (t => t.id_user == (int?) item.ID_USER);
            foreach (tbl_user_gcm_log tblUserGcmLog2 in tblUserGcmLog1.Where<tbl_user_gcm_log>(predicate).ToList<tbl_user_gcm_log>())
            {
              tbl_user_gcm_log gcm = tblUserGcmLog2;
              if (!string.IsNullOrEmpty(gcm.GCMID))
                Task.Run<Notification>((Func<Notification>) (() => new Notification(gcm.GCMID, msg)));
            }
          }
          if (mailId.EMAIL != "" && note != null)
            new addCMS_CategoryModel().sendmail(mailId, note, id_config);
        }
      }
      return "1";
    }
  }
}
