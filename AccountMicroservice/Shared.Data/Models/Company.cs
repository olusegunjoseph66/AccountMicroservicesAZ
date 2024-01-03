using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class Company
    {
        public Company()
        {
            AdminUsers = new HashSet<AdminUser>();
        }

        public byte Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public virtual ICollection<AdminUser> AdminUsers { get; set; }
    }
}
