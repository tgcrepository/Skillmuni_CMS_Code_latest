// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_university_special_points_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_university_special_points_master
  {
    public int id_special_points { get; set; }

    public int id_organization { get; set; }

    public string special_value_name { get; set; }

    public string special_value_description { get; set; }

    public int special_value_type { get; set; }

    public string SPIID { get; set; }

    public int id_creator { get; set; }

    public string status { get; set; }

    public int id_theme { get; set; }

    public DateTime updated_date_time { get; set; }

    public DateTime start_date { get; set; }

    public DateTime expiry_date { get; set; }
  }
}
