// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_sul_seminar_timeslot_new
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_sul_seminar_timeslot_new
  {
    public int id_slot { get; set; }

    public int slot_start_time_hour { get; set; }

    public int slot_start_time_minute { get; set; }

    public string session_start { get; set; }

    public int slot_end_time_hour { get; set; }

    public int slot_end_time_minute { get; set; }

    public string session_end { get; set; }

    public int day { get; set; }

    public int serial_no { get; set; }

    public string speaker_name { get; set; }

    public string description { get; set; }

    public int count_restriction { get; set; }

    public int id_seminar { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public DateTime slot_date { get; set; }
  }
}
