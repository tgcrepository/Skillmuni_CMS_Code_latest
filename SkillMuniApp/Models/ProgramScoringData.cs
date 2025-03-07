// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.ProgramScoringData
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class ProgramScoringData
  {
    public tbl_category program { get; set; }

    public List<m2ostnext.Models.KPIScoring> KPIScoring { get; set; }
  }
}
