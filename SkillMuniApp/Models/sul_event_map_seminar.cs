// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.sul_event_map_seminar
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class sul_event_map_seminar
  {
    public int id_seminar { get; set; }

    public bool isselected { get; set; }

    public string title { get; set; }

    public string objective { get; set; }

    public string stream { get; set; }

    public DateTime seminar_start_time { get; set; }

    public DateTime seminar_end_time { get; set; }

    public string seminar_duration { get; set; }

    public int? time_interval { get; set; }

    public string speaker_name { get; set; }

    public string speaker_organisation { get; set; }

    public string location { get; set; }

    public int? user_count { get; set; }

    public string seminar_status { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
