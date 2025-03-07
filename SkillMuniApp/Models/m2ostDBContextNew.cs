// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.m2ostDBContextNew
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Data.Entity;

namespace m2ostnext.Models
{
  public class m2ostDBContextNew : DbContext
  {
    static m2ostDBContextNew() => Database.SetInitializer<m2ostDBContextNew>((IDatabaseInitializer<m2ostDBContextNew>) null);

    public m2ostDBContextNew()
      : base("name=m2ostcat")
    {
    }
  }
}
