// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_corporate_form
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_corporate_form
  {
    public int id_corporatem { get; set; }

    public string name { get; set; }

    public string contact_number { get; set; }

    public string email { get; set; }

    public string college_name { get; set; }

    public string form_flag { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }
  }
}
