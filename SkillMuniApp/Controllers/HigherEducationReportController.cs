// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.HigherEducationReportController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class HigherEducationReportController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult ReportHigherEducation()
    {
      List<Higher> higherList = new List<Higher>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        higherList = m2ostDbContext.Database.SqlQuery<Higher>("SELECT a.event_title,a.id_event,f.FIRSTNAME,c.higher_education_start_time,c.higher_education_end_time,e.id_user,e.id_register,e.update_date_time,e.slot FROM tbl_sul_fest_master a, tbl_sul_fest_event_mapping b, tbl_sul_higher_education_master c, tbl_sul_higher_education_timeslot d, tbl_sul_higher_education_user_registration e, tbl_profile f WHERE a.id_event = b.id_event AND b.type = 2 AND b.id_higher_education = c.id_higher_education AND c.id_higher_education = d.id_higher_education AND c.id_higher_education = e.id_higher_education AND e.id_user = f.ID_USER").ToList<Higher>();
      this.ViewData["high"] = (object) higherList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReport()
    {
      List<Higher> higherList = new List<Higher>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        higherList = m2ostDbContext.Database.SqlQuery<Higher>("SELECT a.event_title,a.id_event,f.FIRSTNAME,c.higher_education_start_time,c.higher_education_end_time,e.id_user,e.id_register,e.update_date_time,e.slot FROM tbl_sul_fest_master a, tbl_sul_fest_event_mapping b, tbl_sul_higher_education_master c, tbl_sul_higher_education_timeslot d, tbl_sul_higher_education_user_registration e, tbl_profile f WHERE a.id_event = b.id_event AND b.type = 2 AND b.id_higher_education = c.id_higher_education AND c.id_higher_education = d.id_higher_education AND c.id_higher_education = e.id_higher_education AND e.id_user = f.ID_USER").ToList<Higher>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "Id_User";
      excelWorksheet.Cells["B1"].Value = (object) "User Name";
      excelWorksheet.Cells["C1"].Value = (object) "Registration ID";
      excelWorksheet.Cells["D1"].Value = (object) "Event ID";
      excelWorksheet.Cells["E1"].Value = (object) "Event Name";
      excelWorksheet.Cells["F1"].Value = (object) "H.E. Start Time";
      excelWorksheet.Cells["G1"].Value = (object) "H.E. End Time";
      excelWorksheet.Cells["H1"].Value = (object) "Time Slot";
      excelWorksheet.Cells["I1"].Value = (object) "Updated Time";
      int num = 2;
      foreach (Higher higher in higherList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) higher.id_user;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) higher.FIRSTNAME;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) higher.id_register;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) higher.id_event;
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) higher.event_title;
        ExcelRange cell1 = excelWorksheet.Cells[string.Format("F{0}", (object) num)];
        DateTime dateTime = higher.higher_education_start_time;
        string str1 = dateTime.ToString("yyyy-MM-dd-HH:mm:ss");
        cell1.Value = (object) str1;
        ExcelRange cell2 = excelWorksheet.Cells[string.Format("G{0}", (object) num)];
        dateTime = higher.higher_education_end_time;
        string str2 = dateTime.ToString("yyyy-MM-dd-HH:mm:ss");
        cell2.Value = (object) str2;
        excelWorksheet.Cells[string.Format("H{0}", (object) num)].Value = (object) higher.slot;
        ExcelRange cell3 = excelWorksheet.Cells[string.Format("I{0}", (object) num)];
        dateTime = higher.update_date_time;
        string str3 = dateTime.ToString("yyyy-MM-dd-HH:mm:ss");
        cell3.Value = (object) str3;
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
