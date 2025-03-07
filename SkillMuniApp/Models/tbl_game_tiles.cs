// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_game_tiles
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_game_tiles
  {
    public int id_game_tiles { get; set; }

    public string tile_name { get; set; }

    public string tile_image { get; set; }

    public int tile_position { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_org { get; set; }

    public string tile_description { get; set; }
  }
}
