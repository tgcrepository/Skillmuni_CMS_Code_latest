// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.RoleBasedAccess
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Collections.Generic;

namespace m2ostnext.Models
{
  public class RoleBasedAccess
  {
    public bool checkAccess(List<tbl_cms_role_action_mapping> mapping, int type)
    {
      foreach (tbl_cms_role_action_mapping roleActionMapping in mapping)
      {
        int num = type;
        int? idCmsRoleAction = roleActionMapping.id_cms_role_action;
        int valueOrDefault = idCmsRoleAction.GetValueOrDefault();
        if (num == valueOrDefault & idCmsRoleAction.HasValue)
          return true;
      }
      return false;
    }
  }
}
