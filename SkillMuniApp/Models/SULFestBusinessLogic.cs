// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.SULFestBusinessLogic
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace m2ostnext.Models
{
  public class SULFestBusinessLogic
  {
    public string getApiResponseString(string api)
    {
      byte[] bytes = (byte[]) null;
      WebClient webClient1 = new WebClient();
      using (WebClient webClient2 = new WebClient())
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        bytes = webClient2.DownloadData(api);
      }
      return Encoding.UTF8.GetString(bytes);
    }

    public void SendEventPushNotification(List<tbl_user_gcm_log> fcm, tbl_sul_fest_master fes)
    {
      try
      {
        string str1 = "AAAAGrnsAbc:APA91bH3oHyM5R0KrFxEexkW-DmnOr5HD1oyKmsmP_nlUjNEdlmAUu1ZF7YuD3y8NGmMx_760dgynH1hYw603TN7CAnpgD4yp59dUFOMi198H42RweTvKHYEwfVzdusHMMZuKnRvjXUW";
        string str2 = "114788401591";
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          int? collegeRestricted = fes.is_college_restricted;
          int num = 1;
          if (collegeRestricted.GetValueOrDefault() == num & collegeRestricted.HasValue)
          {
            string str3 = m2ostDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) fes.id_college).FirstOrDefault<string>();
            foreach (tbl_user_gcm_log tblUserGcmLog in fcm)
            {
              tbl_profile tblProfile1 = new tbl_profile();
              tbl_profile tblProfile2 = m2ostDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUserGcmLog.id_user).FirstOrDefault<tbl_profile>();
              if (str3 == tblProfile2.COLLEGE)
              {
                WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                webRequest.Method = "post";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str1));
                webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str2));
                webRequest.ContentType = "application/json";
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) new
                {
                  to = tblUserGcmLog.GCMID,
                  priority = "high",
                  content_available = true,
                  notification = new
                  {
                    body = fes.event_objective,
                    title = fes.event_title,
                    badge = 1,
                    icon = fes.city
                  }
                }).ToString());
                webRequest.ContentLength = (long) bytes.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                  requestStream.Write(bytes, 0, bytes.Length);
                  using (WebResponse response = webRequest.GetResponse())
                  {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                      if (responseStream != null)
                      {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                          streamReader.ReadToEnd();
                      }
                    }
                  }
                }
              }
            }
          }
          else
          {
            foreach (tbl_user_gcm_log tblUserGcmLog in fcm)
            {
              WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
              webRequest.Method = "post";
              webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str1));
              webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str2));
              webRequest.ContentType = "application/json";
              byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) new
              {
                to = tblUserGcmLog.GCMID,
                priority = "high",
                content_available = true,
                notification = new
                {
                  body = fes.event_objective,
                  title = fes.event_title,
                  badge = 1,
                  icon = fes.city
                }
              }).ToString());
              webRequest.ContentLength = (long) bytes.Length;
              using (Stream requestStream = webRequest.GetRequestStream())
              {
                requestStream.Write(bytes, 0, bytes.Length);
                using (WebResponse response = webRequest.GetResponse())
                {
                  using (Stream responseStream = response.GetResponseStream())
                  {
                    if (responseStream != null)
                    {
                      using (StreamReader streamReader = new StreamReader(responseStream))
                        streamReader.ReadToEnd();
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void SendEventMailNotification(List<tbl_profile> prof, tbl_sul_fest_master fes)
    {
      try
      {
        string str1 = "skillmuni@thegamificationcompany.com";
        using (m2ostDBContext m2ostDbContext = new m2ostDBContext())
        {
          string str2 = m2ostDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) fes.id_college).FirstOrDefault<string>();
          int? collegeRestricted = fes.is_college_restricted;
          int num = 1;
          if (collegeRestricted.GetValueOrDefault() == num & collegeRestricted.HasValue)
          {
            foreach (tbl_profile tblProfile in prof)
            {
              if (str2 == tblProfile.COLLEGE && tblProfile.EMAIL != null && tblProfile.EMAIL != "")
              {
                string email = tblProfile.EMAIL;
                string str3 = string.Empty;
                using (StreamReader streamReader = new StreamReader((ConfigurationManager.AppSettings["mail_sul_event_opublish"] ?? "") ?? ""))
                  str3 = streamReader.ReadToEnd();
                string str4 = str3;
                DateTime dateTime = fes.registration_start_date;
                string newValue1 = Convert.ToString(dateTime.Date);
                string str5 = str4.Replace("{REG_START}", newValue1);
                dateTime = fes.registration_end_date;
                string newValue2 = Convert.ToString(dateTime.Date);
                string body = str5.Replace("{REG_END}", newValue2).Replace("{FESt_MONTH}", Convert.ToString(fes.event_start_date.ToString("MMMM"))).Replace("{FEST_DATE}", Convert.ToString(fes.event_start_date.Day)).Replace("{COLLEGE_NAME}", Convert.ToString(str2)).Replace("{COLLEGE_ADDRESS}", Convert.ToString(fes.address)).Replace("{START_TIME}", Convert.ToString(fes.event_start_date.ToString("h:mm tt"))).Replace("{END_TIME}", Convert.ToString(fes.event_start_date.ToString("h:mm tt"))).Replace("{CONTACT_NAME}", Convert.ToString(fes.contact_name)).Replace("{CONTACT_NUMBER}", Convert.ToString(fes.contact_number));
                string subject = "New Event Available - " + fes.event_title;
                string eventObjective = fes.event_objective;
                new SmtpClient()
                {
                  Host = "smtp.gmail.com",
                  Port = 587,
                  EnableSsl = true,
                  DeliveryMethod = SmtpDeliveryMethod.Network,
                  Credentials = ((ICredentialsByHost) new NetworkCredential(str1, "03012019@Skillmuni")),
                  Timeout = 30000
                }.Send(new MailMessage(str1, email, subject, body)
                {
                  IsBodyHtml = true
                });
              }
            }
          }
          else
          {
            foreach (tbl_profile tblProfile in prof)
            {
              if (tblProfile.EMAIL != null && tblProfile.EMAIL != "")
              {
                string email = tblProfile.EMAIL;
                string str6 = string.Empty;
                using (StreamReader streamReader = new StreamReader((ConfigurationManager.AppSettings["mail_sul_event_opublish"] ?? "") ?? ""))
                  str6 = streamReader.ReadToEnd();
                string str7 = str6;
                DateTime dateTime = fes.registration_start_date;
                string newValue3 = Convert.ToString(dateTime.Date);
                string str8 = str7.Replace("{REG_START}", newValue3);
                dateTime = fes.registration_end_date;
                string newValue4 = Convert.ToString(dateTime.Date);
                string body = str8.Replace("{REG_END}", newValue4).Replace("{FESt_MONTH}", Convert.ToString(fes.event_start_date.ToString("MMMM"))).Replace("{FEST_DATE}", Convert.ToString(fes.event_start_date.Day)).Replace("{COLLEGE_NAME}", Convert.ToString(str2)).Replace("{COLLEGE_ADDRESS}", Convert.ToString(fes.address)).Replace("{START_TIME}", Convert.ToString(fes.event_start_date.ToString("h:mm tt"))).Replace("{END_TIME}", Convert.ToString(fes.event_start_date.ToString("h:mm tt"))).Replace("{CONTACT_NAME}", Convert.ToString(fes.contact_name)).Replace("{CONTACT_NUMBER}", Convert.ToString(fes.contact_number));
                string subject = "New Event Available - " + fes.event_title;
                string eventObjective = fes.event_objective;
                new SmtpClient()
                {
                  Host = "smtp.gmail.com",
                  Port = 587,
                  EnableSsl = true,
                  DeliveryMethod = SmtpDeliveryMethod.Network,
                  Credentials = ((ICredentialsByHost) new NetworkCredential(str1, "03012019@Skillmuni")),
                  Timeout = 30000
                }.Send(new MailMessage(str1, email, subject, body)
                {
                  IsBodyHtml = true
                });
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
