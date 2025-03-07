// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.EventBody
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

namespace m2ostnext.Models
{
  public class EventBody
  {
    public tbl_user USER { get; set; }

    public tbl_profile PROFILE { get; set; }

    public tbl_scheduled_event_subscription_log LOG { get; set; }

    public tbl_cms_users MANAGER { get; set; }

    public tbl_cms_users ASSIGNER { get; set; }
  }
}
