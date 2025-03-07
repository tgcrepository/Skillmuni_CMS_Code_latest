// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.RoadmapLogic
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnext.Models
{
  public class RoadmapLogic
  {
    private MySqlConnection conn;

    public RoadmapLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public int creategametile(tbl_academic_tiles tile)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO tbl_academic_tiles(id_org, status,tile_description,tile_name,tile_position,updated_date_time,theme_id,url) VALUES (@value1,@value2,@value3,@value5,@value6,@value7,@value8,@value9)";
        command.Parameters.AddWithValue("value1", (object) tile.id_org);
        command.Parameters.AddWithValue("value2", (object) "A");
        command.Parameters.AddWithValue("value3", (object) tile.tile_description);
        command.Parameters.AddWithValue("value5", (object) tile.tile_name);
        command.Parameters.AddWithValue("value6", (object) tile.tile_position);
        command.Parameters.AddWithValue("value7", (object) DateTime.Now);
        command.Parameters.AddWithValue("value8", (object) tile.theme_id);
        command.Parameters.AddWithValue("value9", (object) tile.url);
        command.ExecuteNonQuery();
        num = Convert.ToInt32(command.LastInsertedId);
        this.conn.Close();
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public int UpdateTile(tbl_academic_tiles tile)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "update tbl_academic_tiles set tile_description=@value3,tile_name=@value5,tile_position=@value6,updated_date_time=@value7,theme_id=@value9,url=@value10 where id_academic_tile=@value8";
        command.Parameters.AddWithValue("value1", (object) tile.id_org);
        command.Parameters.AddWithValue("value3", (object) tile.tile_description);
        command.Parameters.AddWithValue("value5", (object) tile.tile_name);
        command.Parameters.AddWithValue("value6", (object) tile.tile_position);
        command.Parameters.AddWithValue("value7", (object) DateTime.Now);
        command.Parameters.AddWithValue("value8", (object) tile.id_academic_tile);
        command.Parameters.AddWithValue("value9", (object) tile.theme_id);
        command.Parameters.AddWithValue("value10", (object) tile.url);
        command.ExecuteNonQuery();
        num = tile.id_academic_tile;
        this.conn.Close();
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public void InsertTileimage(tbl_academic_tiles tile)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "update tbl_academic_tiles set tile_image=@value1 where id_academic_tile=@value2";
        command.Parameters.AddWithValue("value1", (object) tile.tile_image);
        command.Parameters.AddWithValue("value2", (object) tile.id_academic_tile);
        command.ExecuteNonQuery();
        this.conn.Close();
      }
      catch (Exception ex)
      {
      }
    }

    public List<tbl_academic_tiles> getGameTiles(int id_org)
    {
      List<tbl_academic_tiles> gameTiles = new List<tbl_academic_tiles>();
      try
      {
        string str = "select * from tbl_academic_tiles where id_org = @value1 and theme_id=1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_org);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            gameTiles.Add(new tbl_academic_tiles()
            {
              id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString()),
              id_org = Convert.ToInt32(mySqlDataReader[nameof (id_org)].ToString()),
              status = mySqlDataReader["status"].ToString(),
              tile_description = mySqlDataReader["tile_description"].ToString(),
              tile_image = mySqlDataReader["tile_image"].ToString(),
              tile_name = mySqlDataReader["tile_name"].ToString(),
              tile_position = Convert.ToInt32(mySqlDataReader["tile_position"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return gameTiles;
    }

    public tbl_academic_tiles getGameTile(int id_tile)
    {
      tbl_academic_tiles gameTile = new tbl_academic_tiles();
      try
      {
        string str = "select * from tbl_academic_tiles where id_academic_tile = @value1 ";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_tile);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            gameTile.id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString());
            gameTile.id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString());
            gameTile.status = mySqlDataReader["status"].ToString();
            gameTile.tile_description = mySqlDataReader["tile_description"].ToString();
            gameTile.theme_id = Convert.ToInt32(mySqlDataReader["theme_id"].ToString());
            gameTile.url = mySqlDataReader["url"].ToString();
            gameTile.tile_image = mySqlDataReader["tile_image"].ToString();
            gameTile.tile_name = mySqlDataReader["tile_name"].ToString();
            gameTile.tile_position = Convert.ToInt32(mySqlDataReader["tile_position"].ToString());
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return gameTile;
    }

    public void deletegametile(int tile)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "Delete from tbl_academic_tiles where id_academic_tile=@value1";
        command.Parameters.AddWithValue("value1", (object) tile);
        command.ExecuteNonQuery();
        this.conn.Close();
      }
      catch (Exception ex)
      {
      }
    }

    public tbl_brief_tile_academic_mapping GetMappedTile(int id_tile, int id_gametile)
    {
      tbl_brief_tile_academic_mapping mappedTile = new tbl_brief_tile_academic_mapping();
      try
      {
        string str = "select * from tbl_brief_tile_academic_mapping where id_journey_tile = @value1 and id_academic_tile=@value2";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_tile);
        command.Parameters.AddWithValue("value2", (object) id_gametile);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            mappedTile.id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString());
            mappedTile.id_journey_tile = Convert.ToInt32(mySqlDataReader["id_journey_tile"].ToString());
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return mappedTile;
    }

    public string addJourneyToGame(int id_gametile, int id_journeytile, int idorg)
    {
      string game = "2";
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO tbl_brief_tile_academic_mapping(id_academic_tile, id_journey_tile,updated_date_time,status,id_org) VALUES (@value1,@value2,@value3,@value4,@value5)";
        command.Parameters.AddWithValue("value1", (object) id_gametile);
        command.Parameters.AddWithValue("value2", (object) id_journeytile);
        command.Parameters.AddWithValue("value3", (object) DateTime.Now);
        command.Parameters.AddWithValue("value4", (object) "A");
        command.Parameters.AddWithValue("value5", (object) idorg);
        command.ExecuteNonQuery();
        this.conn.Close();
        game = "1";
      }
      catch (Exception ex)
      {
      }
      return game;
    }

    public string removeJourneyFromGame(int id_gametile, int id_journeytile, int idorg)
    {
      string str = "2";
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "delete from tbl_brief_tile_academic_mapping where id_academic_tile=@value1 and id_journey_tile=@value2 and id_org=@value5";
        command.Parameters.AddWithValue("value1", (object) id_gametile);
        command.Parameters.AddWithValue("value2", (object) id_journeytile);
        command.Parameters.AddWithValue("value5", (object) idorg);
        command.ExecuteNonQuery();
        this.conn.Close();
        str = "1";
      }
      catch (Exception ex)
      {
      }
      return str;
    }

    public List<tbl_academic_tiles> AcademicVal(int id_org)
    {
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_academic_tiles where id_org=@id_org";
        command.Parameters.AddWithValue(nameof (id_org), (object) id_org);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblAcademicTilesList.Add(new tbl_academic_tiles()
            {
              id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString()),
              tile_name = mySqlDataReader["tile_name"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return tblAcademicTilesList;
    }

    public bool CreateKpi(
      int id_creator,
      int id_organization,
      string kpi_name,
      string kpi_description,
      string KPIID,
      int kpi_type,
      DateTime expiry_date,
      DateTime start_date,
      string stat,
      DateTime updated_date_time)
    {
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO tbl_university_kpi_master(`id_organization`,`kpi_name`,`kpi_description`,`kpi_type`,`KPIID`,`id_creator`,`status`,`updated_date_time`,`start_date`,`expiry_date`) VALUES (@id_organization,@kpi_name,@kpi_description,@kpi_type,@KPIID,@id_creator,@stat,@updated_date_time,@start_date,@expiry_date)";
        command.Parameters.AddWithValue(nameof (id_creator), (object) id_creator);
        command.Parameters.AddWithValue(nameof (id_organization), (object) id_organization);
        command.Parameters.AddWithValue(nameof (kpi_name), (object) kpi_name);
        command.Parameters.AddWithValue(nameof (kpi_description), (object) kpi_description);
        command.Parameters.AddWithValue(nameof (KPIID), (object) KPIID);
        command.Parameters.AddWithValue(nameof (kpi_type), (object) kpi_type);
        command.Parameters.AddWithValue(nameof (expiry_date), (object) expiry_date);
        command.Parameters.AddWithValue(nameof (start_date), (object) start_date);
        command.Parameters.AddWithValue(nameof (stat), (object) stat);
        command.Parameters.AddWithValue(nameof (updated_date_time), (object) updated_date_time);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool EditKpi(
      int id_creator,
      int id_organization,
      string kpi_name,
      string kpi_description,
      string KPIID,
      int kpi_type,
      DateTime expiry_date,
      DateTime start_date,
      string stat,
      DateTime updated_date_time,
      int id_kpi_mas)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_university_kpi_master` SET `id_organization` = @id_organization,`kpi_name` = @kpi_name,`kpi_description` = @kpi_description,`kpi_type` = @kpi_type ,`KPIID` = @KPIID,`id_creator` = @id_creator, `status` = @stat, `updated_date_time` = @updated_date_time,`start_date` = @start_date,`expiry_date` = @expiry_date WHERE `id_kpi_master` = @id_kpi_mas";
        command.Parameters.AddWithValue(nameof (id_creator), (object) id_creator);
        command.Parameters.AddWithValue(nameof (id_organization), (object) id_organization);
        command.Parameters.AddWithValue(nameof (kpi_name), (object) kpi_name);
        command.Parameters.AddWithValue(nameof (kpi_description), (object) kpi_description);
        command.Parameters.AddWithValue(nameof (KPIID), (object) KPIID);
        command.Parameters.AddWithValue(nameof (kpi_type), (object) kpi_type);
        command.Parameters.AddWithValue(nameof (expiry_date), (object) expiry_date);
        command.Parameters.AddWithValue(nameof (start_date), (object) start_date);
        command.Parameters.AddWithValue(nameof (stat), (object) stat);
        command.Parameters.AddWithValue(nameof (updated_date_time), (object) updated_date_time);
        command.Parameters.AddWithValue(nameof (id_kpi_mas), (object) id_kpi_mas);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public int Kpi_master_inserted_id()
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT id_kpi_master FROM tbl_university_kpi_master ORDER BY id_kpi_master DESC LIMIT 1";
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            tbl_academic_tiles tblAcademicTiles = new tbl_academic_tiles();
            num = Convert.ToInt32(mySqlDataReader["id_kpi_master"].ToString());
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return num;
    }

    public bool Kpi_grid_insert(
      int kpi_value,
      string kpi_text,
      double start_range,
      double end_range,
      string status,
      DateTime updated_datetime,
      int id_kpi_master,
      int id_game,
      int id_metric)
    {
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_university_kpi_grid`(`id_kpi_master`,`start_range`,`end_range`,`kpi_value`,`kpi_text`,`status`,`updated_date_time`,`id_game`,`id_metric`) VALUES (@id_kpi_master,@start_range,@end_range,@kpi_value,@kpi_text,@status,@updated_datetime,@id_game,@id_metric)";
        command.Parameters.AddWithValue(nameof (kpi_value), (object) kpi_value);
        command.Parameters.AddWithValue(nameof (kpi_text), (object) kpi_text);
        command.Parameters.AddWithValue(nameof (start_range), (object) start_range);
        command.Parameters.AddWithValue(nameof (end_range), (object) end_range);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (id_kpi_master), (object) id_kpi_master);
        command.Parameters.AddWithValue(nameof (id_game), (object) id_game);
        command.Parameters.AddWithValue(nameof (id_metric), (object) id_metric);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool Kpi_grid_edit(
      int kpi_value,
      string kpi_text,
      double start_range,
      double end_range,
      string status,
      DateTime updated_datetime,
      int id_kpi_master,
      int id_kpiGri)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_university_kpi_grid` SET `id_kpi_master` = @id_kpi_master,`start_range` = @start_range,`end_range` = @end_range,`kpi_value` = @kpi_value,`kpi_text` = @kpi_text,`status` = @status,`updated_date_time` = @updated_datetime WHERE `id_kpi_grid` = @id_kpiGri";
        command.Parameters.AddWithValue(nameof (kpi_value), (object) kpi_value);
        command.Parameters.AddWithValue(nameof (kpi_text), (object) kpi_text);
        command.Parameters.AddWithValue(nameof (start_range), (object) start_range);
        command.Parameters.AddWithValue(nameof (end_range), (object) end_range);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (id_kpi_master), (object) id_kpi_master);
        command.Parameters.AddWithValue(nameof (id_kpiGri), (object) id_kpiGri);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public bool CheckCategValid(int acad, int categ, int OID)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_university_kpi_master where id_academic_tile=@acad and id_brief_category=@categ and id_organization=@OID";
        command.Parameters.AddWithValue(nameof (acad), (object) acad);
        command.Parameters.AddWithValue(nameof (categ), (object) categ);
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.ExecuteNonQuery();
        if (!command.ExecuteReader().HasRows)
          return false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public List<tbl_theme_leagues> themdetail(int id_theme)
    {
      List<tbl_theme_leagues> tblThemeLeaguesList = new List<tbl_theme_leagues>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_theme_leagues where id_theme=@id_theme order by level asc";
        command.Parameters.AddWithValue(nameof (id_theme), (object) id_theme);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblThemeLeaguesList.Add(new tbl_theme_leagues()
            {
              id_league = Convert.ToInt32(mySqlDataReader["id_league"].ToString()),
              id_theme = Convert.ToInt32(mySqlDataReader[nameof (id_theme)].ToString()),
              league_name = mySqlDataReader["league_name"].ToString(),
              status = mySqlDataReader["status"].ToString(),
              id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return tblThemeLeaguesList;
    }

    public List<tbl_university_kpi_master> Get_KPi_Name(int OID)
    {
      List<tbl_university_kpi_master> kpiName = new List<tbl_university_kpi_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "select * from tbl_university_kpi_master where id_organization=@id_org";
        command.Parameters.AddWithValue("id_org", (object) OID);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            kpiName.Add(new tbl_university_kpi_master()
            {
              id_kpi_master = Convert.ToInt32(mySqlDataReader["id_kpi_master"].ToString()),
              kpi_name = mySqlDataReader["kpi_name"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return kpiName;
    }

    public List<tbl_theme_master> themeOrg(int OID)
    {
      List<tbl_theme_master> tblThemeMasterList = new List<tbl_theme_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "select b.id_theme,b.name from tbl_theme_organisation_mapping a inner join tbl_theme_master b on a.id_theme=b.id_theme where a.id_org=@id_org and b.status='A'";
        command.Parameters.AddWithValue("id_org", (object) OID);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblThemeMasterList.Add(new tbl_theme_master()
            {
              id_theme = Convert.ToInt32(mySqlDataReader["id_theme"].ToString()),
              name = mySqlDataReader["name"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblThemeMasterList;
    }

    public List<tbl_theme_metric> getMetricName(int themVal)
    {
      List<tbl_theme_metric> metricName = new List<tbl_theme_metric>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_theme_metric where id_theme=@themVal";
        command.Parameters.AddWithValue(nameof (themVal), (object) themVal);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            metricName.Add(new tbl_theme_metric()
            {
              id_metric = Convert.ToInt32(mySqlDataReader["id_metric"].ToString()),
              metric_value = mySqlDataReader["metric_value"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return metricName;
    }

    public List<tbl_badge_master> getBadgeName(int themVal)
    {
      List<tbl_badge_master> badgeName = new List<tbl_badge_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_badge_master where id_theme=@themVal";
        command.Parameters.AddWithValue(nameof (themVal), (object) themVal);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            badgeName.Add(new tbl_badge_master()
            {
              id_badge = Convert.ToInt32(mySqlDataReader["id_badge"].ToString()),
              badge_name = mySqlDataReader["badge_name"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return badgeName;
    }

    public List<tbl_currency_points> currencyDetail(int themId)
    {
      List<tbl_currency_points> tblCurrencyPointsList = new List<tbl_currency_points>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_currency_points where id_theme=@themId";
        command.Parameters.AddWithValue(nameof (themId), (object) themId);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblCurrencyPointsList.Add(new tbl_currency_points()
            {
              id_currency = Convert.ToInt32(mySqlDataReader["id_currency"].ToString()),
              currency_value = mySqlDataReader["currency_value"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblCurrencyPointsList;
    }

    public List<tbl_special_metric_master> getSpMetric(int OID)
    {
      List<tbl_special_metric_master> spMetric = new List<tbl_special_metric_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_special_metric_master where id_org=@OID";
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            spMetric.Add(new tbl_special_metric_master()
            {
              id_special_metric = Convert.ToInt32(mySqlDataReader["id_special_metric"].ToString()),
              name = mySqlDataReader["name"].ToString(),
              desc = mySqlDataReader["desc"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return spMetric;
    }

    public bool SpecialMetricAdd(
      string name,
      string desc,
      int id_org,
      string status,
      DateTime updated_date_time)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO tbl_special_metric_master(`name`,`desc`,`id_org`,`status`,`updated_date_time`)VALUES(@name, @desc, @id_org, @status, @updated_date_time)";
        command.Parameters.AddWithValue(nameof (name), (object) name);
        command.Parameters.AddWithValue(nameof (desc), (object) desc);
        command.Parameters.AddWithValue(nameof (id_org), (object) id_org);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_date_time), (object) updated_date_time);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool SpecialMetricUpdate(string name, string desc, int id_org, int MetricID)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_special_metric_master` SET `name` = @name,`desc` = @desc,`updated_date_time` = now() WHERE `id_special_metric` = @MetricID";
        command.Parameters.AddWithValue(nameof (name), (object) name);
        command.Parameters.AddWithValue(nameof (desc), (object) desc);
        command.Parameters.AddWithValue(nameof (id_org), (object) id_org);
        command.Parameters.AddWithValue(nameof (MetricID), (object) MetricID);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool SpecialMetricDelete(int metricId)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "DELETE FROM `tbl_special_metric_master` WHERE id_special_metric= @MetricID";
        command.Parameters.AddWithValue("MetricID", (object) metricId);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool createSpecial_Points(
      int id_creator,
      int id_organization,
      string kpi_name,
      string kpi_description,
      string KPIID,
      int kpi_type,
      DateTime expiry_date,
      DateTime start_date,
      string stat,
      DateTime updated_date_time,
      int Id_them)
    {
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO tbl_university_special_points_master(`id_organization`,`special_value_name`,`special_value_description`,`special_value_type`,`SPIID`,`id_creator`,`status`,`updated_date_time`,`start_date`,`expiry_date`,`id_theme`) VALUES (@id_organization,@kpi_name,@kpi_description,@kpi_type,@KPIID,@id_creator,@stat,@updated_date_time,@start_date,@expiry_date,@Id_them)";
        command.Parameters.AddWithValue(nameof (id_creator), (object) id_creator);
        command.Parameters.AddWithValue(nameof (id_organization), (object) id_organization);
        command.Parameters.AddWithValue(nameof (kpi_name), (object) kpi_name);
        command.Parameters.AddWithValue(nameof (kpi_description), (object) kpi_description);
        command.Parameters.AddWithValue(nameof (KPIID), (object) KPIID);
        command.Parameters.AddWithValue(nameof (kpi_type), (object) kpi_type);
        command.Parameters.AddWithValue(nameof (expiry_date), (object) expiry_date);
        command.Parameters.AddWithValue(nameof (start_date), (object) start_date);
        command.Parameters.AddWithValue(nameof (stat), (object) stat);
        command.Parameters.AddWithValue(nameof (updated_date_time), (object) updated_date_time);
        command.Parameters.AddWithValue(nameof (Id_them), (object) Id_them);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool Special_grid_insert(
      int SpMet_value,
      string SpMet_text,
      double start_range,
      double end_range,
      string status,
      DateTime updated_datetime,
      int id_kpi_master,
      int special_metric)
    {
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_university_special_point_grid`(`id_special_points`,`start_range`,`end_range`,`special_value`,`special_text`,`special_metric`,`status`,`updated_date_time`) VALUES (@id_kpi_master,@start_range,@end_range,@SpMet_value,@SpMet_text,@special_metric,@status,@updated_datetime)";
        command.Parameters.AddWithValue(nameof (SpMet_value), (object) SpMet_value);
        command.Parameters.AddWithValue(nameof (SpMet_text), (object) SpMet_text);
        command.Parameters.AddWithValue(nameof (start_range), (object) start_range);
        command.Parameters.AddWithValue(nameof (end_range), (object) end_range);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (id_kpi_master), (object) id_kpi_master);
        command.Parameters.AddWithValue(nameof (special_metric), (object) special_metric);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public int Special_master_inserted_id()
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT id_special_points FROM tbl_university_special_points_master ORDER BY id_special_points DESC LIMIT 1";
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            num = Convert.ToInt32(mySqlDataReader["id_special_points"].ToString());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return num;
    }

    public List<tbl_university_kpi_master> editKpi_master(int id_kpi, int OID)
    {
      List<tbl_university_kpi_master> universityKpiMasterList = new List<tbl_university_kpi_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "select * from tbl_university_kpi_master where id_organization=@OID and id_kpi_master=@id_kpi";
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.Parameters.AddWithValue(nameof (id_kpi), (object) id_kpi);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            universityKpiMasterList.Add(new tbl_university_kpi_master()
            {
              id_kpi_master = Convert.ToInt32(mySqlDataReader["id_kpi_master"].ToString()),
              id_organization = Convert.ToInt32(mySqlDataReader["id_organization"].ToString()),
              kpi_name = mySqlDataReader["kpi_name"].ToString(),
              kpi_description = mySqlDataReader["kpi_description"].ToString(),
              kpi_type = Convert.ToInt32(mySqlDataReader["kpi_type"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return universityKpiMasterList;
    }

    public List<tbl_university_kpi_grid> editKpi_grid(int id_kpi)
    {
      List<tbl_university_kpi_grid> universityKpiGridList = new List<tbl_university_kpi_grid>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "select * from tbl_university_kpi_grid where id_kpi_master=@id_kpi";
        command.Parameters.AddWithValue(nameof (id_kpi), (object) id_kpi);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            universityKpiGridList.Add(new tbl_university_kpi_grid()
            {
              id_kpi_master = Convert.ToInt32(mySqlDataReader["id_kpi_master"].ToString()),
              id_kpi_grid = Convert.ToInt32(mySqlDataReader["id_kpi_grid"].ToString()),
              start_range = Convert.ToDouble(mySqlDataReader["start_range"].ToString()),
              end_range = (double) Convert.ToInt32(mySqlDataReader["end_range"].ToString()),
              kpi_text = mySqlDataReader["kpi_text"].ToString(),
              kpi_value = Convert.ToInt32(mySqlDataReader["kpi_value"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return universityKpiGridList;
    }

    public List<tbl_university_kpi_master> getkpiList(int OID)
    {
      List<tbl_university_kpi_master> universityKpiMasterList = new List<tbl_university_kpi_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "select * from tbl_university_kpi_master where id_organization=@OID";
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            universityKpiMasterList.Add(new tbl_university_kpi_master()
            {
              id_kpi_master = Convert.ToInt32(mySqlDataReader["id_kpi_master"].ToString()),
              id_organization = Convert.ToInt32(mySqlDataReader["id_organization"].ToString()),
              kpi_name = mySqlDataReader["kpi_name"].ToString(),
              kpi_description = mySqlDataReader["kpi_description"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return universityKpiMasterList;
    }

    public int checkVaidKpiId(int id_kpiGri)
    {
      List<tbl_university_kpi_grid> universityKpiGridList = new List<tbl_university_kpi_grid>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "select * from tbl_university_kpi_grid where id_kpi_master=@id_kpiGri";
        command.Parameters.AddWithValue(nameof (id_kpiGri), (object) id_kpiGri);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            universityKpiGridList.Add(new tbl_university_kpi_grid()
            {
              id_kpi_master = Convert.ToInt32(mySqlDataReader["id_kpi_master"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return universityKpiGridList.Count;
    }

    public List<tbl_special_metric_master> Get_SpecialMet(int OID)
    {
      List<tbl_special_metric_master> specialMet = new List<tbl_special_metric_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_special_metric_master where id_org=@OID";
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            specialMet.Add(new tbl_special_metric_master()
            {
              id_special_metric = Convert.ToInt32(mySqlDataReader["id_special_metric"].ToString()),
              name = mySqlDataReader["name"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return specialMet;
    }

    public List<tbl_university_special_points_master> getSp_points(int OID)
    {
      List<tbl_university_special_points_master> spPoints = new List<tbl_university_special_points_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_university_special_points_master where id_organization=@OID";
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            spPoints.Add(new tbl_university_special_points_master()
            {
              id_special_points = Convert.ToInt32(mySqlDataReader["id_special_points"].ToString()),
              special_value_name = mySqlDataReader["special_value_name"].ToString(),
              special_value_description = mySqlDataReader["special_value_description"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return spPoints;
    }

    public List<tbl_university_special_points_master> getSpMas(int id_sp)
    {
      List<tbl_university_special_points_master> spMas = new List<tbl_university_special_points_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_university_special_points_master where id_special_points=@id_sp";
        command.Parameters.AddWithValue(nameof (id_sp), (object) id_sp);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            spMas.Add(new tbl_university_special_points_master()
            {
              id_special_points = Convert.ToInt32(mySqlDataReader["id_special_points"].ToString()),
              id_organization = Convert.ToInt32(mySqlDataReader["id_organization"].ToString()),
              special_value_name = mySqlDataReader["special_value_name"].ToString(),
              special_value_description = mySqlDataReader["special_value_description"].ToString(),
              special_value_type = Convert.ToInt32(mySqlDataReader["special_value_type"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return spMas;
    }

    public List<tbl_university_special_point_grid> getspGrid(int id_sp)
    {
      List<tbl_university_special_point_grid> specialPointGridList = new List<tbl_university_special_point_grid>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_university_special_point_grid where id_special_points=@id_sp";
        command.Parameters.AddWithValue(nameof (id_sp), (object) id_sp);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            specialPointGridList.Add(new tbl_university_special_point_grid()
            {
              id_special_point_grid = Convert.ToInt32(mySqlDataReader["id_special_point_grid"].ToString()),
              id_special_points = Convert.ToInt32(mySqlDataReader["id_special_points"].ToString()),
              start_range = Convert.ToDouble(mySqlDataReader["start_range"].ToString()),
              end_range = Convert.ToInt32(mySqlDataReader["end_range"].ToString()),
              special_value = Convert.ToInt32(mySqlDataReader["special_value"].ToString()),
              special_text = mySqlDataReader["special_text"].ToString(),
                ////special_metric = Convert.ToInt32(mySqlDataReader["special_metric"].ToString())
                special_metric = mySqlDataReader["special_metric"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return specialPointGridList;
    }

    public int checkVaidSpId(int id_Sp_master)
    {
      List<tbl_university_special_point_grid> specialPointGridList = new List<tbl_university_special_point_grid>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_university_special_point_grid where id_special_points=@id_Sp_master";
        command.Parameters.AddWithValue(nameof (id_Sp_master), (object) id_Sp_master);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            specialPointGridList.Add(new tbl_university_special_point_grid()
            {
              id_special_point_grid = Convert.ToInt32(mySqlDataReader["id_special_point_grid"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return specialPointGridList.Count;
    }

    public bool UpdateSpecial_Points(
      int id_creator,
      int id_organization,
      string SpMet_name,
      string SpMet_description,
      string SPIID,
      int SpMet_type,
      DateTime expiry_date,
      DateTime start_date,
      string stat,
      DateTime updated_date_time,
      int id_spMas)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_university_special_points_master` SET `id_organization` = @id_organization,`special_value_name` = @SpMet_name,`special_value_description` = @SpMet_description,`special_value_type` = @SpMet_type,`SPIID` = @SPIID,`id_creator` = @id_creator,`status` = @stat,`updated_date_time` = @updated_date_time,`start_date` = @start_date,`expiry_date` = @expiry_date WHERE `id_special_points` = @id_spMas";
        command.Parameters.AddWithValue(nameof (id_creator), (object) id_creator);
        command.Parameters.AddWithValue(nameof (id_organization), (object) id_organization);
        command.Parameters.AddWithValue(nameof (SpMet_name), (object) SpMet_name);
        command.Parameters.AddWithValue(nameof (SpMet_description), (object) SpMet_description);
        command.Parameters.AddWithValue(nameof (SPIID), (object) SPIID);
        command.Parameters.AddWithValue(nameof (SpMet_type), (object) SpMet_type);
        command.Parameters.AddWithValue(nameof (expiry_date), (object) expiry_date);
        command.Parameters.AddWithValue(nameof (start_date), (object) start_date);
        command.Parameters.AddWithValue(nameof (stat), (object) stat);
        command.Parameters.AddWithValue(nameof (updated_date_time), (object) updated_date_time);
        command.Parameters.AddWithValue(nameof (id_spMas), (object) id_spMas);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public bool Special_grid_update(
      int SpMet_value,
      string SpMet_text,
      double start_range,
      double end_range,
      string status,
      DateTime updated_datetime,
      int id_kpi_master,
      int special_metric,
      int id_SpGri)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_university_special_point_grid` SET `id_special_points` = @id_kpi_master,`start_range` = @start_range,`end_range` = @end_range,`special_value` = @SpMet_value,`special_text` = @SpMet_text,`special_metric` = @special_metric,`status` = @status,`updated_date_time` = @updated_datetime WHERE `id_special_point_grid` = @id_SpGri ";
        command.Parameters.AddWithValue(nameof (SpMet_value), (object) SpMet_value);
        command.Parameters.AddWithValue(nameof (SpMet_text), (object) SpMet_text);
        command.Parameters.AddWithValue(nameof (start_range), (object) start_range);
        command.Parameters.AddWithValue(nameof (end_range), (object) end_range);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (id_SpGri), (object) id_SpGri);
        command.Parameters.AddWithValue(nameof (special_metric), (object) special_metric);
        command.Parameters.AddWithValue(nameof (id_kpi_master), (object) id_kpi_master);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public List<tbl_game_master> getTblGameMas(int id)
    {
      List<tbl_game_master> tblGameMas = new List<tbl_game_master>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_game_master where id_game=@id_game";
        command.Parameters.AddWithValue("id_game", (object) id);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblGameMas.Add(new tbl_game_master()
            {
              id_game = Convert.ToInt32(mySqlDataReader["id_game"].ToString()),
              name = mySqlDataReader["name"].ToString(),
              description = mySqlDataReader["description"].ToString(),
              id_kpi = Convert.ToInt32(mySqlDataReader["id_kpi"].ToString()),
              game_type = Convert.ToInt32(mySqlDataReader["game_type"].ToString()),
              id_theme = Convert.ToInt32(mySqlDataReader["id_theme"].ToString()),
              id_metric = Convert.ToInt32(mySqlDataReader["id_metric"].ToString()),
              relegation_switch = Convert.ToInt32(mySqlDataReader["relegation_switch"].ToString()),
              id_special_metric = Convert.ToInt32(mySqlDataReader["id_special_metric"].ToString()),
              assignment_flag = mySqlDataReader["assignment_flag"].ToString(),
              status = mySqlDataReader["status"].ToString(),
              start_date = Convert.ToDateTime(mySqlDataReader["start_date"].ToString()),
              end_date = Convert.ToDateTime(mySqlDataReader["end_date"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblGameMas;
    }

    public List<tbl_leagues_data> getTblLeagueData(int id)
    {
      List<tbl_leagues_data> tblLeagueData = new List<tbl_leagues_data>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_leagues_data where id_game=@id_game";
        command.Parameters.AddWithValue("id_game", (object) id);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblLeagueData.Add(new tbl_leagues_data()
            {
              id_league_data = Convert.ToInt32(mySqlDataReader["id_league_data"].ToString()),
              id_league = Convert.ToInt32(mySqlDataReader["id_league"].ToString()),
              id_theme = Convert.ToInt32(mySqlDataReader["id_theme"].ToString()),
              id_game = Convert.ToInt32(mySqlDataReader["id_game"].ToString()),
              minscore = (double) Convert.ToInt32(mySqlDataReader["minscore"].ToString()),
              evaluation_type = Convert.ToInt32(mySqlDataReader["evaluation_type"].ToString()),
              expression_type = Convert.ToInt32(mySqlDataReader["expression_type"].ToString()),
              movement_number = Convert.ToInt32(mySqlDataReader["movement_number"].ToString()),
              level = Convert.ToInt32(mySqlDataReader["level"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblLeagueData;
    }

    public List<tbl_badge_data> getTblBadgeData(int id)
    {
      List<tbl_badge_data> tblBadgeData = new List<tbl_badge_data>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_badge_data where id_game=@id_game";
        command.Parameters.AddWithValue("id_game", (object) id);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblBadgeData.Add(new tbl_badge_data()
            {
              id_badge_data = Convert.ToInt32(mySqlDataReader["id_badge_data"].ToString()),
              required_score = Convert.ToInt32(mySqlDataReader["required_score"].ToString()),
              id_game = Convert.ToInt32(mySqlDataReader["id_game"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblBadgeData;
    }

    public List<tbl_relegtion_data> getTblRelegData(int id)
    {
      List<tbl_relegtion_data> tblRelegData = new List<tbl_relegtion_data>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_relegtion_data where id_game=@id_game";
        command.Parameters.AddWithValue("id_game", (object) id);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblRelegData.Add(new tbl_relegtion_data()
            {
              id_relegation = Convert.ToInt32(mySqlDataReader["id_relegation"].ToString()),
              frequency_type = mySqlDataReader["frequency_type"].ToString(),
              id_game = Convert.ToInt32(mySqlDataReader["id_game"].ToString()),
              id_league = Convert.ToInt32(mySqlDataReader["id_league"].ToString()),
              level = Convert.ToInt32(mySqlDataReader["level"].ToString()),
              relegation_frequency = Convert.ToInt32(mySqlDataReader["relegation_frequency"].ToString()),
              relegation_method = Convert.ToInt32(mySqlDataReader["relegation_method"].ToString()),
              relegation_expression = Convert.ToInt32(mySqlDataReader["relegation_expression"].ToString()),
              min_score = Convert.ToInt32(mySqlDataReader["min_score"].ToString()),
              status = mySqlDataReader["status"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblRelegData;
    }

    public List<tbl_currency_data> getTblCurrData(int id)
    {
      List<tbl_currency_data> tblCurrData = new List<tbl_currency_data>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_currency_data where id_game=@id_game";
        command.Parameters.AddWithValue("id_game", (object) id);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblCurrData.Add(new tbl_currency_data()
            {
              id_currency_data = Convert.ToInt32(mySqlDataReader["id_currency_data"].ToString()),
              id_badge = Convert.ToInt32(mySqlDataReader["id_badge"].ToString()),
              currency_value = Convert.ToInt32(mySqlDataReader["currency_value"].ToString()),
              id_currency = Convert.ToInt32(mySqlDataReader["id_currency"].ToString()),
              status = mySqlDataReader["status"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblCurrData;
    }

    public List<tbl_user_game_assignment> getTblUsrAss(int id)
    {
      List<tbl_user_game_assignment> tblUsrAss = new List<tbl_user_game_assignment>();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_user_game_assignment where id_game=@id_game";
        command.Parameters.AddWithValue("id_game", (object) id);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            tblUsrAss.Add(new tbl_user_game_assignment()
            {
              id_game_assignment = Convert.ToInt32(mySqlDataReader["id_game_assignment"].ToString()),
              id_user = Convert.ToInt32(mySqlDataReader["id_user"].ToString()),
              status = mySqlDataReader["status"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return tblUsrAss;
    }

    public int getIdSpoints(int id_theme)
    {
      tbl_university_special_points_master specialPointsMaster = new tbl_university_special_points_master();
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "SELECT * FROM tbl_university_special_points_master where id_theme=@id_theme";
        command.Parameters.AddWithValue(nameof (id_theme), (object) id_theme);
        command.ExecuteNonQuery();
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            specialPointsMaster.id_special_points = Convert.ToInt32(mySqlDataReader["id_special_points"].ToString());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return specialPointsMaster.id_special_points;
    }

    public void insertSpGrid(
      int id_Spoints,
      double startVal,
      int endVal,
      int spVal,
      string spTxt,
      int spMetr,
      string status,
      DateTime updated_dateTime,
      int idMetr,
      int idGam)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_university_special_point_grid` (`id_special_points`,`start_range`,`end_range`,`special_value`,`special_text`,`special_metric`,`status`,`updated_date_time`,`id_metric`,`id_game`) VALUES (@id_Spoints,@startVal,@endVal,@spVal,@spTxt,@spMetr,@status,@updated_dateTime,@idMetr,@idGam)";
        command.Parameters.AddWithValue(nameof (id_Spoints), (object) id_Spoints);
        command.Parameters.AddWithValue(nameof (startVal), (object) startVal);
        command.Parameters.AddWithValue(nameof (endVal), (object) endVal);
        command.Parameters.AddWithValue(nameof (spVal), (object) spVal);
        command.Parameters.AddWithValue(nameof (spTxt), (object) spTxt);
        command.Parameters.AddWithValue(nameof (spMetr), (object) spMetr);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_dateTime), (object) updated_dateTime);
        command.Parameters.AddWithValue(nameof (idMetr), (object) idMetr);
        command.Parameters.AddWithValue(nameof (idGam), (object) idGam);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
    }

    public bool delKpirow(int kpiId)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "DELETE FROM `tbl_university_kpi_master` WHERE id_kpi_master=@kpiId";
        command.Parameters.AddWithValue(nameof (kpiId), (object) kpiId);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return true;
    }

    public void academrestInser(
      int academicDrop,
      int OID,
      int acadTileCouV,
      DateTime updated_datetime,
      string status,
      int timeStam)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_academy_level_brief_restriction` (`id_academy`,`OID`,`brief_count`,`status`,`updated_date_time`,`time`) VALUES (@academicDrop,@OID,@acadTileCouV,@status,@updated_datetime,@timeStam)";
        command.Parameters.AddWithValue(nameof (academicDrop), (object) academicDrop);
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.Parameters.AddWithValue(nameof (acadTileCouV), (object) acadTileCouV);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (timeStam), (object) timeStam);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
    }

    public void briefrestInser(
      int breifHid,
      int OID,
      int briefTileCouV,
      DateTime updated_datetime,
      string status,
      int timeStam,
      int academicDrop)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_brief_tile_level_brief_restriction` (`id_brief_tile`,`id_academy`,`OID`,`brief_count`,`status`,`updated_date_time`,`time`) VALUES (@breifHid,@academicDrop,@OID,@briefTileCouV,@status,@updated_datetime,@timeStam)";
        command.Parameters.AddWithValue(nameof (breifHid), (object) breifHid);
        command.Parameters.AddWithValue(nameof (academicDrop), (object) academicDrop);
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.Parameters.AddWithValue(nameof (briefTileCouV), (object) briefTileCouV);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (timeStam), (object) timeStam);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
    }

    public void academrestUpdat(
      int academicDrop,
      int OID,
      int acadTileCouV,
      DateTime updated_datetime,
      string status,
      int timeStam)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_academy_level_brief_restriction` SET `brief_count` = @acadTileCouV,`status` = @status,`updated_date_time` = @updated_datetime,`time` = @timeStam WHERE `id_academy` =@academicDrop";
        command.Parameters.AddWithValue(nameof (academicDrop), (object) academicDrop);
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.Parameters.AddWithValue(nameof (acadTileCouV), (object) acadTileCouV);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (timeStam), (object) timeStam);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
    }

    public void briefrestUpdat(
      int breifHid,
      int OID,
      int briefTileCouV,
      DateTime updated_datetime,
      string status,
      int timeStam,
      int academicDrop,
      int idbrief)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_tile_level_brief_restriction` SET `id_brief_tile` = @breifHid,`OID` = @OID, `brief_count` = @briefTileCouV,`status` = @status, `updated_date_time` = @updated_datetime,`time` = @timeStam WHERE `id_restriction` = @idbrief";
        command.Parameters.AddWithValue(nameof (breifHid), (object) breifHid);
        command.Parameters.AddWithValue(nameof (academicDrop), (object) academicDrop);
        command.Parameters.AddWithValue(nameof (OID), (object) OID);
        command.Parameters.AddWithValue(nameof (briefTileCouV), (object) briefTileCouV);
        command.Parameters.AddWithValue(nameof (status), (object) status);
        command.Parameters.AddWithValue(nameof (updated_datetime), (object) updated_datetime);
        command.Parameters.AddWithValue(nameof (timeStam), (object) timeStam);
        command.Parameters.AddWithValue(nameof (idbrief), (object) idbrief);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
    }

    public List<tbl_academic_tiles> getGameTilesDash(int id_org)
    {
      List<tbl_academic_tiles> gameTilesDash = new List<tbl_academic_tiles>();
      try
      {
        string str = "select * from tbl_academic_tiles where id_org = @value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_org);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            gameTilesDash.Add(new tbl_academic_tiles()
            {
              id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString()),
              id_org = Convert.ToInt32(mySqlDataReader[nameof (id_org)].ToString()),
              status = mySqlDataReader["status"].ToString(),
              tile_description = mySqlDataReader["tile_description"].ToString(),
              tile_image = mySqlDataReader["tile_image"].ToString(),
              tile_name = mySqlDataReader["tile_name"].ToString(),
              tile_position = Convert.ToInt32(mySqlDataReader["tile_position"].ToString())
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return gameTilesDash;
    }
  }
}
