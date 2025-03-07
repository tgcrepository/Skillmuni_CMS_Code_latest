// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.cms_content_managementModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System.Configuration;

namespace m2ostnext.Models
{
  public class cms_content_managementModel
  {
    private MySqlConnection conn;

    public cms_content_managementModel() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);
  }
}
