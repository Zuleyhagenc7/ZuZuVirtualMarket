using ZuZuVirtual.Core.Entities;
using System.Net;
using System.Net.Mail;
using ZuZuVirtual.core.Entities;

namespace ZuZuVirtualMarket.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task<bool> SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadi.com",587);
            smtpClient.Credentials = new NetworkCredential("info@siteadi.com", "mailşifresi");
            smtpClient.EnableSsl = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadi.com");
            message.To.Add("bilgi@siteadi.com");
            message.Subject = "A message came from the site!";
            message.Body = $"Name: {contact.Name} - Surname: {contact.Surname} - Email: {contact.Email} - Phone: {contact.Phone} - Message: {contact.Message}  ";
            message.IsBodyHtml = true;
            try
            {
                await smtpClient.SendMailAsync(message);
                smtpClient.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static async Task<bool> SendMailAsync(string email, string subject ,string mailBody)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadi.com",587);
            smtpClient.Credentials = new NetworkCredential("info@siteadi.com", "mailşifresi");
            smtpClient.EnableSsl = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadi.com");
            message.To.Add(email);
            message.Subject = subject;
            message.Body = mailBody;
            message.IsBodyHtml = true;
            try
            {
                await smtpClient.SendMailAsync(message);
                smtpClient.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
