using System.Threading.Tasks;
using WealthHive.API.Models;

namespace WealthHive.API.Services
{
    public interface IAuthService
    {
        Task<(bool success, string message)> SignUpAsync(string email, string username, string name, string familyName);
        Task<(bool success, string message, string? token)> VerifyOTPAndSetPasswordAsync(string email, string otp, string password);
        Task<(bool success, string message, string? token)> LoginAsync(string email, string password);
        Task<string> GenerateJWTTokenAsync(User user);
        Task<string> GenerateOTPAsync(string email);
        Task SendOTPEmailAsync(string email, string otp);
    }
} 