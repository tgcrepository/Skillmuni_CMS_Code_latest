// Decompiled with JetBrains decompiler
// Type: m2ostnext.tbl_brief_answer_ce
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext
{
  public class tbl_brief_answer_ce
  {
    public int id_brief_answer { get; set; }

    public int? id_organization { get; set; }

    public int? id_brief_question { get; set; }

    public string brief_answer { get; set; }

    public string choice_image { get; set; }

    public int? choice_type { get; set; }

    public int? is_correct_answer { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public string answer_key { get; set; }
  }
}
