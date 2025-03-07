// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.BriefCollection
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class BriefCollection
  {
    public string brief_code { get; set; }

    public string brief_title { get; set; }

    public int attempt_no { get; set; }

    public int id_user { get; set; }

    public string FIRSTNAME { get; set; }

    public double brief_result { get; set; }

    public BriefCollection(MySqlDataReader reader)
    {
      this.attempt_no = Convert.ToInt32(reader[nameof (attempt_no)]);
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.brief_result = Convert.ToDouble(reader[nameof (brief_result)]);
      this.brief_code = Convert.ToString(reader[nameof (brief_code)]);
      this.brief_title = Convert.ToString(reader[nameof (brief_title)]);
      this.FIRSTNAME = Convert.ToString(reader[nameof (FIRSTNAME)]);
    }
  }
}
