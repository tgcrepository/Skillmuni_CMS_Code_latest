// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.collegeListmodel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace m2ostnext.Models
{
  public class collegeListmodel
  {
    public class Excelmodel
    {
      public int slNo { get; set; }

      public string Name { get; set; }

      public string Email { get; set; }

      public string Mobile { get; set; }

      public string Gender { get; set; }

      public string City { get; set; }

      public string Graduation_year { get; set; }

      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      [Display(Name = "DOB")]
      public string DOB { get; set; }

      public string Degree { get; set; }

      public string Stream { get; set; }

      public string ReferalCode { get; set; }

      public string ReferalName { get; set; }

      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      [Display(Name = "RegisterDate")]
      public string RegisterDate { get; set; }

      public string SocialCode { get; set; }
    }

    public class tbl_referral_code_master1
    {
      public int id_referral_code { get; set; }

      public string referral_code { get; set; }

      public string referral_name { get; set; }

      public int id_organization { get; set; }

      public int? id_user { get; set; }

      public string status { get; set; }

      public DateTime updated_date_time { get; set; }
    }

    public class CityVal
    {
      public int id { get; set; }

      public string country { get; set; }
    }

    public class CityModel
    {
      public string status { get; set; }

      public int tp { get; set; }

      public string msg { get; set; }

      public List<collegeListmodel.CityVal> result { get; set; }
    }

        public class CityValNew
        {
            public int id { get; set; }

            public string name { get; set; }
        }

        public class CityModelNew
        {
            public string status { get; set; }

            public int tp { get; set; }

            public string msg { get; set; }

            public List<collegeListmodel.CityValNew> Cities { get; set; }
        }
    }
}
