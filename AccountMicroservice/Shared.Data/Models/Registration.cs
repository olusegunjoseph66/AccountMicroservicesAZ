using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class Registration
    {
        public Registration()
        {
            Otps = new HashSet<Otp>();
        }

        public int Id { get; set; }
        public string DistributorNumber { get; set; } = null!;
        public string CompanyCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string ChannelCode { get; set; } = null!;
        public string? DeviceId { get; set; }
        public byte RegistrationStatusId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual RegistrationStatus RegistrationStatus { get; set; } = null!;
        public virtual ICollection<Otp> Otps { get; set; }
    }
}
