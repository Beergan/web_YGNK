using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;

using System.Net.Configuration;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail : System.Web.UI.Page
{
    public SendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Send_Mail(string subject, string body, string to)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("noreplay.slk@gmail.com", "Dụng cụ cây cảnh");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("noreplay.slk@gmail.com", "eeaukwkvvmdovzfo");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(mail);
            }
            return "";
        }
        catch { return ""; }
    }
}