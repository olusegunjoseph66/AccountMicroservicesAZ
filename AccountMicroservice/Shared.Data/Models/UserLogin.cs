using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class UserLogin
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string ChannelCode { get; set; } = null!;
        public string? DeviceId { get; set; }
        public string? IpAddress { get; set; }
        public DateTime LoginDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
