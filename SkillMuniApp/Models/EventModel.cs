﻿// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.EventModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

namespace m2ostnext.Models
{
  public class EventModel
  {
    public string EVENT_NAME { get; set; }

    public int ID_EVENT { get; set; }

    public string EVENT_TYPE { get; set; }

    public string EVENT_GROUP_TYPE { get; set; }

    public string EVENT_CREATOR { get; set; }

    public string EVENT_DATE { get; set; }

    public int NO_OF_USER { get; set; }

    public int APPROVAL_COUNT { get; set; }

    public int PENDING_COUNT { get; set; }

    public int REJECT_COUNT { get; set; }

    public int TOTAL_COUNT { get; set; }

    public string STATUS { get; set; }
  }
}
