// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_university_kpi_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_university_kpi_master
  {
    public int id_kpi_master { get; set; }

    public int id_organization { get; set; }

    public string kpi_name { get; set; }

    public string kpi_description { get; set; }

    public int kpi_type { get; set; }

    public string KPIID { get; set; }

    public int id_academy { get; set; }

    public int id_category { get; set; }

    public int id_creator { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public string metric_name { get; set; }
  }
}
