// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.BriefResultSummery
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class BriefResultSummery
  {
    public double brief_result { get; set; }

    public string prname { get; set; }

    public string rmname { get; set; }

    public DateTime completedtime { get; set; }

    public int attempt_no { get; set; }

    public int id_user { get; set; }

    public BriefResultSummery(MySqlDataReader reader)
    {
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.attempt_no = Convert.ToInt32(reader[nameof (attempt_no)]);
      this.brief_result = Convert.ToDouble(reader[nameof (brief_result)]);
      this.prname = Convert.ToString(reader[nameof (prname)]);
      this.rmname = Convert.ToString(reader[nameof (rmname)]);
      this.completedtime = Convert.ToDateTime(reader[nameof (completedtime)].ToString());
    }
  }
}
