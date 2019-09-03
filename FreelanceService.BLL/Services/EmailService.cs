using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using FreelanceService.BLL.DTO;
using Microsoft.Extensions.Configuration;

//заюзать mailKit

namespace FreelanceService.BLL.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration conf)
        {
            _config = conf;
        }


        public void SendAsync(EmailModel entity)
        {
            {
                var email = _config.GetSection("EmailService:Email").Value;
                var pass = _config.GetSection("EmailService: Pass").Value;

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(email, "FreelanceService");
                // кому отправляем
                MailAddress to = new MailAddress(entity.EmailUser);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = entity.Subject;
                // текст письма
                m.Body = "<h2>" + entity.Description + "</h2>";
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
                // логин и пароль
                smtp.Credentials = new NetworkCredential(email, pass);
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
        }
    }
}
