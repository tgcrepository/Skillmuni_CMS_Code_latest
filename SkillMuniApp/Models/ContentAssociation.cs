// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.ContentAssociation
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class ContentAssociation
  {
    public tbl_content Content { get; set; }

    public List<tbl_category> Category { get; set; }
  }
}
