// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_brief_tile_level_brief_restriction_count
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_brief_tile_level_brief_restriction_count
  {
    public int id_restriction { get; set; }

    public int id_brief_tile { get; set; }

    public int OID { get; set; }

    public int id_academy { get; set; }

    public int brief_count { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int time { get; set; }

    public string category_tile { get; set; }

    public string tile_name { get; set; }
  }
}
