// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.SulEventMappingController
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
  public class SulEventMappingController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult sul_event_mapping()
    {
      List<sul_event_map> sulEventMapList = new List<sul_event_map>();
      List<sul_event_map_seminar> sulEventMapSeminarList = new List<sul_event_map_seminar>();
      List<sul_event_map_higher> sulEventMapHigherList = new List<sul_event_map_higher>();
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          string sql1 = "SELECT * FROM tbl_sul_fest_master WHERE event_status = 'D' AND status='A'";
          sulEventMapList = m2ostDbContext.Database.SqlQuery<sul_event_map>(sql1).ToList<sul_event_map>();
          string sql2 = "SELECT * FROM tbl_sul_seminar_master WHERE seminar_status = 'D' AND status='A'";
          sulEventMapSeminarList = m2ostDbContext.Database.SqlQuery<sul_event_map_seminar>(sql2).ToList<sul_event_map_seminar>();
          string sql3 = "SELECT * FROM tbl_sul_higher_education_master WHERE status = 'A'";
          sulEventMapHigherList = m2ostDbContext.Database.SqlQuery<sul_event_map_higher>(sql3).ToList<sul_event_map_higher>();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this.ViewData["map"] = (object) sulEventMapList;
      this.ViewData["seminar"] = (object) sulEventMapSeminarList;
      this.ViewData["higher"] = (object) sulEventMapHigherList;
      return (ActionResult) this.View();
    }

    public string sul_event_mapping_action(int events, int sem_id, int high_id)
    {
      tbl_sul_fest_event_mapping festEventMapping = new tbl_sul_fest_event_mapping();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        m2ostDbContext.Database.ExecuteSqlCommand("update tbl_sul_fest_master set updated_date_time={0},event_status='P' where id_event={1}", (object) DateTime.Now, (object) events);
      try
      {
        if (sem_id != 0)
        {
          festEventMapping.id_event = events;
          festEventMapping.id_seminar = sem_id;
          festEventMapping.id_higher_education = 0;
          festEventMapping.status = "A";
          festEventMapping.type = 1;
          festEventMapping.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_event_mapping(id_event,id_seminar,id_higher_education,status,type,updated_date_time)values({0},{1},{2},{3},{4},{5})", (object) festEventMapping.id_event, (object) festEventMapping.id_seminar, (object) festEventMapping.id_higher_education, (object) festEventMapping.status, (object) festEventMapping.type, (object) festEventMapping.updated_date_time);
        }
        if (high_id != 0)
        {
          festEventMapping.id_event = events;
          festEventMapping.id_seminar = 0;
          festEventMapping.id_higher_education = high_id;
          festEventMapping.status = "A";
          festEventMapping.type = 2;
          festEventMapping.updated_date_time = DateTime.Now;
          using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_event_mapping(id_event,id_seminar,id_higher_education,status,type,updated_date_time)values({0},{1},{2},{3},{4},{5})", (object) festEventMapping.id_event, (object) festEventMapping.id_seminar, (object) festEventMapping.id_higher_education, (object) festEventMapping.status, (object) festEventMapping.type, (object) festEventMapping.updated_date_time);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return "";
    }
  }
}
