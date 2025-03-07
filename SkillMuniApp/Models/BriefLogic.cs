// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.BriefLogic
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace m2ostnext.Models
{
  public class BriefLogic
  {
    private MySqlConnection conn;

    public BriefLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public int InsertQuestion(tbl_brief_question qtn)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_brief_question` (`id_organization`,`brief_question`,`id_brief_category`,`id_brief_sub_category`,`question_type`,`question_theme_type`,`question_choice_type`,`question_complexity`,`expiry_date`,`status`,`updated_date_time`) VALUES (@id_organization,@brief_question,@id_brief_category,@id_brief_sub_category,@question_type,@question_theme_type,@question_choice_type,@question_complexity,@expiry_date,@status,@updated_date_time);SELECT LAST_INSERT_ID();";
        command.Parameters.AddWithValue("id_organization", (object) qtn.id_organization);
        command.Parameters.AddWithValue("brief_question", (object) qtn.brief_question);
        command.Parameters.AddWithValue("id_brief_category", (object) qtn.id_brief_category);
        command.Parameters.AddWithValue("id_brief_sub_category", (object) qtn.id_brief_sub_category);
        command.Parameters.AddWithValue("question_type", (object) qtn.question_type);
        command.Parameters.AddWithValue("question_theme_type", (object) qtn.question_theme_type);
        command.Parameters.AddWithValue("question_choice_type", (object) qtn.question_choice_type);
        command.Parameters.AddWithValue("question_complexity", (object) qtn.question_complexity);
        command.Parameters.AddWithValue("expiry_date", (object) qtn.expiry_date);
        command.Parameters.AddWithValue("status", (object) qtn.status);
        command.Parameters.AddWithValue("updated_date_time", (object) qtn.updated_date_time);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            num = Convert.ToInt32(mySqlDataReader["LAST_INSERT_ID()"].ToString());
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

    public int InsertQuestionImage(tbl_brief_question qtn)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_brief_question` (`id_organization`,`id_brief_category`,`id_brief_sub_category`,`question_type`,`question_theme_type`,`question_choice_type`,`question_complexity`,`expiry_date`,`status`,`updated_date_time`) VALUES (@id_organization,@id_brief_category,@id_brief_sub_category,@question_type,@question_theme_type,@question_choice_type,@question_complexity,@expiry_date,@status,@updated_date_time);SELECT LAST_INSERT_ID();";
        command.Parameters.AddWithValue("id_organization", (object) qtn.id_organization);
        command.Parameters.AddWithValue("id_brief_category", (object) qtn.id_brief_category);
        command.Parameters.AddWithValue("id_brief_sub_category", (object) qtn.id_brief_sub_category);
        command.Parameters.AddWithValue("question_type", (object) qtn.question_type);
        command.Parameters.AddWithValue("question_theme_type", (object) qtn.question_theme_type);
        command.Parameters.AddWithValue("question_choice_type", (object) qtn.question_choice_type);
        command.Parameters.AddWithValue("question_complexity", (object) qtn.question_complexity);
        command.Parameters.AddWithValue("expiry_date", (object) qtn.expiry_date);
        command.Parameters.AddWithValue("status", (object) qtn.status);
        command.Parameters.AddWithValue("updated_date_time", (object) qtn.updated_date_time);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            num = Convert.ToInt32(mySqlDataReader["LAST_INSERT_ID()"].ToString());
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

    public int InsertChoiceOpt(tbl_brief_answer ans)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_brief_answer` (`id_organization`,`id_brief_question`,`brief_answer`,`choice_type`,`is_correct_answer`,`status`,`updated_date_time`) VALUES (@id_organization,@id_brief_question,@brief_answer,@choice_type,@is_correct_answer,@status,@updated_date_time);SELECT LAST_INSERT_ID();";
        command.Parameters.AddWithValue("id_organization", (object) ans.id_organization);
        command.Parameters.AddWithValue("id_brief_question", (object) ans.id_brief_question);
        command.Parameters.AddWithValue("brief_answer", (object) ans.brief_answer);
        command.Parameters.AddWithValue("choice_type", (object) ans.choice_type);
        command.Parameters.AddWithValue("is_correct_answer", (object) ans.is_correct_answer);
        command.Parameters.AddWithValue("status", (object) ans.status);
        command.Parameters.AddWithValue("updated_date_time", (object) ans.updated_date_time);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            num = Convert.ToInt32(mySqlDataReader["LAST_INSERT_ID()"].ToString());
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

    public void updateQuestion(tbl_brief_question obj)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_question` SET `question_image` = @question_image WHERE `id_brief_question` = @id_brief_question";
        command.Parameters.AddWithValue("id_brief_question", (object) obj.id_brief_question);
        command.Parameters.AddWithValue("question_image", (object) obj.question_image);
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

    public int InsertimgChoiOpt(tbl_brief_answer ans)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "INSERT INTO `tbl_brief_answer` (`id_organization`,`id_brief_question`,`choice_type`,`is_correct_answer`,`status`,`updated_date_time`) VALUES (@id_organization,@id_brief_question,@choice_type,@is_correct_answer,@status,@updated_date_time);SELECT LAST_INSERT_ID();";
        command.Parameters.AddWithValue("id_organization", (object) ans.id_organization);
        command.Parameters.AddWithValue("id_brief_question", (object) ans.id_brief_question);
        command.Parameters.AddWithValue("choice_type", (object) ans.choice_type);
        command.Parameters.AddWithValue("is_correct_answer", (object) ans.is_correct_answer);
        command.Parameters.AddWithValue("status", (object) ans.status);
        command.Parameters.AddWithValue("updated_date_time", (object) ans.updated_date_time);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            num = Convert.ToInt32(mySqlDataReader["LAST_INSERT_ID()"].ToString());
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

    public int updateimgChoiOpt(tbl_brief_answer ans)
    {
      int num = 0;
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_answer` SET `choice_image` = @choice_image WHERE `id_brief_answer` = @id_brief_answer";
        command.Parameters.AddWithValue("id_brief_answer", (object) ans.id_brief_answer);
        command.Parameters.AddWithValue("choice_image", (object) ans.choice_image);
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
      return num;
    }

    public void updateBriefQuestion(tbl_brief_question qtn)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_question` SET `id_organization` = @id_organization,`brief_question` = @brief_question,`id_brief_category` = @id_brief_category,`id_brief_sub_category` = @id_brief_sub_category,`question_type` = @question_type,`question_theme_type` = @question_theme_type,`question_choice_type` = @question_choice_type, `question_complexity` = @question_complexity, `expiry_date` = @expiry_date, `status` = @status, `updated_date_time` = @updated_date_time WHERE `id_brief_question` = @id_brief_question";
        command.Parameters.AddWithValue("id_brief_question", (object) qtn.id_brief_question);
        command.Parameters.AddWithValue("id_organization", (object) qtn.id_organization);
        command.Parameters.AddWithValue("brief_question", (object) qtn.brief_question);
        command.Parameters.AddWithValue("id_brief_category", (object) qtn.id_brief_category);
        command.Parameters.AddWithValue("id_brief_sub_category", (object) qtn.id_brief_sub_category);
        command.Parameters.AddWithValue("question_type", (object) qtn.question_type);
        command.Parameters.AddWithValue("question_theme_type", (object) qtn.question_theme_type);
        command.Parameters.AddWithValue("question_choice_type", (object) qtn.question_choice_type);
        command.Parameters.AddWithValue("question_complexity", (object) qtn.question_complexity);
        command.Parameters.AddWithValue("expiry_date", (object) qtn.expiry_date);
        command.Parameters.AddWithValue("status", (object) qtn.status);
        command.Parameters.AddWithValue("updated_date_time", (object) qtn.updated_date_time);
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

    public void UpdateChoiceOpt(tbl_brief_answer ans)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_answer` SET `id_organization` = @id_organization,`id_brief_question` = @id_brief_question,`brief_answer` = @brief_answer,`is_correct_answer` = @is_correct_answer,`status` = @status,`updated_date_time` = @updated_date_time WHERE `id_brief_answer` = @id_brief_answer";
        command.Parameters.AddWithValue("id_organization", (object) ans.id_organization);
        command.Parameters.AddWithValue("id_brief_question", (object) ans.id_brief_question);
        command.Parameters.AddWithValue("brief_answer", (object) ans.brief_answer);
        command.Parameters.AddWithValue("choice_type", (object) ans.choice_type);
        command.Parameters.AddWithValue("is_correct_answer", (object) ans.is_correct_answer);
        command.Parameters.AddWithValue("status", (object) ans.status);
        command.Parameters.AddWithValue("updated_date_time", (object) ans.updated_date_time);
        command.Parameters.AddWithValue("id_brief_answer", (object) ans.id_brief_answer);
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

    public void UpdateQuestionSec(tbl_brief_question qtn)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_question` SET `id_organization` = @id_organization,`brief_question` = @brief_question,`id_brief_category` = @id_brief_category,`id_brief_sub_category` = @id_brief_sub_category,`question_type` = @question_type,`question_theme_type` = @question_theme_type,`question_choice_type` = @question_choice_type, `question_complexity` = @question_complexity, `expiry_date` = @expiry_date, `status` = @status, `updated_date_time` = @updated_date_time WHERE `id_brief_question` = @id_brief_question";
        command.Parameters.AddWithValue("id_organization", (object) qtn.id_organization);
        command.Parameters.AddWithValue("brief_question", (object) qtn.brief_question);
        command.Parameters.AddWithValue("id_brief_category", (object) qtn.id_brief_category);
        command.Parameters.AddWithValue("id_brief_sub_category", (object) qtn.id_brief_sub_category);
        command.Parameters.AddWithValue("question_type", (object) qtn.question_type);
        command.Parameters.AddWithValue("question_theme_type", (object) qtn.question_theme_type);
        command.Parameters.AddWithValue("question_choice_type", (object) qtn.question_choice_type);
        command.Parameters.AddWithValue("question_complexity", (object) qtn.question_complexity);
        command.Parameters.AddWithValue("expiry_date", (object) qtn.expiry_date);
        command.Parameters.AddWithValue("status", (object) qtn.status);
        command.Parameters.AddWithValue("updated_date_time", (object) qtn.updated_date_time);
        command.Parameters.AddWithValue("id_brief_question", (object) qtn.id_brief_question);
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

    public void UpdateQuestionImage(tbl_brief_question qtn)
    {
      try
      {
        this.conn.CreateCommand();
        menu menu = new menu();
        MySqlCommand command = this.conn.CreateCommand();
        this.conn.Open();
        command.CommandText = "UPDATE `tbl_brief_question` SET `id_organization` = @id_organization,`brief_question` = @brief_question,`id_brief_category` = @id_brief_category,`id_brief_sub_category` = @id_brief_sub_category,`question_type` = @question_type,`question_image` = @question_image,`question_theme_type` = @question_theme_type,`question_choice_type` = @question_choice_type, `question_complexity` = @question_complexity, `expiry_date` = @expiry_date, `status` = @status, `updated_date_time` = @updated_date_time WHERE `id_brief_question` = @id_brief_question";
        command.Parameters.AddWithValue("id_organization", (object) qtn.id_organization);
        command.Parameters.AddWithValue("brief_question", (object) qtn.brief_question);
        command.Parameters.AddWithValue("id_brief_category", (object) qtn.id_brief_category);
        command.Parameters.AddWithValue("id_brief_sub_category", (object) qtn.id_brief_sub_category);
        command.Parameters.AddWithValue("question_type", (object) qtn.question_type);
        command.Parameters.AddWithValue("question_image", (object) qtn.question_image);
        command.Parameters.AddWithValue("question_theme_type", (object) qtn.question_theme_type);
        command.Parameters.AddWithValue("question_choice_type", (object) qtn.question_choice_type);
        command.Parameters.AddWithValue("question_complexity", (object) qtn.question_complexity);
        command.Parameters.AddWithValue("expiry_date", (object) qtn.expiry_date);
        command.Parameters.AddWithValue("status", (object) qtn.status);
        command.Parameters.AddWithValue("updated_date_time", (object) qtn.updated_date_time);
        command.Parameters.AddWithValue("id_brief_question", (object) qtn.id_brief_question);
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
  }
}
