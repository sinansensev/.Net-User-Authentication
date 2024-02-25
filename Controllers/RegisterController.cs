using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayosferIdentity.Models;
using PayosferIdentity.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PayosferIdentity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly EmailService _emailService;

        public RegisterController(IUserService userService, EmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var passwordHash = HashPassword(registerModel.Password);

            var user = new User
            {
                Email = registerModel.Email,
                Password = passwordHash,
                FirstName = registerModel.FirstName,
                Surname = registerModel.Surname,
                PhoneNumber = registerModel.PhoneNumber,
                Role = registerModel.Role
            };

            if (await _userService.RegisterAsync(user))
            {
                var emailSubject = "Registration Successful";
                var emailBody = $"Dear {registerModel.FirstName},<br /><br />Your registration is successful. Thank you for signing up.<br /><br />Your email: {registerModel.Email}<br />Your password: {registerModel.Password}";

                var emailSent = await _emailService.SendEmailAsync(registerModel.Email, emailSubject, emailBody);

                if (emailSent)
                {
                    var token = GenerateJwtToken(user.Email);
                    Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                    return Ok(new { Token = token, Message = "Registration successful. Confirmation email sent." });
                }
                else
                {
                    
                    return BadRequest("Failed to send confirmation email. Please contact support.");
                }
            }
            return BadRequest("Registration failed");
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5265/auth/register",
                audience: "http://localhost:5265/auth/register",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
