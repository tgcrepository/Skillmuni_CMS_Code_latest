// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.tbl_ce_career_evaluation_master
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class tbl_ce_career_evaluation_master
  {
    public int id_ce_career_evaluation_master { get; set; }

    public int id_organization { get; set; }

    public string career_evaluation_title { get; set; }

    public string career_evaluation_code { get; set; }

    public int id_ce_evaluation_tile { get; set; }

    public string ce_description { get; set; }

    public int validation_period { get; set; }

    public int ordering_sequence_number { get; set; }

    public int no_of_question { get; set; }

    public int is_time_enforced { get; set; }

    public int time_enforced { get; set; }

    public int ce_assessment_type { get; set; }

    public int job_points_for_ra { get; set; }

    public DateTime created_date { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public string ce_image { get; set; }
  }
}
