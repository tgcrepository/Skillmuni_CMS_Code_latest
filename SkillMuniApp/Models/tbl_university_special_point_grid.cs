// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_university_special_point_grid
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_university_special_point_grid
  {
    public int id_special_point_grid { get; set; }

    public int id_special_points { get; set; }

    public double start_range { get; set; }

    public int end_range { get; set; }

    public int special_value { get; set; }

    public string special_text { get; set; }

    public int special_metric { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_metric { get; set; }

    public int id_game { get; set; }
  }
}
