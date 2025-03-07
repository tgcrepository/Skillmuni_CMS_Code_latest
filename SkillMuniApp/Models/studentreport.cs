// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.studentreport
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class studentreport
  {
    public int id_enquiry { get; set; }

    public string name { get; set; }

    public string mail { get; set; }

    public string phone { get; set; }

    public string brief_title { get; set; }

    public string enquiry { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
