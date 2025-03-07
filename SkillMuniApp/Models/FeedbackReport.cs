// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.FeedbackReport
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;

namespace m2ostnext.Models
{
  public class FeedbackReport
  {
    public int Feedback_ID { get; set; }

    public string User_Name { get; set; }

    public string Like_Dislike { get; set; }

    public string Feedback { get; set; }

    public string Brief_Title { get; set; }

    public string Issue_Suggestions { get; set; }

    public string Content_UI { get; set; }

    public DateTime Time_Stamp { get; set; }

    public string Image { get; set; }

    public int Brief_ID { get; set; }
  }
}
