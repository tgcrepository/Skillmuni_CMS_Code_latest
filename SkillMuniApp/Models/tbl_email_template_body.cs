// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_email_template_body
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_email_template_body
  {
    public int id_email_temp_body { get; set; }

    public int id_email_template { get; set; }

    public string body_content { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int mail_template { get; set; }

    public int sequence { get; set; }
  }
}
