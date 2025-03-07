// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.GameProgramSummary
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class GameProgramSummary
  {
    public int id_game { get; set; }

    public string game_title { get; set; }

    public int id_category { get; set; }

    public string category_name { get; set; }

    public string user_name { get; set; }

    public string userid { get; set; }

    public string employeeid { get; set; }

    public int id_user { get; set; }

    public double content_score { get; set; }

    public double content_weightage { get; set; }

    public List<scoring_matrix> assessment_score { get; set; }

    public List<scoring_matrix> kpi_score { get; set; }
  }
}
