// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.CMSContentModel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class CMSContentModel
  {
    public tbl_content cur_content { get; set; }

    public tbl_content_answer cur_content_answer { get; set; }

    public tbl_content_metadata cur_content_metadata { get; set; }

    public List<tbl_content> t_content { get; set; }

    public List<tbl_content_answer> t_content_answer { get; set; }

    public List<tbl_content_metadata> t_content_metadata { get; set; }
  }
}
