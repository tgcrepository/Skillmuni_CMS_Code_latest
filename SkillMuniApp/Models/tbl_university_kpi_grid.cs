// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_university_kpi_grid
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

namespace m2ostnext.Models
{
  public class tbl_university_kpi_grid
  {
    public int id_kpi_grid { get; set; }

    public int id_kpi_master { get; set; }

    public double start_range { get; set; }

    public double end_range { get; set; }

    public string kpi_text { get; set; }

    public int kpi_value { get; set; }

    public string status { get; set; }

    public int updated_date_time { get; set; }

    public int id_game { get; set; }

    public int id_metric { get; set; }
  }
}
