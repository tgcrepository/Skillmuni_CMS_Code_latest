// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_ce_evaluation_tile
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_ce_evaluation_tile
  {
    public int id_ce_evaluation_tile { get; set; }

    public int id_organization { get; set; }

    public string ce_evaluation_tile { get; set; }

    public string ce_evaluation_code { get; set; }

    public string description { get; set; }

    public int sequence_order { get; set; }

    public string image_path { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
