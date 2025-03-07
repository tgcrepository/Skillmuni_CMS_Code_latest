// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.BriefUser
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class BriefUser
  {
    public int PRUSER { get; set; }

    public string PRUSERID { get; set; }

    public string PRNAME { get; set; }

    public string PRFUNCTION { get; set; }

    public string PRCITY { get; set; }

    public string PRLOCATION { get; set; }

    public string RMUSER { get; set; }

    public string RMUSERID { get; set; }

    public string RMNAME { get; set; }

    public int id_brief_user_assignment { get; set; }

    public int id_brief_master { get; set; }

    public string PREMPLOYEEID { get; set; }

    public string DATETIMESTAMP { get; set; }

    public BriefUser(MySqlDataReader reader)
    {
      this.PRUSER = Convert.ToInt32(reader[nameof (PRUSER)]);
      this.PRUSERID = Convert.ToString(reader[nameof (PRUSERID)]);
      this.PRNAME = Convert.ToString(reader[nameof (PRNAME)]);
      this.PRFUNCTION = Convert.ToString(reader[nameof (PRFUNCTION)]);
      this.PRCITY = Convert.ToString(reader[nameof (PRCITY)]);
      this.PRLOCATION = Convert.ToString(reader[nameof (PRLOCATION)]);
      this.RMUSER = Convert.ToString(reader[nameof (RMUSER)]);
      this.RMUSERID = Convert.ToString(reader[nameof (RMUSERID)]);
      this.RMNAME = Convert.ToString(reader[nameof (RMNAME)]);
      this.PREMPLOYEEID = Convert.ToString(reader[nameof (PREMPLOYEEID)]);
      this.DATETIMESTAMP = Convert.ToString(reader[nameof (DATETIMESTAMP)]);
      this.id_brief_master = Convert.ToInt32(reader[nameof (id_brief_master)]);
      this.id_brief_user_assignment = Convert.ToInt32(reader[nameof (id_brief_user_assignment)]);
    }
  }
}
