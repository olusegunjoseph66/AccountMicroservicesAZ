using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class AdminUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
