using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class OtpStatus
    {
        public OtpStatus()
        {
            Otps = new HashSet<Otp>();
        }

        public byte Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Otp> Otps { get; set; }
    }
}
