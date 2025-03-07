// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.State
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class State
  {
    public string status { get; set; }

    public int tp { get; set; }

    public string msg { get; set; }

    public List<Result> result { get; set; }
        public List<ResultNew> States { get; set; }
    }
}
