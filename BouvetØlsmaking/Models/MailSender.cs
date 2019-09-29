using System;
using System.Net.Mail;

namespace BouvetØlsmaking.Models
{
    public static class MailSender
    {
        public static void Send(string mailAddress, string subject, string body)
        {
            MailMessage message = new MailMessage(new MailAddress("beertasting@skarets.com", "Beertasting"), new MailAddress(mailAddress));

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("send.one.com");
            client.EnableSsl = false;
            client.Port = 587;
            client.EnableSsl = true;

            var cred = new System.Net.NetworkCredential("beertasting@skarets.com", "7dXBMsZmzyq8");

            client.Credentials = cred;

            try
            {
                client.Send(message);
            }
            catch (Exception)
            {      
                
            }

            return;
        }        
    }
}
