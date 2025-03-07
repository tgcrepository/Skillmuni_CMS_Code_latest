// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.EventBatch
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class EventBatch
  {
    public int id_event_batch { get; set; }

    public int id_event { get; set; }

    public int id_org { get; set; }

    public string status { get; set; }

    public DateTime update_date { get; set; }

    public string batch_time { get; set; }

    public int participants { get; set; }
  }
}
