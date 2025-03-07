// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.HigherEducationController
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
  public class HigherEducationController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult add_higher_education() => (ActionResult) this.View();

    public ActionResult add_higher_education_action()
    {
      try
      {
        tbl_sul_higher_education_master higherEducationMaster = new tbl_sul_higher_education_master();
        tbl_sul_higher_education_timeslot educationTimeslot = new tbl_sul_higher_education_timeslot();
        higherEducationMaster.message_to_display = this.Request.Form["message-to-display"];
        higherEducationMaster.redirect_url = this.Request.Form["redirect-url"];
        higherEducationMaster.event_name = this.Request.Form["event-name"];
        higherEducationMaster.higher_education_start_time = new Utility().StringToDatetime(this.Request.Form["higher-start"].ToString());
        higherEducationMaster.higher_education_end_time = new Utility().StringToDatetime(this.Request.Form["higher-end"].ToString());
        higherEducationMaster.time_interval = Convert.ToInt32(this.Request.Form["time-interval"]);
        higherEducationMaster.location = this.Request.Form["location"];
        higherEducationMaster.status = "A";
        higherEducationMaster.update_date_time = DateTime.Now;
        educationTimeslot.time_slot_start_time_hour = Convert.ToInt32(this.Request.Form["hourstart"]);
        educationTimeslot.time_slot_start_time_minute = Convert.ToInt32(this.Request.Form["minutestart"]);
        if (Convert.ToInt32(this.Request.Form["sessionstart"]) == 1)
          educationTimeslot.session_start_time = "AM";
        else
          educationTimeslot.session_end_time = "PM";
        educationTimeslot.time_slot_end_time_hour = Convert.ToInt32(this.Request.Form["hourend"]);
        educationTimeslot.time_slot_end_time_minute = Convert.ToInt32(this.Request.Form["minuteend"]);
        if (Convert.ToInt32(this.Request.Form["sessionend"]) == 1)
          educationTimeslot.session_start_time = "AM";
        else
          educationTimeslot.session_end_time = "PM";
        educationTimeslot.status = "A";
        educationTimeslot.update_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          higherEducationMaster.id_higher_education = m2ostDbContext.Database.SqlQuery<int>("insert into tbl_sul_higher_education_master(message_to_display,redirect_url,event_name,higher_education_start_time,higher_education_end_time,time_interval,location,status,update_date_time)values({0},{1},{2},{3},{4},{5},{6},{7},{8});select MAX(id_higher_education) from tbl_sul_higher_education_master;", (object) higherEducationMaster.message_to_display, (object) higherEducationMaster.redirect_url, (object) higherEducationMaster.event_name, (object) higherEducationMaster.higher_education_start_time, (object) higherEducationMaster.higher_education_end_time, (object) higherEducationMaster.time_interval, (object) higherEducationMaster.location, (object) higherEducationMaster.status, (object) higherEducationMaster.update_date_time).FirstOrDefault<int>();
          m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_higher_education_timeslot(time_slot_start_time_hour,time_slot_start_time_minute,time_slot_end_time_hour,time_slot_end_time_minute,session_start_time,session_end_time,id_higher_education,status,update_date_time)values({0},{1},{2},{3},{4},{5},{6},{7},{8})", (object) educationTimeslot.time_slot_start_time_hour, (object) educationTimeslot.time_slot_start_time_minute, (object) educationTimeslot.time_slot_end_time_hour, (object) educationTimeslot.time_slot_end_time_minute, (object) educationTimeslot.session_start_time, (object) educationTimeslot.session_end_time, (object) higherEducationMaster.id_higher_education, (object) educationTimeslot.status, (object) educationTimeslot.update_date_time);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.RedirectToAction("higher_education_dashboard");
    }

    public ActionResult edit_higher_education(int id_higher_education)
    {
      tbl_sul_higher_education_master model = new tbl_sul_higher_education_master();
      tbl_sul_higher_education_timeslot educationTimeslot = new tbl_sul_higher_education_timeslot();
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          model = m2ostDbContext.Database.SqlQuery<tbl_sul_higher_education_master>("SELECT * FROM tbl_sul_higher_education_master WHERE id_higher_education=" + (object) id_higher_education + " AND status='A'").FirstOrDefault<tbl_sul_higher_education_master>();
          educationTimeslot = m2ostDbContext.Database.SqlQuery<tbl_sul_higher_education_timeslot>("SELECT * FROM tbl_sul_higher_education_timeslot WHERE id_higher_education=" + (object) id_higher_education + " AND status='A'").FirstOrDefault<tbl_sul_higher_education_timeslot>();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this.ViewData["higher"] = (object) model;
      this.ViewData["slot"] = (object) educationTimeslot;
      return (ActionResult) this.View((object) model);
    }

    public ActionResult edit_higher_education_action()
    {
      try
      {
        tbl_sul_higher_education_master higherEducationMaster = new tbl_sul_higher_education_master();
        tbl_sul_higher_education_timeslot educationTimeslot = new tbl_sul_higher_education_timeslot();
        int int32 = Convert.ToInt32(this.Request.Form["id_higher_education"]);
        higherEducationMaster.message_to_display = this.Request.Form["message-to-display"];
        higherEducationMaster.redirect_url = this.Request.Form["redirect-url"];
        higherEducationMaster.event_name = this.Request.Form["event-name"];
        higherEducationMaster.higher_education_start_time = new Utility().StringToDatetime(this.Request.Form["higher-start"].ToString());
        higherEducationMaster.higher_education_end_time = new Utility().StringToDatetime(this.Request.Form["higher-end"].ToString());
        higherEducationMaster.time_interval = Convert.ToInt32(this.Request.Form["time-interval"]);
        higherEducationMaster.location = this.Request.Form["location"];
        higherEducationMaster.status = "A";
        higherEducationMaster.update_date_time = DateTime.Now;
        educationTimeslot.time_slot_start_time_hour = Convert.ToInt32(this.Request.Form["hourstart"]);
        educationTimeslot.time_slot_start_time_minute = Convert.ToInt32(this.Request.Form["minutestart"]);
        if (Convert.ToInt32(this.Request.Form["sessionstart"]) == 1)
          educationTimeslot.session_start_time = "AM";
        else
          educationTimeslot.session_end_time = "PM";
        educationTimeslot.time_slot_end_time_hour = Convert.ToInt32(this.Request.Form["hourend"]);
        educationTimeslot.time_slot_end_time_minute = Convert.ToInt32(this.Request.Form["minuteend"]);
        if (Convert.ToInt32(this.Request.Form["sessionend"]) == 1)
          educationTimeslot.session_start_time = "AM";
        else
          educationTimeslot.session_end_time = "PM";
        educationTimeslot.status = "A";
        educationTimeslot.update_date_time = DateTime.Now;
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_sul_higher_education_master SET message_to_display={0}, redirect_url={1}, event_name={2}, higher_education_start_time={3}, higher_education_end_time={4}, time_interval={5}, location={6}, status={7}, update_date_time={8} WHERE id_higher_education={9}", (object) higherEducationMaster.message_to_display, (object) higherEducationMaster.redirect_url, (object) higherEducationMaster.event_name, (object) higherEducationMaster.higher_education_start_time, (object) higherEducationMaster.higher_education_end_time, (object) higherEducationMaster.time_interval, (object) higherEducationMaster.location, (object) higherEducationMaster.status, (object) higherEducationMaster.update_date_time, (object) int32);
          m2ostDbContext.Database.ExecuteSqlCommand("UPDATE tbl_sul_higher_education_timeslot SET time_slot_start_time_hour={0}, time_slot_start_time_minute={1}, time_slot_end_time_hour={2}, time_slot_end_time_minute={3}, session_start_time={4}, session_end_time={5},status={6}, update_date_time={7} WHERE id_higher_education={8}", (object) educationTimeslot.time_slot_start_time_hour, (object) educationTimeslot.time_slot_start_time_minute, (object) educationTimeslot.time_slot_end_time_hour, (object) educationTimeslot.time_slot_end_time_minute, (object) educationTimeslot.session_start_time, (object) educationTimeslot.session_end_time, (object) educationTimeslot.status, (object) educationTimeslot.update_date_time, (object) int32);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.RedirectToAction("higher_education_dashboard");
    }

    public ActionResult higher_education_dashboard()
    {
      List<higher_education_dashboard> educationDashboardList = new List<higher_education_dashboard>();
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          educationDashboardList = m2ostDbContext.Database.SqlQuery<higher_education_dashboard>("SELECT IFNULL((SUM(t2.ratings) / COUNT(t2.id_higher_education)), 0) AS ratings, t1.id_higher_education, t1.message_to_display, t1.redirect_url, t1.event_name, t1.location, t1.update_date_time FROM tbl_sul_higher_education_master t1 LEFT JOIN tbl_sul_higher_education_user_registration t2 ON t1.id_higher_education = t2.id_higher_education WHERE t1.status = 'A' GROUP BY t1.id_higher_education").ToList<higher_education_dashboard>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this.ViewData["temp"] = (object) educationDashboardList;
      return (ActionResult) this.View();
    }

    public string deactivateSeminar(string id_higher_education)
    {
      int int32 = Convert.ToInt32(id_higher_education);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_higher_education_master set status='D' where id_higher_education={0}", (object) int32);
      return "";
    }
  }
}
