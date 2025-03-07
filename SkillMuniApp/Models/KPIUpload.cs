// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.KPIUpload
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class KPIUpload
  {
    public string KPIID { get; set; }

    public string KPIName { get; set; }

    public string CategoryName { get; set; }

    public string USERID { get; set; }

    public string EMPLOYEEID { get; set; }

    public string SCORE_TEXT { get; set; }

    public double SCORE { get; set; }

    public int id_category { get; set; }

    public int id_user { get; set; }

    public int id_kpi_master { get; set; }

    public DateTime LOGDATE { get; set; }
  }
}
