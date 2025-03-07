// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_college_list
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_college_list
  {
    public int id_college { get; set; }

    public string college_name { get; set; }

    public string clg_city { get; set; }

    public string clg_state { get; set; }

    public string clg_phone { get; set; }

    public int id_city { get; set; }

    public int id_organization { get; set; }

    public int id_user { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }
  }
}
