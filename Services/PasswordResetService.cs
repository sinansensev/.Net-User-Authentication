// Services/PasswordResetService.cs
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
    public class PasswordResetService
    {
        private readonly AppDbContext _context;

        public PasswordResetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendResetCodeAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }

            
            string code = new Random().Next(100000, 999999).ToString();

            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "******@gmail.com";
                string smtpPassword = "******";

                
                MailMessage message = new MailMessage();
                message.From = new MailAddress("*****@gmail.com");
                message.To.Add(user.Email);
                message.Subject = "Password Reset Code";
                message.Body = $"Your password reset code is: {code}";

                
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                
                await smtpClient.SendMailAsync(message);

                
                user.ResetCode = code;
                user.ResetCodeExpiration = DateTime.UtcNow.AddMinutes(15); 
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(string email, string code, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.ResetCode == code && u.ResetCodeExpiration > DateTime.UtcNow);
            if (user == null)
            {
                return false;
            }

            
            user.Password = newPassword;

            
            user.ResetCode = null;
            user.ResetCodeExpiration = null;

            
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
