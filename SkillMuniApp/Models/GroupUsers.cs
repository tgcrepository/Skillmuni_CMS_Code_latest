// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.GroupUsers
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class GroupUsers
  {
    public int id_game_group { get; set; }

    public int id_organization { get; set; }

    public string user_name { get; set; }

    public string group_status { get; set; }

    public int id_user { get; set; }

    public string userid { get; set; }

    public string user_status { get; set; }

    public GroupUsers(MySqlDataReader reader)
    {
      this.group_status = Convert.ToString(reader[nameof (group_status)]);
      this.userid = Convert.ToString(reader[nameof (userid)]);
      this.user_name = Convert.ToString(reader[nameof (user_name)]);
      this.user_status = Convert.ToString(reader[nameof (user_status)]);
      this.id_organization = Convert.ToInt32(reader[nameof (id_organization)]);
      this.id_game_group = Convert.ToInt32(reader[nameof (id_game_group)]);
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
    }
  }
}
