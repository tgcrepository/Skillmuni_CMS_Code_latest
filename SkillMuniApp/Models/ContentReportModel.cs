// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.ContentReportModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnext.Models
{
  public class ContentReportModel
  {
    private MySqlConnection conn;

    public ContentReportModel() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public int getRecordCount(string str)
    {
      int recordCount = 0;
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            recordCount = Convert.ToInt32(mySqlDataReader["count"].ToString());
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
      return recordCount;
    }

    public List<ContentLike> getContentLikes(string str)
    {
      List<ContentLike> contentLikes = new List<ContentLike>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentLikes.Add(new ContentLike()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            LIKES = Convert.ToInt32(mySqlDataReader["LikeCount"].ToString()),
            DISLIKES = Convert.ToInt32(mySqlDataReader["DisLikeCount"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["COUNTER"].ToString()),
            ENDDATE = mySqlDataReader["EXPIRY_DATE"].ToString(),
            LASTACCESS = mySqlDataReader["LASTACCESS"].ToString(),
            CONTENT = mySqlDataReader["CONTENT_QUESTION"].ToString()
          });
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
      return contentLikes;
    }

    public List<ContentLocationWise> getContentLocation(string str)
    {
      List<ContentLocationWise> contentLocation = new List<ContentLocationWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentLocation.Add(new ContentLocationWise()
          {
            LOCATION = mySqlDataReader["LOCATION"].ToString(),
            FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString(),
            LASTNAME = mySqlDataReader["LASTNAME"].ToString(),
            USERID = mySqlDataReader["USERID"].ToString(),
            ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString())
          });
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
      return contentLocation;
    }

    public List<ContentLocationGenderWise> getContentLocationGender(string str)
    {
      List<ContentLocationGenderWise> contentLocationGender = new List<ContentLocationGenderWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          ContentLocationGenderWise locationGenderWise = new ContentLocationGenderWise()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString()),
            MALE = Convert.ToInt32(mySqlDataReader["MALE"].ToString()),
            FEMALE = Convert.ToInt32(mySqlDataReader["FEMALE"].ToString())
          };
          locationGenderWise.CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString());
          contentLocationGender.Add(locationGenderWise);
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
      return contentLocationGender;
    }

    public List<ContentLocationGenderWise> getLocationGenderAccess(string str)
    {
      List<ContentLocationGenderWise> locationGenderAccess = new List<ContentLocationGenderWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationGenderAccess.Add(new ContentLocationGenderWise()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString()),
            MALE = Convert.ToInt32(mySqlDataReader["MALE"].ToString()),
            FEMALE = Convert.ToInt32(mySqlDataReader["FEMALE"].ToString())
          });
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
      return locationGenderAccess;
    }

    public List<ContentLocationGenderWise> getDesignationAccess(string str)
    {
      List<ContentLocationGenderWise> designationAccess = new List<ContentLocationGenderWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          designationAccess.Add(new ContentLocationGenderWise()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString()),
            DESIGNATION = mySqlDataReader["DESIGNATION"].ToString()
          });
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
      return designationAccess;
    }

    public List<string> getLocationList(int oid, string lAdd)
    {
      List<string> locationList = new List<string>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = "select Distinct LOCATION from tbl_profile where id_user in (select id_user from tbl_role_user_mapping where id_organization=" + (object) oid + lAdd + ")";
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          string str = mySqlDataReader["LOCATION"].ToString();
          if (!string.IsNullOrEmpty(str))
            locationList.Add(str);
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
      return locationList;
    }

    public List<ContentLike> getContentAccess(string str)
    {
      List<ContentLike> contentAccess = new List<ContentLike>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentAccess.Add(new ContentLike()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            LIKES = 0,
            DISLIKES = 0,
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["COUNTER"].ToString()),
            ENDDATE = mySqlDataReader["EXPIRY_DATE"].ToString(),
            LASTACCESS = mySqlDataReader["LASTACCESS"].ToString(),
            CONTENT = mySqlDataReader["CONTENT_QUESTION"].ToString()
          });
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
      return contentAccess;
    }

    public List<ContentLocationWise> getLocationAccess(string str)
    {
      List<ContentLocationWise> locationAccess = new List<ContentLocationWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationAccess.Add(new ContentLocationWise()
          {
            LOCATION = mySqlDataReader["LOCATION"].ToString(),
            FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString(),
            LASTNAME = mySqlDataReader["LASTNAME"].ToString(),
            USERID = mySqlDataReader["USERID"].ToString(),
            ID_USER = Convert.ToInt32(mySqlDataReader["id_user"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString())
          });
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
      return locationAccess;
    }

    public MonthData getLocationCount(string str)
    {
      MonthData locationCount = new MonthData();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationCount.LIKES = Convert.ToInt32(mySqlDataReader["ContentAccess"].ToString());
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
      return locationCount;
    }

    public MonthData getLocationGenderCount(string str)
    {
      MonthData locationGenderCount = new MonthData();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          locationGenderCount.LIKES = Convert.ToInt32(mySqlDataReader["MALE"].ToString());
          locationGenderCount.DISLIKES = Convert.ToInt32(mySqlDataReader["FEMALE"].ToString());
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
      return locationGenderCount;
    }

    public MonthData getContentCount(string str)
    {
      MonthData contentCount = new MonthData();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          contentCount.LIKES = Convert.ToInt32(mySqlDataReader["LIKES"].ToString());
          contentCount.DISLIKES = Convert.ToInt32(mySqlDataReader["DISLIKES"].ToString());
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
      return contentCount;
    }

    public List<ContentLocationWise> getLocationWiseContentAccess(string str)
    {
      List<ContentLocationWise> wiseContentAccess = new List<ContentLocationWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          wiseContentAccess.Add(new ContentLocationWise()
          {
            ID_USER = Convert.ToInt32(mySqlDataReader["id_user"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["COUNTER"].ToString()),
            USERID = mySqlDataReader["USERID"].ToString(),
            FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString(),
            LASTNAME = mySqlDataReader["LASTNAME"].ToString(),
            LOCATION = mySqlDataReader["LOCATION"].ToString().ToUpper()
          });
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
      return wiseContentAccess;
    }

    public List<EventModel> getEventStatusList(string str)
    {
      List<EventModel> eventStatusList = new List<EventModel>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          EventModel eventModel = new EventModel()
          {
            ID_EVENT = Convert.ToInt32(mySqlDataReader["ID_EVENT"].ToString()),
            EVENT_NAME = mySqlDataReader["EVENT_NAME"].ToString(),
            EVENT_TYPE = mySqlDataReader["EVENT_TYPE"].ToString(),
            STATUS = mySqlDataReader["STATUS"].ToString(),
            EVENT_CREATOR = mySqlDataReader["EVENT_CREATOR"].ToString(),
            EVENT_DATE = mySqlDataReader["EVENT_DATE"].ToString(),
            EVENT_GROUP_TYPE = mySqlDataReader["EVENT_GROUP_TYPE"].ToString(),
            NO_OF_USER = Convert.ToInt32(mySqlDataReader["NO_OF_USER"].ToString()),
            APPROVAL_COUNT = Convert.ToInt32(mySqlDataReader["APPROVAL_COUNT"].ToString()),
            PENDING_COUNT = Convert.ToInt32(mySqlDataReader["PENDING_COUNT"].ToString()),
            REJECT_COUNT = Convert.ToInt32(mySqlDataReader["REJECT_COUNT"].ToString())
          };
          eventModel.ID_EVENT = Convert.ToInt32(mySqlDataReader["ID_EVENT"].ToString());
          eventModel.TOTAL_COUNT = Convert.ToInt32(mySqlDataReader["TOTAL_COUNT"].ToString());
          eventStatusList.Add(eventModel);
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
      return eventStatusList;
    }

    public List<EventModel> getEmptyEventStatusList(string str)
    {
      List<EventModel> emptyEventStatusList = new List<EventModel>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          EventModel eventModel = new EventModel();
          eventModel.ID_EVENT = Convert.ToInt32(mySqlDataReader["ID_EVENT"].ToString());
          eventModel.EVENT_NAME = mySqlDataReader["EVENT_NAME"].ToString();
          eventModel.EVENT_TYPE = mySqlDataReader["EVENT_TYPE"].ToString();
          eventModel.STATUS = mySqlDataReader["STATUS"].ToString();
          eventModel.EVENT_CREATOR = mySqlDataReader["EVENT_CREATOR"].ToString();
          eventModel.EVENT_DATE = mySqlDataReader["EVENT_DATE"].ToString();
          eventModel.EVENT_GROUP_TYPE = mySqlDataReader["EVENT_GROUP_TYPE"].ToString();
          eventModel.NO_OF_USER = Convert.ToInt32(mySqlDataReader["NO_OF_USER"].ToString());
          eventModel.APPROVAL_COUNT = 0;
          eventModel.PENDING_COUNT = 0;
          Convert.ToInt32(mySqlDataReader["PENDING_COUNT"].ToString());
          eventModel.REJECT_COUNT = 0;
          eventModel.TOTAL_COUNT = 0;
          emptyEventStatusList.Add(eventModel);
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
      return emptyEventStatusList;
    }
  }
}
