// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.sul_event_map
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class sul_event_map
  {
    public Nullable<int> id_event { get; set; }

    public string event_title { get; set; }

    public string event_objective { get; set; }

    public string event_logo { get; set; }

    public Nullable<int> is_registration_needed { get; set; }

    public DateTime registration_start_date { get; set; }

    public DateTime registration_end_date { get; set; }

    public DateTime event_start_date { get; set; }

    public DateTime event_end_date { get; set; }

    public string event_duration { get; set; }

    public string location_text { get; set; }

    public string state { get; set; }

    public string city { get; set; }

    public string address { get; set; }

    public int is_event_closed { get; set; }

    public int? user_count { get; set; }

    public int is_college_restricted { get; set; }

    public int? id_college { get; set; }

    public Nullable<int> is_paid_event { get; set; }

    public string amount { get; set; }

    public Nullable<int> is_org_specified { get; set; }

    public Nullable<int> id_org { get; set; }

    public Nullable<int> is_sponsor_available { get; set; }

    public Nullable<int> id_sponsor { get; set; }

    public string sponsor_logo { get; set; }

    public string event_status { get; set; }

    public string contact_name { get; set; }

    public string contact_number { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public string cityname { get; set; }

    public int? is_whatsapp_sharing_needed { get; set; }

    public string whatsapp_message { get; set; }

    public string whatsapp_image { get; set; }
  }
}
