// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.SeminarReportController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class SeminarReportController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult ReportSeminar()
    {
      List<seminar> seminarList = new List<seminar>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        seminarList = m2ostDbContext.Database.SqlQuery<seminar>("SELECT IFNULL((e.ratings), 0) AS ratings, c.id_seminar, c.title, c.stream,g.update_date_time, g.speaker_name AS slot_details, c.location, f.FIRSTNAME, e.id_user, CASE WHEN e.feedback IS NULL THEN 'NA' ELSE e.feedback END AS 'feedback', a.updated_date_time AS seminar_date FROM tbl_sul_fest_master a, tbl_sul_fest_event_mapping b, tbl_sul_seminar_master c, tbl_sul_seminar_timeslot d, tbl_sul_seminar_user_registration e, tbl_profile f, tbl_sul_slot_seminar g WHERE a.id_event = b.id_event AND b.type = 1 AND b.id_seminar = c.id_seminar AND c.id_seminar = d.id_seminar AND c.id_seminar = e.id_seminar AND g.id_seminar = c.id_seminar AND e.id_user = f.ID_USER").ToList<seminar>();
      this.ViewData["semi"] = (object) seminarList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReport()
    {
      List<seminar> seminarList = new List<seminar>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        seminarList = m2ostDbContext.Database.SqlQuery<seminar>("SELECT IFNULL((e.ratings), 0) AS ratings, c.id_seminar, c.title, c.stream,g.update_date_time, g.speaker_name AS slot_details, c.location, f.FIRSTNAME, e.id_user, CASE WHEN e.feedback IS NULL THEN 'NA' ELSE e.feedback END AS 'feedback', a.updated_date_time AS seminar_date FROM tbl_sul_fest_master a, tbl_sul_fest_event_mapping b, tbl_sul_seminar_master c, tbl_sul_seminar_timeslot d, tbl_sul_seminar_user_registration e, tbl_profile f, tbl_sul_slot_seminar g WHERE a.id_event = b.id_event AND b.type = 1 AND b.id_seminar = c.id_seminar AND c.id_seminar = d.id_seminar AND c.id_seminar = e.id_seminar AND g.id_seminar = c.id_seminar AND e.id_user = f.ID_USER").ToList<seminar>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "Seminar ID";
      excelWorksheet.Cells["B1"].Value = (object) "Seminar Title";
      excelWorksheet.Cells["C1"].Value = (object) "Stream";
      excelWorksheet.Cells["D1"].Value = (object) "Booked Slot Date and Time";
      excelWorksheet.Cells["E1"].Value = (object) "Slot Text";
      excelWorksheet.Cells["F1"].Value = (object) "Location";
      excelWorksheet.Cells["G1"].Value = (object) "User Name";
      excelWorksheet.Cells["H1"].Value = (object) "Id_user";
      excelWorksheet.Cells["I1"].Value = (object) "Ratings";
      excelWorksheet.Cells["J1"].Value = (object) "Feedback";
      excelWorksheet.Cells["K1"].Value = (object) "Booked Date and Time";
      int num = 2;
      foreach (seminar seminar in seminarList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) seminar.id_seminar;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) seminar.title;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) seminar.stream;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) seminar.update_date_time.ToString("yyyy-MM-dd-HH:mm:ss");
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) seminar.slot;
        excelWorksheet.Cells[string.Format("F{0}", (object) num)].Value = (object) seminar.location;
        excelWorksheet.Cells[string.Format("G{0}", (object) num)].Value = (object) seminar.FIRSTNAME;
        excelWorksheet.Cells[string.Format("H{0}", (object) num)].Value = (object) seminar.id_user;
        excelWorksheet.Cells[string.Format("I{0}", (object) num)].Value = (object) seminar.ratings;
        excelWorksheet.Cells[string.Format("J{0}", (object) num)].Value = (object) seminar.feedback;
        excelWorksheet.Cells[string.Format("K{0}", (object) num)].Value = (object) seminar.seminar_date.ToString("yyyy-MM-dd-HH:mm:ss");
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
