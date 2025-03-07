// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.higher_education_dashboard
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class higher_education_dashboard
  {
    public int id_higher_education { get; set; }

    public string message_to_display { get; set; }

    public string redirect_url { get; set; }

    public string event_name { get; set; }

    public string location { get; set; }

    public string ratings { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
