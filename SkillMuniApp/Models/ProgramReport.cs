// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.ProgramReport
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class ProgramReport
  {
    public int id_user { get; set; }

    public double percentage { get; set; }

    public double weightage { get; set; }

    public double pweightage { get; set; }

    public double score { get; set; }

    public ProgramReport(MySqlDataReader reader)
    {
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.percentage = Convert.ToDouble(reader[nameof (percentage)]);
      this.weightage = Convert.ToDouble(reader[nameof (weightage)]);
      this.pweightage = Convert.ToDouble(reader[nameof (pweightage)]);
      this.score = Convert.ToDouble(reader[nameof (score)]);
    }
  }
}
