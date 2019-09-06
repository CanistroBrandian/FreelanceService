using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using FreelanceService.BLL.Helpers;
using FreelanceService.BLL.Interfaces;

//заюзать mailKit

namespace FreelanceService.BLL.Services
{
    public class EmailService :IEmailService
    {
        //private GetConfigString _conf;
        //private string login;
        //private string pass;
        //public EmailService(GetConfigString configString)
        //{
        //    _conf = configString;
        //    pass = _conf.GetEmailOrPass("Pass");
        //    login = _conf.GetEmailOrPass("Email");
        //}

        public async Task SendEmailAsync(string email, string subject, string message)
        {
       
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("FreelanceService", "brovkokosty148@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("brovkokosty148@yandex.ru", "Looser1488923as");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
