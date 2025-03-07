// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.IndustryRole
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class IndustryRole
  {
    public int id_ce_evaluation_jobrole { get; set; }

    public int id_organization { get; set; }

    public string ce_industry_role { get; set; }

    public string ce_role_code { get; set; }

    public string description { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
