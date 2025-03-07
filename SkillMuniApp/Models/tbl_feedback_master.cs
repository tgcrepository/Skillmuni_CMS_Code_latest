// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_feedback_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class tbl_feedback_master
  {
    public int id_feedback { get; set; }

    public int Issues { get; set; }

    public int Suggestions { get; set; }

    public int UI { get; set; }

    public int Content { get; set; }

    public string Description { get; set; }

    public int MediaFlag { get; set; }

    public int ID_PROFILE { get; set; }

    public string FIRSTNAME { get; set; }

    public int UID { get; set; }

    public int OID { get; set; }

    public DateTime updated_datetime { get; set; }

    public string media { get; set; }

    public List<string> file { get; set; }
  }
}
