// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.BriefModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnext.Models
{
  public class BriefModel
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private MySqlConnection connection;

    public BriefModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<briefView> getBriefView(string sql)
    {
      List<briefView> briefView1 = new List<briefView>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          briefView briefView2 = new briefView(reader);
          briefView1.Add(briefView2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefView1;
    }

    public List<BriefUser> getBriefUserList(string sql)
    {
      List<BriefUser> briefUserList = new List<BriefUser>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          BriefUser briefUser = new BriefUser(reader);
          briefUserList.Add(briefUser);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefUserList;
    }

    public List<string> getUserListById(string sql)
    {
      List<string> userListById = new List<string>();
      try
      {
        string str1 = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          string str2 = Convert.ToString(mySqlDataReader["id_user"]);
          userListById.Add(str2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return userListById;
    }

    public int getAttamptNo(string sql)
    {
      int attamptNo = 0;
      try
      {
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sql;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            attamptNo = Convert.ToInt32(mySqlDataReader["subcount"]);
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return attamptNo;
    }

    public double getQuestionCounter(string sql)
    {
      double questionCounter = 0.0;
      try
      {
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sql;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            questionCounter = Convert.ToDouble(mySqlDataReader["counter"]);
            questionCounter = Math.Round(questionCounter, 2);
          }
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return questionCounter;
    }

    public List<TestBrief> getTestBriefUserList(string sql)
    {
      List<TestBrief> testBriefUserList = new List<TestBrief>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          TestBrief testBrief = new TestBrief(reader);
          testBriefUserList.Add(testBrief);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return testBriefUserList;
    }

    public List<BriefCollection> getUserTestResult(string sql)
    {
      List<BriefCollection> userTestResult = new List<BriefCollection>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          BriefCollection briefCollection = new BriefCollection(reader);
          userTestResult.Add(briefCollection);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return userTestResult;
    }

    public List<BriefResultSummery> getBriefResultSummery(string sql)
    {
      List<BriefResultSummery> briefResultSummery1 = new List<BriefResultSummery>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          BriefResultSummery briefResultSummery2 = new BriefResultSummery(reader);
          briefResultSummery1.Add(briefResultSummery2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefResultSummery1;
    }

    public List<UserAssignmentList> getBriefUnreadSummery(string sql)
    {
      List<UserAssignmentList> briefUnreadSummery = new List<UserAssignmentList>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          UserAssignmentList userAssignmentList = new UserAssignmentList(reader);
          briefUnreadSummery.Add(userAssignmentList);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefUnreadSummery;
    }

    public string getExtensionNumber(string ext)
    {
      ext = ext.ToLower();
      switch (ext)
      {
        case ".3gp":
        case ".avi":
        case ".flv":
        case ".mkv":
        case ".mp4":
        case ".mpeg":
        case ".wmv":
          return "2";
        case ".csv":
        case ".xls":
        case ".xlsx":
          return "4";
        case ".doc":
        case ".docm":
        case ".docx":
        case ".dot":
          return "3";
        case ".gif":
        case ".jpeg":
        case ".jpg":
        case ".png":
          return "1";
        case ".m4a":
        case ".mp3":
        case ".wav":
        case ".wma":
          return "7";
        case ".pdf":
          return "6";
        case ".pps":
        case ".ppt":
        case ".pptx":
          return "5";
        default:
          return "0";
      }
    }
  }
}
