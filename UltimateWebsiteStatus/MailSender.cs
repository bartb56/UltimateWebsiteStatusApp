using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace UltimateWebsiteStatus
{
    public class MailSender
    {
        public async Task SendMail(string content)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("donotreply@bartblokhuis.nl"); //Email address from sender
            message.To.Add(new MailAddress("info@bartblokhuis.nl")); //Email address recepeint

            message.Subject = "Website status report"; //Email subject
            message.Body = content; //The error message

            SmtpClient client = new SmtpClient("mail.ultimateinvoicing.com", 8889); // Create the smtp client
            
            try
            {
                NetworkCredential credentials = new NetworkCredential("donotreply@ultimateinvoicing.com", "YourPassword"); //Email address and password
                client.Credentials = credentials;
                client.EnableSsl = false;
                await client.SendMailAsync(message);
            }
            catch (Exception e)
            { 
            
            }
            finally
            {
                client.Dispose();
            }
            return;
        }
    }
}
