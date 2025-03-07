﻿// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.GameUserSummary
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class GameUserSummary
  {
    public int id_user { get; set; }

    public string USERID { get; set; }

    public string EMPLOYEEID { get; set; }

    public string UNAME { get; set; }

    public string LOCATION { get; set; }

    public string user_designation { get; set; }

    public string USTATUS { get; set; }

    public string start_date { get; set; }

    public string expiry_date { get; set; }

    public GameUserSummary(MySqlDataReader reader)
    {
      this.USERID = Convert.ToString(reader[nameof (USERID)]);
      this.start_date = Convert.ToString(reader[nameof (start_date)]);
      this.expiry_date = Convert.ToString(reader[nameof (expiry_date)]);
      this.EMPLOYEEID = Convert.ToString(reader[nameof (EMPLOYEEID)]);
      this.UNAME = Convert.ToString(reader[nameof (UNAME)]);
      this.user_designation = Convert.ToString(reader[nameof (user_designation)]);
      this.LOCATION = Convert.ToString(reader[nameof (LOCATION)]);
      this.USTATUS = Convert.ToString(reader[nameof (USTATUS)]);
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
    }
  }
}
