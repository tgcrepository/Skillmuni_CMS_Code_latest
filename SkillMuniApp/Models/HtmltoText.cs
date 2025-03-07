// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.HtmltoText
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Text;
using System.Text.RegularExpressions;

namespace m2ostnext.Models
{
  public class HtmltoText
  {
    public string HTMLText(string HTMLCode)
    {
      HTMLCode = HTMLCode.Replace("\n", " ");
      HTMLCode = HTMLCode.Replace("\t", " ");
      HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");
      HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      StringBuilder stringBuilder = new StringBuilder(HTMLCode);
      string[] strArray1 = new string[9]
      {
        "&nbsp;",
        "&amp;",
        "&quot;",
        "&lt;",
        "&gt;",
        "&reg;",
        "&copy;",
        "&bull;",
        "&trade;"
      };
      string[] strArray2 = new string[9]
      {
        " ",
        "&",
        "\"",
        "<",
        ">",
        "Â®",
        "Â©",
        "â€¢",
        "â„¢"
      };
      for (int index = 0; index < strArray1.Length; ++index)
        stringBuilder.Replace(strArray1[index], strArray2[index]);
      stringBuilder.Replace("<br>", "<br>");
      stringBuilder.Replace("<br ", "<br ");
      stringBuilder.Replace("<p ", "<p ");
      return Regex.Replace(stringBuilder.ToString(), "<[^>]*>", "");
    }
  }
}
