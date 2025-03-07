// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.ReportFeedbackController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class ReportFeedbackController : Controller
  {
    public ActionResult Reportfeed()
    {
      List<feedbackmodel> feedbackmodelList = new List<feedbackmodel>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
        feedbackmodelList = m2ostDbContext.Database.SqlQuery<feedbackmodel>("SELECT concat(t3.FIRSTNAME,' ',t3.LASTNAME) as 'Name', case when t1.issues=1 then 'Yes' else 'No Issue' end as 'IfIssue', case when t1.suggestions=1 then 'Yes' else 'No Suggestions' end as 'IfSuggestion', case when t1.content=1 then 'Yes' else 'No Issue in Content' end as 'ContentIssue', case when t1.UI=1 then 'Yes' else 'No UI Issue' end as 'UIIssue', case when t1.MediaFlag=1 then t2.media else null end as 'Attachment', t1.Description, t1.Contact, date_format(t1.updated_date_time,'%d-%m-%Y') as 'CreatedDate' FROM tbl_feedback_master t1 left JOIN tbl_feedback_media t2 ON t2.id_feedback = t1.id_feedback INNER JOIN tbl_profile t3 ON t3.id_user = t1.uid where t1.OID = {0}", (object) int32).ToList<feedbackmodel>();
      }
      this.ViewData["imgPath"] = (object) ConfigurationManager.AppSettings["feedback_path"].ToString();
      this.ViewData["feedback"] = (object) feedbackmodelList;
      return (ActionResult) this.View();
    }
  }
}
