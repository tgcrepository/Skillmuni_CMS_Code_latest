// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.AndroidGCMPushNotification
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.IO;
using System.Net;
using System.Text;

namespace m2ostnext.Models
{
  public class AndroidGCMPushNotification
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public string SendNotification(string deviceRegIds, string message)
    {
      string str1;
      try
      {
        string str2 = "AIzaSyDv-klhBu-VjOZTL3adZPeOejB-DlLiMFs";
        string str3 = "290144898053";
        WebRequest webRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        webRequest.Method = "post";
        webRequest.ContentType = "application/json";
        webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str2));
        webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str3));
        byte[] bytes = Encoding.UTF8.GetBytes("{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"data\": { \"message\" : \"" + message + "\",\"time\": \"" + DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + deviceRegIds + "\"]}");
        webRequest.ContentLength = (long) bytes.Length;
        Stream requestStream = webRequest.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
        WebResponse response;
        HttpStatusCode statusCode = ((HttpWebResponse) (response = webRequest.GetResponse())).StatusCode;
        string str4 = "";
        if (statusCode.Equals((object) HttpStatusCode.Unauthorized) || statusCode.Equals((object) HttpStatusCode.Forbidden))
          str4 = "Unauthorized - need new token";
        else if (!statusCode.Equals((object) HttpStatusCode.OK))
          str4 = "Response from web service isn't OK";
        StreamReader streamReader = new StreamReader(response.GetResponseStream());
        str1 = streamReader.ReadLine();
        streamReader.Close();
        this.db.error_log.Add(new error_log()
        {
          Error_Message = deviceRegIds,
          Error_Inner = str4 + " | " + str1,
          STATUS = "N",
          UPDATEDDATETIME = DateTime.Now
        });
        this.db.SaveChanges();
        streamReader.Close();
        requestStream.Close();
        response.Close();
      }
      catch (Exception ex)
      {
        Stream responseStream = ((WebException) ex).Response.GetResponseStream();
        string str5 = "";
        int num;
        do
        {
          num = responseStream.ReadByte();
          str5 += ((char) num).ToString();
        }
        while (num != -1);
        responseStream.Close();
        str1 = str5;
      }
      return str1;
    }
  }
}
