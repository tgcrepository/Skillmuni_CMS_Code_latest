// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.EventReportController
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
  public class EventReportController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult ReportEvent()
    {
      List<Event> eventList = new List<Event>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        eventList = m2ostDbContext.Database.SqlQuery<Event>("SELECT t2.event_title, t2.event_objective, t2.event_logo, CASE WHEN t2.is_registration_needed = 1 THEN 'Yes' ELSE 'No' END AS 'is_registration_needed', t2.event_start_date, t2.event_end_date, t2.event_duration, t2.location_text, t2.address, CASE WHEN t2.is_event_closed = 1 THEN 'Yes' ELSE 'No' END AS 'is_event_closed', t2.user_count, t2.contact_name, t2.contact_number, t1.UID, t3.FIRSTNAME, t3.MOBILE, t3.EMAIL, t1.updated_date_time FROM tbl_sul_fest_event_registration AS t1 INNER JOIN tbl_sul_fest_master AS t2 ON t2.id_event = t1.id_event INNER JOIN tbl_profile AS t3 ON t3.id_user = t1.UID INNER JOIN tbl_college_list AS t4 ON t4.id_college = t1.id_college").ToList<Event>();
      this.ViewData["events"] = (object) eventList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReport()
    {
      List<Event> eventList = new List<Event>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        eventList = m2ostDbContext.Database.SqlQuery<Event>("SELECT t2.event_title, t2.event_objective, t2.event_logo, CASE WHEN t2.is_registration_needed = 1 THEN 'Yes' ELSE 'No' END AS 'is_registration_needed', t2.event_start_date, t2.event_end_date, t2.event_duration, t2.location_text, t2.address, CASE WHEN t2.is_event_closed = 1 THEN 'Yes' ELSE 'No' END AS 'is_event_closed', t2.user_count, t2.contact_name, t2.contact_number, t1.UID, t3.FIRSTNAME, t3.MOBILE, t3.EMAIL, t1.updated_date_time FROM tbl_sul_fest_event_registration AS t1 INNER JOIN tbl_sul_fest_master AS t2 ON t2.id_event = t1.id_event INNER JOIN tbl_profile AS t3 ON t3.id_user = t1.UID INNER JOIN tbl_college_list AS t4 ON t4.id_college = t1.id_college").ToList<Event>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "Event Title";
      excelWorksheet.Cells["B1"].Value = (object) "Event Objective";
      excelWorksheet.Cells["C1"].Value = (object) "Event Logo";
      excelWorksheet.Cells["D1"].Value = (object) "Event Registration Needed";
      excelWorksheet.Cells["E1"].Value = (object) "Event Start Date";
      excelWorksheet.Cells["F1"].Value = (object) "Event End Date";
      excelWorksheet.Cells["G1"].Value = (object) "Event Duration";
      excelWorksheet.Cells["H1"].Value = (object) "Event Location";
      excelWorksheet.Cells["I1"].Value = (object) "Detailed Address";
      excelWorksheet.Cells["J1"].Value = (object) "Closed Event";
      excelWorksheet.Cells["K1"].Value = (object) "No of participants";
      excelWorksheet.Cells["L1"].Value = (object) "Contact Name";
      excelWorksheet.Cells["M1"].Value = (object) "Contact Number";
      excelWorksheet.Cells["N1"].Value = (object) "id_user";
      excelWorksheet.Cells["O1"].Value = (object) "User Name";
      excelWorksheet.Cells["P1"].Value = (object) "User Contact Number";
      excelWorksheet.Cells["Q1"].Value = (object) "User Email ID";
      excelWorksheet.Cells["R1"].Value = (object) "User Registration Date Time Stamp";
      int num = 2;
      foreach (Event @event in eventList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) @event.event_title;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) @event.event_objective;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) @event.event_logo;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) @event.is_registration_needed;
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) @event.event_start_date.ToString("yyyy-MM-dd-HH:mm:ss");
        excelWorksheet.Cells[string.Format("F{0}", (object) num)].Value = (object) @event.event_end_date.ToString("yyyy-MM-dd-HH:mm:ss");
        excelWorksheet.Cells[string.Format("G{0}", (object) num)].Value = (object) @event.event_duration;
        excelWorksheet.Cells[string.Format("H{0}", (object) num)].Value = (object) @event.location_text;
        excelWorksheet.Cells[string.Format("I{0}", (object) num)].Value = (object) @event.address;
        excelWorksheet.Cells[string.Format("J{0}", (object) num)].Value = (object) @event.is_event_closed;
        excelWorksheet.Cells[string.Format("K{0}", (object) num)].Value = (object) @event.user_count;
        excelWorksheet.Cells[string.Format("L{0}", (object) num)].Value = (object) @event.contact_name;
        excelWorksheet.Cells[string.Format("M{0}", (object) num)].Value = (object) @event.contact_number;
        excelWorksheet.Cells[string.Format("N{0}", (object) num)].Value = (object) @event.UID;
        excelWorksheet.Cells[string.Format("O{0}", (object) num)].Value = (object) @event.FIRSTNAME;
        excelWorksheet.Cells[string.Format("P{0}", (object) num)].Value = (object) @event.MOBILE;
        excelWorksheet.Cells[string.Format("Q{0}", (object) num)].Value = (object) @event.EMAIL;
        excelWorksheet.Cells[string.Format("R{0}", (object) num)].Value = (object) @event.updated_date_time.ToString("yyyy-MM-dd-HH:mm:ss");
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
