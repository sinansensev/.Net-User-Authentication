// EmailService.cs
using Microsoft.EntityFrameworkCore;
using PayosferIdentity.Data;
using PayosferIdentity.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PayosferIdentity.Services
{
    public class EmailService
    {
        private readonly AppDbContext _context;

        public EmailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                string smtpServer = "****.gmail.com";
                int smtpPort = ***;
                string smtpUsername = "******@gmail.com";
                string smtpPassword = "********";

                MailMessage message = new MailMessage();
                message.From = new MailAddress("*******@gmail.com");
                message.To.Add(email);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Email gönderirken bir hata oluştu: {ex.Message}");
                
                return false;
            }
        }
    }
}
