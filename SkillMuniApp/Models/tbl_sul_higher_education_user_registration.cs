// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_sul_higher_education_user_registration
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_sul_higher_education_user_registration
  {
    public int id_register { get; set; }

    public int id_higher_education { get; set; }

    public int id_user { get; set; }

    public string ratings { get; set; }

    public string feedback { get; set; }

    public string slot { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
