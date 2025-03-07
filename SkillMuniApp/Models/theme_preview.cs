// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.theme_preview
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class theme_preview
  {
    public int id_theme { get; set; }

    public string name { get; set; }

    public string description { get; set; }

    public int id_creator { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }

    public string theme_logo { get; set; }

    public List<tbl_theme_metric> metric { get; set; }

    public List<tbl_theme_leagues> league { get; set; }

    public List<tbl_badge_master> badge { get; set; }

    public List<tbl_crrency_points> currency { get; set; }
  }
}
