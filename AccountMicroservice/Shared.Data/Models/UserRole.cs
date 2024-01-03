using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public short RoleId { get; set; }
        public int UserId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
