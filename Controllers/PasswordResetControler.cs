using Microsoft.AspNetCore.Mvc;
using PayosferIdentity.Services;
using System.Threading.Tasks;

namespace PayosferIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly PasswordResetService _passwordResetService;

        public PasswordResetController(PasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        public class SendResetCodeRequest
        {
            public string Email { get; set; }
        }

        [HttpPost("send-reset-code")]
        public async Task<IActionResult> SendResetCode(SendResetCodeRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email address is required.");
            }

            var result = await _passwordResetService.SendResetCodeAsync(request.Email);
            if (result)
            {
                return Ok(new { success = true, message = "Reset code sent successfully." });
            }
            else
            {
                return NotFound(new { success = false, message = "User not found." });
            }
        }

        public class ResetPasswordRequest
        {
            public string Email { get; set; }
            public string Code { get; set; }
            public string NewPassword { get; set; }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Code) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Email, code, and new password are required.");
            }

            var result = await _passwordResetService.ResetPasswordAsync(request.Email, request.Code, request.NewPassword);
            if (result)
            {
                return Ok("Password reset successfully.");
            }
            else
            {
                return BadRequest("Invalid or expired reset code.");
            }
        }

    }
}
