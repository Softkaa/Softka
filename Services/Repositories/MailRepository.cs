using Softka.Infrastructure.Data;
using Softka.Models;
using Microsoft.EntityFrameworkCore;
using Softka.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Softka.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Softka.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Softka.Services
{
    public class MailRepository 
    {
           private readonly Email _emailSettings;

          public MailRepository(IOptions<Email> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

         public void SendEmail(string Email, string subject, string body, User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text =$"Hola, {user.Names},\nESta es tu contraseña {user.Password}." };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            
            }
        }
    }
}
