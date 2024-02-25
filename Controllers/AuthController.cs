using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayosferIdentity.Controllers;
using PayosferIdentity.Data;
using PayosferIdentity.Models;
using PayosferIdentity.Services;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;

    public AuthController(AppDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var user = new User
        {
            Email = registerModel.Email,
            Password = registerModel.Password,
            FirstName = registerModel.FirstName,
            Surname = registerModel.Surname,
            PhoneNumber = registerModel.PhoneNumber,
            Role = registerModel.Role
        };

        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return Conflict("Email already exists");

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var emailSubject = "Registration Successful";
        var emailBody = $"Dear {registerModel.FirstName},<br /><br />Your registration is successful. Thank you for signing up.<br /><br />Your email: {registerModel.Email}<br />Your password: {registerModel.Password}";

        var emailSent = await _emailService.SendEmailAsync(registerModel.Email, emailSubject, emailBody);

        if (emailSent)
            return Ok("Registration successful. Confirmation email sent.");
        else
            return BadRequest("Failed to send confirmation email. Please contact support.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

        if (existingUser == null)
            return Unauthorized("Invalid email or password");

        if (existingUser.Role == "Genel Müdür")
        {
            
            var allUsers = await _context.Users.ToListAsync();

            
            return Ok(allUsers.Select(user => new {
                user.FirstName,
                user.Surname,
                user.Email,
                user.PhoneNumber,
                user.Role,
               
            }));
        }
        else
        {
            
            return Ok(new
            {
                existingUser.FirstName,
                existingUser.Surname,
                existingUser.PhoneNumber,
                existingUser.Role 
            });
        }
    }



}
