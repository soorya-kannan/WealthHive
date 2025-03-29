using System;
using System.Threading.Tasks;
using WealthHive.API.Models;

namespace WealthHive.API.Repositories
{
    public interface IOTPRepository : IRepository<OTP>
    {
        Task<OTP> GetLatestOTPAsync(string email);
        Task<bool> IsOTPValidAsync(string email, string otp);
        Task InvalidateOTPsAsync(string email);
    }
} 