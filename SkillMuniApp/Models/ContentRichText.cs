// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.ContentRichText
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Web.Mvc;

namespace m2ostnext.Models
{
  public class ContentRichText
  {
    public string t_title { get; set; }

    public string t_header { get; set; }

    public string t_question { get; set; }

    public int select_level { get; set; }

    public int select_category { get; set; }

    public string category_list { get; set; }

    public DateTime content_expiry { get; set; }

    public string t_metadata { get; set; }

    [AllowHtml]
    public string content_answer { get; set; }

    public int conId { get; set; }
  }
}
