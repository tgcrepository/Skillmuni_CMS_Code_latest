// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.seminar
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class seminar
  {
    public int id_seminar { get; set; }

    public string title { get; set; }

    public string stream { get; set; }

    public string speaker_name { get; set; }

    public DateTime update_date_time { get; set; }

    public string location { get; set; }

    public string FIRSTNAME { get; set; }

    public string slot { get; set; }

    public int id_user { get; set; }

    public string ratings { get; set; }

    public string feedback { get; set; }

    public DateTime seminar_date { get; set; }

    public DateTime reg_date { get; set; }

    public string slot_details { get; set; }
  }
}
