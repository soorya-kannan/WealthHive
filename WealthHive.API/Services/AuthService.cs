using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WealthHive.API.Models;
using WealthHive.API.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace WealthHive.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPRepository _otpRepository;
        private readonly IConfiguration _configuration;
        private readonly Random _random;

        public AuthService(
            IUserRepository userRepository,
            IOTPRepository otpRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
            _configuration = configuration;
            _random = new Random();
        }

        public async Task<(bool success, string message)> SignUpAsync(string email, string username, string name, string familyName)
        {
            if (await _userRepository.EmailExistsAsync(email))
            {
                return (false, "Email already exists");
            }

            if (await _userRepository.UsernameExistsAsync(username))
            {
                return (false, "Username already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Username = username,
                Name = name,
                FamilyName = familyName,
                Role = UserRole.Member,
                Status = UserStatus.Pending,
                PasswordHash = string.Empty // Initialize with empty string since it's required
            };

            await _userRepository.AddAsync(user);

            var otp = await GenerateOTPAsync(email);
            await SendOTPEmailAsync(email, otp);

            return (true, "Signup successful. Please check your email for OTP.");
        }

        public async Task<(bool success, string message, string? token)> VerifyOTPAndSetPasswordAsync(string email, string otp, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return (false, "User not found", null);
            }

            if (!await _otpRepository.IsOTPValidAsync(email, otp))
            {
                return (false, "Invalid or expired OTP", null);
            }

            user.PasswordHash = BC.HashPassword(password);
            user.Status = UserStatus.Active;
            await _userRepository.UpdateAsync(user);

            await _otpRepository.InvalidateOTPsAsync(email);

            var token = await GenerateJWTTokenAsync(user);
            return (true, "Password set successfully", token);
        }

        public async Task<(bool success, string message, string? token)> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return (false, "Invalid email or password", null);
            }

            if (user.Status != UserStatus.Active)
            {
                return (false, "Account is not active", null);
            }

            if (!BC.Verify(password, user.PasswordHash))
            {
                return (false, "Invalid email or password", null);
            }

            var token = await GenerateJWTTokenAsync(user);
            return (true, "Login successful", token);
        }

        public async Task<string> GenerateJWTTokenAsync(User user)
        {
            var jwtSecret = _configuration["JWT:Secret"];
            var jwtIssuer = _configuration["JWT:Issuer"];
            var jwtAudience = _configuration["JWT:Audience"];

            if (string.IsNullOrEmpty(jwtSecret) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                throw new InvalidOperationException("JWT configuration is missing or invalid");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateOTPAsync(string email)
        {
            var otp = _random.Next(100000, 999999).ToString();
            var otpEntity = new OTP
            {
                Id = Guid.NewGuid(),
                Email = email,
                OTPCode = otp,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };

            await _otpRepository.AddAsync(otpEntity);
            return otp;
        }

        public async Task SendOTPEmailAsync(string email, string otp)
        {
            // TODO: Implement email sending logic
            // For now, we'll just log it
            Console.WriteLine($"Sending OTP {otp} to {email}");
        }
    }
} 