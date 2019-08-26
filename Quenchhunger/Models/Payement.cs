using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Quenchhunger.Models
{
    public class Payement
    {
        public string MerchantId { get; set; }
        public string Tranx_id { get; set; }
        public string Tranx_amt { get; set; }
        public string Pay_amt { get; set; }
        public string Tranx_curr { get; set; }
        public string Cust_id { get; set; }
        public string Order_id { get; set; }
        public string Tranx_memo { get; set; }
        public string Tranx_noti_url { get; set; }
        public string HashValue { get; set; }
        public string callurl(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd();
            return urlText;
        }

        public string SendSms(string toMobile,string Message)
        {
            string url = "http://www.estoresms.com/smsapi.php?username=sangtech&password=goodtech&sender=TD-Quench&recipient=+"+toMobile+"+&message =+"+Message+"&";
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd();
            return urlText;
        }

        public static void SendEmail(string toEmail, string EmailBody)
        {
                MailMessage newmail = new MailMessage();
                SmtpClient smtpserver = new SmtpClient("mail.sangtechtechnologies.com", 587);
                newmail.From = new MailAddress("no-reply@sangtechtechnologies.com");
                newmail.To.Add(toEmail);
                newmail.Subject = "User Name and Password to Access Restaurant Admin Tool";
                newmail.Body = EmailBody;
                smtpserver.Credentials = new NetworkCredential("ajay.sinha@sangtechtechnologies.com", "a@n#1234");
                smtpserver.EnableSsl = false;
                smtpserver.Send(newmail);
        }
    }

}