// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.Event
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class Event
  {
    public string event_title { get; set; }

    public string event_objective { get; set; }

    public string event_logo { get; set; }

    public string is_registration_needed { get; set; }

    public DateTime event_start_date { get; set; }

    public DateTime event_end_date { get; set; }

    public string event_duration { get; set; }

    public string location_text { get; set; }

    public string address { get; set; }

    public string is_event_closed { get; set; }

    public int user_count { get; set; }

    public string contact_name { get; set; }

    public string contact_number { get; set; }

    public int UID { get; set; }

    public string FIRSTNAME { get; set; }

    public string MOBILE { get; set; }

    public string EMAIL { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
