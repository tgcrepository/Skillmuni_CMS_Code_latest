// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.ConfigureTwoFactorViewModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;
using System.Web.Mvc;

namespace m2ostnext.Models
{
  public class ConfigureTwoFactorViewModel
  {
    public string SelectedProvider { get; set; }

    public ICollection<SelectListItem> Providers { get; set; }
  }
}
