// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.UserAssignmentList
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class UserAssignmentList
  {
    public string prname { get; set; }

    public string rmname { get; set; }

    public DateTime assignedtime { get; set; }

    public int id_user { get; set; }

    public int id_brief_user_assignment { get; set; }

    public string brief_code { get; set; }

    public UserAssignmentList(MySqlDataReader reader)
    {
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.id_brief_user_assignment = Convert.ToInt32(reader[nameof (id_brief_user_assignment)]);
      this.brief_code = Convert.ToString(reader[nameof (brief_code)]);
      this.prname = Convert.ToString(reader[nameof (prname)]);
      this.rmname = Convert.ToString(reader[nameof (rmname)]);
      this.assignedtime = Convert.ToDateTime(reader[nameof (assignedtime)].ToString());
    }
  }
}
