// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.VendorMailModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace m2ostnext.Models
{
  public class VendorMailModel
  {
    private MySqlConnection connection;

    public VendorMailModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public VendorMail getEmailDetails(string id)
    {
      VendorMail emailDetails = new VendorMail();
      try
      {
        string str = "SELECT * FROM tbl_vendor_mail WHERE id_vendor_mail = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          emailDetails = new VendorMail()
          {
            id_cscc_mail = Convert.ToInt32(mySqlDataReader["id_vendor_mail"].ToString()),
            email_id = mySqlDataReader["email_id"].ToString(),
            user_name = mySqlDataReader["user_name"].ToString(),
            password = mySqlDataReader["password"].ToString()
          };
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return emailDetails;
    }

    public int Update_reason(string reason, int ids, string status)
    {
      try
      {
        string str = "UPDATE tbl_vendor_registration SET status_reason = @value2,status= @value3 WHERE id_vendor_registation = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) ids);
        command.Parameters.AddWithValue("value2", (object) reason);
        command.Parameters.AddWithValue("value3", (object) status);
        return command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }
  }
}
