using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WealthHive.API.Models;
using WealthHive.API.Services;

namespace WealthHive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var (success, message) = await _authService.SignUpAsync(
                request.Email,
                request.Username,
                request.Name,
                request.FamilyName
            );

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTPAndSetPassword([FromBody] VerifyOTPRequest request)
        {
            var (success, message, token) = await _authService.VerifyOTPAndSetPasswordAsync(
                request.Email,
                request.OTP,
                request.Password
            );

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message, token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var (success, message, token) = await _authService.LoginAsync(
                request.Email,
                request.Password
            );

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message, token });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var username = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new
            {
                userId,
                email,
                username,
                role
            });
        }
    }

    public class SignUpRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string FamilyName { get; set; }
    }

    public class VerifyOTPRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string OTP { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string Password { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
} 