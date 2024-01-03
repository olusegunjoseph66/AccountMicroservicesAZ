using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class UserPasswordHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Password { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
