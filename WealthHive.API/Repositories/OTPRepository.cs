using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WealthHive.API.Data;
using WealthHive.API.Models;

namespace WealthHive.API.Repositories
{
    public class OTPRepository : Repository<OTP>, IOTPRepository
    {
        public OTPRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<OTP> GetLatestOTPAsync(string email)
        {
            return await _dbSet
                .Where(o => o.Email == email && o.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(o => o.ExpiresAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsOTPValidAsync(string email, string otp)
        {
            var latestOTP = await GetLatestOTPAsync(email);
            return latestOTP != null && latestOTP.OTPCode == otp;
        }

        public async Task InvalidateOTPsAsync(string email)
        {
            var otps = await _dbSet
                .Where(o => o.Email == email)
                .ToListAsync();

            foreach (var otp in otps)
            {
                otp.ExpiresAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
} 