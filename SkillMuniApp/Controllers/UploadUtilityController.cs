// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.UploadUtilityController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class UploadUtilityController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult AppUserUpload(int error = 0)
    {
      if (error == 1)
        this.ViewData[nameof (error)] = (object) "Uploaded sheet is not in correct format.Please refer template for refrance";
      else
        this.ViewData[nameof (error)] = (object) "null";
      return (ActionResult) this.View();
    }

    public ActionResult UserUploadDisplay()
    {
      List<UserUploadClass> userUploadClassList = new List<UserUploadClass>();
      List<UserUploadClass> source = new List<UserUploadClass>();
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int orgid = Convert.ToInt32(content.id_ORGANIZATION);
      string upKey = "UP_" + content.id_ORGANIZATION + "_" + content.ID_USER;
      if (this.Request != null)
      {
        HttpPostedFileBase file = this.Request.Files["uploadBtn"];
        if (file != null)
        {
          if (file.ContentLength >= 0 || !string.IsNullOrEmpty(file.FileName))
          {
            string fileName = file.FileName;
            string contentType = file.ContentType;
            byte[] buffer = new byte[file.ContentLength];
            file.InputStream.Read(buffer, 0, Convert.ToInt32(file.ContentLength));
            using (ExcelPackage excelPackage = new ExcelPackage(file.InputStream))
            {
              ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault<ExcelWorksheet>();
              if (excelWorksheet != null)
              {
                int column = excelWorksheet.Dimension.End.Column;
                int row = excelWorksheet.Dimension.End.Row;
                if (column != 21)
                  return (ActionResult) this.RedirectToAction("AppUserUpload", (object) new
                  {
                    error = 1
                  });
                for (int Row = 2; Row <= row; ++Row)
                {
                  UserUploadClass userUploadClass = new UserUploadClass();
                  try
                  {
                    if (excelWorksheet.Cells[Row, 1].Value != null)
                    {
                      userUploadClass.EMPLOYEEID = excelWorksheet.Cells[Row, 1].Value.ToString();
                      userUploadClass.ROLE = excelWorksheet.Cells[Row, 2].Value.ToString();
                      userUploadClass.USERID = excelWorksheet.Cells[Row, 3].Value.ToString();
                      userUploadClass.PASSWORD = excelWorksheet.Cells[Row, 4].Value.ToString();
                      userUploadClass.FIRSTNAME = excelWorksheet.Cells[Row, 5].Value.ToString();
                      userUploadClass.LASTNAME = Convert.ToString(excelWorksheet.Cells[Row, 6].Value);
                      userUploadClass.AGE = excelWorksheet.Cells[Row, 7].Value.ToString();
                      userUploadClass.EMAIL = excelWorksheet.Cells[Row, 8].Value.ToString();
                      userUploadClass.MOBILE = excelWorksheet.Cells[Row, 9].Value.ToString();
                      userUploadClass.GENDER = excelWorksheet.Cells[Row, 10].Value.ToString();
                      userUploadClass.CITY = excelWorksheet.Cells[Row, 11].Value.ToString();
                      userUploadClass.OFFICE_ADDRESS = excelWorksheet.Cells[Row, 12].Value.ToString();
                      string dateString1 = excelWorksheet.Cells[Row, 13].Value.ToString();
                      string dateString2 = excelWorksheet.Cells[Row, 14].Value.ToString();
                      DateTime datetime1 = new Utility().StringToDatetime(dateString1);
                      DateTime datetime2 = new Utility().StringToDatetime(dateString2);
                      userUploadClass.DATE_OF_BIRTH = datetime1.ToString("dd-MM-yyyy");
                      userUploadClass.DATE_OF_JOINING = datetime2.ToString("dd-MM-yyyy");
                      userUploadClass.user_department = excelWorksheet.Cells[Row, 15].Value.ToString();
                      userUploadClass.user_designation = excelWorksheet.Cells[Row, 16].Value.ToString();
                      userUploadClass.user_function = excelWorksheet.Cells[Row, 17].Value.ToString();
                      userUploadClass.user_grade = excelWorksheet.Cells[Row, 18].Value.ToString();
                      userUploadClass.user_status = excelWorksheet.Cells[Row, 19].Value.ToString();
                      userUploadClass.reporting_manager = excelWorksheet.Cells[Row, 21].Value.ToString();
                      userUploadClassList.Add(userUploadClass);
                    }
                  }
                  catch (Exception ex)
                  {
                    userUploadClassList.Add(userUploadClass);
                  }
                }
              }
            }
            foreach (UserUploadClass userUploadClass1 in userUploadClassList)
            {
              UserUploadClass item = userUploadClass1;
              UserUploadClass userUploadClass2 = new UserUploadClass();
              tbl_user tblUser1 = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.EMPLOYEEID == item.EMPLOYEEID && t.ID_ORGANIZATION == (int?) orgid && t.USERID.ToLower() == item.USERID.ToLower())).FirstOrDefault<tbl_user>();
              userUploadClass2.EMPLOYEEID = item.EMPLOYEEID.Trim();
              userUploadClass2.ROLE = item.ROLE;
              string rStr = item.ROLE;
              tbl_csst_role tblCsstRole = this.db.tbl_csst_role.Where<tbl_csst_role>((Expression<Func<tbl_csst_role, bool>>) (t => t.csst_role.ToUpper() == rStr.ToUpper() && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_csst_role>();
              userUploadClass2.STATUS = "A";
              if (tblCsstRole == null)
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.id_role = 0;
                userUploadClass2.Message = "Role Not Found";
              }
              else
                userUploadClass2.id_role = tblCsstRole.id_csst_role;
              userUploadClass2.USERID = item.USERID;
              userUploadClass2.PASSWORD = item.PASSWORD;
              if (item.FIRSTNAME != null)
              {
                userUploadClass2.FIRSTNAME = item.FIRSTNAME.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.LASTNAME != null)
              {
                userUploadClass2.LASTNAME = item.LASTNAME.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              userUploadClass2.AGE = item.AGE;
              if (item.EMAIL != null)
              {
                userUploadClass2.EMAIL = item.EMAIL.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              userUploadClass2.AGE = item.AGE;
              if (item.MOBILE != null)
              {
                userUploadClass2.MOBILE = item.MOBILE.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.GENDER != null)
              {
                userUploadClass2.GENDER = item.GENDER.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.CITY != null)
              {
                userUploadClass2.CITY = item.CITY.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.OFFICE_ADDRESS != null)
              {
                userUploadClass2.OFFICE_ADDRESS = item.OFFICE_ADDRESS.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.DATE_OF_BIRTH != null)
              {
                userUploadClass2.DATE_OF_BIRTH = item.DATE_OF_BIRTH.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.DATE_OF_JOINING != null)
              {
                userUploadClass2.DATE_OF_JOINING = item.DATE_OF_JOINING.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.user_department != null)
              {
                userUploadClass2.user_department = item.user_department.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.user_designation != null)
              {
                userUploadClass2.user_designation = item.user_designation.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.user_function != null)
              {
                userUploadClass2.user_function = item.user_function.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.user_grade != null)
              {
                userUploadClass2.user_grade = item.user_grade.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.user_status != null)
              {
                userUploadClass2.user_status = item.user_status.Trim();
              }
              else
              {
                userUploadClass2.STATUS = "R";
                userUploadClass2.Message = "|Some data Not Found";
              }
              if (item.reporting_manager != null)
                userUploadClass2.reporting_manager = item.reporting_manager.Trim();
              if (!(item.reporting_manager == "NA"))
              {
                tbl_user tblUser2 = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.EMPLOYEEID == item.reporting_manager && t.ID_ORGANIZATION == (int?) orgid)).FirstOrDefault<tbl_user>();
                if (tblUser2 == null)
                {
                  userUploadClass2.STATUS = "R";
                  userUploadClass2.id_reporting_manager = 0;
                  userUploadClass2.Message += "|Reporting Manager Not Found";
                }
                else
                  userUploadClass2.id_reporting_manager = tblUser2.ID_USER;
              }
              if (tblUser1 != null)
              {
                userUploadClass2.Message += "|User already Present.";
                userUploadClass2.STATUS = "R";
              }
              if (userUploadClass2.DATE_OF_BIRTH == null)
              {
                userUploadClass2.Message += "|Date of Birth is not in correct format";
                userUploadClass2.STATUS = "R";
              }
              if (userUploadClass2.DATE_OF_JOINING == null)
              {
                userUploadClass2.Message += "|Date of joining is not in correct format";
                userUploadClass2.STATUS = "R";
              }
              if (userUploadClass2.USERID == null)
              {
                userUploadClass2.Message += "|USERID is null";
                userUploadClass2.STATUS = "R";
              }
              if (userUploadClass2.PASSWORD == null)
              {
                userUploadClass2.Message += "|USERID is null";
                userUploadClass2.STATUS = "R";
              }
              source.Add(userUploadClass2);
            }
            DbSet<tbl_temp_user_upload> tblTempUserUpload = this.db.tbl_temp_user_upload;
            Expression<Func<tbl_temp_user_upload, bool>> predicate = (Expression<Func<tbl_temp_user_upload, bool>>) (t => t.temp_user_upload_key == upKey);
            foreach (tbl_temp_user_upload entity in tblTempUserUpload.Where<tbl_temp_user_upload>(predicate).ToList<tbl_temp_user_upload>())
            {
              this.db.tbl_temp_user_upload.Remove(entity);
              this.db.SaveChanges();
            }
            foreach (UserUploadClass userUploadClass in source)
            {
              this.db.tbl_temp_user_upload.Add(new tbl_temp_user_upload()
              {
                EMPLOYEEID = userUploadClass.EMPLOYEEID,
                ROLE = userUploadClass.ROLE,
                status = userUploadClass.STATUS,
                USERID = userUploadClass.USERID,
                PASSWORD = userUploadClass.PASSWORD,
                FIRSTNAME = userUploadClass.FIRSTNAME,
                LASTNAME = userUploadClass.LASTNAME,
                AGE = userUploadClass.AGE,
                EMAIL = userUploadClass.EMAIL,
                MOBILE = userUploadClass.MOBILE,
                GENDER = userUploadClass.GENDER,
                CITY = userUploadClass.CITY,
                OFFICE_ADDRESS = userUploadClass.OFFICE_ADDRESS,
                DATE_OF_BIRTH = userUploadClass.DATE_OF_BIRTH,
                DATE_OF_JOINING = userUploadClass.DATE_OF_JOINING,
                user_department = userUploadClass.user_department,
                user_designation = userUploadClass.user_designation,
                user_function = userUploadClass.user_function,
                user_grade = userUploadClass.user_grade,
                user_status = userUploadClass.user_status,
                reporting_manager = userUploadClass.reporting_manager,
                id_reporting_manager = new int?(userUploadClass.id_reporting_manager),
                ID_ROLE = new int?(userUploadClass.id_role),
                temp_user_upload_key = upKey
              });
              this.db.SaveChanges();
            }
          }
        }
        else
        {
          DbSet<tbl_temp_user_upload> tblTempUserUpload1 = this.db.tbl_temp_user_upload;
          Expression<Func<tbl_temp_user_upload, bool>> predicate = (Expression<Func<tbl_temp_user_upload, bool>>) (t => t.temp_user_upload_key == upKey);
          foreach (tbl_temp_user_upload tblTempUserUpload2 in tblTempUserUpload1.Where<tbl_temp_user_upload>(predicate).ToList<tbl_temp_user_upload>())
            source.Add(new UserUploadClass()
            {
              EMPLOYEEID = tblTempUserUpload2.EMPLOYEEID,
              ROLE = tblTempUserUpload2.ROLE,
              STATUS = tblTempUserUpload2.status,
              USERID = tblTempUserUpload2.USERID,
              PASSWORD = tblTempUserUpload2.PASSWORD,
              FIRSTNAME = tblTempUserUpload2.FIRSTNAME,
              LASTNAME = tblTempUserUpload2.LASTNAME,
              AGE = tblTempUserUpload2.AGE,
              EMAIL = tblTempUserUpload2.EMAIL,
              MOBILE = tblTempUserUpload2.MOBILE,
              GENDER = tblTempUserUpload2.GENDER,
              CITY = tblTempUserUpload2.CITY,
              OFFICE_ADDRESS = tblTempUserUpload2.OFFICE_ADDRESS,
              DATE_OF_BIRTH = tblTempUserUpload2.DATE_OF_BIRTH,
              DATE_OF_JOINING = tblTempUserUpload2.DATE_OF_JOINING,
              user_department = tblTempUserUpload2.user_department,
              user_designation = tblTempUserUpload2.user_designation,
              user_function = tblTempUserUpload2.user_function,
              user_grade = tblTempUserUpload2.user_grade,
              user_status = tblTempUserUpload2.user_status,
              reporting_manager = tblTempUserUpload2.reporting_manager
            });
        }
      }
      List<UserUploadClass> list1 = source.Where<UserUploadClass>((Func<UserUploadClass, bool>) (t => t.STATUS == "A")).ToList<UserUploadClass>();
      List<UserUploadClass> list2 = source.Where<UserUploadClass>((Func<UserUploadClass, bool>) (t => t.STATUS != "A")).ToList<UserUploadClass>();
      this.ViewData["acceptList"] = (object) list1;
      this.ViewData["rejectList"] = (object) list2;
      return (ActionResult) this.View();
    }

    public ActionResult UpdateUserData()
    {
      UserSession content = (UserSession) this.HttpContext.Session.Contents["UserSession"];
      int orgid = Convert.ToInt32(content.id_ORGANIZATION);
      string upKey = "UP_" + content.id_ORGANIZATION + "_" + content.ID_USER;
      List<tbl_temp_user_upload> list = this.db.tbl_temp_user_upload.Where<tbl_temp_user_upload>((Expression<Func<tbl_temp_user_upload, bool>>) (t => t.temp_user_upload_key == upKey && t.status == "A")).ToList<tbl_temp_user_upload>();
      foreach (tbl_temp_user_upload tblTempUserUpload in list)
      {
        tbl_temp_user_upload item = tblTempUserUpload;
        tbl_user entity = new tbl_user();
        entity.EMPLOYEEID = item.EMPLOYEEID;
        entity.USERID = item.USERID.Trim();
        if (this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.EMPLOYEEID == item.EMPLOYEEID && t.ID_ORGANIZATION == (int?) orgid && t.USERID.ToLower() == item.USERID.ToLower())).FirstOrDefault<tbl_user>() == null)
        {
          string encryptedString = new AESAlgorithm().getEncryptedString(item.PASSWORD);
          entity.PASSWORD = encryptedString;
          entity.ID_ORGANIZATION = new int?(orgid);
          entity.FBSOCIALID = "";
          entity.GPSOCIALID = "";
          entity.EXPIRY_DATE = new DateTime?(DateTime.Now);
          entity.STATUS = "A";
          entity.UPDATEDTIME = DateTime.Now;
          entity.ID_CODE = 1;
          entity.user_department = item.user_department;
          entity.user_designation = item.user_designation;
          entity.user_function = item.user_function;
          entity.user_grade = item.user_grade;
          entity.user_status = item.user_status;
          entity.ID_ROLE = Convert.ToInt32((object) item.ID_ROLE);
          entity.reporting_manager = item.id_reporting_manager;
          this.db.tbl_user.Add(entity);
          this.db.SaveChanges();
          if (entity.ID_USER > 0)
          {
            this.db.tbl_role_user_mapping.Add(new tbl_role_user_mapping()
            {
              id_csst_role = item.ID_ROLE,
              id_user = new int?(entity.ID_USER),
              status = "A",
              updated_date_time = new DateTime?(DateTime.Now)
            });
            this.db.SaveChanges();
            this.db.tbl_profile.Add(new tbl_profile()
            {
              FIRSTNAME = item.FIRSTNAME,
              LASTNAME = item.LASTNAME,
              EMAIL = new AESAlgorithm().getEncryptedString(item.EMAIL),
              MOBILE = new AESAlgorithm().getEncryptedString(item.MOBILE),
              AGE = new int?((int) Convert.ToInt16(item.AGE)),
              CITY = item.CITY,
              LOCATION = "",
              DATE_OF_BIRTH = new DateTime?(new Utility().StringToDatetime(item.DATE_OF_BIRTH)),
              DATE_OF_JOINING = new DateTime?(new Utility().StringToDatetime(item.DATE_OF_JOINING)),
              DESIGNATION = item.user_designation,
              GENDER = item.GENDER,
              ID_USER = entity.ID_USER,
              OFFICE_ADDRESS = item.OFFICE_ADDRESS,
              REPORTING_MANAGER = item.reporting_manager
            });
            this.db.SaveChanges();
          }
        }
      }
      this.db.tbl_temp_user_upload.Where<tbl_temp_user_upload>((Expression<Func<tbl_temp_user_upload, bool>>) (t => t.temp_user_upload_key == upKey)).ToList<tbl_temp_user_upload>();
      foreach (tbl_temp_user_upload entity in list)
      {
        this.db.tbl_temp_user_upload.Remove(entity);
        this.db.SaveChanges();
      }
      return (ActionResult) this.RedirectToAction("AppUserUpload", "UploadUtility");
    }
  }
}
