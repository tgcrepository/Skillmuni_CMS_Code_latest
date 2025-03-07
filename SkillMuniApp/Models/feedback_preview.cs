// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.feedback_preview
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class feedback_preview
  {
    public int id_feedback { get; set; }

    public string name { get; set; }

    public string description { get; set; }

    public DateTime updated_datetime { get; set; }

    public int Issues { get; set; }

    public List<tbl_profile> FIRSTNAME { get; set; }

    public List<tbl_profile> ID_PROFILE { get; set; }
  }
}
