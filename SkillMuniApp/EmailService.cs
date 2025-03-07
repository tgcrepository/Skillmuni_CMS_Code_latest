// Decompiled with JetBrains decompiler
// Type: m2ostnext.EmailService
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace m2ostnext
{
  public class EmailService : IIdentityMessageService
  {
    public Task SendAsync(IdentityMessage message) => (Task) Task.FromResult<int>(0);
  }
}
