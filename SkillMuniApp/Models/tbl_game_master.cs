// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_game_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class tbl_game_master
  {
    public int id_game { get; set; }

    public string name { get; set; }

    public string description { get; set; }

    public int id_kpi { get; set; }

    public int game_type { get; set; }

    public int id_theme { get; set; }

    public int id_metric { get; set; }

    public int id_org { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int relegation_switch { get; set; }

    public int id_special_metric { get; set; }

    public string assignment_flag { get; set; }

    public DateTime start_date { get; set; }

    public DateTime end_date { get; set; }

    public string metricname { get; set; }

    public List<int> kpiids { get; set; }

    public int metrics_completion_flag { get; set; }

    public int added_metric_count { get; set; }
  }
}
