// Decompiled with JetBrains decompiler
// Type: m2ostnext.tbl_brief_question_ce
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;

namespace m2ostnext
{
  public class tbl_brief_question_ce
  {
    public int id_brief_question { get; set; }

    public int? id_organization { get; set; }

    public string brief_question { get; set; }

    public int? id_brief_category { get; set; }

    public int? id_brief_sub_category { get; set; }

    public string question_image { get; set; }

    public int question_type { get; set; }

    public int? question_theme_type { get; set; }

    public int? question_choice_type { get; set; }

    public int? question_complexity { get; set; }

    public DateTime? expiry_date { get; set; }

    public string complexity_label { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public int ordering_sequence { get; set; }

    public List<tbl_ce_evalution_psychometric_setup> psychoSetup { get; set; }

    public List<tbl_brief_answer_ce> answer { get; set; }

    public List<tbl_ce_evalution_answer_key> answer_key { get; set; }
  }
}
