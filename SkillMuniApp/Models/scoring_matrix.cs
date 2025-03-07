// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.scoring_matrix
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnext.Models
{
  public class scoring_matrix
  {
    public int id_element { get; set; }

    public string element_name { get; set; }

    public double vk_score { get; set; }

    public double vk_weightage { get; set; }

    public scoring_matrix(MySqlDataReader reader)
    {
      this.id_element = Convert.ToInt32(reader[nameof (id_element)]);
      this.element_name = Convert.ToString(reader[nameof (element_name)]);
      this.vk_score = Convert.ToDouble(reader[nameof (vk_score)]);
      this.vk_weightage = Convert.ToDouble(reader[nameof (vk_weightage)]);
    }
  }
}
