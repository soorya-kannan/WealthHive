using System;
using System.ComponentModel.DataAnnotations;

namespace WealthHive.API.Models
{
    public class OTP
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string OTPCode { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
} 