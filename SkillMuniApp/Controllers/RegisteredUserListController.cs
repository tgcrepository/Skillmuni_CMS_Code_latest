// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.RegisteredUserListController
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
  public class RegisteredUserListController : Controller
  {
    public ActionResult Registerdlistview()
    {
      List<Registerreport> registerreportList = new List<Registerreport>();
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        registerreportList = m2ostDbContext.Database.SqlQuery<Registerreport>("SELECT t1.id_user, CONCAT(t1.FIRSTNAME, ' ', t1.LASTNAME) AS 'Name', t1.EMAIL, CASE WHEN LENGTH(t1.MOBILE) <= 2 THEN '-' ELSE t1.MOBILE END AS 'MOBILE', CASE WHEN LENGTH(t1.GENDER) <= 2 THEN '-' ELSE t1.GENDER END AS 'GENDER', t1.CITY, CASE WHEN LENGTH(t1.COLLEGE) <= 2 THEN '-' ELSE t1.COLLEGE END AS 'COLLEGE', CASE WHEN ISNULL(t1.GRADUATIONYEAR) OR LENGTH(t1.GRADUATIONYEAR) <= 3 OR LENGTH(t1.GRADUATIONYEAR) >= 5 THEN '-' ELSE t1.GRADUATIONYEAR END AS 'GRADUATIONYEAR', CASE WHEN DATE_FORMAT(t1.DATE_OF_BIRTH, '%d-%m-%Y') LIKE '01-01-0001' THEN '-' ELSE DATE_FORMAT(t1.DATE_OF_BIRTH, '%d-%m-%Y') END AS 'DATE_OF_BIRTH', CASE WHEN ISNULL(t3.degree) OR LENGTH(t3.degree) < 2 THEN '-' ELSE t3.degree END AS 'degree', CASE WHEN ISNULL(t4.stream) OR LENGTH(t4.stream) < 2 THEN '-' ELSE t4.stream END AS 'stream', CASE WHEN ISNULL(t1.OTHERSTREAM) OR LENGTH(t1.OTHERSTREAM) < 2 THEN '-' ELSE t1.OTHERSTREAM END AS 'OTHERSTREAM', CASE WHEN ISNULL(t5.referral_code) OR LENGTH(t5.referral_code) < 2 THEN '-' ELSE t5.referral_code END AS 'referral_code', CASE WHEN ISNULL(t6.referral_name) THEN '-' ELSE t6.referral_name END AS 'referral_name', CASE WHEN t1.STUDENt = 1 THEN 'Studying' WHEN t1.STUDENT = 2 THEN 'Graduated' ELSE 'Working' END AS 'Type', CASE WHEN ISNULL(t8.foundation) OR LENGTH(t8.foundation) < 2 THEN '-' ELSE t8.foundation END AS 'foundation', DATE_FORMAT(t2.UPDATEDTIME, '%d-%m-%Y') AS 'EXPIRY_DATE', t7.social_code, DATE_FORMAT(t2.UPDATEDTIME, '%M-%Y') AS 'EXPIRY_MONTH' FROM tbl_profile AS t1 INNER JOIN tbl_user AS t2 ON t2.id_user = t1.id_user LEFT JOIN tbl_degree_master AS t3 ON t3.id_degree = t1.id_degree LEFT JOIN tbl_stream_master AS t4 ON t4.id_stream = t1.id_stream LEFT JOIN tbl_referral_code_user_mapping AS t5 ON t5.id_user = t1.id_user AND t5.status = 'A' LEFT JOIN tbl_referral_code_master AS t6 ON t6.referral_code = t5.referral_code INNER JOIN tbl_social_platform_active_directory AS t7 ON t7.id_user = t1.id_user LEFT JOIN tbl_foundation_master AS t8 ON t8.id_foundation = t1.id_foundation WHERE t2.id_organization = {0} ORDER BY t2.EXPIRY_DATE ASC", (object) int32).ToList<Registerreport>();
      this.ViewData["feedback"] = (object) registerreportList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReportNew()
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<Registerreport> registerreportList = new List<Registerreport>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        registerreportList = m2ostDbContext.Database.SqlQuery<Registerreport>("SELECT t1.id_user, CONCAT(t1.FIRSTNAME, ' ', t1.LASTNAME) AS 'Name', t1.EMAIL, CASE WHEN LENGTH(t1.MOBILE) <= 2 THEN '-' ELSE t1.MOBILE END AS 'MOBILE', CASE WHEN LENGTH(t1.GENDER) <= 2 THEN '-' ELSE t1.GENDER END AS 'GENDER', t1.CITY, CASE WHEN LENGTH(t1.COLLEGE) <= 2 THEN '-' ELSE t1.COLLEGE END AS 'COLLEGE', CASE WHEN ISNULL(t1.GRADUATIONYEAR) OR LENGTH(t1.GRADUATIONYEAR) <= 3 OR LENGTH(t1.GRADUATIONYEAR) >= 5 THEN '-' ELSE t1.GRADUATIONYEAR END AS 'GRADUATIONYEAR', CASE WHEN DATE_FORMAT(t1.DATE_OF_BIRTH, '%d-%m-%Y') LIKE '01-01-0001' THEN '-' ELSE DATE_FORMAT(t1.DATE_OF_BIRTH, '%d-%m-%Y') END AS 'DATE_OF_BIRTH', CASE WHEN ISNULL(t3.degree) OR LENGTH(t3.degree) < 2 THEN '-' ELSE t3.degree END AS 'degree', CASE WHEN ISNULL(t4.stream) OR LENGTH(t4.stream) < 2 THEN '-' ELSE t4.stream END AS 'stream', CASE WHEN ISNULL(t1.OTHERSTREAM) OR LENGTH(t1.OTHERSTREAM) < 2 THEN '-' ELSE t1.OTHERSTREAM END AS 'OTHERSTREAM', CASE WHEN ISNULL(t5.referral_code) OR LENGTH(t5.referral_code) < 2 THEN '-' ELSE t5.referral_code END AS 'referral_code', CASE WHEN ISNULL(t6.referral_name) THEN '-' ELSE t6.referral_name END AS 'referral_name', CASE WHEN t1.STUDENt = 1 THEN 'Studying' WHEN t1.STUDENT = 2 THEN 'Graduated' ELSE 'Working' END AS 'Type', CASE WHEN ISNULL(t8.foundation) OR LENGTH(t8.foundation) < 2 THEN '-' ELSE t8.foundation END AS 'foundation', DATE_FORMAT(t2.UPDATEDTIME, '%d-%m-%Y') AS 'EXPIRY_DATE', t7.social_code, DATE_FORMAT(t2.UPDATEDTIME, '%M-%Y') AS 'EXPIRY_MONTH' FROM tbl_profile AS t1 INNER JOIN tbl_user AS t2 ON t2.id_user = t1.id_user LEFT JOIN tbl_degree_master AS t3 ON t3.id_degree = t1.id_degree LEFT JOIN tbl_stream_master AS t4 ON t4.id_stream = t1.id_stream LEFT JOIN tbl_referral_code_user_mapping AS t5 ON t5.id_user = t1.id_user AND t5.status = 'A' LEFT JOIN tbl_referral_code_master AS t6 ON t6.referral_code = t5.referral_code INNER JOIN tbl_social_platform_active_directory AS t7 ON t7.id_user = t1.id_user LEFT JOIN tbl_foundation_master AS t8 ON t8.id_foundation = t1.id_foundation WHERE t2.id_organization = {0} ORDER BY t2.EXPIRY_DATE ASC", (object) int32).ToList<Registerreport>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "slNo";
      excelWorksheet.Cells["B1"].Value = (object) "Name";
      excelWorksheet.Cells["C1"].Value = (object) "Email";
      excelWorksheet.Cells["D1"].Value = (object) "Mobile";
      excelWorksheet.Cells["E1"].Value = (object) "Gender";
      excelWorksheet.Cells["F1"].Value = (object) "City";
      excelWorksheet.Cells["G1"].Value = (object) "College";
      excelWorksheet.Cells["H1"].Value = (object) "Graduation";
      excelWorksheet.Cells["I1"].Value = (object) "DOB";
      excelWorksheet.Cells["J1"].Value = (object) "Degree";
      excelWorksheet.Cells["K1"].Value = (object) "Stream";
      excelWorksheet.Cells["L1"].Value = (object) "OtherStream";
      excelWorksheet.Cells["M1"].Value = (object) "ReferalCode";
      excelWorksheet.Cells["N1"].Value = (object) "ReferalName";
      excelWorksheet.Cells["O1"].Value = (object) "Type";
      excelWorksheet.Cells["P1"].Value = (object) "Foundation";
      excelWorksheet.Cells["Q1"].Value = (object) "RegisterDate";
      excelWorksheet.Cells["R1"].Value = (object) "SocialCode";
      excelWorksheet.Cells["S1"].Value = (object) "ExpireMonth";
      int num = 2;
      foreach (Registerreport registerreport in registerreportList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) registerreport.id_user;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) registerreport.Name;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) registerreport.EMAIL;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) registerreport.MOBILE;
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) registerreport.GENDER;
        excelWorksheet.Cells[string.Format("F{0}", (object) num)].Value = (object) registerreport.CITY;
        excelWorksheet.Cells[string.Format("G{0}", (object) num)].Value = (object) registerreport.COLLEGE;
        excelWorksheet.Cells[string.Format("H{0}", (object) num)].Value = (object) registerreport.GRADUATIONYEAR;
        excelWorksheet.Cells[string.Format("I{0}", (object) num)].Value = (object) registerreport.DATE_OF_BIRTH;
        excelWorksheet.Cells[string.Format("J{0}", (object) num)].Value = (object) registerreport.degree;
        excelWorksheet.Cells[string.Format("K{0}", (object) num)].Value = (object) registerreport.stream;
        excelWorksheet.Cells[string.Format("L{0}", (object) num)].Value = (object) registerreport.OTHERSTREAM;
        excelWorksheet.Cells[string.Format("M{0}", (object) num)].Value = (object) registerreport.referral_code;
        excelWorksheet.Cells[string.Format("N{0}", (object) num)].Value = (object) registerreport.referral_name;
        excelWorksheet.Cells[string.Format("O{0}", (object) num)].Value = (object) registerreport.Type;
        excelWorksheet.Cells[string.Format("P{0}", (object) num)].Value = (object) registerreport.foundation;
        excelWorksheet.Cells[string.Format("Q{0}", (object) num)].Value = (object) registerreport.EXPIRY_DATE.ToString();
        excelWorksheet.Cells[string.Format("R{0}", (object) num)].Value = (object) registerreport.social_code;
        excelWorksheet.Cells[string.Format("S{0}", (object) num)].Value = (object) registerreport.EXPIRY_MONTH.ToString();
        ++num;
      }
      excelWorksheet.Cells["A:AZ"].AutoFitColumns();
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      this.Response.AddHeader("content-disposition", "attachment: filename=SulRegisteredUsers.xlsx");
      this.Response.BinaryWrite(excelPackage.GetAsByteArray());
      this.Response.End();
    }

    public ActionResult Studentlistview()
    {
      List<studentreport> studentreportList = new List<studentreport>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        studentreportList = m2ostDbContext.Database.SqlQuery<studentreport>("SELECT * from tbl_brief_enquiry").ToList<studentreport>();
      this.ViewData["feedback"] = (object) studentreportList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReport()
    {
      List<studentreport> studentreportList = new List<studentreport>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        studentreportList = m2ostDbContext.Database.SqlQuery<studentreport>("SELECT * from tbl_brief_enquiry").ToList<studentreport>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "Id_Enquiry";
      excelWorksheet.Cells["B1"].Value = (object) "Name";
      excelWorksheet.Cells["C1"].Value = (object) "Email";
      excelWorksheet.Cells["D1"].Value = (object) "Mobile";
      excelWorksheet.Cells["E1"].Value = (object) "Brief_Title";
      excelWorksheet.Cells["F1"].Value = (object) "Enquiry";
      excelWorksheet.Cells["G1"].Value = (object) "Enquiry Date";
      int num = 2;
      foreach (studentreport studentreport in studentreportList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) studentreport.id_enquiry;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) studentreport.name;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) studentreport.mail;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) studentreport.phone;
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) studentreport.brief_title;
        excelWorksheet.Cells[string.Format("F{0}", (object) num)].Value = (object) studentreport.enquiry;
        excelWorksheet.Cells[string.Format("G{0}", (object) num)].Value = (object) studentreport.update_date_time.ToString("yyyy-MM-dd-HH:mm:ss");
        ++num;
      }
      excelWorksheet.Cells["A:AZ"].AutoFitColumns();
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      this.Response.AddHeader("content-disposition", "attachment: filename=ExcelReport.xlsx");
      this.Response.BinaryWrite(excelPackage.GetAsByteArray());
      this.Response.End();
    }

    public ActionResult Studentjoblistview()
    {
      List<studentappliedjob> studentappliedjobList = new List<studentappliedjob>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        studentappliedjobList = m2ostDbContext.Database.SqlQuery<studentappliedjob>("SELECT t1.id_user, t3.COMPANY_NAME, t3.COMPANY_NO, t2.jobtitle, t2.minsalary, t2.maxsalary, t2.noofopen, t2.gender, t2.minquali, t2.experience, t2.name, t2.mailid, t2.expdate, t4.FIRSTNAME, t4.EMAIL, t4.MOBILE, t4.GENDER AS A_GENDER,t4.CITY,t4.DATE_OF_BIRTH,t4.COLLEGE,t4.GRADUATIONYEAR,case when t4.ResumeFlag = 1 then 'CVUploaded' else 'CVNotUploaded' end as 'CVUploadStatus',case when t1.status ='A' then 'Applied' when t1.status ='V' then 'Viewed' when t1.status ='S' then 'Shortlisted' when t1.status = 'O' then 'Offered' else  'Rejected' end as 'status', t1.updated_date_time FROM tbl_job_user_log as t1 inner join tbl_job as t2 on t2.id_job = t1.id_job inner join job_organization as t3 on t3.id_organization = t2.id_organization inner  join tbl_profile as t4 on t4.id_user = t1.id_user").ToList<studentappliedjob>();
      this.ViewData["feedback"] = (object) studentappliedjobList;
      return (ActionResult) this.View();
    }

    public void ExcelAllReportJob()
    {
      List<studentappliedjob> studentappliedjobList = new List<studentappliedjob>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        studentappliedjobList = m2ostDbContext.Database.SqlQuery<studentappliedjob>("SELECT t1.id_user, t3.COMPANY_NAME, t3.COMPANY_NO, t2.jobtitle, t2.minsalary, t2.maxsalary, t2.noofopen, t2.gender, t2.minquali, t2.experience, t2.name, t2.mailid, t2.expdate, t4.FIRSTNAME, t4.EMAIL, t4.MOBILE, t4.GENDER AS A_GENDER,t4.CITY,t4.DATE_OF_BIRTH,t4.COLLEGE,t4.GRADUATIONYEAR,case when t4.ResumeFlag = 1 then 'CVUploaded' else 'CVNotUploaded' end as 'CVUploadStatus',case when t1.status ='A' then 'Applied' when t1.status ='V' then 'Viewed' when t1.status ='S' then 'Shortlisted' when t1.status = 'O' then 'Offered' else  'Rejected' end as 'status', t1.updated_date_time FROM tbl_job_user_log as t1 inner join tbl_job as t2 on t2.id_job = t1.id_job inner join job_organization as t3 on t3.id_organization = t2.id_organization inner  join tbl_profile as t4 on t4.id_user = t1.id_user").ToList<studentappliedjob>();
      ExcelPackage excelPackage = new ExcelPackage();
      ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Report");
      excelWorksheet.Cells["A1:S1"].Style.Font.Bold = true;
      excelWorksheet.Cells.Style.Font.Size = 10f;
      excelWorksheet.Cells["A1"].Value = (object) "Id_user";
      excelWorksheet.Cells["B1"].Value = (object) "Company_name";
      excelWorksheet.Cells["C1"].Value = (object) "Company_No";
      excelWorksheet.Cells["D1"].Value = (object) "Job_Title";
      excelWorksheet.Cells["E1"].Value = (object) "Min_Salary";
      excelWorksheet.Cells["F1"].Value = (object) "Max_Salary";
      excelWorksheet.Cells["G1"].Value = (object) "No_Of_Opening";
      excelWorksheet.Cells["H1"].Value = (object) "Gender";
      excelWorksheet.Cells["I1"].Value = (object) "Min_Qualification";
      excelWorksheet.Cells["J1"].Value = (object) "Experience";
      excelWorksheet.Cells["K1"].Value = (object) "Name";
      excelWorksheet.Cells["L1"].Value = (object) "Mail_Id";
      excelWorksheet.Cells["M1"].Value = (object) "Expdate";
      excelWorksheet.Cells["N1"].Value = (object) "First_Name";
      excelWorksheet.Cells["O1"].Value = (object) "Mail_Id";
      excelWorksheet.Cells["P1"].Value = (object) "Mobile";
      excelWorksheet.Cells["Q1"].Value = (object) "Gender";
      excelWorksheet.Cells["R1"].Value = (object) "City";
      excelWorksheet.Cells["S1"].Value = (object) "Date_Of_Birth";
      excelWorksheet.Cells["T1"].Value = (object) "College";
      excelWorksheet.Cells["U1"].Value = (object) "GraduationYear";
      excelWorksheet.Cells["V1"].Value = (object) "CvUploadStatus";
      excelWorksheet.Cells["W1"].Value = (object) "Status";
      excelWorksheet.Cells["X1"].Value = (object) "Applied Date";
      int num = 2;
      foreach (studentappliedjob studentappliedjob in studentappliedjobList)
      {
        excelWorksheet.Cells[string.Format("A{0}", (object) num)].Value = (object) studentappliedjob.id_user;
        excelWorksheet.Cells[string.Format("B{0}", (object) num)].Value = (object) studentappliedjob.COMPANY_NAME;
        excelWorksheet.Cells[string.Format("C{0}", (object) num)].Value = (object) studentappliedjob.COMPANY_NO;
        excelWorksheet.Cells[string.Format("D{0}", (object) num)].Value = (object) studentappliedjob.jobtitle;
        excelWorksheet.Cells[string.Format("E{0}", (object) num)].Value = (object) studentappliedjob.minsalary;
        excelWorksheet.Cells[string.Format("F{0}", (object) num)].Value = (object) studentappliedjob.maxsalary;
        excelWorksheet.Cells[string.Format("G{0}", (object) num)].Value = (object) studentappliedjob.noofopen;
        excelWorksheet.Cells[string.Format("H{0}", (object) num)].Value = (object) studentappliedjob.gender;
        excelWorksheet.Cells[string.Format("I{0}", (object) num)].Value = (object) studentappliedjob.minquali;
        excelWorksheet.Cells[string.Format("J{0}", (object) num)].Value = (object) studentappliedjob.experience;
        excelWorksheet.Cells[string.Format("K{0}", (object) num)].Value = (object) studentappliedjob.name;
        excelWorksheet.Cells[string.Format("L{0}", (object) num)].Value = (object) studentappliedjob.mailid;
        excelWorksheet.Cells[string.Format("M{0}", (object) num)].Value = (object) studentappliedjob.expdate;
        excelWorksheet.Cells[string.Format("N{0}", (object) num)].Value = (object) studentappliedjob.FIRSTNAME;
        excelWorksheet.Cells[string.Format("O{0}", (object) num)].Value = (object) studentappliedjob.EMAIL;
        excelWorksheet.Cells[string.Format("P{0}", (object) num)].Value = (object) studentappliedjob.MOBILE;
        excelWorksheet.Cells[string.Format("Q{0}", (object) num)].Value = (object) studentappliedjob.A_GENDER;
        excelWorksheet.Cells[string.Format("R{0}", (object) num)].Value = (object) studentappliedjob.CITY;
        excelWorksheet.Cells[string.Format("S{0}", (object) num)].Value = (object) studentappliedjob.DATE_OF_BIRTH;
        excelWorksheet.Cells[string.Format("T{0}", (object) num)].Value = (object) studentappliedjob.COLLEGE;
        excelWorksheet.Cells[string.Format("U{0}", (object) num)].Value = (object) studentappliedjob.GRADUATIONYEAR;
        excelWorksheet.Cells[string.Format("V{0}", (object) num)].Value = (object) studentappliedjob.CVUploadStatus;
        excelWorksheet.Cells[string.Format("W{0}", (object) num)].Value = (object) studentappliedjob.status;
        excelWorksheet.Cells[string.Format("X{0}", (object) num)].Value = (object) studentappliedjob.updated_date_time.ToString("yyyy-MM-dd-HH:mm:ss");
        ++num;
      }
      excelWorksheet.Cells["A:AZ"].AutoFitColumns();
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      this.Response.AddHeader("content-disposition", "attachment: filename=ExcelReport.xlsx");
      this.Response.BinaryWrite(excelPackage.GetAsByteArray());
      this.Response.End();
    }

    public ActionResult ScoreLog()
    {
      List<tbl_academic_tiles> tblAcademicTilesList = new List<tbl_academic_tiles>();
      List<tbl_brief_category> tblBriefCategoryList = new List<tbl_brief_category>();
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
      {
        tblAcademicTilesList = m2ostDbContext.Database.SqlQuery<tbl_academic_tiles>("select * from tbl_academic_tiles where id_org=130 and status = 'A'").ToList<tbl_academic_tiles>();
        tblBriefCategoryList = m2ostDbContext.Database.SqlQuery<tbl_brief_category>("select * from tbl_brief_category where id_organization=130 and status='A'").ToList<tbl_brief_category>();
      }
      this.ViewData["acad"] = (object) tblAcademicTilesList;
      this.ViewData["category"] = (object) tblBriefCategoryList;
      return (ActionResult) this.View();
    }

    public string getExportDataList(string acid, string bcid)
    {
      List<scorelist> scorelistList = new List<scorelist>();
      string sql = "SELECT t5.tile_name,t3.brief_category,t2.brief_title,concat(t4.firstname,'',t4.lastname) as 'StudentName',t4.CITY, t4.EMAIL,t4.MOBILE,t4.GENDER,t4.COLLEGE,t4.GRADUATIONYEAR,t1.score FROM tbl_user_game_score_log as t1 INNER JOIN tbl_brief_master as t2 ON t2.id_brief_master = t1.id_brief INNER JOIN tbl_brief_category as t3 ON t3.id_brief_category = t2.id_brief_category inner join tbl_academic_tiles as t5 on t5.id_academic_tile = t1.id_academic_tile INNER JOIN tbl_profile as t4 ON t4.id_user = t1.ID_USER where t1.id_academic_tile =" + acid + " and t3.id_brief_category =" + bcid + "  ORDER BY t1.id_user ";
      using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        scorelistList = m2ostDbContext.Database.SqlQuery<scorelist>(sql).ToList<scorelist>();
      string str = "";
      foreach (scorelist scorelist in scorelistList)
      {
        str += "<tr>";
        str = str + "<td>" + scorelist.tile_name + "</td>";
        str = str + "<td>" + scorelist.brief_category + "</td>";
        str = str + "<td>" + scorelist.brief_title + "</td>";
        str = str + "<td>" + scorelist.StudentName + "</td>";
        str = str + "<td>" + scorelist.CITY + "</td>";
        str = str + "<td>" + scorelist.EMAIL + "</td>";
        str = str + "<td>" + scorelist.MOBILE + "</td>";
        str = str + "<td>" + scorelist.GENDER + "</td>";
        str = str + "<td>" + scorelist.COLLEGE + "</td>";
        str = str + "<td>" + scorelist.GRADUATIONYEAR + "</td>";
        str = str + "<td>" + (object) scorelist.score + "</td>";
        str += "</tr>";
      }
      return "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"><thead><tr><th>TILE NAME</th><th>BRIEF CATEGORY</th><th>BRIEF TITLE</th><th>NAME</th><th>CITY</th><th>EMAIL</th><th>MOBILE</th><th>GENDER</th><th>COLLEGE</th><th>GRADUATIONYEAR</th><th>SCORE</th></tr></thead>" + "<tbody>" + str + "</tbody></table>";
    }
  }
}
