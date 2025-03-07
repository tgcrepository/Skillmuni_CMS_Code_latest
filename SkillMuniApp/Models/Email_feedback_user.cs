// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.Email_feedback_user
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class Email_feedback_user
  {
    public int id_feedback_user { get; set; }

    public int id_role { get; set; }

    public string rolename { get; set; }

    public string emp_emailID { get; set; }

    public string emp_name { get; set; }

    public int emp_id { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
