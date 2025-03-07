// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.FeedbackdashboardController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class FeedbackdashboardController : Controller
  {
    public ActionResult Feedbacklistview(string image)
    {
      List<FeedbackReport> feedbackReportList = new List<FeedbackReport>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        feedbackReportList = m2ostDbContext.Database.SqlQuery<FeedbackReport>("SELECT t1.id_feedback AS Feedback_ID, t2.FIRSTNAME AS User_Name, concat(CASE WHEN t1.liked = 1 THEN 'liked' WHEN t1.disliked = 1 THEN 'disliked'  else 'NA' END ) AS Like_Dislike, t1.id_brief_master AS Brief_ID, t3.brief_title AS Brief_Title, concat(CASE WHEN t4.issues=1 THEN  'Issue' WHEN t4.suggestions=1 THEN 'Suggestions' else '' END ) AS Issue_Suggestions, concat(case when t4.content=1 THEN 'Content' WHEN t4.UI=1 then 'Ui' else '' end ) as Content_UI, CASE WHEN isnull(t1.reason)then '' else t1.reason END AS 'Feedback', case when t4.MediaFlag=1 then t5.media else null end as 'Image', t1.updated_date_time AS Time_Stamp FROM tbl_brief_user_feedback_master AS t1 INNER JOIN tbl_profile AS t2 ON t1.UID = t2.ID_USER LEFT JOIN tbl_brief_master AS t3 ON t1.id_brief_master = t3.id_brief_master LEFT JOIN  tbl_feedback_master AS t4 ON t1.id_feedback = t4.id_feedback LEFT JOIN tbl_brief_feedback_media AS t5 ON  t4.id_feedback = t5.id_feedback").ToList<FeedbackReport>();
      this.ViewData["imgPath"] = (object) ConfigurationManager.AppSettings["feedback_path"].ToString();
      this.ViewData["feedback"] = (object) feedbackReportList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReport()
    {
      List<FeedbackReport> feedbackReportList = new List<FeedbackReport>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        feedbackReportList = m2ostDbContext.Database.SqlQuery<FeedbackReport>("SELECT t1.id_feedback AS Feedback_ID, t2.FIRSTNAME AS User_Name, concat(CASE WHEN t1.liked = 1 THEN 'liked' WHEN t1.disliked = 1 THEN 'disliked'  else 'NA' END ) AS Like_Dislike, t1.id_brief_master AS Brief_ID, t3.brief_title AS Brief_Title, concat(CASE WHEN t4.issues=1 THEN  'Issue' WHEN t4.suggestions=1 THEN 'Suggestions' else '' END ) AS Issue_Suggestions, concat(case when t4.content=1 THEN 'Content' WHEN t4.UI=1 then 'Ui' else '' end ) as Content_UI, CASE WHEN isnull(t1.reason)then '' else t1.reason END AS 'Feedback', case when t4.MediaFlag=1 then t5.media else null end as 'Image', t1.updated_date_time AS Time_Stamp FROM tbl_brief_user_feedback_master AS t1 INNER JOIN tbl_profile AS t2 ON t1.UID = t2.ID_USER LEFT JOIN tbl_brief_master AS t3 ON t1.id_brief_master = t3.id_brief_master LEFT JOIN  tbl_feedback_master AS t4 ON t1.id_feedback = t4.id_feedback LEFT JOIN tbl_brief_feedback_media AS t5 ON  t4.id_feedback = t5.id_feedback").ToList<FeedbackReport>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "Feedback ID";
      excelWorksheet.Cells["B1"].Value = (object) "User Name";
      excelWorksheet.Cells["C1"].Value = (object) "Like/Dislike";
      excelWorksheet.Cells["D1"].Value = (object) "Brief ID";
      excelWorksheet.Cells["E1"].Value = (object) "Brief Title";
      excelWorksheet.Cells["F1"].Value = (object) "Issue/Suggestion";
      excelWorksheet.Cells["G1"].Value = (object) "Content/UI";
      excelWorksheet.Cells["H1"].Value = (object) "Feedback";
      excelWorksheet.Cells["I1"].Value = (object) "Image";
      excelWorksheet.Cells["J1"].Value = (object) "Time Stamp";
      int num = 2;
      foreach (FeedbackReport feedbackReport in feedbackReportList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) feedbackReport.Feedback_ID;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) feedbackReport.User_Name;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) feedbackReport.Like_Dislike;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) feedbackReport.Brief_ID;
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) feedbackReport.Brief_Title;
        excelWorksheet.Cells[string.Format("F{0}", (object) num)].Value = (object) feedbackReport.Issue_Suggestions;
        excelWorksheet.Cells[string.Format("G{0}", (object) num)].Value = (object) feedbackReport.Content_UI;
        excelWorksheet.Cells[string.Format("H{0}", (object) num)].Value = (object) feedbackReport.Feedback;
        excelWorksheet.Cells[string.Format("I{0}", (object) num)].Value = (object) feedbackReport.Image;
        excelWorksheet.Cells[string.Format("J{0}", (object) num)].Value = (object) feedbackReport.Time_Stamp.ToString("yyyy-MM-dd-HH:mm:ss");
        ++num;
      }
      excelWorksheet.Cells["A:AZ"].AutoFitColumns();
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      this.Response.AddHeader("content-disposition", "attachment: filename=ExcelReport.xlsx");
      this.Response.BinaryWrite(excelPackage.GetAsByteArray());
      this.Response.End();
    }
  }
}
