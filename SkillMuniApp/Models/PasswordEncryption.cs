// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.PasswordEncryption
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace m2ostnext.Models
{
  public static class PasswordEncryption
  {
    public static string ToMD5Hash(this byte[] bytes)
    {
      StringBuilder hash = new StringBuilder();
      ((IEnumerable<byte>) MD5.Create().ComputeHash(bytes)).ToList<byte>().ForEach((Action<byte>) (b => hash.AppendFormat("{0:x2}", (object) b)));
      return hash.ToString();
    }

    public static string ToMD5Hash(this string inputString) => Encoding.UTF8.GetBytes(inputString).ToMD5Hash();
  }
}
