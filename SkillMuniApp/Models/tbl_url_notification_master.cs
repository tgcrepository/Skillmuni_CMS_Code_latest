// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_url_notification_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_url_notification_master
  {
    public int id_notifcation { get; set; }

    public string notifcation_title { get; set; }

    public string notification_message { get; set; }

    public string notification_url { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }
  }
}
