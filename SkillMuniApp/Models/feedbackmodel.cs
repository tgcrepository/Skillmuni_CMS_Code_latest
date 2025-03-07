// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.feedbackmodel
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

namespace m2ostnext.Models
{
  public class feedbackmodel
  {
    public int id_user { get; set; }

    public int uid { get; set; }

    public int OID { get; set; }

    public int id_feedback { get; set; }

    public string Name { get; set; }

    public string Contact { get; set; }

    public string Description { get; set; }

    public string IfIssue { get; set; }

    public string IfSuggestion { get; set; }

    public string ContentIssue { get; set; }

    public string UIIssue { get; set; }

    public string Attachment { get; set; }

    public string CreatedDate { get; set; }
  }
}
