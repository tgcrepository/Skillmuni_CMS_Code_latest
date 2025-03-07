// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.UniversityAdminLogic
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace m2ostnext.Models
{
  public class UniversityAdminLogic
  {
    public int insertTheme(tbl_theme_master theme)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          return m2ostDbContext.Database.SqlQuery<int>("insert into tbl_theme_master (name,description,id_creator,status,updated_datetime) values({0},{1},{2},{3},{4});SELECT LAST_INSERT_ID()", (object) theme.name, (object) theme.description, (object) theme.id_creator, (object) theme.status, (object) theme.updated_datetime).FirstOrDefault<int>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void updateTheme(tbl_theme_master theme)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_theme_master set name={0},description={1},id_creator={2},status={3},updated_datetime={4} where id_theme={5}", (object) theme.name, (object) theme.description, (object) theme.id_creator, (object) theme.status, (object) theme.updated_datetime, (object) theme.id_theme);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void updatethemelogo(tbl_theme_master theme)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
          m2ostDbContext.Database.ExecuteSqlCommand("update tbl_theme_master set theme_logo ={0} where id_theme={1}", (object) theme.theme_logo, (object) theme.id_theme);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void insertMetric(List<tbl_theme_metric> metric)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_theme_metric tblThemeMetric in metric)
            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_theme_metric (metric_value,id_theme,status,updated_datetime) values({0},{1},{2},{3})", (object) tblThemeMetric.metric_value, (object) tblThemeMetric.id_theme, (object) tblThemeMetric.status, (object) tblThemeMetric.updated_datetime);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void UpdateMetric(List<tbl_theme_metric> metric)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_theme_metric tblThemeMetric in metric)
            m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_theme_metric set metric_value={0},id_theme={1},status={2},updated_datetime={3} where id_metric={4}", (object) tblThemeMetric.metric_value, (object) tblThemeMetric.id_theme, (object) tblThemeMetric.status, (object) tblThemeMetric.updated_datetime, (object) tblThemeMetric.id_metric);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void insertLeague(List<tbl_theme_leagues> league)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_theme_leagues tblThemeLeagues in league)
            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_theme_leagues (id_theme,league_name,league_logo,status,id_org,updated_date_time,level) values({0},{1},{2},{3},{4},{5},{6})", (object) tblThemeLeagues.id_theme, (object) tblThemeLeagues.league_name, (object) tblThemeLeagues.league_logo, (object) tblThemeLeagues.status, (object) tblThemeLeagues.id_org, (object) tblThemeLeagues.updated_date_time, (object) tblThemeLeagues.level);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void UpdateLeague(List<tbl_theme_leagues> league)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_theme_leagues tblThemeLeagues in league)
            m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_theme_leagues set id_theme={0},league_name={1},status={2},id_org={3},updated_date_time={4},level={5} where id_league={6}", (object) tblThemeLeagues.id_theme, (object) tblThemeLeagues.league_name, (object) tblThemeLeagues.status, (object) tblThemeLeagues.id_org, (object) tblThemeLeagues.updated_date_time, (object) tblThemeLeagues.level, (object) tblThemeLeagues.id_league);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void insertBadge(List<tbl_badge_master> badge)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_badge_master tblBadgeMaster in badge)
            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_badge_master (id_theme,badge_name,badge_logo,status,updated_datetime) values({0},{1},{2},{3},{4})", (object) tblBadgeMaster.id_theme, (object) tblBadgeMaster.badge_name, (object) tblBadgeMaster.badge_logo, (object) tblBadgeMaster.status, (object) tblBadgeMaster.updated_datetime);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void UpdateBadge(List<tbl_badge_master> badge)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_badge_master tblBadgeMaster in badge)
            m2ostDbContext.Database.ExecuteSqlCommand("update tbl_badge_master set id_theme={0},badge_name={1},status={2},updated_datetime={3} where id_badge={4} ", (object) tblBadgeMaster.id_theme, (object) tblBadgeMaster.badge_name, (object) tblBadgeMaster.status, (object) tblBadgeMaster.updated_datetime, (object) tblBadgeMaster.id_badge);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void insertCurrency(List<tbl_crrency_points> currency)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_crrency_points tblCrrencyPoints in currency)
            m2ostDbContext.Database.ExecuteSqlCommand("insert into tbl_currency_points (id_theme,currency_value,currency_logo,status,updated_datetime) values({0},{1},{2},{3},{4})", (object) tblCrrencyPoints.id_theme, (object) tblCrrencyPoints.currency_value, (object) tblCrrencyPoints.currency_logo, (object) tblCrrencyPoints.status, (object) tblCrrencyPoints.updated_datetime);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void updateCurrency(List<tbl_crrency_points> currency)
    {
      try
      {
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          foreach (tbl_crrency_points tblCrrencyPoints in currency)
            m2ostDbContext.Database.ExecuteSqlCommand("update  tbl_currency_points set id_theme={0},currency_value={1},status={2},updated_datetime={3} where id_currency={4}", (object) tblCrrencyPoints.id_theme, (object) tblCrrencyPoints.currency_value, (object) tblCrrencyPoints.status, (object) tblCrrencyPoints.updated_datetime, (object) tblCrrencyPoints.id_currency);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
