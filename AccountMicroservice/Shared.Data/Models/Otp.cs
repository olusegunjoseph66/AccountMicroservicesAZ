using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class Otp
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Reference { get; set; } = null!;
        public byte OtpStatusId { get; set; }
        public int? RegistrationId { get; set; }
        public int? UserId { get; set; }
        public short? NumberOfRetries { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpiry { get; set; }

        public virtual OtpStatus OtpStatus { get; set; } = null!;
        public virtual Registration? Registration { get; set; }
        public virtual User? User { get; set; }
    }
}
