using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GameHub.Helper.SendEmail
{
    // SendEmail class
    // This class is a configuration for sending an email 
    public class SendEmail
    {
        public bool EmailSend(string SenderEmail, string Subject, string Message, bool IsBodyHtml = false)
        {
            bool status = false;
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("Vahidalizadeh1990@gmail.com");
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.To.Add(new MailAddress(SenderEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = "Vahid1234";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;
                smtp.Port = Convert.ToInt32(587);
                smtp.Send(mailMessage);
                status = true;
                return status;
            }
            catch (Exception e)
            {
                return status;
            }
        }
    }
}