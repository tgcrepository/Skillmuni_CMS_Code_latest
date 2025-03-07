// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.SeminarDashboardController
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
  public class SeminarDashboardController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult add_seminar() => (ActionResult) this.View();

    public ActionResult add_seminar_action()
    {
      try
      {
        tbl_sul_seminar_master sulSeminarMaster = new tbl_sul_seminar_master();
        sulSeminarMaster.title = this.Request.Form["seminar-title"];
        sulSeminarMaster.objective = this.Request.Form["seminar-Objective"];
        sulSeminarMaster.stream = this.Request.Form["stream"];
        sulSeminarMaster.seminar_start_time = new Utility().StringToDatetime(this.Request.Form["seminar-start"].ToString());
        sulSeminarMaster.seminar_end_time = new Utility().StringToDatetime(this.Request.Form["seminar-end"].ToString());
        sulSeminarMaster.seminar_duration = this.Request.Form["seminar-duration"];
        sulSeminarMaster.speaker_organisation = this.Request.Form["speaker-organisation"];
        sulSeminarMaster.location = this.Request.Form["seminar-location"];
        sulSeminarMaster.fest_type = 0;
        sulSeminarMaster.seminar_status = Convert.ToInt32(this.Request.Form["seminarmode"]) != 1 ? "P" : "D";
        sulSeminarMaster.status = "A";
        sulSeminarMaster.update_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_seminar_master(title,objective,stream,seminar_start_time,seminar_end_time,seminar_duration,speaker_name,speaker_organisation,location,user_count,seminar_status,status,update_date_time,fest_type)values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});select MAX(id_seminar) from tbl_sul_seminar_master;", (object) sulSeminarMaster.title, (object) sulSeminarMaster.objective, (object) sulSeminarMaster.stream, (object) sulSeminarMaster.seminar_start_time, (object) sulSeminarMaster.seminar_end_time, (object) sulSeminarMaster.seminar_duration, (object) sulSeminarMaster.speaker_name, (object) sulSeminarMaster.speaker_organisation, (object) sulSeminarMaster.location, (object) sulSeminarMaster.user_count, (object) sulSeminarMaster.seminar_status, (object) sulSeminarMaster.status, (object) sulSeminarMaster.update_date_time, (object) sulSeminarMaster.fest_type);
        tbl_sul_seminar_timeslot_new seminarTimeslotNew = new tbl_sul_seminar_timeslot_new();
        double num = Convert.ToDouble(this.Request.Form["seminar-duration"]);
        for (int index1 = 1; (double) index1 <= num; ++index1)
        {
          int int32_1 = Convert.ToInt32(this.Request.Form["days" + (object) index1]);
          for (int index2 = 1; index2 <= int32_1; ++index2)
          {
            string[] strArray1 = this.Request.Form["start_time_hour" + (object) index1 + (object) index2].Split(' ');
            string str1 = strArray1[1];
            string[] strArray2 = strArray1[0].Split(':');
            string str2 = strArray2[0];
            string str3 = strArray2[1];
            string[] strArray3 = this.Request.Form["end_time_hour" + (object) index1 + (object) index2].Split(' ');
            string str4 = strArray3[1];
            string[] strArray4 = strArray3[0].Split(':');
            string str5 = strArray4[0];
            string str6 = strArray4[1];
            string str7 = this.Request.Form["mspeaker-name" + (object) index1 + (object) index2];
            int int32_2 = Convert.ToInt32(this.Request.Form["mcount-restriction" + (object) index1 + (object) index2]);
            seminarTimeslotNew.slot_start_time_hour = Convert.ToInt32(str2);
            seminarTimeslotNew.slot_start_time_minute = Convert.ToInt32(str3);
            seminarTimeslotNew.session_start = str1;
            seminarTimeslotNew.slot_end_time_hour = Convert.ToInt32(str5);
            seminarTimeslotNew.slot_end_time_minute = Convert.ToInt32(str6);
            seminarTimeslotNew.session_end = str4;
            seminarTimeslotNew.speaker_name = str7;
            seminarTimeslotNew.count_restriction = int32_2;
            seminarTimeslotNew.serial_no = index2;
            seminarTimeslotNew.status = "A";
            seminarTimeslotNew.updated_date_time = DateTime.Now;
            seminarTimeslotNew.slot_date = DateTime.Now;
            using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            {
              sulSeminarMaster.id_seminar = m2ostDbContext.Database.SqlQuery<int>("select MAX(id_seminar) from tbl_sul_seminar_master").FirstOrDefault<int>();
              m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_seminar_timeslot_new(slot_start_time_hour,slot_start_time_minute,session_start,slot_end_time_hour,slot_end_time_minute,session_end,day,serial_no,speaker_name,description,count_restriction,id_seminar,status,updated_date_time,slot_date)values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})", (object) seminarTimeslotNew.slot_start_time_hour, (object) seminarTimeslotNew.slot_start_time_minute, (object) seminarTimeslotNew.session_start, (object) seminarTimeslotNew.slot_end_time_hour, (object) seminarTimeslotNew.slot_end_time_minute, (object) seminarTimeslotNew.session_end, (object) index1, (object) seminarTimeslotNew.serial_no, (object) seminarTimeslotNew.speaker_name, (object) seminarTimeslotNew.description, (object) seminarTimeslotNew.count_restriction, (object) sulSeminarMaster.id_seminar, (object) seminarTimeslotNew.status, (object) seminarTimeslotNew.updated_date_time, (object) seminarTimeslotNew.slot_date);
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.RedirectToAction("seminar_dashboard");
    }

    public ActionResult edit_seminar(int id_seminar)
    {
      string[] strArray1 = "11:53 AM".Split(' ');
      string str1 = strArray1[1];
      string[] strArray2 = strArray1[0].Split(':');
      string str2 = strArray2[0];
      string str3 = strArray2[1];
      tbl_sul_seminar_master model = new tbl_sul_seminar_master();
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          model = m2ostDbContext.Database.SqlQuery<tbl_sul_seminar_master>("SELECT * FROM tbl_sul_seminar_master WHERE id_seminar =" + (object) id_seminar + " AND status='A'").FirstOrDefault<tbl_sul_seminar_master>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this.ViewData["seminar"] = (object) model;
      return (ActionResult) this.View((object) model);
    }

    public ActionResult edit_seminar_action()
    {
      try
      {
        int int32 = Convert.ToInt32(this.Request.Form["id_seminar"]);
        tbl_sul_seminar_master sulSeminarMaster = new tbl_sul_seminar_master();
        sulSeminarMaster.title = this.Request.Form["seminar-title"];
        sulSeminarMaster.objective = this.Request.Form["seminar-Objective"];
        sulSeminarMaster.stream = this.Request.Form["stream"];
        sulSeminarMaster.seminar_start_time = new Utility().StringToDatetime(this.Request.Form["seminar-start"].ToString());
        sulSeminarMaster.seminar_end_time = new Utility().StringToDatetime(this.Request.Form["seminar-end"].ToString());
        sulSeminarMaster.seminar_duration = this.Request.Form["seminar-duration"];
        sulSeminarMaster.speaker_organisation = this.Request.Form["speaker-organisation"];
        sulSeminarMaster.location = this.Request.Form["seminar-location"];
        sulSeminarMaster.seminar_status = Convert.ToInt32(this.Request.Form["seminarmode"]) != 1 ? "P" : "D";
        sulSeminarMaster.status = "A";
        sulSeminarMaster.update_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_sul_seminar_master SET title={0},objective={1},stream={2},seminar_start_time={3},seminar_end_time={4},seminar_duration={5},speaker_name={6},speaker_organisation={7}, location={8},user_count={9},seminar_status={10},status={11},update_date_time={12}  WHERE id_seminar={13}", (object) sulSeminarMaster.title, (object) sulSeminarMaster.objective, (object) sulSeminarMaster.stream, (object) sulSeminarMaster.seminar_start_time, (object) sulSeminarMaster.seminar_end_time, (object) sulSeminarMaster.seminar_duration, (object) sulSeminarMaster.speaker_name, (object) sulSeminarMaster.speaker_organisation, (object) sulSeminarMaster.location, (object) sulSeminarMaster.user_count, (object) sulSeminarMaster.seminar_status, (object) sulSeminarMaster.status, (object) sulSeminarMaster.update_date_time, (object) int32);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.RedirectToAction("seminar_dashboard");
    }

    public ActionResult seminar_dashboard()
    {
      List<m2ostnext.Models.seminar_dashboard> seminarDashboardList = new List<m2ostnext.Models.seminar_dashboard>();
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          seminarDashboardList = m2ostDbContext.Database.SqlQuery<m2ostnext.Models.seminar_dashboard>("SELECT IFNULL((SUM(t2.ratings) / COUNT(t2.id_seminar)),0) AS ratings, t1.id_seminar, t1.title, t1.stream, t1.speaker_name, t1.update_date_time, t1.location, t1.seminar_status,t1.status FROM tbl_sul_seminar_master t1 LEFT JOIN tbl_sul_seminar_user_registration t2 ON  t1.id_seminar = t2.id_seminar WHERE t1.status = 'A' GROUP BY t1.id_seminar").ToList<m2ostnext.Models.seminar_dashboard>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this.ViewData["temp"] = (object) seminarDashboardList;
      return (ActionResult) this.View();
    }

    public string activateSeminar(string id_seminar)
    {
      int int32 = Convert.ToInt32(id_seminar);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_seminar_master set status='A' where id_seminar={0}", (object) int32);
      return "";
    }

    public string deactivateSeminar(string id_seminar)
    {
      int int32 = Convert.ToInt32(id_seminar);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_seminar_master set status='D' where id_seminar={0}", (object) int32);
      return "";
    }
  }
}
