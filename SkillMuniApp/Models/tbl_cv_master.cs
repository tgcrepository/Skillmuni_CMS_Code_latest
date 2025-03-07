// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_cv_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class tbl_cv_master
  {
    public int id_cv { get; set; }

    public int id_user { get; set; }

    public int oid { get; set; }

    public DateTime created_date { get; set; }

    public DateTime modified_date { get; set; }

    public string status { get; set; }

    public int cv_type { get; set; }

    public string CVRequest { get; set; }

    public List<string> tbl_video_cv { get; set; }

    public string videoname { get; set; }
  }
}
