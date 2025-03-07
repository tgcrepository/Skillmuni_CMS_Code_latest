// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.briefView
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class briefView
  {
    public int id_brief_master { get; set; }

    public string brief_title { get; set; }

    public int brief_type { get; set; }

    public string brief_code { get; set; }

    public int question_count { get; set; }

    public DateTime scheduled_timestamp { get; set; }

    public string brief_category { get; set; }

    public string brief_subcategory { get; set; }

    public string brief_status { get; set; }

    public int status_code { get; set; }

    public string creator_name { get; set; }

    public briefView(MySqlDataReader reader)
    {
      this.id_brief_master = Convert.ToInt32(reader[nameof (id_brief_master)]);
      this.brief_type = Convert.ToInt32(reader[nameof (brief_type)]);
      this.question_count = Convert.ToInt32(reader[nameof (question_count)]);
      this.status_code = Convert.ToInt32(reader[nameof (status_code)]);
      this.brief_title = Convert.ToString(reader[nameof (brief_title)]);
      this.brief_code = Convert.ToString(reader[nameof (brief_code)]);
      this.brief_category = Convert.ToString(reader[nameof (brief_category)]);
      this.brief_subcategory = Convert.ToString(reader[nameof (brief_subcategory)]);
      this.brief_status = Convert.ToString(reader[nameof (brief_status)]);
      this.scheduled_timestamp = Convert.ToDateTime(reader[nameof (scheduled_timestamp)]);
      try
      {
        this.creator_name = Convert.ToString(reader[nameof (creator_name)]);
      }
      catch (Exception ex)
      {
        this.creator_name = "";
      }
    }
  }
}
