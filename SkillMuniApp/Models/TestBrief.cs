// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.TestBrief
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class TestBrief
  {
    public string brief_title { get; set; }

    public string brief_code { get; set; }

    public int id_brief_master { get; set; }

    public int id_user { get; set; }

    public string firstname { get; set; }

    public TestBrief(MySqlDataReader reader)
    {
      this.id_brief_master = Convert.ToInt32(reader[nameof (id_brief_master)]);
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.brief_title = Convert.ToString(reader[nameof (brief_title)]);
      this.firstname = Convert.ToString(reader[nameof (firstname)]);
      this.brief_code = Convert.ToString(reader[nameof (brief_code)]);
    }
  }
}
