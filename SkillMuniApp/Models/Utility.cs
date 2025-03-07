// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.Utility
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Globalization;
using System.Linq;

namespace m2ostnext.Models
{
  public class Utility
  {
    private Random random = new Random();

    public DateTime StringToDatetime(string dateString)
    {
      DateTime result = new DateTime();
      if (string.IsNullOrEmpty(dateString))
      {
        result = new DateTime(DateTime.Now.Year, 1, 1);
      }
      else
      {
        dateString = dateString.Trim();
        if (!DateTime.TryParseExact(dateString, "dd-MM-yyyy HH:mm", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd-MM-yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd-MM-yy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd-MM-yyyy HH:mm", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "MM/dd/yyyy hh:mm:tt", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd-MM-yyyy hh:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "yyyy-MM-dd hh:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd/MM/yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "yyyy-dd-MM hh:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "MM/dd/yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd-MMM-yy hh:mm:ss tt", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "MM-dd-yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "dd-MM-yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "yyyy-MM-dd hh:mm", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
          result = DateTime.ParseExact("2001-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      return result;
    }

    public string RandomString(int length) => new string(Enumerable.Repeat<string>("ABCDEFGHIJK01234LMNOPQRSTUVWXYZ56789", length).Select<string, char>((Func<string, char>) (s => s[this.random.Next(s.Length)])).ToArray<char>());
  }
}
