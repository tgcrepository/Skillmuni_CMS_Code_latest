﻿// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.GameGroupSummary
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class GameGroupSummary
  {
    public string group_name { get; set; }

    public string start_date { get; set; }

    public string expiry_date { get; set; }

    public int id_game_group { get; set; }

    public GameGroupSummary(MySqlDataReader reader)
    {
      this.group_name = Convert.ToString(reader[nameof (group_name)]);
      this.start_date = Convert.ToString(reader[nameof (start_date)]);
      this.expiry_date = Convert.ToString(reader[nameof (expiry_date)]);
      this.id_game_group = Convert.ToInt32(reader[nameof (id_game_group)]);
    }
  }
}
