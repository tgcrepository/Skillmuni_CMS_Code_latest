// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.Higher
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class Higher
  {
    public int id_user { get; set; }

    public string FIRSTNAME { get; set; }

    public int id_register { get; set; }

    public int id_event { get; set; }

    public string event_title { get; set; }

    public DateTime higher_education_start_time { get; set; }

    public DateTime higher_education_end_time { get; set; }

    public string slot { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
