// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.BuisinessLogic
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnext.Models
{
  public class BuisinessLogic
  {
    private MySqlConnection conn;

    public BuisinessLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public tbl_non_disclousure_clause_content getContent(int oid)
    {
      tbl_non_disclousure_clause_content content = new tbl_non_disclousure_clause_content();
      try
      {
        string str = "select * from tbl_non_disclousure_clause_content where id_org = @value1 ";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) oid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            content.content = mySqlDataReader["content"].ToString();
            content.status = mySqlDataReader["status"].ToString();
            content.content_title = mySqlDataReader["content_title"].ToString();
            content.id_clause_content = Convert.ToInt32(mySqlDataReader["id_clause_content"].ToString());
            content.id_creator = new int?(Convert.ToInt32(mySqlDataReader["id_creator"].ToString()));
            content.id_org = new int?(Convert.ToInt32(mySqlDataReader["id_org"].ToString()));
            content.updated_date_time = new DateTime?(Convert.ToDateTime(mySqlDataReader["updated_date_time"].ToString()));
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
      return content;
    }

    public void UpdateContent(tbl_non_disclousure_clause_content content)
    {
      this.conn.CreateCommand();
      menu menu = new menu();
      MySqlCommand command = this.conn.CreateCommand();
      this.conn.Open();
      command.CommandText = "update  tbl_non_disclousure_clause_content set content_title=@value1, content=@value2, updated_date_time=@value3 , id_creator=@value4, status=@value6 where id_org=@value5";
      command.Parameters.AddWithValue("value1", (object) content.content_title);
      command.Parameters.AddWithValue("value2", (object) content.content);
      command.Parameters.AddWithValue("value3", (object) content.updated_date_time);
      command.Parameters.AddWithValue("value4", (object) content.id_creator);
      command.Parameters.AddWithValue("value5", (object) content.id_org);
      command.Parameters.AddWithValue("value6", (object) "A");
      command.ExecuteNonQuery();
      this.conn.Close();
    }

    public void AddContent(tbl_non_disclousure_clause_content content)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO tbl_non_disclousure_clause_content(id_org, content,content_title,id_creator,updated_date_time,status) VALUES (@value1,@value2,@value3,@value5,@value6,@value7)";
        command.Parameters.AddWithValue("value1", (object) content.id_org);
        command.Parameters.AddWithValue("value2", (object) content.content);
        command.Parameters.AddWithValue("value3", (object) content.content_title);
        command.Parameters.AddWithValue("value5", (object) content.id_creator);
        command.Parameters.AddWithValue("value6", (object) content.updated_date_time);
        command.Parameters.AddWithValue("value7", (object) "A");
        command.ExecuteNonQuery();
        this.conn.Close();
      }
      catch (Exception ex)
      {
      }
    }

    public void ActivateNonDisclosureContent(int oid, int uid, string status)
    {
      this.conn.CreateCommand();
      menu menu = new menu();
      MySqlCommand command = this.conn.CreateCommand();
      this.conn.Open();
      command.CommandText = "update  tbl_non_disclousure_clause_content set  updated_date_time=@value3 , id_creator=@value4, status=@value6 where id_org=@value5";
      command.Parameters.AddWithValue("value3", (object) DateTime.Now);
      command.Parameters.AddWithValue("value4", (object) uid);
      command.Parameters.AddWithValue("value5", (object) oid);
      command.Parameters.AddWithValue("value6", (object) status);
      command.ExecuteNonQuery();
      this.conn.Close();
    }

    public void AddBatch(List<EventBatch> batch)
    {
      try
      {
        foreach (EventBatch eventBatch in batch)
        {
          this.conn.CreateCommand();
          menu menu = new menu();
          MySqlCommand command = this.conn.CreateCommand();
          this.conn.Open();
          command.CommandText = "INSERT INTO tbl_batch(id_event, batch_time,id_org,status,update_date,participants) VALUES (@value1,@value2,@value3,@value4,@value5,@value6)";
          command.Parameters.AddWithValue("value1", (object) eventBatch.id_event);
          command.Parameters.AddWithValue("value2", (object) eventBatch.batch_time);
          command.Parameters.AddWithValue("value3", (object) eventBatch.id_org);
          command.Parameters.AddWithValue("value4", (object) eventBatch.status);
          command.Parameters.AddWithValue("value5", (object) eventBatch.update_date);
          command.Parameters.AddWithValue("value6", (object) eventBatch.participants);
          command.ExecuteNonQuery();
          this.conn.Close();
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void UpdateBatch(List<EventBatch> batch)
    {
      try
      {
        foreach (EventBatch eventBatch in batch)
        {
          this.conn.CreateCommand();
          menu menu = new menu();
          MySqlCommand command = this.conn.CreateCommand();
          this.conn.Open();
          command.CommandText = "Update  tbl_batch set id_event=@value1, batch_time=@value2,id_org=@value3,status=@value4,update_date=@value5,participants=@value6 where id_event_batch=@value7";
          command.Parameters.AddWithValue("value1", (object) eventBatch.id_event);
          command.Parameters.AddWithValue("value2", (object) eventBatch.batch_time);
          command.Parameters.AddWithValue("value3", (object) eventBatch.id_org);
          command.Parameters.AddWithValue("value4", (object) eventBatch.status);
          command.Parameters.AddWithValue("value5", (object) eventBatch.update_date);
          command.Parameters.AddWithValue("value6", (object) eventBatch.participants);
          command.Parameters.AddWithValue("value7", (object) eventBatch.id_event_batch);
          command.ExecuteNonQuery();
          this.conn.Close();
        }
      }
      catch (Exception ex)
      {
      }
    }

    public List<EventBatch> getBatchList(int id)
    {
      List<EventBatch> batchList = new List<EventBatch>();
      try
      {
        string str = "select * from tbl_batch where id_event = @value1 ";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            batchList.Add(new EventBatch()
            {
              batch_time = mySqlDataReader["batch_time"].ToString(),
              id_event = Convert.ToInt32(mySqlDataReader["id_event"].ToString()),
              id_event_batch = Convert.ToInt32(mySqlDataReader["id_event_batch"].ToString()),
              id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString()),
              participants = Convert.ToInt32(mySqlDataReader["participants"].ToString()),
              status = mySqlDataReader["status"].ToString(),
              update_date = Convert.ToDateTime(mySqlDataReader["update_date"].ToString())
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
      return batchList;
    }

    public List<string> getLocation(string qry)
    {
      List<string> location = new List<string>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = qry;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          string str = mySqlDataReader["Location"].ToString();
          location.Add(str);
        }
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return location;
    }

    public void insert_locationfilter(int id, string location, int faci)
    {
      this.conn.CreateCommand();
      menu menu = new menu();
      MySqlCommand command = this.conn.CreateCommand();
      this.conn.Open();
      command.CommandText = "update  tbl_scheduled_event set  location=@value1 , id_facilitator=@value3 where id_scheduled_event=@value2";
      command.Parameters.AddWithValue("value1", (object) location);
      command.Parameters.AddWithValue("value2", (object) id);
      command.Parameters.AddWithValue("value3", (object) faci);
      command.ExecuteNonQuery();
      this.conn.Close();
    }

    public string getLocationFilter(int id)
    {
      string locationFilter = "";
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = "SELECT location FROM tbl_scheduled_event where id_scheduled_event=@value1";
        command.Parameters.AddWithValue("value1", (object) id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationFilter = mySqlDataReader["location"].ToString();
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return locationFilter;
    }

    public List<tbl_facilitator_users> getFacilitators()
    {
      List<tbl_facilitator_users> facilitators = new List<tbl_facilitator_users>();
      try
      {
        string str = "select * from tbl_facilitator_users ";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            facilitators.Add(new tbl_facilitator_users()
            {
              ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"].ToString()),
              firstname = mySqlDataReader["firstname"].ToString() + mySqlDataReader["lastname"].ToString()
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
      return facilitators;
    }

    public string getFaciName(int id)
    {
      string faciName = "";
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = "SELECT * FROM tbl_facilitator_users where ID_USER=@value1";
        command.Parameters.AddWithValue("value1", (object) id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          faciName = mySqlDataReader["firstname"].ToString() + mySqlDataReader["lastname"].ToString();
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return faciName;
    }

    public tbl_facilitator_users getSelectedFaci(int id)
    {
      tbl_facilitator_users selectedFaci = new tbl_facilitator_users();
      try
      {
        string str = "select * from tbl_facilitator_users where ID_USER= " + (object) id;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            selectedFaci.ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"].ToString());
            selectedFaci.firstname = mySqlDataReader["firstname"].ToString() + mySqlDataReader["lastname"].ToString();
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
      return selectedFaci;
    }

    public int getFaciId(int id)
    {
      int faciId = 0;
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = "SELECT id_facilitator FROM tbl_scheduled_event where id_scheduled_event=@value1";
        command.Parameters.AddWithValue("value1", (object) id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          faciId = Convert.ToInt32(mySqlDataReader["id_facilitator"].ToString());
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return faciId;
    }
  }
}
